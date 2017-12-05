using System.Collections.Generic;

public abstract class State {
  public List<Action> action = new List<Action>();
  public List<Action> entryAction = new List<Action>();
  public List<Action> exitAction = new List<Action>();
  public List<Transition> transitions = new List<Transition>();

  public List<Action> getAction() { return action; }
  public List<Action> getEntryAction() { return entryAction; }
  public List<Action> getExitAction() { return exitAction; }
  public List<Transition> getTransitions() { return transitions; }
}