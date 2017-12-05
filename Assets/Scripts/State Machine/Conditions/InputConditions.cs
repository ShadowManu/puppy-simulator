using UnityEngine;

/** Returns true when key is pressed */
public class KeyDownCondition : Condition {
  string key;

  public KeyDownCondition(string key) {
    this.key = key;
  }
  
  public bool Test() { return Input.GetKeyDown(key); }
}