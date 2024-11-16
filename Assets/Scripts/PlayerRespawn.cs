using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public float respawnHeight = -50f;  // 리스폰 조건, y값이 이 이하로 떨어지면 리스폰

    private SceneReloder sceneReloder;  // SceneReloder 스크립트 참조

    void Start()
    {
        sceneReloder = FindObjectOfType<SceneReloder>();  // 씬을 리로드할 SceneReloder 스크립트를 찾음
    }

    void Update()
    {
        // 플레이어의 y좌표가 지정된 높이 이하로 떨어지면 리스폰
        if (transform.position.y <= respawnHeight)
        {
            Respawn();
        }
    }

    // 리스폰 함수
    void Respawn()
    {
        // SceneReloder의 리로드 함수 호출
        if (sceneReloder != null)
        {
            sceneReloder.ReloadScene();  // 현재 씬을 다시 로드
        }
    }
}
