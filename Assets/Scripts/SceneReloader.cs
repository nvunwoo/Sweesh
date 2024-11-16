using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneReloder : MonoBehaviour
{
    // 현재 씬을 리로드
    public void ReloadScene()
    {
        // 현재 활성화된 씬을 로드
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
