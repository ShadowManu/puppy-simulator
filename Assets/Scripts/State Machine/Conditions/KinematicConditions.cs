using UnityEngine;

public class ClosenessCondition : Condition {
  Transform source;
  Transform dest;
  float distance;

  public ClosenessCondition(Transform source, Transform dest, float distance) {
    this.source = source;
    this.dest = dest;
    this.distance = distance;
  }

  public bool Test() {
    var s = Vector3.ProjectOnPlane(source.position, Vector3.up);
    var d = Vector3.ProjectOnPlane(dest.position, Vector3.up);

    return (d - s).magnitude < distance;
  }
}