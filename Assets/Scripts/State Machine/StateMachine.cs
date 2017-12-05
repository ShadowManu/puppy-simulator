using System.Collections.Generic;

/** This class implements a generic Finite State Machine */
public class StateMachine {

  public State currentState;

  /** Returns a list of actions to run as the result of the State Machine */
  public List<Action> update() {
    List<Transition> transitions = currentState.getTransitions();

    // Find first triggered transition (if it exists)
    var triggeredTransition = transitions.Find(trans => trans.isTriggered());

    // If it does
    if (triggeredTransition != null) {
      var targetState = triggeredTransition.getTargetState();

      // Collect actions from the transition timeline
      var actions = currentState.getExitAction()
      .Concat(triggeredTransition.getAction())
      .Concat(targetState.getEntryAction());

      currentState = targetState;

      return actions;
    }
    // Otherwise, return current action
    else return currentState.getAction();
  }
}
