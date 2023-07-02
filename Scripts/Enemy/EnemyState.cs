using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
    [Header("Enemy Status")]
    [SerializeField] string enemyName = " ";
    [SerializeField] float enemyHP = 20;
    [SerializeField] float enemyAT = 5;
    [SerializeField] Sprite enemySprite;
    GameObject myObj;
    [SerializeField] GameObject setObj;

    public float MaxHP
    {
        get { return maxHP; }
    }
    float maxHP = 100;

    public float DefaultHP
    {
        get { return defaultHP; }
    }
    float defaultHP;

    public float DamageAmount
    {
        get { return damageAmount; }
        set { damageAmount = value; }
    }
    float damageAmount;

    public float GaugeFillAmount
    {
        get { return gaugeFillAmount; }
        set { gaugeFillAmount = value; }
    }
    float gaugeFillAmount;


    /*
    //enemyHP / enemyHP * 100 = maxHP
    //Damage * enemyHP / 100 = damageFillAmount
    ( maxHP - damageFillAmount)  / 100 = gaugeFillAmount
    */

    public string EnemyName
    {
        get { return enemyName; }
    }
    public float EnemyHP
    { 
        get { return enemyHP; } 
        set { enemyHP = value; }
    }
    public float EnemyAT
    { 
        get { return enemyAT; } 
        set { enemyAT = value; }
    }
    public Sprite EnemySprite
    {
        get { return enemySprite; }
    }


    public float AttackIntervalTimer //攻撃のインターバル
    {
        get { return attackIntervalTimer; }
        set { attackIntervalTimer = value; }
    }
    [SerializeField] float attackIntervalTimer = 0f;


    public enum EnemyType //Enemyの種類
    {
        Slime,
        Spider,
        Skelton,
        Mimic,
        Orc,
    }
    [SerializeField] EnemyType enemyType;

    public bool InIncideCamera //Camera内かどうか
    {
        get { return inIncideCamera; }
    }
    bool inIncideCamera;

    public bool BesidePlayer //Playerの近くかどうか
    {
        get { return besidePlayer; }
    }
    bool besidePlayer;

    public bool PlayerField //UI用Playerの近くかどうか
    {
        get { return playerField; }
    }
    bool playerField;

    public bool IsDead //死んでいるかどうか
    {
        get { return isDead; }
        set { isDead = value; }
    }
    bool isDead;


    EnemyAction enemyAction;
    EnemyState enemyState;
    EnemyUI enemyUI;
    PlayerState playerState;
    DamageObjManager damageObjManager;

    void Awake()
    {
        enemyAction = GetComponent<EnemyAction>();
        enemyState = GetComponent<EnemyState>();
        enemyUI = GetComponent<EnemyUI>();
    }
    void Start()
    {
        //enemyHP / enemyHP * 100 = maxHP
        defaultHP = enemyHP;
        maxHP = defaultHP / defaultHP * 100f;
        damageObjManager = null;
        inIncideCamera = false;
        besidePlayer = false;
        playerField = false;
        isDead = false;
    }
    private void Update()
    {
        if(myObj != null)
        {
            enemyUI.ShowEnemyUI(myObj);
        }
        
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "MainCamera")
        {
            inIncideCamera = true;
        }
        if (collision.tag == "DamageObj")
        {
            enemyAction.Damage(enemyState, playerState);
            if(myObj != null)
            {
                enemyUI.CurrentFillAmount(myObj, playerState);
            }
            
            damageObjManager.DamageObjDestroy();
        }
        if (collision.tag == "PlayerField")
        {
            playerField = true;
            myObj = setObj;
            
        }

    }

    

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "MainCamera")
        {
            inIncideCamera = false;
        }
        if (collision.tag == "PlayerField")
        {
            playerField = false;
            
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            damageObjManager = collision.gameObject.GetComponent<DamageObjManager>();
            playerState = collision.gameObject.GetComponent<PlayerState>();
            besidePlayer = true; 
        }
        
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            besidePlayer = false;
        }
    }


    void AttackDelay()
    {

    }



}
