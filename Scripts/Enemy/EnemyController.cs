using Cainos.PixelArtMonster_Dungeon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Rigidbody2D enemyRb;
    EnemyState enemyState;
    EnemyAction enemyAction;
    PixelMonster piMo;
    DamageObjManager damageObjManager;

    [SerializeField] float moveSpeed = 5.0f;

    void Awake()
    {
        enemyRb = GetComponent<Rigidbody2D>();
        enemyState = GetComponent<EnemyState>();
        enemyAction = GetComponent<EnemyAction>();
        piMo = GetComponent<PixelMonster>();
        damageObjManager = GetComponent<DamageObjManager>();
    }
    void Start()
    {
    }
    void Update()
    {
        if (enemyState.IsDead)
        {
            piMo.IsDead = true;
            Destroy(this.gameObject, 2f);
        }
    }
    void FixedUpdate()
    {
        if(!enemyState.IsDead)
        {
            Run();
        }
        
    }

    void Run()
    {
        if (enemyState.InIncideCamera)
        {
            if (!enemyState.BesidePlayer)
            {
                enemyRb.velocity = new Vector2(-moveSpeed, enemyRb.velocity.y);

                //ëñÇÈÉÇÅ[ÉVÉáÉì
                piMo.MovingBlend = Mathf.Abs(moveSpeed);
            }
            else
            {
                enemyRb.velocity = Vector3.zero;
                piMo.MovingBlend = 0.0f;
            }
        }
        else
        {
            enemyRb.velocity = Vector3.zero;
            piMo.MovingBlend = 0.0f;
        }
    }

    
}
