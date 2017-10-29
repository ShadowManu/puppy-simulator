using UnityEngine;
using System.Collections;

public class Throwing {
  
  public static Vector3 CalculateThrowing(Vector3 start, Vector3 end, float muzzleVel, Vector3 gravity) {
    Vector3 delta;
    Vector3 result;
    float a,b,c;
    float aux;
    float time0,time1,time;
    delta = end - start;

    a = Vector3.Dot(gravity,gravity);
    b = -4 * (Vector3.Dot(gravity,delta) + (muzzleVel*muzzleVel));
    c = 4 * Vector3.Dot(delta,delta);

    if (4*a*c > b*b) return Vector3.zero;  

    aux = b*b - 4*a*c;
    time0 = Mathf.Sqrt((-b + Mathf.Sqrt(aux)) / (2*a));
    time1 = Mathf.Sqrt((-b - Mathf.Sqrt(aux)) / (2*a));

    if (time0 < 0) {
      if (time1 < 0) { 
        return Vector3.zero;
      }
      else {
        time = time1;
      }
    }
    else {
      if (time1 < 0) { 
        time = time0; 
      }
      else {
        time = Mathf.Min(time0,time1);
      }
    }

    result = (2*delta - gravity * (time*time)) / (2*time);
    Debug.Log("Velocidad: " + result);
    return result;
  }
}