using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuppyController : MonoBehaviour {
  public GameObject player;

	public Mode mode = Mode.None;

  public float maxSpeed;
  public float minDistance;
  public float maxDistance;

  public GameObject target { get { return player; } }
  public float targetDistance { get { return (target.transform.position - transform.position).magnitude; } }

  /* Initialization */
  void Start() {
    player = GameObject.Find("Player");
  }

  /* Update is called once per frame */
  void Update() {
    Steering steering = Steering.GetSteering(gameObject, target, maxSpeed);

    // Change mode
		if (Input.GetKeyDown("m")) mode = mode.NextMode();

    // Update position
    if (CanMove()) {
			if (mode == Mode.Seek) transform.position += steering.velocity * Time.deltaTime;
			if (mode == Mode.Flee) transform.position -= steering.velocity * Time.deltaTime;
    }
  }

  /** Defines guard conditions to allow the position to be modified */
  bool CanMove() {
		return
      (mode == Mode.None) ||
			(mode == Mode.Seek && targetDistance >= minDistance) ||
			(mode == Mode.Flee && targetDistance <= maxDistance);
  }
}
