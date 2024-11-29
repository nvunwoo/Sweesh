using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heart : MonoBehaviour
{
    public float floatAmplitude = 0.2f; // 위아래 움직임의 크기
    public float floatSpeed = 1.0f;     // 움직임의 속도

    private Vector3 startPosition;

    private void Start()
    {
        // 아이템의 초기 위치 저장
        startPosition = transform.position;
    }

    private void Update()
    {
        // Y축 기준으로 Sin 함수를 사용해 부드럽게 이동
        float newY = startPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatAmplitude;
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        // 충돌한 오브젝트의 태그가 "Player"인지 확인
        if (other.CompareTag("Player"))
        {
            // 아이템 오브젝트를 제거
            Destroy(gameObject);
        }
    }
}
