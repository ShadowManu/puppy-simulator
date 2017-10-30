using System;
using System.Linq;
using UnityEngine;

public class OldTriangle {
  public Vector3 point;

  protected static double DEFAULT_SIDE = 3;

  private int row;
  private int column;
  
  private double side;

  private GameObject gameObject;

  public OldTriangle(int row, int column): this(row, column, Triangle.DEFAULT_SIDE) { }

  public OldTriangle(int row, int column, double side) {
    this.row = row;
    this.column = column;
    this.side = side;

    this.calcCharacteristicPoint();
    this.attachGameObject();
  }

  public double height { get { return side * Math.Sqrt(3) / 2; } }

  public double inRadius { get { return height / 3; } }

  public bool pointsUp { get { return (row + column) % 2 != 0; } }

  private void calcCharacteristicPoint() {
    float x = (float) ((column + 1) * side / 2);
    float z = (float) ((row * height) + (pointsUp ? inRadius : inRadius * 2));
    point = new Vector3(x, 0, z);
  }

  private void attachGameObject() {
    GameObject block = GameObject.Find("Grass");
    gameObject = UnityEngine.Object.Instantiate(block, point, Quaternion.identity);

    Mesh mesh = new Mesh();
    gameObject.GetComponent<MeshFilter>().mesh = mesh;

    float RADIUS = (float) height / 3;
    float HALF = (float) side / 2;

    Vector3[] ov;

    if (pointsUp) {
      ov = new Vector3[] {
        // TOP
        new Vector3(-HALF, 1, -RADIUS),
        new Vector3(0, 1, 2 * RADIUS),
        new Vector3(+HALF, 1, -RADIUS),

        // BOTTOM
        new Vector3(-HALF, 0, -RADIUS),
        new Vector3(0, 0, 2 * RADIUS),
        new Vector3(+HALF, 0, -RADIUS),
      };

    } else {
      ov = new Vector3[] {
        // TOP
        new Vector3(+HALF, 1, +RADIUS),
        new Vector3(0, 1, -2 * RADIUS),
        new Vector3(-HALF, 1, +RADIUS),
        // BOTTOM
        new Vector3(+HALF, 0, +RADIUS),
        new Vector3(0, 0, -2 * RADIUS),
        new Vector3(-HALF, 0, +RADIUS),
      };
    }

    mesh.vertices = new Vector3[] {
      // Top
      ov[0], ov[1], ov[2],
      // Lefts
      ov[0], ov[3], ov[4],
      ov[0], ov[4], ov[1],
      // Rights
      ov[1], ov[4], ov[5],
      ov[1], ov[5], ov[2],
      // Front
      ov[0], ov[2], ov[5],
      ov[0], ov[5], ov[3],
      // Bottom
      ov[3], ov[5], ov[4],
    };

    mesh.triangles = Enumerable.Range(0, mesh.vertices.Length).ToArray();

    // UV for texturing
    Vector2[] uvs = new Vector2[mesh.vertices.Length];
    for (int i = 0; i < uvs.Length; i++) {
      uvs[i] = new Vector2(mesh.vertices[i].x, mesh.vertices[i].z);
    }
    mesh.uv = uvs;

    mesh.RecalculateNormals();
  }
}