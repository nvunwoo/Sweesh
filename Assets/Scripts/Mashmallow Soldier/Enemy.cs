using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngineInternal;

public class Enemy : MonoBehaviour
{
    //������
    public Transform target;
    //���
    NavMeshAgent agent;
    //������ ���ϸ����� ����
    public Animator anim;
    //z

    enum State
    {
        Idle,
        Walk,
        Attack
    }
    State state;

    // Start is called before the first frame update
    void Start()
    {
        state = State.Idle;
        //����� �������༭
        agent = GetComponent<NavMeshAgent>();


    }

    // Update is called once per frame
    void Update()
    {
        target = GameObject.Find("Character").transform;
        Vector3 ori = transform.position;
        Vector3 flipDecision = ori - target.transform.position;
        if (flipDecision.x > 0) { transform.localScale = new Vector3(0.5f, 0.5f, 0.5f); }
        else { transform.localScale = new Vector3(-0.5f, 0.5f, 0.5f); }

        if (state == State.Idle)
        {
            UpdateIdle();
        }
        else if (state == State.Walk)
        {
            UpdateWalk();
        }
        else if (state == State.Attack)
        {
            UpdateAttack();
        }
    }
    private void UpdateIdle()
    {
        agent.speed = 0;

        target = GameObject.Find("Character").transform;

        if (target != null)
        {
            state = State.Walk;
            anim.SetTrigger("Walk");
        }
    }

    protected void UpdateWalk()
    {
        float distance = Vector3.Distance(transform.position, target.transform.position);
        if (distance <= 2)
        {
            state = State.Attack;
            anim.SetTrigger("Attack");
        }
        agent.speed = 3.5f;
        agent.destination = target.transform.position;
    }

    private void UpdateAttack()
    {
        agent.speed = 0;
        float distance = Vector3.Distance(transform.position, target.transform.position);
        if (distance > 2)
        {
            state = State.Walk;
            anim.SetTrigger("Walk");
        }
    }
}
