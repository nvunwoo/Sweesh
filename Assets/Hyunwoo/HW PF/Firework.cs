using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firework : MonoBehaviour
{
    public GameObject fireworksPrefab1; // ù ��° ���� ��ƼŬ
    public GameObject fireworksPrefab2; // �� ��° ���� ��ƼŬ
    public float yOffset = 2f;  // y�� �������� ������ �� ��

    private void OnTriggerEnter(Collider other)
    {
        // player �±׿� �浹�ϸ� ���� 2�� ������
        if (other.CompareTag("Player"))
        {
            GameObject fireworks1 = Instantiate(fireworksPrefab1, transform.position + new Vector3(0, yOffset, 0), Quaternion.Euler(230, 90, 0));
            GameObject fireworks2 = Instantiate(fireworksPrefab2, transform.position + new Vector3(0, yOffset, 0), Quaternion.Euler(310, 90, 0));
            
        }
    }
}
