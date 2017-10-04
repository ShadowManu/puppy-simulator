using UnityEngine;

public class Dynamic {

  public static Steering Arrive(SteeringElement source, Element target, ArriveOptions opts) {
    Steering steering = new Steering();
    Vector3 distance = target.transform.position - source.transform.position; distance.y = 0; // Dismiss Y Axis
    float targetSpeed;

    // This hack allows to stop before the target
    distance = distance - Vector3.ClampMagnitude(Vector3.Normalize(distance) * 3, distance.magnitude);

    if (distance.magnitude < opts.targetRadius) return steering;
    if (distance.magnitude > opts.slowRadius) targetSpeed = opts.maxVelocity;
    else targetSpeed = distance.magnitude * opts.maxVelocity / opts.slowRadius;

    Vector3 targetVelocity = Vector3.Normalize(distance) * targetSpeed;

    steering.linear = (targetVelocity - source.steering.velocity) / opts.timeToTarget;
    if (steering.linear.magnitude > opts.maxLinear) steering.linear = Vector3.Normalize(distance) * opts.maxLinear;

    steering.velocity = Vector3.ClampMagnitude(source.steering.velocity + steering.linear * Time.fixedDeltaTime, opts.maxVelocity);

    return steering;
  }
}

public class ArriveOptions {
  public float maxVelocity = 20f;
  public float maxLinear = 100f;

  public float slowRadius = 10f;
  public float targetRadius = 0.1f;
  public float timeToTarget = 0.1f;
  
  public ArriveOptions() { }

  public ArriveOptions(float maxVelocity, float maxLinear, float slowRadius, float targetRadius, float timeToTarget) {
    this.maxVelocity = maxVelocity;
    this.maxLinear = maxLinear;
    this.slowRadius = slowRadius;
    this.targetRadius = targetRadius;
    this.timeToTarget = timeToTarget;
  }
}