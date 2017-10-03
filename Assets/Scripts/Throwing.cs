using UnityEngine;
using System.Collections;

public struct Throwing {
  public Vector3 throwing;

  public Throwing(Vector3 th) {
    throwing = th;
  }
  
  public static Throwing CalculateThrowing(Vector3 start, Vector3 end, float muzzleVel, Vector3 gravity) {
    Vector3 delta;
    float a,b,c;    
    float time0,time1,time;
    float aux;
    Vector3 result;
    delta = start - end;


    a = Vector3.Dot(gravity,gravity);
    b = -4 * (Vector3.Dot(gravity,delta) + (muzzleVel*muzzleVel));
    c = 4 * Vector3.Dot(delta,delta);

    if (4*a*c > b*b) { return new Throwing(Vector3.zero); }

    aux = b*b - 4*a*c;
    time0 = Mathf.Sqrt((-b + Mathf.Sqrt(aux)) / (2*a));
    time1 = Mathf.Sqrt((-b - Mathf.Sqrt(aux)) / (2*a));

    if (time0 < 0) {
      if (time1 < 0) { 
        return new Throwing(Vector3.zero); 
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

    result = (2*delta - gravity * (time*time)) / (2*muzzleVel*time);
    return new Throwing(result);
  }
}