using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cpuddmove : MonoBehaviour
{
    public float moveSpeed = 6f; // 왼쪽으로 이동 속도
    public float detectionRange = 5f; // 플레이어 탐지 범위

    private Transform target; // 플레이어(타겟)
    private Rigidbody rb;
    private bool isTriggered = false; // 플레이어 탐지 여부

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // "Character"라는 이름의 플레이어를 찾음
        GameObject player = GameObject.Find("Character");
        if (player != null)
        {
            target = player.transform;
        }
        else
        {
            Debug.LogWarning("Character 오브젝트를 찾을 수 없습니다!");
        }

    }

    void Update()
    {
        // 타겟이 없으면 실행 중지
        if (target == null) return;

        // 플레이어 탐지
        if (!isTriggered && PlayerInRange())
        {
            TriggerCharge();
        }

        // 왼쪽 이동 (탐지된 경우)
        if (isTriggered)
        {
            MoveLeft();
        }

        
    }

    private bool PlayerInRange()
    {
        // 타겟이 탐지 범위에 있는지 확인
        float distanceToTarget = Vector3.Distance(transform.position, target.position);
        return distanceToTarget <= detectionRange;
    }

    private void TriggerCharge()
    {
        isTriggered = true;


        Debug.Log("Player detected! Charging...");
    }

    private void MoveLeft()
    {
        // 지속적으로 왼쪽으로 이동
        rb.velocity = new Vector3(-moveSpeed, rb.velocity.y, 0f);
    }

    void OnDrawGizmosSelected()
    {
        // 탐지 범위 시각화
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

    }
}

