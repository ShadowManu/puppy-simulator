using UnityEngine;

public struct Steering {
  public Vector3 velocity;
  public float rotation;

  public Steering(Vector3 vel, float rot) {
    velocity = vel;
    rotation = rot;
  }

  public static Steering GetSteering(GameObject source, GameObject target, float maxSpeed) {
    Vector3 velocity = target.transform.position - source.transform.position;
    velocity.y = 0; // Dismiss Y Axis

    velocity = Vector3.Normalize(velocity);
    velocity *= maxSpeed;

    return new Steering(velocity, 0.0f); // TODO ROTATION
  }
}