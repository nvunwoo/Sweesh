using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // 각 스테이지 클리어 여부를 관리하는 플래그
    public bool isStage1Cleared = false;
    public bool isStage2Cleared = false;

    void Start()
    {
        // 게임 시작 시 플래그 초기화
        isStage1Cleared = false;
        isStage2Cleared = false;
    }

    private void Awake()
    {
        // 게임 매니저가 씬을 넘어서 유지되도록 설정
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시에도 삭제되지 않도록 설정
        }
        else
        {
            Destroy(gameObject); // 이미 존재하면 이 오브젝트는 삭제
        }
    }

    // 스테이지 1 클리어 시 호출
    public void Stage1Cleared()
    {
        isStage1Cleared = true;
        isStage2Cleared = false;
    }

    // 스테이지 2 클리어 시 호출
    public void Stage2Cleared()
    {
        isStage1Cleared = true;
        isStage2Cleared = true;
    }

    
}
