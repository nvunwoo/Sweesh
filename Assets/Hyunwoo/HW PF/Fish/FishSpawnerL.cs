using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawnerL : MonoBehaviour
{
    public GameObject[] fishPrefabs;  // ����� ������ �迭
    public float spawnInterval = 7f;  // ����� ���� ����
    public float spawnChance = 0.5f;  // ����� ���� Ȯ��

    private Transform player;  // �÷��̾��� ��ġ�� ������ ����

    void Start()
    {
        // "Player" �±׸� ���� ��ü�� ã�� �÷��̾��� ��ġ�� ����
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // ����� ���� �ֱ� ����
        InvokeRepeating("TrySpawnFish", 0f, spawnInterval);
    }

    void TrySpawnFish()
    {
        // ���� Ȯ���� ���� ����� ����
        if (Random.value <= spawnChance)
        {
            SpawnFish();
        }
    }

    void SpawnFish()
    {
        // ������ ����� ������ ����
        int randomIndex = Random.Range(0, fishPrefabs.Length);
        GameObject selectedFish = fishPrefabs[randomIndex];

        // �÷��̾ �������� 10f ������ x���� ������ y�� ����
        Vector3 spawnPosition = new Vector3(player.position.x - 10f, Random.Range(-3f, 4f), 3f);

        // ����� ����
        Instantiate(selectedFish, spawnPosition, Quaternion.identity);
    }
}
