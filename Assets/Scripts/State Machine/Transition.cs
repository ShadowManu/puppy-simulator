using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Transition {

  public Condition condition;
  public State targetState;
  public List<Action> action = new List<Action>();

  public bool isTriggered() { return condition.Test(); }
  public State getTargetState() { return targetState; }
  public List<Action> getAction() { return action; }
}