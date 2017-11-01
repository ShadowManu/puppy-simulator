using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class GenericMachine {

  public List<State> states;
  public List<Action> actions = new List<Action>();
  public State initState;
  public State currentState;

  public void SetMachine(List<State> ls, State init) {
    states = ls;
    initState = init;
    currentState = init;
  }

  public List<Action> Update() {
    Transition triggeredTransition;
    State targetState;

    triggeredTransition = null;
    foreach (Transition t in currentState.transitions) {
      if (t.isTriggered()) {
        triggeredTransition = t;
        break;
      }
    }

    if (triggeredTransition != null) {
      targetState = triggeredTransition.targetState;
      actions = (List<Action>) currentState.exitActions.Concat(triggeredTransition.actions.Concat(targetState.entryActions));
      currentState = targetState;
      return actions;
    }
    else {
      return currentState.actions;
    }
  }

}
