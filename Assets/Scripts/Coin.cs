using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public AudioClip itemCoin; 
    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = itemCoin;
        audioSource.playOnAwake = false; 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (itemCoin != null)
            {
                audioSource.Play();
            }

            float destroyTime = itemCoin != null ? Mathf.Max(itemCoin.length - 0.5f, 0f) : 0f;
            Destroy(gameObject, destroyTime);
        }
    }
}
