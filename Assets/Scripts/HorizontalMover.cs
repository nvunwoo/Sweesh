using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMover : MonoBehaviour
{
    public float speed = 2f;  // �̵� �ӵ�
    public float distance = 4f; // �̵� �Ÿ�

    private Vector3 startPos;

    void Start()
    {
        // �ʱ� ��ġ ����
        startPos = transform.position;
    }

    void Update()
    {
        // PingPong�� ����Ͽ� X�� ��ġ�� ��� (�¿�� �̵�)
        float offsetX = Mathf.PingPong(Time.time * speed, distance) - (distance / 2);
        transform.position = new Vector3(startPos.x + offsetX, startPos.y, startPos.z);
    }
}
