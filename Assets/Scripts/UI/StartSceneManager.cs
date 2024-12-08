using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartSceneManager : MonoBehaviour
{
    public Button stage2Button; // 스테이지 2로 가는 버튼
    public Button stage3Button; // 스테이지 3으로 가는 버튼

    void Start()
    {
        UpdateButtonState();
    }

    

    void OnEnable()
    {
        UpdateButtonState();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 버튼 상태를 씬이 로드된 후 갱신
        UpdateButtonState();
    }

    public void UpdateButtonState()
    {
        stage2Button.interactable = GameManager.instance.isStage1Cleared;
        stage3Button.interactable = GameManager.instance.isStage2Cleared;
    }
}