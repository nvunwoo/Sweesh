using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour
{
    // 버튼 클릭 시 호출될 씬 전환 함수
    public void ChangeScene(string sceneName)
    {
        // 지정한 씬 이름을 통해 씬을 로드
        SceneManager.LoadScene(sceneName);
    }
}
