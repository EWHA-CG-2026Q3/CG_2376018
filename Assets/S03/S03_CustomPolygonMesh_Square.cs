  using UnityEngine;

  [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
  public class S03_CustomPolygonMesh_Square : MonoBehaviour
  {
      void Start()
      {
          // TODO 1: 원하는 다각형의 정점 좌표를 채우세요 (최소 4개)
          Vector3[] vertices = new Vector3[]
          {
            new Vector3(0f, 1.5f, 0f),     // 0: 위
            new Vector3(-1.5f, 0.5f, 0f),  // 1: 왼쪽 위
            new Vector3(-1f, -1f, 0f),     // 2: 왼쪽 아래
            new Vector3(1f, -1f, 0f),      // 3: 오른쪽 아래
            new Vector3(1.5f, 0.5f, 0f),   // 4: 오른쪽 위
          };

          // TODO 2: 정점 3개씩 묶어 삼각형들을 구성하세요
          int[] triangles = new int[]
          {
              0, 2, 1, // 삼각형 1
              0, 3, 2, // 삼각형 2
              0, 4, 3, // 삼각형 3
          };

          Mesh mesh = new Mesh();
          mesh.vertices = vertices;
          mesh.triangles = triangles;
          mesh.RecalculateNormals();

          GetComponent<MeshFilter>().mesh = mesh;
          GetComponent<MeshRenderer>().sharedMaterial = new Material(Shader.Find("Universal Render Pipeline/Lit"));
      }
  }