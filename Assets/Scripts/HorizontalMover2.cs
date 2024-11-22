using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMover2 : MonoBehaviour
{
    public float speed = 2f;   // 이동 속도
    public float distance = 4f; // 이동 거리

    private Vector3 startPos;

    void Start()
    {
        // 초기 위치 저장
        startPos = transform.position;
    }

    void Update()
    {
        // PingPong 값을 음수로 만들어 반대 방향으로 이동
        float offsetX = -Mathf.PingPong(Time.time * speed, distance) + (distance / 2);
        transform.position = new Vector3(startPos.x + offsetX, startPos.y, startPos.z);
    }
}
