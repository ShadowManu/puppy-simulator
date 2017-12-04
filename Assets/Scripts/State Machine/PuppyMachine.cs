using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuppyMachine {

  public List<State> states;
  public List<Action> entryActions;
  public List<Action> actions;
  public List<Action> exitActions;
  public List<Transition> transitions;
  public Action auxAction;
  
  public State lookOwner;
  public State lookBall;
  public State findBall;

  public Transition thrownBall;
  public Transition ballFound;

  


}