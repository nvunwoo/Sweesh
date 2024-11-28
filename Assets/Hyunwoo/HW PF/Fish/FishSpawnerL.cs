using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawnerL : MonoBehaviour
{
    public GameObject[] fishPrefabs;  // 물고기 프리팹 배열
    public float spawnInterval = 7f;  // 물고기 생성 간격
    public float spawnChance = 0.5f;  // 물고기 생성 확률

    private Transform player;  // 플레이어의 위치를 저장할 변수

    void Start()
    {
        // "Player" 태그를 가진 객체를 찾아 플레이어의 위치를 참조
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // 물고기 생성 주기 시작
        InvokeRepeating("TrySpawnFish", 0f, spawnInterval);
    }

    void TrySpawnFish()
    {
        // 생성 확률에 따라 물고기 생성
        if (Random.value <= spawnChance)
        {
            SpawnFish();
        }
    }

    void SpawnFish()
    {
        // 랜덤한 물고기 프리팹 선택
        int randomIndex = Random.Range(0, fishPrefabs.Length);
        GameObject selectedFish = fishPrefabs[randomIndex];

        // 플레이어를 기준으로 10f 떨어진 x값과 랜덤한 y값 생성
        Vector3 spawnPosition = new Vector3(player.position.x - 10f, Random.Range(-3f, 4f), 3f);

        // 물고기 생성
        Instantiate(selectedFish, spawnPosition, Quaternion.identity);
    }
}
