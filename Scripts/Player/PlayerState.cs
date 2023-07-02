using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    [Header("Player Status")]
    [SerializeField] int playerAT = 0;
    [SerializeField] int playerHP = 100; 
    [SerializeField] float attackIntervalTimer = 0f;
    int currentAT;
    float timer = 0f;

    DamageObjManager damageObjManager;
    PlayerAction playerAction;

    #region PLAYER STATUS GETSET
    public int PlayerAT //攻撃力
    {
        get { return playerAT; }
        set { playerAT = value; }
    }
    public int PlayerHP //体力
    {
        get { return playerHP; }
        set { playerHP = value; }
    }

    public float AttackIntervalTimer //攻撃のインターバル
    {
        get { return attackIntervalTimer; }
        set { attackIntervalTimer = value; }
    }

    public int CurrentAT
    {
        get { return currentAT; }
        set { currentAT = value; }
    }
    #endregion

    #region PLAYER SITUATION GETSET
    public GameObject GetWeapon //チェスト内の武器を参照
    {
        get { return getWeapon; }
        set { getWeapon = value; }
    }
    GameObject getWeapon;

    public GameObject HaveWeapon //所持している武器
    {
        get { return haveWeapon; }
        set { haveWeapon = value; }
    }
    GameObject haveWeapon;

    public Chest Chest //触れているチェスト
    {
        get { return chest; }
        set { chest= value; }
    }
    Chest chest;

    public bool EntrySauna //サウナエリア内か否か
    {
        get { return entrySauna; }
    }
    bool entrySauna;

    public bool BesideChest //Chestの近くか否か
    {
        get { return besideChest; }
        set { besideChest = value; }
    }
    bool besideChest;

    public bool BesideWeapon //Weaponの近くか否か
    {
        get { return besideWeapon; }
        set { besideWeapon = value; }
    }
    bool besideWeapon;

    public bool BesideWordsPoint //WordsPointの近くか否か
    {
        get { return besideWordsPoint; }
    }
    bool besideWordsPoint;


    public GameObject WordsPoint
    {
        get { return wordsPoint; }
    }
    GameObject wordsPoint;



    public bool BesideEnemy //Enemyの近くかどうか
    {
        get { return besideEnemy; }
    }
    bool besideEnemy;

    public bool IsEnd
    {
        get { return isEnd; }
        set { isEnd = value; }
    }
    bool isEnd;

    public bool HasWeapon //Weaponを持っているかどうか
    {
        get { return hasWeapon; }
        set { hasWeapon = value; }
    }
    bool hasWeapon;

    public bool IsAttack
    {
        get { return isAttack; }
        set { isAttack = value; }
    }
    bool isAttack;

    
    #endregion

    void Awake()
    {
        playerAction = GetComponent<PlayerAction>();
    }
    void Start()
    {
        currentAT = 0;
        getWeapon = null;
        haveWeapon = null;
        chest = null;
        entrySauna = false;
        besideChest = false;
        besideWeapon = false;
        besideWordsPoint = false;
        besideEnemy = false;
        isAttack = false;
        hasWeapon = false;
        isEnd = false;
    }





    #region PLAYER COLLISION
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Sauna")
        {
            entrySauna = true;
            AudioManager.instance.FireSE();
        }
        if(collision.tag == "Chest")
        {
            besideChest = true;

            //範囲内のチェスト取得
            chest = collision.GetComponent<Chest>();

            chest.IsOpen = true;

            //チェスト内のアイテム取得
            getWeapon = chest.WeaponPrefab;
        }
        if (collision.tag == "DamageObj")
        {

            //TODO:::::EnemyDamage()処理を書く

            //当たったDamageObjを破壊する
            damageObjManager.DamageObjDestroy();
        }
        if(collision.tag == "WordsPoint")
        {
            besideWordsPoint = true;
            wordsPoint = collision.gameObject;
        }
        if (collision.tag == "LastWords")
        {
            wordsPoint = collision.gameObject;
            isEnd = true;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Sauna")
        {
            entrySauna = false;
            AudioManager.instance.StopFireSE();
        }
        if (collision.tag == "Chest")
        {
            besideChest = false;
            chest.IsOpen = false;

            //チェストを閉じる
            playerAction.ChestClose();

            getWeapon = null;
            chest = null;
        }
        if (collision.tag == "WordsPoint")
        {
            besideWordsPoint = false;
            wordsPoint = null;
        }
        if (collision.tag == "LastWords")
        {
            wordsPoint = null;
            isEnd = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            besideEnemy = true;

            //触れた敵のDamageObjManager取得
            damageObjManager = collision.gameObject.GetComponent<DamageObjManager>();
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            besideEnemy = false;
        }
    }
    #endregion

    public bool AttackTimer()
    {
        
        
        if(attackIntervalTimer > timer)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
