using UnityEngine;

public class Dynamic {
  public static Steering Align(SteeringElement source, float target, AlignOptions opts) {
    Steering steering = new Steering();
    float targetRotation;

    float rotation = Utils.MapToRange(target - source.transform.rotation.w);
    float rotationSize = System.Math.Abs(rotation);

    if (rotationSize < opts.targetRadius) return steering;
    if (rotationSize > opts.slowRadius) targetRotation = opts.maxRotation;
    else targetRotation = rotationSize * opts.maxRotation / opts.slowRadius;

    steering.angular = (targetRotation - source.steering.rotation) / opts.timeToTarget;

    float angularSize = System.Math.Abs(steering.angular);
    if (steering.angular > opts.maxAngular) steering.angular = steering.angular * opts.maxAngular / angularSize;

    steering.rotation = source.steering.rotation + steering.angular * Time.fixedDeltaTime;

    return steering;
  }

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

public class AlignOptions {
  public float maxRotation = 3;
  public float maxAngular = 3;

  public float slowRadius = 1;
  public float targetRadius = 0.1f;
  public float timeToTarget = 0.1f;

  public AlignOptions() { }

  public AlignOptions(float maxRotation, float maxAngular, float slowRadius, float targetRadius, float timeToTarget) {
    this.maxRotation = maxRotation;
    this.maxAngular = maxAngular;

    this.slowRadius = slowRadius;
    this.targetRadius = targetRadius;
    this.timeToTarget = timeToTarget;
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