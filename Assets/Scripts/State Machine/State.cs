using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State {
  public List<Action> entryActions;
  public List<Action> actions;
  public List<Action> exitActions;
  public List<Transition> transitions;

  public void setState(List<Action> entry, List<Action> a, List<Action> exit, List<Transition> lt) {
    entryActions = entry;
    actions = a;
    exitActions = exit;
    transitions = lt;
  }
}