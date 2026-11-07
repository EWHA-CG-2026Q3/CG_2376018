using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Mid_CylinderMesh : MonoBehaviour
{
    [Header("원기둥 설정")]
    [SerializeField] private int segments = 24;
    [SerializeField] private float radius = 0.5f;
    [SerializeField] private float height = 2f;

    void Start()
    {
        segments = Mathf.Max(3, segments);

        Vector3[] vertices = new Vector3[segments * 2 + 2];
        int[] triangles = new int[segments * 12];

        float halfHeight = height / 2f;

        // ① 반복문으로 원기둥의 정점 생성
        for (int i = 0; i < segments; i++)
        {
            float angle = 2f * Mathf.PI * i / segments;
            float x = Mathf.Cos(angle) * radius;
            float z = Mathf.Sin(angle) * radius;

            vertices[i] = new Vector3(x, -halfHeight, z);
            vertices[i + segments] = new Vector3(x, halfHeight, z);
        }

        // 위쪽과 아래쪽 중심 정점
        int bottomCenter = segments * 2;
        int topCenter = segments * 2 + 1;

        vertices[bottomCenter] = new Vector3(0, -halfHeight, 0);
        vertices[topCenter] = new Vector3(0, halfHeight, 0);

        // ② 반복문으로 삼각형 연결
        for (int i = 0; i < segments; i++)
        {
            int next = (i + 1) % segments;
            int t = i * 12;

            // 옆면
            triangles[t] = i;
            triangles[t + 1] = i + segments;
            triangles[t + 2] = next;

            triangles[t + 3] = next;
            triangles[t + 4] = i + segments;
            triangles[t + 5] = next + segments;

            // 아래쪽 면
            triangles[t + 6] = bottomCenter;
            triangles[t + 7] = i;
            triangles[t + 8] = next;

            // 위쪽 면
            triangles[t + 9] = topCenter;
            triangles[t + 10] = next + segments;
            triangles[t + 11] = i + segments;
        }

        // ③ Mesh 생성
        Mesh mesh = new Mesh();
        mesh.name = "Mid_ProceduralCylinder";
        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        GetComponent<MeshFilter>().mesh = mesh;
    }
}