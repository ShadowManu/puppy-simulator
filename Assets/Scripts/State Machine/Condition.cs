using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Condition {

  public virtual bool Test() {
    return false;
  }
}

class FloatCondition: Condition {
  public float minValue;
  public float maxValue;    
  public float testValue;

  public void setCondition(float min, float max, float val) {
    minValue = min;
    maxValue = max;
    testValue = val;
  }

  public override bool Test() {
    return (minValue <= testValue) && (testValue <= maxValue);
  }
}

class AndCondition: Condition {
  public Condition conditionA;
  public Condition conditionB;

  public void setCondition(Condition a, Condition b) {
    conditionA = a;
    conditionB = b;
  }

  public override bool Test() {
    return conditionA.Test() && conditionB.Test();
  }
}

class NotCondition: Condition {
  public Condition condition;

  public void setCondition(Condition c) {
    condition = c;
  }

  public override bool Test() {
    return !condition.Test();
  }
}

class OrCondition: Condition {
  public Condition conditionA;
  public Condition conditionB;
  
  public void setCondition(Condition a, Condition b) {
    conditionA = a;
    conditionB = b;
  }

  public override bool Test() {
    return conditionA.Test() || conditionB.Test();
  }
}
