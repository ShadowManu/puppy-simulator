using System.Linq;

using TriangleNet.Meshing;

using UnityEngine;

public class ArriveAction : Action {
  Kinematic source;
  ILocation target;
  Steering sourceSteering;

  ArriveOptions arriveOptions = new ArriveOptions();
  AlignOptions alignOptions = new AlignOptions();

  public ArriveAction(Kinematic source, Steering sourceSteering, ILocation target) {
    this.source = source;
    this.sourceSteering = sourceSteering;
    this.target = target;
  }

  public void run() {
    var arrive = Dynamic.Arrive(source, sourceSteering, target, arriveOptions);
    var face = Dynamic.Face(source, sourceSteering, target, alignOptions);

    sourceSteering = arrive.Combine(face);
    source.UpdateKinematic(sourceSteering);
  }
}

public class IdleAction : Action {
  public void run() {}
}

public class ChaseAction : Action {
  Kinematic kinematic;
  Transform transform;

  Steering steering = new Steering();
  ArriveOptions arriveOptions = new ArriveOptions();
  AlignOptions alignOptions = new AlignOptions();

  IMesh mesh;

  public ChaseAction(Kinematic kinematic, Transform transform) {
    this.kinematic = kinematic;
    this.transform = transform;

    mesh = GameObject.Find("Map").GetComponent<ShapeCreator>().shapes.makeMesh();
  }

  public void run() {
    var graph = new Graph2(mesh);
    var source = graph.Quantize(kinematic.position);
    var dest = graph.Quantize(transform.position);

    var path = graph.FindPath(source, dest).Select(n => n.position).ToList();

    ILocation target;

    // Path is short, go straight
    if (path.Count <= 2) {
      target = new Location(transform.position);
    
    // Go to first segment of path
    } else {
      target = new Location(path[1]);
    }

    var arrive = Dynamic.Arrive(kinematic, steering, target, arriveOptions);
    var face = Dynamic.Face(kinematic, steering, new Location(transform.position), alignOptions);

    steering = arrive.Combine(face);

    kinematic.UpdateKinematic(steering);
  }
}