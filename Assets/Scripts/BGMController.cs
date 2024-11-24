using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMController : MonoBehaviour
{
    private static BGMController instance;
    private AudioSource audioSource;

    // Ư�� �������� ���� ����
    [SerializeField] private string[] scenesToKeepMusic;

    private void Awake()
    {
        // Singleton �������� ������Ʈ �ߺ� ����
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        // �� �ε� �̺�Ʈ ���
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // �� �ε� �̺�Ʈ ����
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // ���� ���� �����ؾ� �� ������ Ȯ��
        if (IsSceneInList(scene.name))
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play(); // ���� ���
            }
        }
        else
        {
            Destroy(gameObject); // �ش� ������ ������Ʈ ����
        }
    }

    private bool IsSceneInList(string sceneName)
    {
        // Ư�� �� ��Ͽ� ���� ���� ���ԵǾ� �ִ��� Ȯ��
        foreach (string name in scenesToKeepMusic)
        {
            if (name == sceneName) return true;
        }
        return false;
    }
}
