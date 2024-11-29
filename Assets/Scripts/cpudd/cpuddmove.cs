using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cpuddmove : MonoBehaviour
{
    public float moveSpeed = 6f; // �������� �̵� �ӵ�
    public float detectionRange = 5f; // �÷��̾� Ž�� ����

    private Transform target; // �÷��̾�(Ÿ��)
    private Rigidbody rb;
    private bool isTriggered = false; // �÷��̾� Ž�� ����

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // "Character"��� �̸��� �÷��̾ ã��
        GameObject player = GameObject.Find("Character");
        if (player != null)
        {
            target = player.transform;
        }
        else
        {
            Debug.LogWarning("Character ������Ʈ�� ã�� �� �����ϴ�!");
        }

    }

    void Update()
    {
        // Ÿ���� ������ ���� ����
        if (target == null) return;

        // �÷��̾� Ž��
        if (!isTriggered && PlayerInRange())
        {
            TriggerCharge();
        }

        // ���� �̵� (Ž���� ���)
        if (isTriggered)
        {
            MoveLeft();
        }

        
    }

    private bool PlayerInRange()
    {
        // Ÿ���� Ž�� ������ �ִ��� Ȯ��
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
        // ���������� �������� �̵�
        rb.velocity = new Vector3(-moveSpeed, rb.velocity.y, 0f);
    }

    void OnDrawGizmosSelected()
    {
        // Ž�� ���� �ð�ȭ
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

    }
}

