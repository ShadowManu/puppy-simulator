using System;
using System.Collections.Generic;
using UnityEngine;

public class Map {
  public Dictionary<string, Polygon> polygons;

  public Map(Dictionary<string, Polygon> polygons) {
    this.polygons = polygons;
  }

  public static Map GenerateMap() {
    var polygons = new Dictionary<string, Polygon>();

    // Query all centers and create polygons from them
    GameObject centers = GameObject.Find("Centers");

    foreach (Transform transform in centers.transform) {
      GameObject center = transform.gameObject;
      CenterController cc = center.GetComponent<CenterController>();
      string name = center.name;

      Polygon polygon = new Polygon(name, center, cc.vertices, cc.neighborNames);

      polygons.Add(name, polygon);
    }

    Map.checkPolygonValidity(polygons);

    return new Map(polygons);
  }

  private static void checkPolygonValidity(Dictionary<string, Polygon> polygons) {
    foreach (Polygon polygon in polygons.Values) {
      // Verify its a triangle
      if (polygon.vertices.Length != 3) {
        throw new Exception("polygon \"" + polygon.name + "\" does not have 3 vertices");
      }

      // Verify it has a center
      if (polygon.center == null) {
        throw new Exception("polygon \"" + polygon.name + "\" does not have a center");
      }
    }
  }
}