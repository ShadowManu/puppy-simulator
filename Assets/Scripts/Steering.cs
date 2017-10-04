using UnityEngine;
using System.Collections;

public class Steering {
  public Vector3 velocity; // Position speed
  public Vector3 linear;   // Position acceleration

  // Direction
  public float rotation;   // Direction speed
  public float angular;    // Direction acceleration

  public Steering() {
    velocity = Vector3.zero;
    linear = Vector3.zero;

    rotation = 0.0f;
    angular = 0.0f;
  }

  public Steering(Vector3 velocity, Vector3 linear, float rotation, float angular) {
    this.velocity = velocity;
    this.linear = linear;

    this.rotation = rotation;
    this.angular = angular;
  }
}