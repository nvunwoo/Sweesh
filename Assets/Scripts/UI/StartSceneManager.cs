using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartSceneManager : MonoBehaviour
{
    public Button stage2Button; // �������� 2�� ���� ��ư
    public Button stage3Button; // �������� 3���� ���� ��ư

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
        // ��ư ���¸� ���� �ε�� �� ����
        UpdateButtonState();
    }

    public void UpdateButtonState()
    {
        stage2Button.interactable = GameManager.instance.isStage1Cleared;
        stage3Button.interactable = GameManager.instance.isStage2Cleared;
    }
}