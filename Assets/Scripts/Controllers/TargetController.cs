using UnityEngine;

public class TargetController : MonoBehaviour {
  new Renderer renderer;
  Kinematic player;

  bool showAndMove;
  new bool enabled;

  public static float floorHeight = 0.1f;

  void Start() {
    renderer = GetComponent<Renderer>();
    player = GameObject.Find("Player").GetComponent<PlayerController>().kinematic;

    DisableState();
  }

  void Update() {
    // TODO TEMPORAL
    if (Input.GetKeyDown("t")) showAndMove = !showAndMove;

    UpdateEnabledState();
    if (enabled) MoveTarget();
  }

  void MoveTarget() {
    float h = Input.GetAxis("Mouse X");
    float v = Input.GetAxis("Mouse Y");

    transform.position += new Vector3(h, 0, v);
  }

  void UpdateEnabledState() {
    if (showAndMove && !enabled) EnableState();
    if (!showAndMove && enabled) DisableState();
  }

  void EnableState() {
    enabled = true;
    renderer.enabled = true;
    transform.position = new Vector3(0, TargetController.floorHeight, 0) + Vector3.ProjectOnPlane(player.position, Vector3.up) + Kinematic.Orient2Vec(player.orientation) * 5;
  }

  void DisableState() {
    enabled = false;
    renderer.enabled = false;
  }
}