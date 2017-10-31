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

    foreach (Transform childTransform in vertices.transform) {
      GameObject child = childTransform.gameObject;
      String[] tags = child.GetComponent<VertexController>().tags;

      foreach (string tag in tags) {
        if (!polygons.ContainsKey(tag)) polygons.Add(tag, new Polygon());
        polygons[tag].AddVertex(childTransform.position);
      }
    }

    // Query all centers and assign to polygons
    GameObject centers = GameObject.Find("Centers");

    foreach (Transform childTransform in centers.transform) {
      GameObject child = childTransform.gameObject;
      String polyTag = child.GetComponent<CenterController>().polyTag;

      if (!polygons.ContainsKey(polyTag)) throw new ArgumentException("Polygon Tag \"" + polyTag + "\" could not be found before assigning center");

      polygons[polyTag].SetCenter(childTransform.position);
    }

    return new Map(polygons);

  }
}