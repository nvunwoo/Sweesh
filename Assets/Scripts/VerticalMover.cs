using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalMover : MonoBehaviour
{
    public float speed = 2f; // 이동 속도
    public float height = 2f; // 이동 범위

    private Vector3 startPos;

    void Start()
    {
        // 초기 위치 저장
        startPos = transform.position;
    }

    void Update()
    {
        // PingPong을 사용하여 위치를 반복적으로 변경
        float newY = Mathf.PingPong(Time.time * speed, height) - (height / 2);
        transform.position = new Vector3(startPos.x, startPos.y + newY, startPos.z);
    }
}

