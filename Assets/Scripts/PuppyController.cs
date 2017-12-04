using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PuppyController : MonoBehaviour {
  public Kinematic kinematic;
  public Steering steering { get; set; }

  public Kinematic player;
  // public Kinematic target { get { return player; } }

  public float angle;
  public Vector3 axis;

  public ArriveOptions arriveOpts = new ArriveOptions();
  public AlignOptions alignOptions = new AlignOptions();

  // Path testing
  int index = 0;
  List<Location> path;

  void Start() {
    kinematic = new Kinematic(transform);
    steering = new Steering();
    player = GameObject.Find("Player").GetComponent<PlayerController>().kinematic;

    path = makePathFromMap();
  }

  private List<Location> makePathFromMap() {
    ShapeCreator creator = GameObject.Find("Map").GetComponent<ShapeCreator>();
    var mesh = creator.shapes.makeMesh();
    var graph = new Graph2(mesh);
    
    var nodeList = graph.nodes.Values;
    var src = nodeList.First();
    var dst = nodeList.Last();

    var nodePath = graph.FindPath(src, dst);
    return nodePath.Select(n => new Location(n.position)).ToList();
  }

  void Update() {
    // if (Input.GetKeyDown("c")) map.ToggleCenters();
    // if (Input.GetKeyDown("l")) map.ToggleLines();
    // if (Input.GetKeyDown("v")) map.ToggleVertices();

    Location target = path[index];

    if (((target.position - kinematic.position).magnitude) < 1 && index + 1 < path.Count) index++;

    Steering arrive = Dynamic.Arrive(kinematic, steering, target, arriveOpts);
    Steering align = Dynamic.Face(kinematic, steering, target, alignOptions);
    steering = arrive.Combine(align);

    kinematic.UpdateKinematic(steering);
  }
}