using UnityEngine;

public struct Steering {
  public Vector3 velocity;
  public float rotation;

  public Steering(Vector3 vel, float rot) {
    velocity = vel;
    rotation = rot;
  }

  public static Steering GetSteering(GameObject source, GameObject target, Mode mode, float maxSpeed, float timeToTarget) {
    Vector3 velocity;
    Vector3 distance = target.transform.position - source.transform.position;
    distance.y = 0; // Dismiss Y Axis

    switch (mode) {
      default:
      case Mode.None:
      case Mode.Seek:
      case Mode.SeekNoOvershoot:
        velocity = Vector3.Normalize(distance);
        velocity *= maxSpeed;

        return new Steering(velocity, 0.0f); // TODO ROTATION

      case Mode.Arriving:
        velocity = distance / timeToTarget;

        // Cap to maxSpeed
        if (velocity.magnitude > maxSpeed) {
          velocity = Vector3.Normalize(velocity);
          velocity *= maxSpeed;
        }

        return new Steering(velocity, 0.0f); // TODO ROTATION
    }
  }
}