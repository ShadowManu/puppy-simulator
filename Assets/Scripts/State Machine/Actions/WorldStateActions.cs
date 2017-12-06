
public class ChangeBallAction : Action {
  BallState state;

  public ChangeBallAction(BallState state) {
    this.state = state;
  }

  public void run() {
    WorldState.ball = state;
  }
}