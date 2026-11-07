using UnityEngine;

public class Mid_CrystalFloating : MonoBehaviour
{
    public float amplitude = 0.2f; // 위아래 이동 거리
    public float speed = 1.5f;     // 움직이는 속도

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.localPosition;
    }

    void Update()
    {
        float offset = Mathf.Sin(Time.time * speed) * amplitude;
        transform.localPosition = startPosition + Vector3.up * offset;
    }
}