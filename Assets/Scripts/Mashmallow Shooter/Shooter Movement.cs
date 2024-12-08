using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShooterMovement : MonoBehaviour
{
    // 목적지
    public Transform target;
    // 요원
    NavMeshAgent agent;
    // 연결할 애니메이터 생성
    public Animator anim;
    // 총알
    public GameObject bullet;

    // 상태 관리
    enum State
    {
        Idle,
        Attack
    }
    State state;

    // 공격 딜레이 타이머
    float timer = 0.0f;
    float waitingTime = 1.5f;

    private SpriteRenderer playerSpriteRenderer;

    void Start()
    {
        state = State.Idle;
        agent = GetComponent<NavMeshAgent>();
        playerSpriteRenderer = GetComponent<SpriteRenderer>();

        // 초기 타겟 설정
        target = GameObject.Find("Character").transform;
    }

    void Update()
    {
        // 상태에 따른 업데이트
        switch (state)
        {
            case State.Idle:
                UpdateIdle();
                break;
            case State.Attack:
                UpdateAttack();
                break;
        }
    }

    private void UpdateIdle()
    {
        agent.speed = 0;

        if (target == null)
        {
            Debug.LogWarning("Target not found!");
            return;
        }

        float distance = Vector3.Distance(transform.position, target.position);

        if (distance <= 7)
        {
            state = State.Attack;

            // Idle 상태 트리거 초기화 및 Attack 트리거 설정
            anim.ResetTrigger("Idle");
            anim.SetTrigger("Attack");
        }
    }

    private void UpdateAttack()
    {
        agent.speed = 0;

        if (target == null)
        {
            Debug.LogWarning("Target not found!");
            return;
        }

        float distance = Vector3.Distance(transform.position, target.position);

        // 타겟에서 멀어진 경우 Idle 상태로 전환
        if (distance > 7)
        {
            state = State.Idle;

            // Attack 상태 트리거 초기화 및 Idle 트리거 설정
            anim.ResetTrigger("Attack");
            anim.SetTrigger("Idle");
            return;
        }

        // 타겟 방향에 따라 스프라이트 뒤집기 및 공격
        Vector3 flipDecision = transform.position - target.position;
        timer += Time.deltaTime;

        if (flipDecision.x > 0)
        {
            playerSpriteRenderer.flipX = false;

            if (timer > waitingTime)
            {
                // 총알 생성 및 애니메이션 트리거
                Instantiate(bullet, transform.position - new Vector3(1, 0, 0), transform.rotation);
                anim.SetTrigger("Attack");
                timer = 0.0f;
            }
        }
        else
        {
            playerSpriteRenderer.flipX = true;

            if (timer > waitingTime)
            {
                // 총알 생성 및 애니메이션 트리거
                Instantiate(bullet, transform.position + new Vector3(1, 0, 0), Quaternion.Euler(0f, 180f, 0f));
                anim.SetTrigger("Attack");
                timer = 0.0f;
            }
        }
    }
}
