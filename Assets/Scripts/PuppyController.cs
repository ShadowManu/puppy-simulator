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
  List<Kinematic> path;
  Map map;

  void Start() {
    kinematic = new Kinematic(transform);
    steering = new Steering();
    player = GameObject.Find("Player").GetComponent<PlayerController>().kinematic;

    map = Map.GenerateMap();
    Graph graph = new Graph(map);

    Node source = graph.nodes["A"];
    Node destination = graph.nodes["D"];

    path = new List<Kinematic>(graph.FindPath(source, destination).Select(node => new Kinematic(node.polygon.center.transform)));
  }

  void Update() {
    if (Input.GetKeyDown("c")) map.ToggleCenters();
    if (Input.GetKeyDown("l")) map.ToggleLines();
    if (Input.GetKeyDown("v")) map.ToggleVertices();

    Kinematic target = path[index];

    if (((target.position - kinematic.position).magnitude) < 1 && index + 1 < path.Count) index++;

    Steering arrive = Dynamic.Arrive(kinematic, steering, target, arriveOpts);
    Steering align = Dynamic.Face(kinematic, steering, target, alignOptions);
    steering = arrive.Combine(align);

    kinematic.UpdateKinematic(steering);
  }
}