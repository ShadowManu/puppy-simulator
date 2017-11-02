using System;
using UnityEngine;

public class Dynamic {

  /** Generates a steering that dinamically aligns the orientation to face the target */
  public static Steering Face(Kinematic source, Steering sourceSteering, Kinematic target, AlignOptions opts) {
    return Dynamic.Align(source, sourceSteering, Kinematic.Vec2Orient(target.position - source.position), opts);
  }

  /** Generates a steering that dinamically aligns the orientation to the target orientation  */
  public static Steering Align(Kinematic source, Steering sourceSteering, float targetOrientation, AlignOptions opts) {
    Steering steering = new Steering();

    // Define objectives
    float rotation = Kinematic.OrientDiff(source.orientation, targetOrientation);
    float rotationSize = System.Math.Abs(rotation);
    float targetRotation;

    // Define target rotation
    if (rotationSize < opts.targetRadius) return steering;
    if (rotationSize > opts.slowRadius) targetRotation = opts.maxRotation;
    else targetRotation = rotationSize * opts.maxRotation / opts.slowRadius;

    // Define steering based on target rotation
    steering.angular = Math.Min((targetRotation * Mathf.Sign(rotation) - sourceSteering.rotation) / opts.timeToTarget, opts.maxAngular);
    steering.rotation = sourceSteering.rotation + steering.angular * Time.deltaTime;

    return steering;
  }

  public static Steering Arrive(Kinematic source, Steering sourceSteering, Kinematic target, ArriveOptions opts) {
    Steering steering = new Steering();

    // Define objectives
    Vector3 distance = Vector3.ProjectOnPlane(target.position - source.position, Vector3.up);
    float targetSpeed;

    // This hack allows to stop before the target
    // distance = distance - Vector3.ClampMagnitude(Vector3.Normalize(distance) * 3, distance.magnitude);

    // Define target velocity
    if (distance.magnitude < opts.targetRadius) return steering;
    if (distance.magnitude > opts.slowRadius) targetSpeed = opts.maxVelocity;
    else targetSpeed = distance.magnitude * opts.maxVelocity / opts.slowRadius;

    Vector3 targetVelocity = Vector3.Normalize(distance) * targetSpeed;

    // Define steering based on target velocity
    steering.linear = (targetVelocity - sourceSteering.velocity) / opts.timeToTarget;
    if (steering.linear.magnitude > opts.maxLinear) steering.linear = Vector3.Normalize(distance) * opts.maxLinear;

    steering.velocity = Vector3.ClampMagnitude(sourceSteering.velocity + steering.linear * Time.fixedDeltaTime, opts.maxVelocity);

    return steering;
  }
}

public class AlignOptions {
  public float maxRotation = 200;
  public float maxAngular = 200;

  public float slowRadius = 75;
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