using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ShapeCreator))]
public class ShapeEditor : Editor {
  ShapeCreator creator;
  PointSelection selection;

  Repainter repainter = new Repainter();

  void OnEnable() {
     creator = target as ShapeCreator;
     selection = new PointSelection();
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

  /** Handles input-related events */
  private void HandleInput(Event guiEvent) {

    Ray mouseRay = HandleUtility.GUIPointToWorldRay(guiEvent.mousePosition);
    Vector3 mousePosition = PositionFromRay(mouseRay);

    if (guiEvent.type == EventType.MouseDown && guiEvent.button == 0 && guiEvent.modifiers == EventModifiers.None)
      HandleLeftMouseDown(mousePosition);
    
    if (guiEvent.type == EventType.MouseUp && guiEvent.button == 0 && guiEvent.modifiers == EventModifiers.None)
      HandleLeftMouseUp(mousePosition);
    
    if (guiEvent.type == EventType.MouseDrag && guiEvent.button == 0 && guiEvent.modifiers == EventModifiers.None)
      HandleLeftMouseDrag(mousePosition);

    if (!selection.pointSelected)
      UpdateInfo(mousePosition);
  }

  /** Handles changes when clicking down the left mouse button */
  private void HandleLeftMouseDown(Vector3 mousePosition) {
    if (!selection.overPoint) {
      Undo.RecordObject(creator, "Add shape point");

      int index = selection.overLine ? selection.lineIndex + 1 : creator.points.Count;
      creator.points.Insert(index, mousePosition);
      selection.pointIndex = index;
    }

    selection.pointSelected = true;
    selection.startPosition = mousePosition;
    repainter.scheduleRepaint();
  }

  /** Handles changes when clicking up the left mouse button */
  private void HandleLeftMouseUp(Vector3 mousePosition) {
    if (selection.pointSelected) {
      // Undo function
      creator.points[selection.pointIndex] = selection.startPosition;
      Undo.RecordObject(creator, "Move Point");
      creator.points[selection.pointIndex] = mousePosition;

      selection.pointSelected = false;
      selection.pointIndex = -1;
      repainter.scheduleRepaint();
    }
  }

  private void HandleLeftMouseDrag(Vector3 mousePosition) {
    if (selection.pointSelected) {
      creator.points[selection.pointIndex] = mousePosition;
      repainter.scheduleRepaint();
    }
  }

  /** Updates selection info when there is no point selected */
  private void UpdateInfo(Vector3 mousePosition) {
    var points = creator.points;
    int pointIndex = -1; // Index of the point the mouse is over

    // Find if the mouse if over one of the points
    for (int i = 0; i < points.Count; i++) {
      var point = points[i];

      if (Vector3.Distance(mousePosition, point) < creator.handleRadius) {
        pointIndex = i;
        break;
      }
    }

    // If the point change
    if (pointIndex != selection.pointIndex) {
      selection.pointIndex = pointIndex;
      repainter.scheduleRepaint();
    }

    // Handle hover over lines

    if (selection.overPoint) {
      selection.lineIndex = -1;

    } else {
      int lineIndex = -1;
      float closestDistance = creator.handleRadius;

      // Search the closest line index the mouse hovers (if it exists)
      for (int i = 0; i < points.Count; i++) {
        var point = points[i];
        var next = points[(i + 1) % points.Count];

        float distance = HandleUtility.DistancePointToLineSegment(mousePosition.ToXZ(), point.ToXZ(), next.ToXZ());

        if (distance < closestDistance) {
          closestDistance = distance;
          lineIndex = i;
        }
      }

      // If the line changes
      if (selection.lineIndex != lineIndex) {
        selection.lineIndex = lineIndex;
        repainter.scheduleRepaint();
      }
    }
  }

  /** Draw elements in Unity's Editor */
  private void Draw() {
    var points = creator.points;

    // For each point
    for (int i = 0; i < points.Count; i++) {
      var point = points[i];
      var next = points[(i + 1) % points.Count];

      // Draw lines
      if (selection.lineIndex == i) {
        Handles.color = Color.red;
        Handles.DrawLine(point, next);
      } else {
        Handles.color = Color.black;
        Handles.DrawDottedLine(point, next, 4);
      }

      // Draw points
      if (selection.pointIndex == i && selection.pointSelected) Handles.color = Color.black;
      else if (selection.pointIndex == i) Handles.color = Color.red;
      else Handles.color = Color.white;
      Handles.DrawSolidDisc(point, Vector3.up, creator.handleRadius);
    }
  }

  private Vector3 PositionFromRay(Ray ray) {
    float height = 0;
    float distance = (height - ray.origin.y) / ray.direction.y;
    return ray.GetPoint(distance);
  }
}