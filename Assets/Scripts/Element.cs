using UnityEngine;

public interface Element {
  Transform transform { get; }
  float orientation { get; }
}

public interface SteeringElement : Element {
  Steering steering { get; }
}