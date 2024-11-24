using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneBGM : MonoBehaviour
{
    private void Start()
    {
        // AudioSource ������Ʈ ��������
        AudioSource audioSource = GetComponent<AudioSource>();

        // ���� ���
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}

