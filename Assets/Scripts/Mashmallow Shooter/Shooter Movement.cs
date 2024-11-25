using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class ShooterMovement : MonoBehaviour
{
    //목적지
    public Transform target;
    //요원
    NavMeshAgent agent;
    //연결할 에니메이터 생성
    public Animator anim;
    //총알
    public GameObject bullet;
    enum State
    {
        Idle,
        Attack
    }
    State state;
    float timer = 0.0f;
    float waitingTime = 1.5f;

    private SpriteRenderer  playerSpriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        state = State.Idle;
        //요원을 정의해줘서
        agent = GetComponent<NavMeshAgent>();
        playerSpriteRenderer = GetComponent<SpriteRenderer>();


    }

    // Update is called once per frame
    void Update()
    {
        target = GameObject.Find("Character").transform;
        Vector3 ori = transform.position;
        Vector3 flipDecision = ori - target.transform.position;
        

        if (state == State.Idle)
        {
            UpdateIdle();
        }
        else if (state == State.Attack)
        {
            UpdateAttack();
            this.timer += Time.deltaTime;
            if (flipDecision.x > 0) 
            { 
                playerSpriteRenderer.flipX = false;
                if (timer > waitingTime)
                {
                    Instantiate(bullet, transform.position - new Vector3(1, 0, 0), transform.rotation);
                    timer = 0.0f;
                }
            }
            else 
            { 
                playerSpriteRenderer.flipX = true;
                if (timer > waitingTime)
                {
                    Instantiate(bullet, transform.position + new Vector3(1,0,0), Quaternion.Euler(0f, 180f, 0f));
                    timer = 0.0f;
                }
            }
            
        }

    }
    private void UpdateIdle()
    {
        agent.speed = 0;
        float distance = Vector3.Distance(transform.position, target.transform.position);
        target = GameObject.Find("Character").transform;

        if (target != null&& distance <= 7)
        {
            state = State.Attack;
            anim.SetTrigger("Attack");
        }
    }
    private void UpdateAttack()
    {
        agent.speed = 0;
        float distance = Vector3.Distance(transform.position, target.transform.position);
        if (distance > 7)
        {
            state = State.Idle;
            anim.SetTrigger("Idle");
        }
    }
}
