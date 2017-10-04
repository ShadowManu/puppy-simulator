using UnityEngine;

public interface Element {
  Transform transform { get; }
}

public interface SteeringElement : Element {
  Steering steering { get; }
}