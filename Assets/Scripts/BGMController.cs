using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMController : MonoBehaviour
{
    private static BGMController instance;
    private AudioSource audioSource;

    // 특정 씬에서만 음악 유지
    [SerializeField] private string[] scenesToKeepMusic;

    private void Awake()
    {
        // Singleton 패턴으로 오브젝트 중복 방지
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
        // 씬 로드 이벤트 등록
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // 씬 로드 이벤트 해제
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 현재 씬이 유지해야 할 씬인지 확인
        if (IsSceneInList(scene.name))
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play(); // 음악 재생
            }
        }
        else
        {
            Destroy(gameObject); // 해당 씬에서 오브젝트 삭제
        }
    }

    private bool IsSceneInList(string sceneName)
    {
        // 특정 씬 목록에 현재 씬이 포함되어 있는지 확인
        foreach (string name in scenesToKeepMusic)
        {
            if (name == sceneName) return true;
        }
        return false;
    }
}
