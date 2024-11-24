using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneBGM : MonoBehaviour
{
    private void Start()
    {
        // AudioSource 컴포넌트 가져오기
        AudioSource audioSource = GetComponent<AudioSource>();

        // 음악 재생
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}

