using UnityEngine;

public class Cat1HideState : State {
  
  public Cat1HideState() {
    var kinematic = GameObject.Find("Cat1").GetComponent<Cat1Controller>().kinematic;
    var transform = GameObject.Find("Hideout1").transform;
    
    action.Add(new ChaseAction(kinematic, transform));

    transitions.Add(new Cat1HideToWaitTransition());
  }
}

public class Cat1IdleState : State {

  public Cat1IdleState() {
    action.Add(new IdleAction());

    transitions.Add(new Cat1IdleToHideTransition());
  }
}

public class Cat1ReturnToOriginState : State {

  public Cat1ReturnToOriginState() {
    var kinematic = GameObject.Find("Cat1").GetComponent<Cat1Controller>().kinematic;
    var transform = GameObject.Find("OriginCat1").transform;

    action.Add(new ChaseAction(kinematic, transform));

    transitions.Add(new Cat1ReturnToOriginToHideTransition());
    transitions.Add(new Cat1ReturnToOriginToIdleTransition());
  }
}

public class Cat1WaitSate : State {
  
  public Cat1WaitSate() {
    action.Add(new IdleAction());
    
    transitions.Add(new Cat1WaitToReturnToOriginTransition());
  }
}

public class Cat2HideState : State {
  
  public Cat2HideState() {
    var kinematic = GameObject.Find("Cat2").GetComponent<Cat2Controller>().kinematic;
    var transform = GameObject.Find("Hideout2").transform;
    
    action.Add(new ChaseAction(kinematic, transform));

    transitions.Add(new Cat2HideToWaitTransition());
  }
}

public class Cat2IdleState : State {

  public Cat2IdleState() {
    action.Add(new IdleAction());

    transitions.Add(new Cat2IdleToHideTransition());
  }
}

public class Cat2ReturnToOriginState : State {

  public Cat2ReturnToOriginState() {
    var kinematic = GameObject.Find("Cat2").GetComponent<Cat2Controller>().kinematic;
    var transform = GameObject.Find("OriginCat2").transform;

    action.Add(new ChaseAction(kinematic, transform));

    transitions.Add(new Cat2ReturnToOriginToHideTransition());
    transitions.Add(new Cat2ReturnToOriginToIdleTransition());
  }
}

public class Cat2WaitSate : State {
  
  public Cat2WaitSate() {
    action.Add(new IdleAction());
    
    transitions.Add(new Cat2WaitToReturnToOriginTransition());
  }
}

public class Cat3HideState : State {
  
  public Cat3HideState() {
    var kinematic = GameObject.Find("Cat3").GetComponent<Cat3Controller>().kinematic;
    var transform = GameObject.Find("Hideout3").transform;
    
    action.Add(new ChaseAction(kinematic, transform));

    transitions.Add(new Cat3HideToWaitTransition());
  }
}

public class Cat3IdleState : State {

  public Cat3IdleState() {
    action.Add(new IdleAction());

    transitions.Add(new Cat3IdleToHideTransition());
  }
}

public class Cat3ReturnToOriginState : State {

  public Cat3ReturnToOriginState() {
    var kinematic = GameObject.Find("Cat3").GetComponent<Cat3Controller>().kinematic;
    var transform = GameObject.Find("OriginCat3").transform;

    action.Add(new ChaseAction(kinematic, transform));

    transitions.Add(new Cat3ReturnToOriginToHideTransition());
    transitions.Add(new Cat3ReturnToOriginToIdleTransition());
  }
}

public class Cat3WaitSate : State {
  
  public Cat3WaitSate() {
    action.Add(new IdleAction());
    
    transitions.Add(new Cat3WaitToReturnToOriginTransition());
  }
}

public class Cat4HideState : State {
  
  public Cat4HideState() {
    var kinematic = GameObject.Find("Cat4").GetComponent<Cat4Controller>().kinematic;
    var transform = GameObject.Find("Hideout4").transform;
    
    action.Add(new ChaseAction(kinematic, transform));

    transitions.Add(new Cat4HideToWaitTransition());
  }
}

public class Cat4IdleState : State {

  public Cat4IdleState() {
    action.Add(new IdleAction());

    transitions.Add(new Cat4IdleToHideTransition());
  }
}

public class Cat4ReturnToOriginState : State {

  public Cat4ReturnToOriginState() {
    var kinematic = GameObject.Find("Cat4").GetComponent<Cat4Controller>().kinematic;
    var transform = GameObject.Find("OriginCat4").transform;

    action.Add(new ChaseAction(kinematic, transform));

    transitions.Add(new Cat4ReturnToOriginToHideTransition());
    transitions.Add(new Cat4ReturnToOriginToIdleTransition());
  }
}

public class Cat4WaitSate : State {
  
  public Cat4WaitSate() {
    action.Add(new IdleAction());
    
    transitions.Add(new Cat4WaitToReturnToOriginTransition());
  }
}