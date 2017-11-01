using System;
using System.Collections.Generic;
using UnityEngine;

/** Represents a triangle in space with useful related methods */
public class Polygon {
  public List<GameObject> vertices = new List<GameObject>();
  public GameObject center;
  public string[] neighborTags;

  public void AddVertex(GameObject vertex) {
    this.vertices.Add(vertex);
  }

  public void SetCenter(GameObject center) {
    this.center = center;
  }

  public void SetNeighborTags(string[] neighborTags) {
    this.neighborTags = neighborTags;
  }

  /**
   * Defines if points is inside triangle
   * Based on http://wiki.unity3d.com/index.php?title=PolyContainsPoint
   */
  public bool ContainsPoint(Vector3 p) {
    List<Vector3> v = new List<Vector3>();

    foreach (GameObject go in vertices) {
      v.Add(go.transform.position);
    }

    int j = v.Capacity - 1; // Last index
    bool inside = false; 

    for (int i = 0; i < v.Count; j = i++) { 
      if ( ((v[i].z <= p.z && p.z < v[j].z) || (v[j].z <= p.z && p.z < v[i].z)) && 
         (p.x < (v[j].x - v[i].x) * (p.z - v[i].z) / (v[j].z - v[i].z) + v[i].x)) 
         inside = !inside; 
    } 
    return inside; 
  }
}