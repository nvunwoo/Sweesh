using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class puddingmove : MonoBehaviour
{

    public Transform target;
    //요원
    NavMeshAgent agent;
    //연결할 에니메이터 생성
    //z

    enum State
    {
        Idle,
        Charge
    }
    State state;
    // Start is called before the first frame update
    void Start()
    {
        state = State.Idle;
        //요원을 정의해줘서
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        target = GameObject.Find("Character").transform;
        Vector3 ori = transform.position;
        //Vector3 flipDecision = ori - target.transform.position;
        //if (flipDecision.x > 0) { transform.localScale = new Vector3(0.5f, 0.5f, 0.5f); }
        //else { transform.localScale = new Vector3(-0.5f, 0.5f, 0.5f); }

        if (state == State.Idle)
        {
            UpdateIdle();
        }
        else if (state == State.Charge)
        {
            UpdateCharge();
        }
    }

    private void UpdateIdle()
    {
        agent.speed = 0;

        target = GameObject.Find("Character").transform;
        float distance = Vector3.Distance(transform.position, target.transform.position);

        if (distance <= 3)
        {
            state = State.Charge;
        }
    }

    protected void UpdateCharge()
    {
        float distance = Vector3.Distance(transform.position, target.transform.position);
        
        agent.speed = 3.5f;
        agent.destination = target.transform.position;
    }
}
