using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShooterMovement : MonoBehaviour
{
    // ������
    public Transform target;
    // ���
    NavMeshAgent agent;
    // ������ �ִϸ����� ����
    public Animator anim;
    // �Ѿ�
    public GameObject bullet;

    // ���� ����
    enum State
    {
        Idle,
        Attack
    }
    State state;

    // ���� ������ Ÿ�̸�
    float timer = 0.0f;
    float waitingTime = 1.5f;

    private SpriteRenderer playerSpriteRenderer;

    void Start()
    {
        state = State.Idle;
        agent = GetComponent<NavMeshAgent>();
        playerSpriteRenderer = GetComponent<SpriteRenderer>();

        // �ʱ� Ÿ�� ����
        target = GameObject.Find("Character").transform;
    }

    void Update()
    {
        // ���¿� ���� ������Ʈ
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

            // Idle ���� Ʈ���� �ʱ�ȭ �� Attack Ʈ���� ����
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

        // Ÿ�ٿ��� �־��� ��� Idle ���·� ��ȯ
        if (distance > 7)
        {
            state = State.Idle;

            // Attack ���� Ʈ���� �ʱ�ȭ �� Idle Ʈ���� ����
            anim.ResetTrigger("Attack");
            anim.SetTrigger("Idle");
            return;
        }

        // Ÿ�� ���⿡ ���� ��������Ʈ ������ �� ����
        Vector3 flipDecision = transform.position - target.position;
        timer += Time.deltaTime;

        if (flipDecision.x > 0)
        {
            playerSpriteRenderer.flipX = false;

            if (timer > waitingTime)
            {
                // �Ѿ� ���� �� �ִϸ��̼� Ʈ����
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
                // �Ѿ� ���� �� �ִϸ��̼� Ʈ����
                Instantiate(bullet, transform.position + new Vector3(1, 0, 0), Quaternion.Euler(0f, 180f, 0f));
                anim.SetTrigger("Attack");
                timer = 0.0f;
            }
        }
    }
}
