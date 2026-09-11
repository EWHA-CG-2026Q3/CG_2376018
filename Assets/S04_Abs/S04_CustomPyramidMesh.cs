using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class S04_CustomPyramidMesh : MonoBehaviour
{
    void Start()
    {
        Vector3[] vertices = new Vector3[]
        {
            new Vector3(0f, 1.5f, 0f),    // 0: 꼭짓점
            new Vector3(-1f, 0f, -1f),   // 1: 밑면
            new Vector3(1f, 0f, -1f),    // 2: 밑면
            new Vector3(1f, 0f, 1f),     // 3: 밑면
            new Vector3(-1f, 0f, 1f),    // 4: 밑면
        };
    }
}
