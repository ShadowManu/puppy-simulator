public class BallStateCondition : Condition {
  BallState state;

  public BallStateCondition(BallState state) {
    this.state = state;
  }

  public bool Test() {
    return state == WorldState.ball;
  }
}