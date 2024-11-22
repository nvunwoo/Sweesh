using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class redcontroller : MonoBehaviour
{
    public float moveSpeed = 3f; // �̵� �ӵ�
    public float moveRange = 5f; // �̵� ����
    public bool startMovingRight = true; // �ʱ� �̵� ����

    private float startX; // �ʱ� ��ġ ����
    private bool movingRight; // ���� �̵� ����

    // Start is called before the first frame update
    void Start()
    {
        // �ʱ� ����
        startX = transform.position.x;
        movingRight = startMovingRight;
    }

    // Update is called once per frame
    void Update()
    {
        // �̵� ����
        if (movingRight)
        {
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
            if (transform.position.x > startX + moveRange)
            {
                FlipDirection();
            }
        }
        else
        {
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
            if (transform.position.x < startX - moveRange)
            {
                FlipDirection();
            }
        }
    }

    private void FlipDirection()
    {
        movingRight = !movingRight;

        // ��������Ʈ ���� (�ɼ�)
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
