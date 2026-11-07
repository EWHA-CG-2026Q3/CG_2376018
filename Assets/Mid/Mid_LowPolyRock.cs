using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class Mid_LowPolyRock : MonoBehaviour
{
    [SerializeField] private float roughness = 0.2f;

    void Start()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        Mesh original = meshFilter.sharedMesh;

        // 기본 Sphere를 직접 수정하지 않고 복제
        Mesh mesh = Instantiate(original);
        Vector3[] vertices = mesh.vertices;

        // 표면이 가시처럼 튀어나오는 현상 방지
        Random.InitState(42);

        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 p = vertices[i];

            float noise =
                Mathf.Sin(p.x * 5f) *
                Mathf.Cos(p.y * 4f) *
                Mathf.Sin(p.z * 5f);

            vertices[i] = p + p.normalized * noise * roughness;
        }

        mesh.vertices = vertices;

        // 각 삼각형이 독립된 정점을 사용하도록 변경
        int[] triangles = mesh.triangles;
        Vector3[] flatVertices = new Vector3[triangles.Length];
        int[] flatTriangles = new int[triangles.Length];

        for (int i = 0; i < triangles.Length; i++)
        {
            flatVertices[i] = vertices[triangles[i]];
            flatTriangles[i] = i;
        }

        mesh.Clear();
        mesh.vertices = flatVertices;
        mesh.triangles = flatTriangles;

        // 각 면의 Normal을 별도로 계산
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        meshFilter.mesh = mesh;
    }
}