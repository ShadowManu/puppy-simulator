using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuppyController : MonoBehaviour {
  public GameObject player;
  public Mode mode = Mode.None;

  public GameObject target { get { return player; } }
  public float targetDistance { get { return (target.transform.position - transform.position).magnitude; } }

  /* Initialization */
  void Start() {
    player = GameObject.Find("Player");
  }

  /* Update is called once per frame */
  void Update() {
    Steering steering = Steering.GetSteering(gameObject, target, new SteeringOptions(mode));

    updatePosition(steering);

    // Change mode
    if (Input.GetKeyDown("m")) mode = mode.NextMode();
  }

  /** Update the object position given a steerig  */
  void updatePosition(Steering steering) {
    transform.position += steering.velocity * Time.deltaTime;
  }
}
