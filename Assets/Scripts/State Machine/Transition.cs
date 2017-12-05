using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Transition {

  public Condition condition;
  public List<Action> action = new List<Action>();

  public bool isTriggered() { return condition.Test(); }
  public List<Action> getAction() { return action; }

  public abstract State getTargetState();
}