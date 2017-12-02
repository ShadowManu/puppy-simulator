using UnityEditor;

public class Repainter {
  bool repaint = false;

  public void scheduleRepaint() {
    repaint = true;
  }

  public void flushRepaint() {
    if (!repaint) return;

    HandleUtility.Repaint();
    repaint = false;
  }
}