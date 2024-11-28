using System.Collections;
using UnityEngine;

public class FishMoverR : MonoBehaviour
{
    public float moveSpeed = 3f;  // ����� �̵� �ӵ�
    private float lifetime = 20f;  // ������� ���� �ð� (20�� �Ŀ� ����)
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;  // ���� ��ġ ����
        // ���� �ð��� ������ ����⸦ ����
        StartCoroutine(DestroyAfterTime(lifetime));
    }

    void Update()
    {
        // ����� �̵�
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
    }

    // ����� ���Ÿ� ���� �ڷ�ƾ
    IEnumerator DestroyAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);  // ����� ����
    }
}
