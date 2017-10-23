using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuppyController : MonoBehaviour {
  public Kinematic kinematic;
  public Steering steering { get; set; }

  public Kinematic player;
  public Kinematic target { get { return player; } }

  public float angle;
  public Vector3 axis;

  public ArriveOptions arriveOpts = new ArriveOptions();
  public AlignOptions alignOptions = new AlignOptions();

  void Start() {
    kinematic = new Kinematic(transform);
    steering = new Steering();
    player = GameObject.Find("Player").GetComponent<PlayerController>().kinematic;
  }

  void Update() {
    Steering arrive = Dynamic.Arrive(kinematic, steering, target, arriveOpts);
    Steering align = Dynamic.Face(kinematic, steering, target, alignOptions);
    steering = arrive.Combine(align);

    kinematic.UpdateKinematic(steering);
  }
}