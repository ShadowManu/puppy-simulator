using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuppyController : MonoBehaviour, SteeringElement {
  public Steering steering { get; set; }

  public Element player;
  public Element target { get { return player; } }

  public ArriveOptions arriveOpts = new ArriveOptions();

  /* Initialization */
  void Start() { Init(); }

  /* Update is called once per frame */
  void Update() {
    Init();
    // Change mode
    // if (Input.GetKeyDown("m")) mode = mode.NextMode();

    steering = Dynamic.Arrive(this, target, arriveOpts);
    updatePosition(steering);
  }

  /** Update the object position given a steerig  */
  void updatePosition(Steering steering) {
    transform.position += steering.velocity * Time.deltaTime;
  }

  private void Init() {
    if (player == null) player = GameObject.Find("Player").GetComponent<PlayerController>();
    if (steering == null) steering = new Steering();
  }
}
