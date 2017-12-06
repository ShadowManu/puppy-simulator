using UnityEngine;

public class PuppyIdleState : State {

  public PuppyIdleState() {
    action.Add(new IdleAction());

    transitions.Add(new PuppyIdleToFollowBallTransition());
    transitions.Add(new PuppyIdleToFollowPlayerTransition());
  }
}

public class PuppyFollowBallState : State {

  public PuppyFollowBallState() {
    var kinematic = GameObject.Find("Puppy").GetComponent<PuppyController>().kinematic;
    var transform = GameObject.Find("Ball").transform;

    action.Add(new ChaseAction(kinematic, transform));

    exitAction.Add(new ChangeBallAction(BallState.Puppy));

    transitions.Add(new PuppyFollowBallToReturnBallTransition());
  }
}

public class PuppyFollowPlayerState : State {

  public PuppyFollowPlayerState() {
    var kinematic = GameObject.Find("Puppy").GetComponent<PuppyController>().kinematic;
    var transform = GameObject.Find("Player").GetComponent<PlayerController>().transform;

    action.Add(new ChaseAction(kinematic, transform));

    transitions.Add(new PuppyFollowPlayerToFollowBallTransition());
    transitions.Add(new PuppyFollowPlayerToIdleTransition());
  }

}

public class PuppyReturnBallState : State {

  public PuppyReturnBallState() {
    var kinematic = GameObject.Find("Puppy").GetComponent<PuppyController>().kinematic;
    var transform = GameObject.Find("Player").transform;

    action.Add(new ChaseAction(kinematic, transform));

    exitAction.Add(new ChangeBallAction(BallState.Player));

    transitions.Add(new PuppyReturnBallToIdleTransition());
  }
}