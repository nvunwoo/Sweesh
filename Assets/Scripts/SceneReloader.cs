using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneReloder : MonoBehaviour
{
    // ���� ���� ���ε�
    public void ReloadScene()
    {
        // ���� Ȱ��ȭ�� ���� �ε�
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
