using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ShapeCreator))]
public class ShapeEditor : Editor {
  ShapeCreator creator;
  SelectionInfo selection;

  Repainter repainter = new Repainter();

  void OnEnable() {
    creator = target as ShapeCreator;
    selection = new SelectionInfo();
    Undo.undoRedoPerformed += OnUndoOrRedo;
  }

  void OnDisable() {
    Undo.undoRedoPerformed -= OnUndoOrRedo;
  }

  void OnUndoOrRedo() {
    if (selection.shapeIndex >= creator.shapes.Count) {
      selection.shapeIndex = creator.shapes.Count - 1;
    }
  }

  void OnSceneGUI() {
    Event guiEvent = Event.current;

    // Repaint
    if (guiEvent.type == EventType.Repaint) Draw();

    // Avoid unselecting object on outside click
    else if (guiEvent.type == EventType.Layout)
      HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));
    
    // Input handling
    else HandleInput(guiEvent);

    repainter.flushRepaint();
  }

  /** Creates a new shape */
  private void CreateNewShape() {
    Undo.RecordObject(creator, "Create Shape");
    creator.shapes.Add(new Shape());
    selection.shapeIndex = creator.shapes.Count - 1;
  }

  /** Creates a new point in current shape */
  private void CreateNewPoint(Vector3 position) {
    Undo.RecordObject(creator, "Add shape point");

    bool mouseOverSelectedShape = selection.shapeIndex == selection.mouseShapeIndex;
    int index = (selection.overLine && mouseOverSelectedShape) ? selection.lineIndex + 1 : currentShape.points.Count;
    currentShape.points.Insert(index, position);
    selection.pointIndex = index;
    selection.mouseShapeIndex = selection.shapeIndex;
    SelectPointUnderMouse();

    repainter.scheduleRepaint();
  }

  /** Configures selection properties as a point been selected, using the current point index */
  private void SelectPointUnderMouse() {
    selection.pointSelected = true;
    selection.lineIndex = -1;
    selection.startPosition = currentShape.points[selection.pointIndex];

    repainter.scheduleRepaint();
  }

  private void SelectShapeUnderMouse() {
    if (selection.mouseShapeIndex != -1) {
      selection.shapeIndex = selection.mouseShapeIndex;
      repainter.scheduleRepaint();
    }
  }

  /** Quick access to current shape */
  private Shape currentShape { get { return creator.shapes[selection.shapeIndex]; } }

  /** Handles input-related events */
  private void HandleInput(Event guiEvent) {

    Ray mouseRay = HandleUtility.GUIPointToWorldRay(guiEvent.mousePosition);
    Vector3 mousePosition = PositionFromRay(mouseRay);

    if (guiEvent.type == EventType.MouseDown && guiEvent.button == 0 && guiEvent.modifiers == EventModifiers.Shift)
      HandleShiftLeftMouseDown(mousePosition);
    
    if (guiEvent.type == EventType.MouseDown && guiEvent.button == 0 && guiEvent.modifiers == EventModifiers.None)
      HandleLeftMouseDown(mousePosition);
    
    if (guiEvent.type == EventType.MouseUp && guiEvent.button == 0)
      HandleLeftMouseUp(mousePosition);
    
    if (guiEvent.type == EventType.MouseDrag && guiEvent.button == 0 && guiEvent.modifiers == EventModifiers.None)
      HandleLeftMouseDrag(mousePosition);

    if (!selection.pointSelected)
      UpdateInfo(mousePosition);
  }

  /** Handles changes when clicking down the left mouse button and holding shift */
  private void HandleShiftLeftMouseDown(Vector3 mousePosition) {
    CreateNewShape();
    CreateNewPoint(mousePosition);
  }

  /** Handles changes when clicking down the left mouse button */
  private void HandleLeftMouseDown(Vector3 mousePosition) {
    if (creator.shapes.Count == 0) CreateNewShape();

    SelectShapeUnderMouse();

    if (selection.overPoint) SelectPointUnderMouse();
    else CreateNewPoint(mousePosition);
  }

  /** Handles changes when clicking up the left mouse button */
  private void HandleLeftMouseUp(Vector3 mousePosition) {
    if (selection.pointSelected) {
      // Undo function
      currentShape.points[selection.pointIndex] = selection.startPosition;
      Undo.RecordObject(creator, "Move Point");
      currentShape.points[selection.pointIndex] = mousePosition;

      selection.pointSelected = false;
      selection.pointIndex = -1;
      repainter.scheduleRepaint();
    }
  }

  private void HandleLeftMouseDrag(Vector3 mousePosition) {
    if (selection.pointSelected) {
      currentShape.points[selection.pointIndex] = mousePosition;
      repainter.scheduleRepaint();
    }
  }

  /** Updates selection info when there is no point selected */
  private void UpdateInfo(Vector3 mousePosition) {
    int mouseShapeIndex = -1; // Index of the shape the mouse is over
    int pointIndex = -1; // Index of the point the mouse is over

    // For every shape
    for (int j = 0; j < creator.shapes.Count; j++) {
      var points = creator.shapes[j].points;

      // Find if the mouse if over one of the points
      for (int i = 0; i < points.Count; i++) {
        var point = points[i];

        if (Vector3.Distance(mousePosition, point) < creator.handleRadius) {
          mouseShapeIndex = j;
          pointIndex = i;
          break;
        }
      }
    }

    // If the point changes or the shape changes
    if (pointIndex != selection.pointIndex || mouseShapeIndex != selection.mouseShapeIndex) {
      selection.mouseShapeIndex = mouseShapeIndex;
      selection.pointIndex = pointIndex;
      repainter.scheduleRepaint();
    }

    // Handle hover over lines

    if (selection.overPoint) {
      selection.lineIndex = -1;

    } else {
      int lineIndex = -1;
      float closestDistance = creator.handleRadius;

      // For every shape
      for (int j = 0; j < creator.shapes.Count; j++) {
        var points = creator.shapes[j].points;

        // Search the closest line index the mouse hovers (if it exists)
        for (int i = 0; i < points.Count; i++) {
          var point = points[i];
          var next = points[(i + 1) % points.Count];

          float distance = HandleUtility.DistancePointToLineSegment(mousePosition.ToXZ(), point.ToXZ(), next.ToXZ());

          if (distance < closestDistance) {
            closestDistance = distance;
            mouseShapeIndex = j;
            lineIndex = i;
          }
        }
      }

      // If the line changes
      if (lineIndex != selection.lineIndex || mouseShapeIndex != selection.mouseShapeIndex) {
        selection.mouseShapeIndex = mouseShapeIndex;
        selection.lineIndex = lineIndex;
        repainter.scheduleRepaint();
      }
    }
  }

  /** Draw elements in Unity's Editor */
  private void Draw() {
    for (int j = 0; j < creator.shapes.Count; j++) {
      var points = creator.shapes[j].points;
      var isSelectedShape = selection.shapeIndex == j;
      var isMouseOverShape = selection.mouseShapeIndex == j;
      var deselectedColor = new Color(.3f, .3f, .3f); 

      // For each point
      for (int i = 0; i < points.Count; i++) {
        var point = points[i];
        var next = points[(i + 1) % points.Count];

        // Draw lines
        if (isMouseOverShape && selection.lineIndex == i) {
          Handles.color = Color.red;
          Handles.DrawLine(point, next);
        } else {
          Handles.color = isSelectedShape ? Color.black : deselectedColor;
          Handles.DrawDottedLine(point, next, 4);
        }

        // Draw points
        if (isMouseOverShape && selection.pointIndex == i && selection.pointSelected) Handles.color = Color.black;
        else if (isMouseOverShape && selection.pointIndex == i) Handles.color = Color.red;
        else if (isSelectedShape) Handles.color = Color.white;
        else Handles.color = deselectedColor;
        Handles.DrawSolidDisc(point, Vector3.up, creator.handleRadius);
      }
    }
  }

  private Vector3 PositionFromRay(Ray ray) {
    float height = 0;
    float distance = (height - ray.origin.y) / ray.direction.y;
    return ray.GetPoint(distance);
  }
}