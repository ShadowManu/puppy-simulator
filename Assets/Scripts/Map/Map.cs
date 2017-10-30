using System;
using System.Collections.Generic;
using UnityEngine;

public class Map {
  public Dictionary<string, Polygon> polygons;

  public Map(Dictionary<string, Polygon> polygons) {
    this.polygons = polygons;
  }

  public static Map GenerateMap() {
    GameObject parent = GameObject.Find("Vertices");

    var polygons = new Dictionary<string, Polygon>();
    
    // Query all vertices and fill polygons with its information
    foreach (Transform childTransform in parent.transform) {
      GameObject child = childTransform.gameObject;
      String[] tags = child.GetComponent<VertexController>().tags;

      foreach (string tag in tags) {
        if (!polygons.ContainsKey(tag)) polygons.Add(tag, new Polygon());
        polygons[tag].AddVertex(childTransform.position);
      }
    }

    return new Map(polygons);
  }
}