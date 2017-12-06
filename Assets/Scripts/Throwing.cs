using UnityEngine;
using System.Collections;

public class Throwing { 
  
  /**
   * Calculates a velocity vector required to throw a projectile from 
   * a start location to finish on an end location, given a desired velocity and gravity
   */
  public static Vector3 Velocity(Vector3 start, Vector3 end, float velocity, Vector3 gravity) {
    // Vector from target back to the start
    Vector3 delta = end - start;

    // Calculate coeficients of conventional quadratic equation
    var a = Vector3.Dot(gravity, gravity);
    var b = -4 * (Vector3.Dot(gravity, delta) + velocity * velocity);
    var c = 4 * Vector3.Dot(delta, delta);  

    // Check if there is no real solutions
    if (4*a*c > b*b) return Vector3.zero;

    var root = Mathf.Sqrt(b*b - 4*a*c);
    var time0 = Mathf.Sqrt((-b + root) / (2*a));
    var time1 = Mathf.Sqrt((-b - root) / (2*a));

    // No positive answers
    if (time0 < 0 && time1 < 0) return Vector3.zero;

    // Choose better time
    float time = 0;
    if (time0 > 0 && time1 > 0) time = Mathf.Min(time0, time1);
    else if (time0 > 0 && time0 < time1) time = time0;
    else if (time1 > 0 && time1 < time0) time = time1;

    // Return the firing vector
    return (2*delta - gravity * (time*time)) / (2*time);
  }
}