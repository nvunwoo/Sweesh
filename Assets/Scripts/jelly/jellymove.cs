using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jellymove : MonoBehaviour
{
    public float jumpForce = 6f; // 점프 힘
    public float moveSpeed = 2f; // 왼쪽으로 이동 속도
    public float detectionRange = 5f; // 플레이어 탐지 범위
    public Transform groundCheck; // Ground 체크 위치
    public float groundCheckRadius = 0.3f; // Ground 체크 반경

    private Transform target; // 플레이어(타겟)
    private Rigidbody rb;
    private bool isGrounded = false; // 바닥에 있는지 여부
    private bool isTriggered = false; // 플레이어 탐지 여부
    private bool isJumping = false; // 점프 중인지 여부
    private Animator animator; // 애니메이터 컴포넌트

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

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

        // Idle 애니메이션 설정
        if (animator != null)
        {
            animator.SetBool("jelly_idle", true);
        }
    }

    void Update()
    {
        // 타겟이 없으면 실행 중지
        if (target == null) return;

        // 바닥 체크
        CheckGround();

        // 플레이어 탐지
        if (!isTriggered && PlayerInRange())
        {
            TriggerJumpState();
        }

        // 점프 행동
        if (isTriggered && isGrounded && !isJumping)
        {
            Jump();
        }

        // 왼쪽 이동
        if (isTriggered)
        {
            rb.velocity = new Vector3(-moveSpeed, rb.velocity.y, 0f);
        }
    }

    private bool PlayerInRange()
    {
        // 타겟이 탐지 범위에 있는지 확인
        float distanceToTarget = Vector3.Distance(transform.position, target.position);
        return distanceToTarget <= detectionRange;
    }

    private void TriggerJumpState()
    {
        isTriggered = true;

        // Idle 애니메이션 중지
        if (animator != null)
        {
            animator.SetBool("jelly_idle", false);
        }
    }

    private void Jump()
    {
        // 점프
        rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);

        // 점프 애니메이션 트리거
        if (animator != null)
        {
            animator.SetTrigger("jelly_jump");
        }

        isJumping = true; // 점프 상태 활성화
        StartCoroutine(ResetJump());
    }

    private IEnumerator ResetJump()
    {
        // 점프 상태를 일정 시간 후 초기화
        yield return new WaitForSeconds(1f);
        isJumping = false;
    }

    private void CheckGround()
    {
        // OverlapSphere로 바닥 탐지
        Collider[] hits = Physics.OverlapSphere(groundCheck.position, groundCheckRadius);

        // "Ground" 태그를 가진 오브젝트가 있는지 확인
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
        // 탐지 범위 시각화
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        // 바닥 체크 위치 시각화
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
