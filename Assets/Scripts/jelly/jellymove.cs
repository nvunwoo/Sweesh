using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jellymove : MonoBehaviour
{
    public float detectionRange = 5f; // �÷��̾� Ž�� ����
    public float jumpForce = 5f; // ���� ��
    public float moveSpeed = 2f; // �������� �̵� �ӵ�
    public Vector2 destructionBounds = new Vector2(-10f, -5f); // ���� ��ġ (ȭ�� ����, �Ʒ�)
    public Transform groundCheck; // Ground üũ ��ġ
    public float groundCheckRadius = 0.2f; // Ground üũ �ݰ�

    private Transform target; // Ÿ��(�÷��̾�) Transform
    private Rigidbody2D rb;
    private Animator animator; // �ִϸ����� ������Ʈ
    private bool isTriggered = false; // �÷��̾� Ž�� ����
    private bool isGrounded = false; // �ٴڿ� �ִ��� ����

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // Ÿ�� ����
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

        // �ٴ� üũ
        CheckGround();

        // �÷��̾� Ž�� �� ����
        if (!isTriggered && PlayerInRange() && isGrounded)
        {
            TriggerJump();
        }

        // ���� �̵�
        if (isTriggered)
        {
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
        }

        // �� ���� ����
        if (transform.position.x < destructionBounds.x || transform.position.y < destructionBounds.y)
        {
            Destroy(gameObject);
        }
    }

    private void CheckGround()
    {
        // OverlapCircle�� �ֺ� �浹ü Ž��
        Collider2D[] hits = Physics2D.OverlapCircleAll(groundCheck.position, groundCheckRadius);

        // "Ground" �±׸� ���� ������Ʈ�� �ִ��� Ȯ��
        isGrounded = false;
        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Ground"))
            {
                isGrounded = true;
                break;
            }
        }
    }

    private bool PlayerInRange()
    {
        // Ÿ���� Ž�� ������ �ִ��� Ȯ��
        float distanceToTarget = Vector2.Distance(transform.position, target.position);
        return distanceToTarget <= detectionRange;
    }

    private void TriggerJump()
    {
        isTriggered = true;

        // ���� ����
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);

        // ���� �ִϸ��̼� ���
        if (animator != null)
        {
            animator.SetTrigger("Jump");
        }
    }

    void OnDrawGizmosSelected()
    {
        // Ž�� ���� �ð�ȭ
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        // �ٴ� üũ ��ġ �ð�ȭ
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);

        // ���� ��ġ �ð�ȭ
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(destructionBounds.x, -10f, 0), new Vector3(destructionBounds.x, 10f, 0)); // ���� ���
        Gizmos.DrawLine(new Vector3(-10f, destructionBounds.y, 0), new Vector3(10f, destructionBounds.y, 0)); // �Ʒ� ���
    }
}
