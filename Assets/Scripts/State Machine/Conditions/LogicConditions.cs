/** Negate input condition */
class NotCondition : Condition {
  public Condition condition;

  public NotCondition(Condition c) { this.condition = c; }

  public bool Test() { return !condition.Test(); }
}

/** Joins condition with OR */
class OrCondition : Condition {
  public Condition a;
  public Condition b;
  
  public OrCondition(Condition a, Condition b) {
    this.a = a;
    this.b = b;
  }

  public bool Test() {
    return a.Test() || b.Test();
  }
}

/** Joins conditions with AND */
class AndCondition : Condition {
  public Condition a;
  public Condition b;

  public AndCondition(Condition a, Condition b) {
    this.a = a;
    this.b = b;
  }

  public bool Test() { return a.Test() && b.Test(); }
}