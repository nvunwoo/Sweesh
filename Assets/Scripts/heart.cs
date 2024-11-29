using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heart : MonoBehaviour
{
    public float floatAmplitude = 0.2f; // ���Ʒ� �������� ũ��
    public float floatSpeed = 1.0f;     // �������� �ӵ�

    private Vector3 startPosition;

    private void Start()
    {
        // �������� �ʱ� ��ġ ����
        startPosition = transform.position;
    }

    private void Update()
    {
        // Y�� �������� Sin �Լ��� ����� �ε巴�� �̵�
        float newY = startPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatAmplitude;
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        // �浹�� ������Ʈ�� �±װ� "Player"���� Ȯ��
        if (other.CompareTag("Player"))
        {
            // ������ ������Ʈ�� ����
            Destroy(gameObject);
        }
    }
}
