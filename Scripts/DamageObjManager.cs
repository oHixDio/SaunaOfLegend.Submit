using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageObjManager : MonoBehaviour
{
    [SerializeField] GameObject damageObjectPrefab;
    [SerializeField] Transform damageObjSpownPoint;

    public GameObject DamageObj
    {
        get { return damageObj; }
    }
    GameObject damageObj;


    public bool DamageObjMoving
    {
        get { return damageObjMoving; }
        set { damageObjMoving = value; }
    }
    bool damageObjMoving;

    public enum ActoType
    {
        Player,
        Enemy,
    }
    [SerializeField] ActoType actoType;

    float actoTypeFloat;

    void Start()
    {
        ActoTypeCheck();
        damageObjMoving = false;
    }

    void ActoTypeCheck()
    {
        if (actoType == ActoType.Player)
        {
            actoTypeFloat = 0.04f;
        }
        else if (actoType == ActoType.Enemy)
        {
            actoTypeFloat = -0.04f;
        }
    }

    public void DamageObjSpown()
    {
        damageObjMoving = true;
        damageObj = Instantiate(damageObjectPrefab, damageObjSpownPoint.position, Quaternion.identity);
    }
    public void DamageObjMove()
    {
        
        damageObj.transform.Translate(actoTypeFloat, 0, 0);
    }
    public void DamageObjDestroy()
    {
        damageObjMoving = false;
        Destroy(damageObj);
        
    }

    
}
