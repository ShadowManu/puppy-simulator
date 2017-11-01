using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/** Represents a triangle in space with useful related methods */
public class Polygon {
  public string name;
  public GameObject center;
  public GameObject[] vertices;
  public string[] neighborNames;

  public Polygon(string name, GameObject center, GameObject[] vertices, string[] neighborNames) {
    this.name = name;
    this.center = center;
    this.vertices = vertices;
    this.neighborNames = neighborNames;
  }

  /**
   * Defines if points is inside triangle
   * Based on http://wiki.unity3d.com/index.php?title=PolyContainsPoint
   */
  public bool ContainsPoint(Vector3 p) {
    Vector3[] v = vertices.Select(vert => vert.transform.position).ToArray();

    int j = v.Length - 1; // Last index
    bool inside = false; 

    for (int i = 0; i < v.Length; j = i++) { 
      if ( ((v[i].z <= p.z && p.z < v[j].z) || (v[j].z <= p.z && p.z < v[i].z)) && 
         (p.x < (v[j].x - v[i].x) * (p.z - v[i].z) / (v[j].z - v[i].z) + v[i].x)) 
         inside = !inside; 
    } 
    return inside; 
  }
}