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
    
    // Query all vertices and create polygons
    GameObject vertices = GameObject.Find("Vertices");

    foreach (Transform transform in vertices.transform) {
      GameObject vertex = transform.gameObject;
      String[] tags = vertex.GetComponent<VertexController>().tags;

      foreach (string tag in tags) {
        if (!polygons.ContainsKey(tag)) polygons.Add(tag, new Polygon());
        polygons[tag].AddVertex(vertex);
      }
    }

    // Query all centers and assign centers and neighbors to polygons
    GameObject centers = GameObject.Find("Centers");

    foreach (Transform transform in centers.transform) {
      GameObject center = transform.gameObject;
      CenterController cc = center.GetComponent<CenterController>();

      if (!polygons.ContainsKey(cc.polyTag)) throw new ArgumentException("Polygon Tag \"" + cc.polyTag + "\" could not be found before assigning center");

      Polygon polygon = polygons[cc.polyTag];

      // Assign center
      polygon.SetCenter(center);

      // Assign neighbors
      polygon.SetNeighborTags(cc.neighborTags);
    }

    Map.checkPolygonValidity(polygons);

    return new Map(polygons);
  }

  private static void checkPolygonValidity(Dictionary<string, Polygon> polygons) {
    foreach (Polygon polygon in polygons.Values) {
      // Verify its a triangle
      if (polygon.vertices.Count != 3) {
        throw new Exception("polygon does not have 3 vertices");
      }

      // Verify it has a center
      if (polygon.center == null) {
        throw new Exception("polygon does not have a center");
      }
    }
  }
}