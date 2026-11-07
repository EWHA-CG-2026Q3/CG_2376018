using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Mid_CrystalMesh : MonoBehaviour
{
    [Header("다이아몬드 원본 정점 (0=아래 꼭짓점, 1~4=중간 사각형, 5=위 꼭짓점)")]
    [SerializeField]
    Vector3[] baseVertices = new Vector3[]
    {
        new Vector3(0.5f, 0f,   0.5f), // 0 — 아래 꼭짓점
        new Vector3(0f,   0.5f, 0f),   // 1 — 중간 사각형
        new Vector3(1f,   0.5f, 0f),   // 2
        new Vector3(1f,   0.5f, 1f),   // 3
        new Vector3(0f,   0.5f, 1f),   // 4
        new Vector3(0.5f, 1f,   0.5f), // 5 — 위 꼭짓점
    };

    // 바깥에서 봤을 때 시계 방향 = 앞면 (Unity 규칙)
    static readonly int[] triangles = new int[]
    {
        0,1,2, 0,2,3, 0,3,4, 0,4,1,   // 아래 사각뿔 4면
        5,2,1, 5,3,2, 5,4,3, 5,1,4,   // 위 사각뿔 4면
    };

    Mesh mesh;
    public Vector3[] BaseVertices => baseVertices;
    void OnEnable()
    {
        EnsureLitMaterial();

        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        SetVertices(baseVertices);
    }

    public void SetVertices(Vector3[] verts)
    {
        mesh.Clear();
        mesh.vertices = verts;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
    }

    // 머티리얼이 없거나 셰이더가 깨져 있으면(분홍색) Lit 머티리얼을 새로 만들어 붙임
    void EnsureLitMaterial()
    {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        Material current = meshRenderer.sharedMaterial;

        bool needsMaterial = current == null
                          || current.shader == null
                          || !current.shader.isSupported
                          || current.shader.name == "Hidden/InternalErrorShader";
        if (!needsMaterial) return;

        Shader lit = Shader.Find("Universal Render Pipeline/Lit");
        if (lit == null) lit = Shader.Find("Standard");   // URP가 아닌 프로젝트용 대비
        if (lit == null) return;

        meshRenderer.sharedMaterial = new Material(lit) { name = "Diamond_Lit (자동 생성)" };
    }
}