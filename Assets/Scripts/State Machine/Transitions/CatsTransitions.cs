using UnityEngine;

public class Cat1HideToWaitTransition : Transition {
  
  public Cat1HideToWaitTransition() {
    var cat = GameObject.Find("Cat1").transform;
    var hideout = GameObject.Find("Hideout1").transform;

    condition = new ClosenessCondition(cat, hideout, 0.1f);
  }

  public override State getTargetState() {
    return new Cat1WaitSate();
  }
}

public class Cat1IdleToHideTransition : Transition {

  public Cat1IdleToHideTransition() {
    var cat1 = GameObject.Find("Cat1").transform;
    var puppy = GameObject.Find("Puppy").transform;
    
    condition = new ClosenessCondition(cat1, puppy, 20f);
  }

  public override State getTargetState() {
    return new Cat1HideState();
  }
}

public class Cat1ReturnToOriginToIdleTransition : Transition {

  public Cat1ReturnToOriginToIdleTransition() {
    var cat = GameObject.Find("Cat1").transform;
    var origin = GameObject.Find("OriginCat1").transform;

    condition = new ClosenessCondition(cat, origin, 0.1f);
  }

  public override State getTargetState() {
    return new Cat1IdleState();
  }

}

public class Cat1ReturnToOriginToHideTransition : Transition {
  
  public Cat1ReturnToOriginToHideTransition() {
    var cat1 = GameObject.Find("Cat1").transform;
    var puppy = GameObject.Find("Puppy").transform;
    
    condition = new ClosenessCondition(cat1, puppy, 20f);
  }

  public override State getTargetState() {
    return new Cat1HideState();
  }
}

public class Cat1WaitToReturnToOriginTransition : Transition {
  
  public Cat1WaitToReturnToOriginTransition() {
    var cat1 = GameObject.Find("Cat1").transform;
    var puppy = GameObject.Find("Puppy").transform;

    condition = new NotCondition(new ClosenessCondition(cat1, puppy, 40f));
  }

  public override State getTargetState() {
    return new Cat1ReturnToOriginState();
  }
}

public class Cat2HideToWaitTransition : Transition {
  
  public Cat2HideToWaitTransition() {
    var cat = GameObject.Find("Cat2").transform;
    var hideout = GameObject.Find("Hideout2").transform;

    condition = new ClosenessCondition(cat, hideout, 0.1f);
  }

  public override State getTargetState() {
    return new Cat2WaitSate();
  }
}

public class Cat2IdleToHideTransition : Transition {

  public Cat2IdleToHideTransition() {
    var Cat2 = GameObject.Find("Cat2").transform;
    var puppy = GameObject.Find("Puppy").transform;
    
    condition = new ClosenessCondition(Cat2, puppy, 20f);
  }

  public override State getTargetState() {
    return new Cat2HideState();
  }
}

public class Cat2ReturnToOriginToIdleTransition : Transition {

  public Cat2ReturnToOriginToIdleTransition() {
    var cat = GameObject.Find("Cat2").transform;
    var origin = GameObject.Find("OriginCat2").transform;

    condition = new ClosenessCondition(cat, origin, 0.1f);
  }

  public override State getTargetState() {
    return new Cat2IdleState();
  }

}

public class Cat2ReturnToOriginToHideTransition : Transition {
  
  public Cat2ReturnToOriginToHideTransition() {
    var Cat2 = GameObject.Find("Cat2").transform;
    var puppy = GameObject.Find("Puppy").transform;
    
    condition = new ClosenessCondition(Cat2, puppy, 20f);
  }

  public override State getTargetState() {
    return new Cat2HideState();
  }
}

public class Cat2WaitToReturnToOriginTransition : Transition {
  
  public Cat2WaitToReturnToOriginTransition() {
    var Cat2 = GameObject.Find("Cat2").transform;
    var puppy = GameObject.Find("Puppy").transform;

    condition = new NotCondition(new ClosenessCondition(Cat2, puppy, 40f));
  }

  public override State getTargetState() {
    return new Cat2ReturnToOriginState();
  }
}

public class Cat3HideToWaitTransition : Transition {
  
  public Cat3HideToWaitTransition() {
    var cat = GameObject.Find("Cat3").transform;
    var hideout = GameObject.Find("Hideout3").transform;

    condition = new ClosenessCondition(cat, hideout, 0.1f);
  }

  public override State getTargetState() {
    return new Cat3WaitSate();
  }
}

public class Cat3IdleToHideTransition : Transition {

  public Cat3IdleToHideTransition() {
    var Cat3 = GameObject.Find("Cat3").transform;
    var puppy = GameObject.Find("Puppy").transform;
    
    condition = new ClosenessCondition(Cat3, puppy, 20f);
  }

  public override State getTargetState() {
    return new Cat3HideState();
  }
}

public class Cat3ReturnToOriginToIdleTransition : Transition {

  public Cat3ReturnToOriginToIdleTransition() {
    var cat = GameObject.Find("Cat3").transform;
    var origin = GameObject.Find("OriginCat3").transform;

    condition = new ClosenessCondition(cat, origin, 0.1f);
  }

  public override State getTargetState() {
    return new Cat3IdleState();
  }

}

public class Cat3ReturnToOriginToHideTransition : Transition {
  
  public Cat3ReturnToOriginToHideTransition() {
    var Cat3 = GameObject.Find("Cat3").transform;
    var puppy = GameObject.Find("Puppy").transform;
    
    condition = new ClosenessCondition(Cat3, puppy, 20f);
  }

  public override State getTargetState() {
    return new Cat3HideState();
  }
}

public class Cat3WaitToReturnToOriginTransition : Transition {
  
  public Cat3WaitToReturnToOriginTransition() {
    var Cat3 = GameObject.Find("Cat3").transform;
    var puppy = GameObject.Find("Puppy").transform;

    condition = new NotCondition(new ClosenessCondition(Cat3, puppy, 40f));
  }

  public override State getTargetState() {
    return new Cat3ReturnToOriginState();
  }
}

public class Cat4HideToWaitTransition : Transition {
  
  public Cat4HideToWaitTransition() {
    var cat = GameObject.Find("Cat4").transform;
    var hideout = GameObject.Find("Hideout4").transform;

    condition = new ClosenessCondition(cat, hideout, 0.1f);
  }

  public override State getTargetState() {
    return new Cat4WaitSate();
  }
}

public class Cat4IdleToHideTransition : Transition {

  public Cat4IdleToHideTransition() {
    var Cat4 = GameObject.Find("Cat4").transform;
    var puppy = GameObject.Find("Puppy").transform;
    
    condition = new ClosenessCondition(Cat4, puppy, 20f);
  }

  public override State getTargetState() {
    return new Cat4HideState();
  }
}

public class Cat4ReturnToOriginToIdleTransition : Transition {

  public Cat4ReturnToOriginToIdleTransition() {
    var cat = GameObject.Find("Cat4").transform;
    var origin = GameObject.Find("OriginCat4").transform;

    condition = new ClosenessCondition(cat, origin, 0.1f);
  }

  public override State getTargetState() {
    return new Cat4IdleState();
  }

}

public class Cat4ReturnToOriginToHideTransition : Transition {
  
  public Cat4ReturnToOriginToHideTransition() {
    var Cat4 = GameObject.Find("Cat4").transform;
    var puppy = GameObject.Find("Puppy").transform;
    
    condition = new ClosenessCondition(Cat4, puppy, 20f);
  }

  public override State getTargetState() {
    return new Cat4HideState();
  }
}

public class Cat4WaitToReturnToOriginTransition : Transition {
  
  public Cat4WaitToReturnToOriginTransition() {
    var Cat4 = GameObject.Find("Cat4").transform;
    var puppy = GameObject.Find("Puppy").transform;

    condition = new NotCondition(new ClosenessCondition(Cat4, puppy, 40f));
  }

  public override State getTargetState() {
    return new Cat4ReturnToOriginState();
  }
}