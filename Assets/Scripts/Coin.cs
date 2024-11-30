using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public AudioClip coin_2; 
    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = coin_2;
        audioSource.playOnAwake = false; 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (coin_2 != null)
            {
                audioSource.Play();
            }

            float destroyTime = coin_2 != null ? Mathf.Max(coin_2.length - 0.5f, 0f) : 0f;
            Destroy(gameObject, destroyTime);
        }
    }
}
