using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMover : MonoBehaviour
{
    public float speed = 2f;  // 이동 속도
    public float distance = 4f; // 이동 거리

    private Vector3 startPos;

    void Start()
    {
        // 초기 위치 저장
        startPos = transform.position;
    }

    void Update()
    {
        // PingPong을 사용하여 X축 위치를 계산 (좌우로 이동)
        float offsetX = Mathf.PingPong(Time.time * speed, distance) - (distance / 2);
        transform.position = new Vector3(startPos.x + offsetX, startPos.y, startPos.z);
    }
}
