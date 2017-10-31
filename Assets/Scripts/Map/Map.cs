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

    // Query all centers and assign to polygons
    GameObject centers = GameObject.Find("Centers");

    foreach (Transform transform in centers.transform) {
      GameObject center = transform.gameObject;
      String polyTag = center.GetComponent<CenterController>().polyTag;

      if (!polygons.ContainsKey(polyTag)) throw new ArgumentException("Polygon Tag \"" + polyTag + "\" could not be found before assigning center");

      polygons[polyTag].SetCenter(center);
    }

    return new Map(polygons);

  }
}