using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{

    [SerializeField] GameObject followObj;

    [SerializeField] float posX = 0f;
    [SerializeField] float posY = 0f;
    [SerializeField] float posZ = 0f;


    private void LateUpdate()
    {
        float followObjPosX = followObj.transform.position.x + posX;
        transform.position =  new Vector3(followObjPosX, posY,posZ);
    }

}
