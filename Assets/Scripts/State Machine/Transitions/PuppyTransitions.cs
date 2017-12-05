public class PuppyIdleToFollowPlayerTransition : Transition {

  public PuppyIdleToFollowPlayerTransition() {
    condition = new KeyDownCondition("h");
  }

  public override State getTargetState() {
    return new PuppyFollowPlayerState();
  }
}

public class PuppyFollowPlayerToIdleTransition : Transition {

  public PuppyFollowPlayerToIdleTransition() {
    condition = new KeyDownCondition("h");
  }

  public override State getTargetState() {
    return new PuppyIdleState();
  }
}