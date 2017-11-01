using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition {

  public List<Action> actions;
  public State targetState;
  public Condition condition;

  public void setTransition(List<Action> la, State ts, Condition c) {
    actions = la;
    targetState = ts;
    condition = c;
  }

  public bool isTriggered() {
    return condition.Test();
  }
  

}