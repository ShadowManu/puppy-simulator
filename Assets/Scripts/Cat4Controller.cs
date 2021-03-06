using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Cat4Controller : MonoBehaviour {
  public Kinematic kinematic;
  public Steering steering { get; set; }
  public StateMachine stateMachine;

  void Start() {
    kinematic = new Kinematic(transform);
    steering = new Steering();
    stateMachine = new Cat4Machine();
  }

  void Update() {
    stateMachine.update().AndRun();
  }
}