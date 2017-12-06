using UnityEngine;

public class PuppyFollowBallToReturnBallTransition : Transition {
  public PuppyFollowBallToReturnBallTransition() {
    var puppy = GameObject.Find("Puppy").transform;
    var ball = GameObject.Find("Ball").transform;

    condition = new ClosenessCondition(puppy, ball, 0.5f);
  }

  public override State getTargetState() {
    return new PuppyReturnBallState();
  }
}

public class PuppyFollowPlayerToFollowBallTransition : Transition {

  public PuppyFollowPlayerToFollowBallTransition() {
    condition = new BallStateCondition(BallState.Floor);
  }

  public override State getTargetState() {
    return new PuppyFollowBallState();
  }
}

public class PuppyFollowPlayerToIdleTransition : Transition {

  public PuppyFollowPlayerToIdleTransition() {
    var puppy = GameObject.Find("Puppy").transform;
    var player = GameObject.Find("Player").transform;

    condition = new ClosenessCondition(puppy, player, 1f);
  }

  public override State getTargetState() {
    return new PuppyIdleState();
  }
}

public class PuppyIdleToFollowBallTransition : Transition {

  public PuppyIdleToFollowBallTransition() {
    condition = new BallStateCondition(BallState.Floor);
  }

  public override State getTargetState() {
    return new PuppyFollowBallState();
  }
}

public class PuppyIdleToFollowPlayerTransition : Transition {

  public PuppyIdleToFollowPlayerTransition() {
    var puppy = GameObject.Find("Puppy").transform;
    var player = GameObject.Find("Player").transform;

    condition = new NotCondition(new ClosenessCondition(puppy, player, 6f));
  }

  public override State getTargetState() {
    return new PuppyFollowPlayerState();
  }
}

public class PuppyReturnBallToIdleTransition : Transition {

  public PuppyReturnBallToIdleTransition() {
    var puppy = GameObject.Find("Puppy").transform;
    var player = GameObject.Find("Player").transform;

    condition = new ClosenessCondition(puppy, player, 1f);
  }

  public override State getTargetState() {
    return new PuppyIdleState();
  }
}