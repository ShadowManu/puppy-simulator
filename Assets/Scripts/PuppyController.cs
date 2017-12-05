using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PuppyController : MonoBehaviour {
  public Kinematic kinematic;
  public Steering steering { get; set; }
  public StateMachine stateMachine = new PuppyMachine();

  void Start() {
    kinematic = new Kinematic(transform);
    steering = new Steering();
  }

  // private List<Location> makePathFromMap() {
  //   ShapeCreator creator = GameObject.Find("Map").GetComponent<ShapeCreator>();
  //   var mesh = creator.shapes.makeMesh();
  //   var graph = new Graph2(mesh);
    
  //   var src = graph.Quantize(kinematic.position);
  //   var dst = graph.Quantize(player.position);

  //   var nodePath = graph.FindPath(src, dst);
  //   return nodePath.Select(n => new Location(n.position)).ToList();
  // }

  void Update() {
    stateMachine.update().AndRun();
  }
}