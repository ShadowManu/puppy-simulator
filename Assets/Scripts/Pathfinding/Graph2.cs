using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using IMesh = TriangleNet.Meshing.IMesh;
using TriangleNet.Meshing;
using TriangleNet.Topology;

/** Common representation of a Graph by list of nodes with lists of neighbors */
public class Graph2 {
  public Dictionary<int, Node2> nodes = new Dictionary<int, Node2>();

  public Graph2(IMesh mesh) {
    // Create Nodes for every triangle in the map
    foreach (var triangle in mesh.Triangles) {
      nodes.Add(triangle.ID, new Node2(triangle));
    }
  }

  /**
   * Runs Dijkstra over the graph, returning the path from source to destination.
   * returns null if the destination if not reachable from source.
   */
  public List<Node2> FindPath(Node2 source, Node2 destination) {
    SortedSet<Node2> open = new SortedSet<Node2>();
    HashSet<Node2> closed = new HashSet<Node2>();

    // Initialize source properties and open list
    source.path.Add(source);
    source.cost = 0;
    open.Add(source);

    // While nodes to process
    while (open.Count > 0) {
      // Pop shortest-path node
      Node2 current = open.Min;
      open.Remove(current);

      // End step: the current node is the destination
      if (current == destination) {
        return destination.path;
      }

      // Process each neighbor
      foreach (var id in current.neighbors) {
        Node2 neighbor = nodes[id];
        float cost = current.cost + (current.position - neighbor.position).magnitude;

        // Update cost and path if shortest
        if (cost < neighbor.cost) {
          neighbor.cost = cost;

          neighbor.path = new List<Node2>(current.path);
          neighbor.path.Add(neighbor);
        }

        // Mark neighbor as open
        // only if not closed already
        // if it already is, then it is noop (SortedSet implements this)
        if (!closed.Contains(neighbor)) open.Add(neighbor);
      }

      // Mark current as closed
      closed.Add(current);
    }

    return null;
  }
}