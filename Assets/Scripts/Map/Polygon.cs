using System;
using System.Collections.Generic;
using UnityEngine;

/** Represents a triangle in space with useful related methods */
public class Polygon {
  public List<Vector3> vertices = new List<Vector3>();
  public Vector3 center;

  public void AddVertex(Vector3 vertex) {
    this.vertices.Add(vertex);
  }

  /**
   * Defines if points is inside triangle
   * Based on http://wiki.unity3d.com/index.php?title=PolyContainsPoint
   */
  public bool ContainsPoint(Vector3 p) {
    List<Vector3> v = vertices;

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