using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {
  public Kinematic kinematic;
  public Steering steering;
  public Transform target;

  public static Vector3 gravity = new Vector3(0, -20f, 0);
  public static float floorHeight = 0.25f;
  
  BallState currentState;

  Vector3 initialPosition;

  void Start() {
    kinematic = new Kinematic(transform);
    target = GameObject.Find("Target").transform;
    initialPosition = transform.position;
  }
  
  void Update() {
    SyncState();
    RunState();
  }

  /** Configures and syncs properties required on a state change */
  void SyncState() {
    if (WorldState.ball != currentState) {
      currentState = WorldState.ball;

      switch (currentState) {
        case BallState.Air:
          var velocity = Mathf.Max((transform.position - target.position).magnitude, 20);
          steering = new Steering();
          steering.velocity = Throwing.Velocity(transform.position, target.position + new Vector3(0, BallController.floorHeight, 0), velocity, gravity);
          break;
      }
    }
  }

  /** Runs actions for the current state */
  void RunState() {
    switch (currentState) {
      case BallState.Player:
        // Update position relative to puppy
        transform.position = GameObject.Find("Player").transform.position + new Vector3(0, 0, -1.1f);

        if (Input.GetKeyDown(KeyCode.Space)) WorldState.ball = BallState.Air;
        break;

      case BallState.Air:
        // Update position and velocity
        transform.position += steering.velocity * Time.deltaTime;
        steering.velocity += gravity * Time.deltaTime;
        
        // Stop moving
        if (transform.position.y <= BallController.floorHeight) WorldState.ball = BallState.Floor;
        break;
      
      case BallState.Puppy:
        var go = GameObject.Find("Puppy");
        var controller = go.GetComponent<PuppyController>();
        var offset = Kinematic.Orient2Vec(controller.kinematic.orientation) * 1.5f + new Vector3(0, 1f, 0);

        transform.position = go.transform.position + offset;
        break;
    }
  }
}

public enum BallState {
  None,
  Player,
  Air,
  Floor,
  Puppy
}