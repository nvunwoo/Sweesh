using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMovement : CharacterState
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    private bool isGrounded;
    private Rigidbody rb;
    SpriteRenderer spriteRenderer;
    float timer = 0;

    public Image Heart1;
    public Image Heart2;
    public Image Heart3;

    private bool isOnInvincibleCalled = false;

    public Animator anim;
    enum State
    {
        Idle,
        Attack,
        onDamaged
    }
    State state;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Application.targetFrameRate = 60;
        state = State.Idle;
    }

    void Update()
    {
        Move();
        Jump();
        if (isOnInvincibleCalled == true)
        {
            ChangeColor(gameObject.transform);

        }
    }

    // �¿� �̵�
    void Move()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector3(moveInput * moveSpeed, rb.velocity.y, 0f);
        anim.SetTrigger("Idle");
    }

    // ����
    void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, 0f);
            isGrounded = false;
            anim.SetTrigger("Jump");
        }
    }

    // ĳ���Ͱ� ���� ��Ҵ��� üũ
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;  // ���� ������Ƿ� ������ �� �ְ� ����
        }
        if (collision.gameObject.tag == "Enemy")
        {   //������ ó�� ���� ���� ��ġ�ϰ� �Ʒ��� �������� �ӵ��� ������ ��Ƽ� ����ų.
            if (rb.velocity.y < 0 && transform.position.y > collision.transform.position.y)
            {
                OnAttack(collision.transform);
            }
            else if (this.gameObject.layer == 8 && collision.gameObject.tag == "Enemy")
            {
                OnAttack(collision.transform);
            }
            else
            {
                OnDamaged(collision.transform);

                characterHP -= 1;
                UpdateHeartUI(); // HP ���� �� UI ������Ʈ
            }
        }
 
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            if (this.gameObject.layer == 8)
            {

            }
            else 
            { 
                OnDamaged(other.transform);
                characterHP -= 1;
                UpdateHeartUI(); // HP ���� �� UI ������Ʈ
            }
        }
        if (other.gameObject.tag == "Jewel")
        {
            OnInvincible(other.transform);

        }
    }

    void UpdateHeartUI()
    {
        Heart1.color = characterHP >= 1 ? new Color(1, 1, 1, 1) : new Color(1, 1, 1, 0);
        Heart2.color = characterHP >= 2 ? new Color(1, 1, 1, 1) : new Color(1, 1, 1, 0);
        Heart3.color = characterHP >= 3 ? new Color(1, 1, 1, 1) : new Color(1, 1, 1, 0);

        // characterHP�� 0�� �Ǹ� SphereCollider ����
        if (characterHP <= 0)
        {
            SphereCollider sphereCollider = GetComponent<SphereCollider>();
            if (sphereCollider != null)
            {
                Destroy(sphereCollider); // Collider ����
            }
        }
    }

    void OnAttack(Transform enemy)
    {
        //Reaction Force  : �ݹ߷� 
        rb.AddForce(Vector3.up * 5, ForceMode.Impulse);
        //Enemy Die
        EnemyMove enemyMove = enemy.GetComponent<EnemyMove>();
        enemyMove.OnDamaged();
    }

    void OnDamaged(Transform enemy)
    {
        gameObject.layer = 7;
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        int dirc = transform.position.x- enemy.position.x > 0 ? 15 : -15;
        rb.AddForce(new Vector3(dirc, 5, 0)*7, ForceMode.Impulse);
        anim.SetTrigger("onDamaged");
        Invoke("OffDamaged", 2);
    }

    void OffDamaged()
    {
        gameObject.layer = 0;
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }

    void OnInvincible(Transform enemy)
    {
        gameObject.layer = 8;
        isOnInvincibleCalled = true;
        ChangeColor(enemy);
     

        Invoke("OffInvincible", 5);
    }

    void OffInvincible()
    {
        gameObject.layer = 0;
        spriteRenderer.color = new Color(1, 1, 1, 1);
        isOnInvincibleCalled = false;
    }
    void ChangeColor(Transform enemy)
    {
        timer += Time.deltaTime;
        float alpha = (Mathf.Sin(timer*10) + 1) / 2;
        spriteRenderer.color = new Color(alpha, alpha, alpha, 1);
    }
}
