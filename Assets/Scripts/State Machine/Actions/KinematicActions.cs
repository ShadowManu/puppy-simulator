public class ArriveAction : Action {
  Kinematic source;
  Location target;
  Steering sourceSteering;

  ArriveOptions arriveOptions = new ArriveOptions();
  AlignOptions alignOptions = new AlignOptions();

  public ArriveAction(Kinematic source, Steering sourceSteering, Location target) {
    this.source = source;
    this.target = target;
  }

  public void run() {
    var arrive = Dynamic.Arrive(source, sourceSteering, target, arriveOptions);
    var face = Dynamic.Face(source, sourceSteering, target, alignOptions);

    var steering = arrive.Combine(face);
    source.UpdateKinematic(steering);
  }
}

public class IdleAction : Action {
  public void run() {}
}