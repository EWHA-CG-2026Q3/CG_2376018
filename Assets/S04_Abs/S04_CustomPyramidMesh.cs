using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class S04_CustomPyramidMesh : MonoBehaviour
{
    void Start()
    {
        Vector3[] vertices = new Vector3[]
        {
            new Vector3(0f, 1.5f, 0f),    // 0: 꼭짓점
            new Vector3(-1f, 0f, -1f),    // 1: 밑면
            new Vector3(1f, 0f, -1f),     // 2: 밑면
            new Vector3(1f, 0f, 1f),      // 3: 밑면
            new Vector3(-1f, 0f, 1f),     // 4: 밑면
        };

        int[] triangles = new int[]
        {
            // 밑면: 사각형을 삼각형 2개로 구성
            1, 2, 3,
            1, 3, 4,

            // 옆면: 삼각형 4개
            0, 2, 1,
            0, 3, 2,
            0, 4, 3,
            0, 1, 4,
        };

        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        GetComponent<MeshFilter>().mesh = mesh;
        GetComponent<MeshRenderer>().sharedMaterial =
            new Material(Shader.Find("Universal Render Pipeline/Lit"));
    }
}
