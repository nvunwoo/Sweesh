using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public float respawnHeight = -50f;  // ������ ����, y���� �� ���Ϸ� �������� ������

    private SceneReloder sceneReloder;  // SceneReloder ��ũ��Ʈ ����

    void Start()
    {
        sceneReloder = FindObjectOfType<SceneReloder>();  // ���� ���ε��� SceneReloder ��ũ��Ʈ�� ã��
    }

    void Update()
    {
        // �÷��̾��� y��ǥ�� ������ ���� ���Ϸ� �������� ������
        if (transform.position.y <= respawnHeight)
        {
            Respawn();
        }
    }

    // ������ �Լ�
    void Respawn()
    {
        // SceneReloder�� ���ε� �Լ� ȣ��
        if (sceneReloder != null)
        {
            sceneReloder.ReloadScene();  // ���� ���� �ٽ� �ε�
        }
    }
}
