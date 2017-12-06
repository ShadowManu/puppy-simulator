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
    var location = new Location(GameObject.Find("Ball").transform.position);

    action.Add(new ChaseAction(kinematic, location));

    exitAction.Add(new ChangeBallAction(BallState.Puppy));

    transitions.Add(new PuppyFollowBallToReturnBallTransition());
  }
}

public class PuppyFollowPlayerState : State {

  public PuppyFollowPlayerState() {
    var puppy = GameObject.Find("Puppy").GetComponent<PuppyController>().kinematic;
    var player = GameObject.Find("Player").GetComponent<PlayerController>().kinematic;

    action.Add(new ArriveAction(puppy, new Steering(), player));

    transitions.Add(new PuppyFollowPlayerToFollowBallTransition());
    transitions.Add(new PuppyFollowPlayerToIdleTransition());
  }

}

public class PuppyReturnBallState : State {

  public PuppyReturnBallState() {
    var kinematic = GameObject.Find("Puppy").GetComponent<PuppyController>().kinematic;
    var location = new Location(GameObject.Find("Player").transform.position);

    action.Add(new ChaseAction(kinematic, location));

    exitAction.Add(new ChangeBallAction(BallState.Player));

    transitions.Add(new PuppyReturnBallToIdleTransition());
  }
}