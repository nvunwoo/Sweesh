using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour
{
    // ��ư Ŭ�� �� ȣ��� �� ��ȯ �Լ�
    public void ChangeScene(string sceneName)
    {
        // ������ �� �̸��� ���� ���� �ε�
        SceneManager.LoadScene(sceneName);
    }
}
