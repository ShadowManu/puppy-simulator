using UnityEngine;

/**
  This class is a representation of a
  agent kinematic properties. Its goal is to provide
  a kinematic implementation outside of Unity constraints
 */
public class Kinematic : ILocation {
  /** Position of a kinematic agent, measured with Unity's Vector3 */
  public Vector3 position {
    get { return transform.position; }
    set { transform.position = value; }
  }

  /** Orientation of a kinematic agent, measured in 360 degrees, starting from the West, positive clockwise */
  public float orientation {
    get { return transform.rotation.eulerAngles.y; }
    set { transform.rotation = Quaternion.AngleAxis(value, Vector3.up); }
  }

  /** Internal link against Unity positioning system */
  private Transform transform;

  public Kinematic(Transform transform) {
    this.transform = transform;
  }

  /** Updates agent kinematic with a given steering */
  public void UpdateKinematic(Steering steering) {
    position += steering.velocity * Time.deltaTime;
    orientation += steering.rotation * Time.deltaTime;
  }

  /** Transforms a vector in a kinematic orientation, using x and z values  */
  public static float Vec2Orient(Vector3 vector) {
    Vector3 flat = Vector3.ProjectOnPlane(vector, Vector3.up);
    return Vector3.SignedAngle(Vector3.left, flat, Vector3.up);
  }

  /** Transforms an orientation in a vector, useful to calculate differences */
  public static Vector3 Orient2Vec(float orientation) {
    return Quaternion.Euler(0, orientation, 0) * Vector3.left;
  }

  /** Orientation difference. Canonical implementation, as it outputs the minimal angle direction
   *  and does not have to deal with Y coordinates in vector representations that may have miscalculations if not removed properly */
  public static float OrientDiff(float source, float target) {
    Vector3 sourceVec = Kinematic.Orient2Vec(source);
    Vector3 targetVec = Kinematic.Orient2Vec(target);
    return Vector3.SignedAngle(sourceVec, targetVec, Vector3.up);
  }
}
