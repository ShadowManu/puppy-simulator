using UnityEngine;

public class PuppyIdleState : State {

  public PuppyIdleState() {
    action.Add(new IdleAction());

    transitions.Add(new PuppyIdleToFollowPlayerTransition());
  }
}

public class PuppyFollowPlayerState : State {

  public PuppyFollowPlayerState() {
    var puppy = GameObject.Find("Puppy").GetComponent<PuppyController>().kinematic;
    var player = GameObject.Find("Player").GetComponent<PlayerController>().kinematic;

    action.Add(new ArriveAction(puppy, new Steering(), player));

    transitions.Add(new PuppyFollowPlayerToIdleTransition());
  }

}