using UnityEngine;

public class Location: ILocation {
  public Vector3 position { get; set; }

  public Location(Vector3 pos) {
    position = pos;
  }
}

public interface ILocation {
  Vector3 position { get; set; }
}