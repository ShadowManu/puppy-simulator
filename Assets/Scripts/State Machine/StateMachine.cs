using System.Collections.Generic;
using System.Linq;

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
      .Concat(targetState.getEntryAction())
      .ToList();

      currentState = targetState;

      return actions;
    }
    // Otherwise, return current action
    else return currentState.getAction();
  }
}

public class PuppyMachine : StateMachine {

  public PuppyMachine() {
    currentState = new PuppyIdleState();
  }
}

public class Cat1Machine : StateMachine {

  public Cat1Machine() {
    currentState = new Cat1IdleState();
  }
}

public class Cat2Machine : StateMachine {

  public Cat2Machine() {
    currentState = new Cat2IdleState();
  }
}

public class Cat3Machine : StateMachine {

  public Cat3Machine() {
    currentState = new Cat3IdleState();
  }
}

public class Cat4Machine : StateMachine {

  public Cat4Machine() {
    currentState = new Cat4IdleState();
  }
}