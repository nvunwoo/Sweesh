using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pinkcontroller : MonoBehaviour
{
    public float moveSpeed = 2f; // 이동 속도
    public float moveRange = 5f; // 이동 범위
    public bool startMovingRight = false; // 초기 이동 방향

    private float startX; // 초기 위치 저장
    private bool movingRight; // 현재 이동 방향

    // Start is called before the first frame update
    void Start()
    {
        // 초기 설정
        startX = transform.position.x;
        movingRight = startMovingRight;
    }

    // Update is called once per frame
    void Update()
    {
        // 이동 로직
        if (movingRight)
        {
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
            if (transform.position.x > startX + moveRange)
            {
                FlipDirection();
            }
        }
        else
        {
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
            if (transform.position.x < startX - moveRange)
            {
                FlipDirection();
            }
        }
    }

    private void FlipDirection()
    {
        movingRight = !movingRight;

        // 스프라이트 반전 (옵션)
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
