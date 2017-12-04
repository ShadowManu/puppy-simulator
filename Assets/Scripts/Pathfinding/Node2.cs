using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

using TriangleNet.Topology;

/** Representation of a Node Graph, with data coming internally from a Polygon */
public class Node2 : IComparable<Node2> {
  private Triangle triangle;

  public int ID { get { return triangle.ID; } }
  public Vector3 position;
  public List<int> neighbors;

  public float pathCost = float.MaxValue;
  public float heuristicCost = float.MaxValue;
  public List<Node2> path = new List<Node2>();

  public Node2(Triangle triangle) {
    this.triangle = triangle;
    this.position = calcCentroid(triangle);
    this.neighbors = new int[] { 0, 1, 2 }.Select(i => triangle.GetNeighborID(i)).Where(id => id >= 0).ToList();
  }

  public bool ContainsPoint(Vector3 p) {
    return triangle.ContainsPoint(p);
  }

  public int CompareTo(Node2 other) {
    if (this == other) return 0;
    return (pathCost + heuristicCost).CompareTo(other.pathCost + other.heuristicCost);
  }

  private Vector3 calcCentroid(Triangle triangle) {
    var v1 = triangle.GetVertex(0);
    var v2 = triangle.GetVertex(1);
    var v3 = triangle.GetVertex(2);

    var x = (float) (v1[0] + v2[0] + v3[0]) / 3;
    var z = (float) (v1[1] + v2[1] + v3[1]) / 3;
    return new Vector3(x, 0, z);
  }
}