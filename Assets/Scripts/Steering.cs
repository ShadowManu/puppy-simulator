using UnityEngine;
using System.Collections;

public struct Steering {
  public Vector3 velocity;
  public float rotation;

  public Steering() {
    velocity = Vector3.zero;
    rotation = 0.0f;
  }

  public Steering(Vector3 vel, float rot) {
    velocity = vel;
    rotation = rot;
  }

  public static Steering GetSteering(GameObject source, GameObject target) {
    return Steering.GetSteering(source, target, new SteeringOptions());
  }

  public static Steering GetSteering(GameObject source, GameObject target, SteeringOptions options) {
    Vector3 velocity;
    Vector3 distance = target.transform.position - source.transform.position;
    distance.y = 0; // Dismiss Y Axis
    float orientation = 0.0f;

    switch (options.mode) {
      default:
      case Mode.None:
        return new Steering();

      case Mode.Seek:
        velocity = Vector3.Normalize(distance);
        velocity *= options.maxSpeed;

        return new Steering(velocity, orientation);

      case Mode.SeekNoOvershoot:
        if (distance.magnitude <= options.minDistance)
          return new Steering();

        velocity = Vector3.Normalize(distance);
        velocity *= options.maxSpeed;

        return new Steering(velocity, orientation);

      case Mode.Flee:
        if (distance.magnitude >= options.maxDistance)
          return new Steering();
        velocity = Vector3.Normalize(distance);
        velocity *= options.maxSpeed;

        return new Steering(-velocity, orientation);

      case Mode.Arriving:
        velocity = distance / options.timeToTarget;

        // Cap to maxSpeed
        if (velocity.magnitude > options.maxSpeed) {
          velocity = Vector3.Normalize(velocity);
          velocity *= options.maxSpeed;
        }

        return new Steering(velocity, orientation);
    }
  }
}