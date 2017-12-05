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