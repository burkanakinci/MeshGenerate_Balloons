using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[RequireComponent(typeof(MeshFilter))]
public class MeshGenerate : MonoBehaviour
{
    private Mesh mesh;
    private List<Vector3> vertices = new List<Vector3>();
    private List<int> triangles = new List<int>();
    private MeshRenderer meshRenderer;
    [SerializeField] private Material material;
    public GameObject parachute;
    void Start()
    {
        mesh = new Mesh();
        parachute.GetComponent<MeshFilter>().mesh = mesh;
        meshRenderer = parachute.GetComponent<MeshRenderer>();
        meshRenderer.material = material;

    }
    public void CreateShape(Vector3[] _linePoints)
    {

        VerticesAlign(_linePoints);

        triangles.Clear();

        triangles.Add(2);
        triangles.Add(3);
        triangles.Add(0);
        triangles.Add(3);
        triangles.Add(1);
        triangles.Add(0);

        int vertexConstant = 0;

        for (int j = 0; j < _linePoints.Length - 1; j++)
        {
            //LeftFaces
            triangles.Add(vertexConstant + 6);
            triangles.Add(vertexConstant + 7);
            triangles.Add(vertexConstant + 2);
            triangles.Add(vertexConstant + 7);
            triangles.Add(vertexConstant + 3);
            triangles.Add(vertexConstant + 2);

            //TopFaces
            triangles.Add(vertexConstant + 3);
            triangles.Add(vertexConstant + 7);
            triangles.Add(vertexConstant + 1);
            triangles.Add(vertexConstant + 7);
            triangles.Add(vertexConstant + 5);
            triangles.Add(vertexConstant + 1);

            //BottomFaces
            triangles.Add(vertexConstant);
            triangles.Add(vertexConstant + 4);
            triangles.Add(vertexConstant + 2);
            triangles.Add(vertexConstant + 4);
            triangles.Add(vertexConstant + 6);
            triangles.Add(vertexConstant + 2);

            //RightFaces
            triangles.Add(vertexConstant);
            triangles.Add(vertexConstant + 1);
            triangles.Add(vertexConstant + 4);
            triangles.Add(vertexConstant + 1);
            triangles.Add(vertexConstant + 5);
            triangles.Add(vertexConstant + 4);

            vertexConstant += 4;
        }

        triangles.Add(vertices.Count - 4);
        triangles.Add(vertices.Count - 3);
        triangles.Add(vertices.Count - 2);
        triangles.Add(vertices.Count - 3);
        triangles.Add(vertices.Count - 1);
        triangles.Add(vertices.Count - 2);

        UpdateMesh();
    }
    void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices.ToArray();

        mesh.triangles = triangles.ToArray();

        mesh.RecalculateNormals();

    }
    void VerticesAlign(Vector3[] linePoints)
    {


        vertices.Clear();

        for (int i = 0; i < linePoints.Length; i++)
        {
            vertices.Add(new Vector3(linePoints[i].x, linePoints[i].y - 0.5f, linePoints[i].z - 0.5f));
            vertices.Add(new Vector3(linePoints[i].x, linePoints[i].y + 0.5f, linePoints[i].z - 0.5f));
            vertices.Add(new Vector3(linePoints[i].x, linePoints[i].y - 0.5f, linePoints[i].z + 0.5f));
            vertices.Add(new Vector3(linePoints[i].x, linePoints[i].y + 0.5f, linePoints[i].z + 0.5f));
        }
    }
}