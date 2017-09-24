using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuppyController : MonoBehaviour
{
  public GameObject player;

	public SteeringMode STEERING_MODE;

  public float maxSpeed;
  public float minDistance;
  public float maxDistance;

  // Use this for initialization
  void Start()
  {
    player = GameObject.Find("Player");
  }

  // Update is called once per frame
  void Update()
  {
    Steering steering = getSteering();

		if (Input.GetKeyDown("m")) switchSteeringMode();

    // Only update position if its far enough
    if (canMove())
    {
			if (STEERING_MODE == SteeringMode.Seek) {
				transform.position += steering.velocity * Time.deltaTime;
			}
			if (STEERING_MODE == SteeringMode.Flee) {
				transform.position -= steering.velocity * Time.deltaTime;
			}
    }
  }

  public GameObject target
  {
    get { return player; }
  }

  Steering getSteering()
  {
    Vector3 velocity = target.transform.position - transform.position;
    velocity.y = 0;
    velocity = Vector3.Normalize(velocity);
    velocity *= maxSpeed;

    // Noop for now
    float rotation = 0.0f;

    return new Steering(velocity, rotation);
  }

  bool canMove()
  {
		if (STEERING_MODE == SteeringMode.None) return false;

		return
			(STEERING_MODE == SteeringMode.Seek && targetDistance >= minDistance) ||
			(STEERING_MODE == SteeringMode.Flee && targetDistance <= maxDistance);
  }

  public float targetDistance
  {
    get { return (target.transform.position - transform.position).magnitude; }
  }

	public void switchSteeringMode() {
		if (STEERING_MODE == SteeringMode.None) STEERING_MODE = SteeringMode.Seek;
		else if (STEERING_MODE == SteeringMode.Seek) STEERING_MODE = SteeringMode.Flee;
		else if (STEERING_MODE == SteeringMode.Flee) STEERING_MODE = SteeringMode.None;
	}
}

public enum SteeringMode {
	None,
	Seek,
	Flee
}

public struct Steering
{
  public Vector3 velocity;
  public float rotation;

  public Steering(Vector3 vel, float rot)
  {
    velocity = vel;
    rotation = rot;
  }
}
