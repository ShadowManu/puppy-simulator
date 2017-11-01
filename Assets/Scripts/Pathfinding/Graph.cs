using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/** Common representation of a Graph by list of nodes with lists of neighbors */
public class Graph {
  public Dictionary<string, Node> nodes = new Dictionary<string, Node>();

  public Graph(Map map) {
    // Create Nodes for every polygon in the map
    foreach (KeyValuePair<string, Polygon> entry in map.polygons) {
      nodes.Add(entry.Key, new Node(entry.Value));
    }
  }

  /**
   * Runs Dijkstra over the graph, returning the path from source to destination.
   * returns null if the destination if not reachable from source.
   */
  public List<Node> FindPath(Node source, Node destination) {
    SortedSet<Node> open = new SortedSet<Node>();
    HashSet<Node> closed = new HashSet<Node>();

    // Initialize source properties and open list
    source.path.Add(source);
    source.cost = 0;
    open.Add(source);

    // While nodes to process
    while (open.Count > 0) {
      // Pop shortest-path node
      Node current = open.Min;
      open.Remove(current);

      // End step: the current node is the destination
      if (current == destination) {
        return destination.path;
      }

      // Process each neighbor
      foreach (string tag in current.neighborTags) {
        Node neighbor = nodes[tag];
        float cost = current.cost + (current.position - neighbor.position).magnitude;

        // Update cost and path if shortest
        if (cost < neighbor.cost) {
          neighbor.cost = cost;

          neighbor.path = new List<Node>(current.path);
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