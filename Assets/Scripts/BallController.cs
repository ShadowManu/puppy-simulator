using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {
  public Vector3 gravity;
  public Vector3 velocity;
  public GameObject targetPosition;
  
  // Use this for initialization
  void Start () {
    targetPosition = GameObject.Find("ballTarget1");
    gravity = new Vector3 (0,-9.8f,0);
    velocity = Vector3.zero;
  }
  
  // Update is called once per frame
  void Update () {

    if (Input.GetKeyDown(KeyCode.Space)) {
      velocity = Throwing.Velocity(transform.position, targetPosition.transform.position, 15, gravity);
    }
    
    updatePosition();
    if (velocity != Vector3.zero) velocity += gravity * Time.deltaTime;
    if (transform.position.y <= 0.5) velocity = Vector3.zero;

  }

  void updatePosition() {
    transform.position += velocity * Time.deltaTime;
  }
}
