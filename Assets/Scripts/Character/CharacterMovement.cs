using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterMovement : CharacterState
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    private bool isGrounded;
    private Rigidbody rb;
    SpriteRenderer spriteRenderer;
    float timer = 0;

    public AudioClip deathAudioClip; // 죽는 소리
    public AudioClip jumpAudioClip;   // 점프 소리
    public AudioClip damageAudioClip; // 다치는 소리
    private AudioSource audioSource; // AudioClip 재생용 AudioSource

    public Image Heart1;
    public Image Heart2;
    public Image Heart3;

    public TextMeshProUGUI coinNumber;

    private bool isOnInvincibleCalled = false;

    private bool isDeadSoundPlayed = false; // 사운드 재생 상태 확인용 플래그

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
        audioSource = GetComponent<AudioSource>();
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

    // 좌우 이동
    void Move()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector3(moveInput * moveSpeed, rb.velocity.y, 0f);
        anim.SetTrigger("Idle");
    }

    // 점프
    void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, 0f);
            isGrounded = false;
            anim.SetTrigger("Jump");

            // 점프 소리 재생
            if (jumpAudioClip != null && audioSource != null)
            {
                audioSource.PlayOneShot(jumpAudioClip);
            }
        }
    }

    // 캐릭터가 땅에 닿았는지 체크
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;  // 땅에 닿았으므로 점프할 수 있게 설정
        }

        if (gameObject.layer == 7) // 데미지 입으면 충돌 무시
        {
            return;
        }

        if (collision.gameObject.tag == "Enemy")
        {   //마리오 처럼 몬스터 위에 위치하고 아래로 내려오는 속도가 있으면 밟아서 몬스터킬.
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
                UpdateHeartUI(); // HP 감소 후 UI 업데이트
            }
        }
 
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Jewel")
        {
            OnInvincible(other.transform);
        }

        if (other.gameObject.tag == "Coin")
        {
            coin++; // 숫자 증가
            coinNumber.text = coin.ToString("D2"); // 두 자릿수 형식으로 업데이트
            if (coin >= 10)
            {
                characterHP += 1; // characterHP 증가
                UpdateHeartUI(); // HP 증가 후 UI 업데이트
            }
        }

        if (other.gameObject.tag == "Heart")
        {
            if (characterHP < 3)
            {
                characterHP += 1; // characterHP 증가
                UpdateHeartUI(); // HP 증가 후 UI 업데이트
            }
        }

        if (gameObject.layer == 7) // 데미지 입으면 충돌 무시
        {
            return;
        }

        if (other.gameObject.tag == "Bullet")
        {
            if (this.gameObject.layer != 8)
            {
                OnDamaged(other.transform);
                characterHP -= 1;
                UpdateHeartUI(); // HP 감소 후 UI 업데이트
            }
        }

        if (other.gameObject.tag == "Ghost")
        {
            if (this.gameObject.layer != 8)
            {
                OnDamaged(other.transform);
                characterHP -= 1;
                UpdateHeartUI(); // HP 감소 후 UI 업데이트
            }
        }
    }

    void UpdateHeartUI()
    {
        Debug.Log("UpdateHeartUI called. Current HP: " + characterHP);

        Heart1.color = characterHP >= 1 ? new Color(1, 1, 1, 1) : new Color(1, 1, 1, 0);
        Heart2.color = characterHP >= 2 ? new Color(1, 1, 1, 1) : new Color(1, 1, 1, 0);
        Heart3.color = characterHP >= 3 ? new Color(1, 1, 1, 1) : new Color(1, 1, 1, 0);

        if (characterHP <= 0)
        {
            Debug.LogError("캐릭터 죽음 소리 조건 충족. isDeadSoundPlayed: " + isDeadSoundPlayed);


            if (!isDeadSoundPlayed && deathAudioClip != null && audioSource != null)
            {
                Debug.LogError("캐릭터 죽음 소리 재생.");
                audioSource.PlayOneShot(deathAudioClip);
                isDeadSoundPlayed = true;
            }

            SphereCollider sphereCollider = GetComponent<SphereCollider>();
            if (sphereCollider != null)
            {
                Destroy(sphereCollider);
            }
        }
    }

    void OnAttack(Transform enemy)
    {
        //Reaction Force  : 반발력 
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

        // 다치는 소리 재생
        if (characterHP > 0 && damageAudioClip != null && audioSource != null)
        {
            audioSource.PlayOneShot(damageAudioClip);
        }
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
