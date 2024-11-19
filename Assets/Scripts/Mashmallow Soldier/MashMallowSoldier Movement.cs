using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MashMallowSoldierMovement : MonoBehaviour
{
    GameObject Character;
    // Start is called before the first frame update
    void Start()
    {
        this.Character = GameObject.Find("Character");
    }

    // Update is called once per frame
    void Update()
    {
    //인식 범위와 공격 범위
        Vector3 MashSoldierVec = transform.position;
        Vector3 CharacterVec = this.Character.transform.position;
        Vector3 dir = MashSoldierVec - CharacterVec;

        float dirBetweenMashSoldierCharacter = dir.magnitude;
        if (dirBetweenMashSoldierCharacter < 100)
        {

        }
    }
}
