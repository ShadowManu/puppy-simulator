using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
  public Kinematic kinematic;

  public float speed;
  private Rigidbody rb;

  void Awake() {
    rb = GetComponent<Rigidbody>();
    kinematic = new Kinematic(transform);
  }
  
  void FixedUpdate () {
    float moveHorizontal = Input.GetAxis("Horizontal");
    float moveVertical = Input.GetAxis("Vertical");
    rb.AddForce(new Vector3(moveHorizontal, 0.0f, moveVertical) * speed);
  }
}
