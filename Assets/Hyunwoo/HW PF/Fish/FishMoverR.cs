using System.Collections;
using UnityEngine;

public class FishMoverR : MonoBehaviour
{
    public float moveSpeed = 3f;  // 물고기 이동 속도
    private float lifetime = 20f;  // 물고기의 생존 시간 (20초 후에 제거)
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;  // 시작 위치 저장
        // 일정 시간이 지나면 물고기를 제거
        StartCoroutine(DestroyAfterTime(lifetime));
    }

    void Update()
    {
        // 물고기 이동
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
    }

    // 물고기 제거를 위한 코루틴
    IEnumerator DestroyAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);  // 물고기 제거
    }
}
