using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jellymove : MonoBehaviour
{
    public float jumpForce = 6f; // ���� ��
    public float moveSpeed = 2f; // �������� �̵� �ӵ�
    public float detectionRange = 5f; // �÷��̾� Ž�� ����
    public Transform groundCheck; // Ground üũ ��ġ
    public float groundCheckRadius = 0.3f; // Ground üũ �ݰ�

    private Transform target; // �÷��̾�(Ÿ��)
    private Rigidbody rb;
    private bool isGrounded = false; // �ٴڿ� �ִ��� ����
    private bool isTriggered = false; // �÷��̾� Ž�� ����
    private bool isJumping = false; // ���� ������ ����
    private Animator animator; // �ִϸ����� ������Ʈ

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

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

        // Idle �ִϸ��̼� ����
        if (animator != null)
        {
            animator.SetBool("jelly_idle", true);
        }
    }

    void Update()
    {
        // Ÿ���� ������ ���� ����
        if (target == null) return;

        // �ٴ� üũ
        CheckGround();

        // �÷��̾� Ž��
        if (!isTriggered && PlayerInRange())
        {
            TriggerJumpState();
        }

        // ���� �ൿ
        if (isTriggered && isGrounded && !isJumping)
        {
            Jump();
        }

        // ���� �̵�
        if (isTriggered)
        {
            rb.velocity = new Vector3(-moveSpeed, rb.velocity.y, 0f);
        }
    }

    private bool PlayerInRange()
    {
        // Ÿ���� Ž�� ������ �ִ��� Ȯ��
        float distanceToTarget = Vector3.Distance(transform.position, target.position);
        return distanceToTarget <= detectionRange;
    }

    private void TriggerJumpState()
    {
        isTriggered = true;

        // Idle �ִϸ��̼� ����
        if (animator != null)
        {
            animator.SetBool("jelly_idle", false);
        }
    }

    private void Jump()
    {
        // ����
        rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);

        // ���� �ִϸ��̼� Ʈ����
        if (animator != null)
        {
            animator.SetTrigger("jelly_jump");
        }

        isJumping = true; // ���� ���� Ȱ��ȭ
        StartCoroutine(ResetJump());
    }

    private IEnumerator ResetJump()
    {
        // ���� ���¸� ���� �ð� �� �ʱ�ȭ
        yield return new WaitForSeconds(1f);
        isJumping = false;
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

    void OnDrawGizmosSelected()
    {
        // Ž�� ���� �ð�ȭ
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        // �ٴ� üũ ��ġ �ð�ȭ
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
