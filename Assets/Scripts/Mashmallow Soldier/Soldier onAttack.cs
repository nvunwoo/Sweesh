using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SoldieronAttack : MonoBehaviour
{
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        target = GameObject.Find("Character").transform;
        float distance = Vector3.Distance(transform.position, target.transform.position);
        if (distance <= 2)
        {
            gameObject.GetComponent<CapsuleCollider>().enabled = true;
        }
        else
        {
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer.Equals(LayerMask.NameToLayer("Character"))) 
        { 
            Debug.Log("hit"); 
        }
       
    }
}
