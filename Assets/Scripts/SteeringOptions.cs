public class SteeringOptions {
  public float minDistance = 2;
  public float maxDistance = 20;
  public float maxSpeed = 10;
  public float timeToTarget = 2;

  public Mode mode = Mode.None;

  /** Default Constructor */
  public SteeringOptions() { }

  /** Basic Constructor */
  public SteeringOptions(Mode mode): this() {
    this.mode = mode;
  }

  /** Full Constructor */
  public SteeringOptions(
    float minDistance,
    float maxDistance,
    float maxSpeed,
    float timeToTarget,
    Mode mode
  ) {
    this.minDistance = minDistance;
    this.maxDistance = maxDistance;
    this.maxSpeed = maxSpeed;
    this.timeToTarget = timeToTarget;
    this.mode = mode;
  }
}