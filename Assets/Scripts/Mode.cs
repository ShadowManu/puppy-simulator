/**
  * Defines the desired mode of the demonstration.
  * In the future, it will be controlled globally automatically depending the state of the demo.
  */
public enum Mode {
  None,
  Seek,
  SeekNoOvershoot,
  Flee,
  Arriving,
}

static class ModeMethods {
  /** Hardcoded way to cycle between modes */
  public static Mode NextMode(this Mode mode) {
    switch (mode) {
      default:
      case Mode.None:            return Mode.Seek;
      case Mode.Seek:            return Mode.SeekNoOvershoot;
      case Mode.SeekNoOvershoot: return Mode.Flee;
      case Mode.Flee:            return Mode.Arriving;
      case Mode.Arriving:        return Mode.None;
    }
  }
}