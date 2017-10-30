using System;
using UnityEngine;

/** Represents a triangle in space with useful related methods */
public class Triangle {
  public Vector3 a;
  public Vector3 b;
  public Vector3 c;

  public Triangle(Vector3 a, Vector3 b, Vector3 c) {
    this.a = a;
    this.b = b;
    this.c = c;
  }

  /**
   * Defines if points is inside triangle
   * Based on http://wiki.unity3d.com/index.php?title=PolyContainsPoint
   */
  public bool containsPoint(Vector3 p) {
    Vector3[] v = new Vector3[] { a, b, c };

    int j = v.Length - 1; // Last index
    bool inside = false; 

    for (int i = 0; i < v.Length; j = i++) { 
      if ( ((v[i].z <= p.z && p.z < v[j].z) || (v[j].z <= p.z && p.z < v[i].z)) && 
         (p.x < (v[j].x - v[i].x) * (p.z - v[i].z) / (v[j].z - v[i].z) + v[i].x)) 
         inside = !inside; 
    } 
    return inside; 
  }

  /**
   * Calculates triangle circumcenter. Useful to transform Delaunay triangulations
   * to Voronoi Diagrams (the circumcenters are the seeds of the diagram)
   */
  public Vector3 calcCircumcenter() {
    Vector3 center = new Vector3(0, 0, 0);

    Vector3 bP = b - a;
    Vector3 cP = c - a;

    float dP = 2 * (bP.x * cP.z - bP.z * cP.x);

    center.x = (cP.z * (sq(bP.x) + sq(bP.z)) - bP.z * (sq(cP.x) + sq(cP.z))) / dP;
    center.z = (bP.x * (sq(cP.x) + sq(cP.z)) - cP.x * (sq(bP.x) + sq(bP.z))) / dP;
    
    return center + a;
  }

  public float sq(float n) { return n * n; }

}