using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalMover2 : MonoBehaviour
{
    public float speed = 2f;  // 이동 속도
    public float height = 4f; // 이동 높이

    private Vector3 startPos;

    void Start()
    {
        // 초기 위치 저장
        startPos = transform.position;
    }

    void Update()
    {
        // PingPong을 사용하여 Y축 위치를 계산 (위에서 아래로 이동)
        float offsetY = Mathf.PingPong(Time.time * speed, height);
        transform.position = new Vector3(startPos.x, startPos.y - offsetY, startPos.z);
    }
}

