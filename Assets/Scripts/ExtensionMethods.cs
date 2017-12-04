using System.Collections.Generic;
using System.Linq;

using TriangleNet.Geometry;
using IMesh = TriangleNet.Meshing.IMesh;

using UnityEngine;

public static class ExtensionMethods {
  public static Vector2 ToXZ(this Vector3 v3) {
    return new Vector2(v3.x, v3.z);
  }

  public static Vector3 ToVector3(this Vertex vert) {
    return new Vector3((float) vert[0], 0, (float) vert[1]);
  }

  public static List<Vertex> ToListVertex(this List<Vector3> points) {
    return points.Select(p => new Vertex(p.x, p.z)).ToList();
  }

  /**
   * Defines if points is inside triangle
   * Based on http://wiki.unity3d.com/index.php?title=PolyContainsPoint
   */
  public static bool ContainsPoint(this TriangleNet.Topology.Triangle triangle, Vector3 p) {
    Vector3[] v = triangle.vertices.Select(vert => vert.ToVector3()).ToArray();

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
   * Creates a TriangleNet IMesh from a list of shapes. The first shape is considered the base
   * and the rest as hole in that base.
   */
  public static IMesh makeMesh(this List<Shape> shapes) {
    var polygon = new TriangleNet.Geometry.Polygon();

    // Add shapes
    for (int i = 0; i < shapes.Count; i++) {
      var points = shapes[i].points;
      if (points.Count < 3) return null;

      var isHole = i > 0;
      polygon.Add(new Contour(points.ToListVertex()), isHole);
    }

    return polygon.Triangulate();
  }
}