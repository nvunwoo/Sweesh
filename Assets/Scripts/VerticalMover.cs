using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalMover : MonoBehaviour
{
    public float speed = 2f; // �̵� �ӵ�
    public float height = 2f; // �̵� ����

    private Vector3 startPos;

    void Start()
    {
        // �ʱ� ��ġ ����
        startPos = transform.position;
    }

    void Update()
    {
        // PingPong�� ����Ͽ� ��ġ�� �ݺ������� ����
        float newY = Mathf.PingPong(Time.time * speed, height) - (height / 2);
        transform.position = new Vector3(startPos.x, startPos.y + newY, startPos.z);
    }
}

