using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firework : MonoBehaviour
{
    public GameObject fireworksPrefab1; // 첫 번째 폭죽 파티클
    public GameObject fireworksPrefab2; // 두 번째 폭죽 파티클
    public float yOffset = 2f;  // y축 방향으로 오프셋 줄 값

    private void OnTriggerEnter(Collider other)
    {
        // player 태그와 충돌하면 폭죽 2개 생성함
        if (other.CompareTag("Player"))
        {
            GameObject fireworks1 = Instantiate(fireworksPrefab1, transform.position + new Vector3(0, yOffset, 0), Quaternion.Euler(230, 90, 0));
            GameObject fireworks2 = Instantiate(fireworksPrefab2, transform.position + new Vector3(0, yOffset, 0), Quaternion.Euler(310, 90, 0));
            
        }
    }
}
