using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puddingmoveX : MonoBehaviour
{
    public float moveSpeed = 2f; // 왼쪽으로 이동 속도
    public Vector3 destructionBounds = new Vector3(-10f, -10f, -10f); // 제거 위치 (화면 왼쪽, 아래)
    public Transform groundCheck; // Ground 체크 위치
    public float groundCheckRadius = 0.3f; // Ground 체크 반경

    private Rigidbody rb;
    private bool isGrounded = false; // 바닥에 있는지 여부

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // 바닥 체크
        CheckGround();

        // 바닥에 있을 경우 이동
        if (isGrounded)
        {
            MoveLeft();
        }

        // 적 제거 조건
        if (transform.position.x < destructionBounds.x ||
            transform.position.y < destructionBounds.y ||
            transform.position.z < destructionBounds.z)
        {
            Destroy(gameObject);
        }
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

    private void MoveLeft()
    {
        // 왼쪽으로 이동
        rb.velocity = new Vector3(-moveSpeed, rb.velocity.y, 0f);
    }

    void OnDrawGizmosSelected()
    {
        // 바닥 체크 위치 시각화
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);

        // 제거 위치 시각화
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(destructionBounds.x, -100f, destructionBounds.z),
                        new Vector3(destructionBounds.x, 100f, destructionBounds.z)); // 왼쪽 경계
    }
}
