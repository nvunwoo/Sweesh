using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerX : MonoBehaviour
{
    public Transform player;
    public float offsetX = 0f;

    private float fixedY;
    private float fixedZ;

    void Start()
    {
        fixedY = transform.position.y;
        fixedZ = transform.position.z;
    }

    void Update()
    {
        transform.position = new Vector3(player.position.x + offsetX, fixedY, fixedZ);
    }
}
