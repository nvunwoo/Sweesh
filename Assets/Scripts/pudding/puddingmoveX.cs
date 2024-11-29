using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puddingmoveX : MonoBehaviour
{
    public float moveSpeed = 2f; // �������� �̵� �ӵ�
    public Vector3 destructionBounds = new Vector3(-10f, -10f, -10f); // ���� ��ġ (ȭ�� ����, �Ʒ�)
    public Transform groundCheck; // Ground üũ ��ġ
    public float groundCheckRadius = 0.3f; // Ground üũ �ݰ�

    private Rigidbody rb;
    private bool isGrounded = false; // �ٴڿ� �ִ��� ����

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // �ٴ� üũ
        CheckGround();

        // �ٴڿ� ���� ��� �̵�
        if (isGrounded)
        {
            MoveLeft();
        }

        // �� ���� ����
        if (transform.position.x < destructionBounds.x ||
            transform.position.y < destructionBounds.y ||
            transform.position.z < destructionBounds.z)
        {
            Destroy(gameObject);
        }
    }

    private void CheckGround()
    {
        // OverlapSphere�� �ٴ� Ž��
        Collider[] hits = Physics.OverlapSphere(groundCheck.position, groundCheckRadius);

        // "Ground" �±׸� ���� ������Ʈ�� �ִ��� Ȯ��
        isGrounded = false;
        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Ground"))
            {
                isGrounded = true;
                break;
            }
        }
    }

    private void MoveLeft()
    {
        // �������� �̵�
        rb.velocity = new Vector3(-moveSpeed, rb.velocity.y, 0f);
    }

    void OnDrawGizmosSelected()
    {
        // �ٴ� üũ ��ġ �ð�ȭ
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);

        // ���� ��ġ �ð�ȭ
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(destructionBounds.x, -100f, destructionBounds.z),
                        new Vector3(destructionBounds.x, 100f, destructionBounds.z)); // ���� ���
    }
}
