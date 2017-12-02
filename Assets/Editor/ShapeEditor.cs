using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ShapeCreator))]
public class ShapeEditor : Editor {

  ShapeCreator shapeCreator;

  void OnEnable() {
     shapeCreator = target as ShapeCreator;
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
  }

  /** Handles input-related events */
  private void HandleInput(Event guiEvent) {
    // Input events can trigger a repaint
    Repainter repainter = new Repainter();

    // Mouse Left click (no modifiers)
    if (guiEvent.type == EventType.MouseDown && guiEvent.button == 0 && guiEvent.modifiers == EventModifiers.None) {
      // Undo function
      Undo.RecordObject(shapeCreator, "Add shape point");

      Ray mouseRay = HandleUtility.GUIPointToWorldRay(guiEvent.mousePosition);
      shapeCreator.points.Add(PositionFromRay(mouseRay));
      repainter.scheduleRepaint();
    }

    // Repaint if needed
    repainter.flushRepaint();
  }

  /** Draw elements in Unity's Editor */
  private void Draw() {
    var points = shapeCreator.points;

    // For each point
    for (int i = 0; i < points.Count; i++) {
      var point = points[i];
      var next = points[(i + 1) % points.Count];

      // Draw lines
      Handles.color = Color.black;
      Handles.DrawDottedLine(point, next, 4);

      // Draw points
      Handles.color = Color.white;
      Handles.DrawSolidDisc(point, Vector3.up, .5f);
    }
  }

  private Vector3 PositionFromRay(Ray ray) {
    float height = 0;
    float distance = (height - ray.origin.y) / ray.direction.y;
    return ray.GetPoint(distance);
  }
}