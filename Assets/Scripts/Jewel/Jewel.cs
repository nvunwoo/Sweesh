using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jewel : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            transform.Translate(Vector3.up * 0.01f);
            spriteRenderer.color = new Color(1, 1, 1, 0.4f);
            gameObject.GetComponent<SphereCollider>().enabled = false;
            Destroy(gameObject, 1);
        }
    }
}
