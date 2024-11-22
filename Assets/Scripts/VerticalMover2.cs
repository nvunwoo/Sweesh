using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalMover2 : MonoBehaviour
{
    public float speed = 2f;  // �̵� �ӵ�
    public float height = 4f; // �̵� ����

    private Vector3 startPos;

    void Start()
    {
        // �ʱ� ��ġ ����
        startPos = transform.position;
    }

    void Update()
    {
        // PingPong�� ����Ͽ� Y�� ��ġ�� ��� (������ �Ʒ��� �̵�)
        float offsetY = Mathf.PingPong(Time.time * speed, height);
        transform.position = new Vector3(startPos.x, startPos.y - offsetY, startPos.z);
    }
}

