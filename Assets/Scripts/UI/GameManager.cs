using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // �� �������� Ŭ���� ���θ� �����ϴ� �÷���
    public bool isStage1Cleared = false;
    public bool isStage2Cleared = false;

    void Start()
    {
        // ���� ���� �� �÷��� �ʱ�ȭ
        isStage1Cleared = false;
        isStage2Cleared = false;
    }

    private void Awake()
    {
        // ���� �Ŵ����� ���� �Ѿ �����ǵ��� ����
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // �� ��ȯ �ÿ��� �������� �ʵ��� ����
        }
        else
        {
            Destroy(gameObject); // �̹� �����ϸ� �� ������Ʈ�� ����
        }
    }

    // �������� 1 Ŭ���� �� ȣ��
    public void Stage1Cleared()
    {
        isStage1Cleared = true;
        isStage2Cleared = false;
    }

    // �������� 2 Ŭ���� �� ȣ��
    public void Stage2Cleared()
    {
        isStage1Cleared = true;
        isStage2Cleared = true;
    }

    
}
