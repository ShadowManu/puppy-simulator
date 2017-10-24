using System;
using UnityEngine;

public class Triangle {
  public Vector3 point;

  protected static double DEFAULT_SIDE = 1;

  private int row;
  private int column;
  
  private double side;

  private GameObject gameObject;

  public Triangle(int row, int column): this(row, column, Triangle.DEFAULT_SIDE) { }

  public Triangle(int row, int column, double side) {
    this.row = row;
    this.column = column;
    this.side = side;

    // Build up Characteristic Point
    bool pointsUp = (row + column) % 2 == 0; 
    float x = (float) (column * side + (side / 2)); 
    float z = (float) ((row * height) + (pointsUp ? inRadius : inRadius * 2));
    point = new Vector3(x, 0, z);

    this.attachGameObject();
  }

  public double height { get { return side * Math.Sqrt(3) / 2; } }

  public double inRadius { get { return height / 3; } }

  private void attachGameObject() {
    GameObject block = GameObject.Find("Grass");
    gameObject = UnityEngine.Object.Instantiate(block, new Vector3(5, 1, 5), Quaternion.identity);

    Mesh mesh = new Mesh();
    gameObject.GetComponent<MeshFilter>().mesh = mesh;

    // TODO This does not flips it yet

    mesh.vertices = new Vector3[] {
      // TOP
      new Vector3(0, 1, 0),
      new Vector3((float) side / 2, 1, (float) height),
      new Vector3((float) side, 1, 0),
      // BOTTOM

      new Vector3(0, 0, 0),
      new Vector3((float) side / 2, 0, (float) height),
      new Vector3((float) side, 0, 0),
    };

    mesh.triangles = new int[] {
      // TOP
      0, 1, 2,
      // BOTTOM
      3, 5, 4,
      // BASE
      0, 2, 3,
      3, 2, 5,
      // LEFT
      0, 3, 1,
      1, 3, 4,
      // RIGHT
      1, 4, 2,
      2, 4, 5
    };

    mesh.RecalculateNormals();
  }
}