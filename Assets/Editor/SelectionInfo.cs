using UnityEngine;

/** Information about a point */
public class PointSelection {
  /** Point index */
  public int pointIndex = -1;

  /** Flag if mouse over a point */
  public bool overPoint {
    get { return pointIndex != -1; }
  }

  /** Flag if point is currently selected */
  public bool pointSelected;

  /** Position at the start of drag */
  public Vector3 startPosition;


  /** Line index */
  public int lineIndex = -1;

  /** Flag if mouse over a line */
  public bool overLine {
    get { return lineIndex != -1; }
  }
}
