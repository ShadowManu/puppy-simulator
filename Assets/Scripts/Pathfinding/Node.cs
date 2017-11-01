using System;
using System.Collections.Generic;
using UnityEngine;

/** Representation of a Node Graph, with data coming internally from a Polygon */
public class Node : IComparable {
  public Polygon polygon;

  public Vector3 position { get { return polygon.center.transform.position; } }
  public string[] neighborTags { get { return polygon.neighborTags; } }

  public float cost = float.MaxValue;
  public List<Node> path = new List<Node>();

  public Node(Polygon polygon) {
    this.polygon = polygon;
  }

  public int CompareTo(System.Object obj) {
    Node other = obj as Node;
    return cost.CompareTo(other.cost);
  }
}