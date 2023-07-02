using Cainos.CustomizablePixelCharacter;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    

    Vector2 moveInput;

    Rigidbody2D myRb2D;
    PixelCharacter piCha;
    PlayerAction playerAction;
    PlayerState playerState;
    DamageObjManager damageObjManager;
    GameManager gameManager;


    void Awake()
    {
        
        myRb2D = GetComponent<Rigidbody2D>();
        piCha = GetComponent<PixelCharacter>();
        playerAction = GetComponent<PlayerAction>();
        playerState = GetComponent<PlayerState>();
        damageObjManager = GetComponent<DamageObjManager>();
        gameManager = FindObjectOfType<GameManager>();
    }
    void Start()
    {
        moveInput = Vector2.zero;
    }
    void FixedUpdate()
    {
        if (gameManager.IsPlayMode)
        {
            Run();
        }
        
        if (damageObjManager.DamageObjMoving)
        {
            damageObjManager.DamageObjMove();
        }
        
    }

    #region INPUT SYSTEM
    void OnMove(InputValue Value)
    {
        if (gameManager.IsPlayMode)
        {
            moveInput = Value.Get<Vector2>();
        }
    } //移動コマンド:A,D

    void OnAttack(InputValue value)
    {
        if (value.isPressed && gameManager.IsPlayMode)
        {
            //攻撃モーション
            piCha.Attack();


            //攻撃処理
            
            if(playerState.BesideEnemy && playerState.HasWeapon)
            {
                damageObjManager.DamageObjSpown();
            }
            
        }
    } //攻撃コマンド:Space
    
    void OnAction(InputValue value)
    {
        if (value.isPressed)
        {
            if (playerState.BesideChest)
            {
                playerAction.ChestOpen();
                
            }
            else if (playerState.BesideWeapon)
            {
                playerAction.AddWeapon(playerState.GetWeapon);
                playerState.HaveWeapon = playerState.GetWeapon;
                playerState.HasWeapon = true;
                playerAction.PlayerATAmount();
                playerAction.ChestOpen();
                playerState.Chest.BoxCollider2D.enabled = false;
            }
            else if (playerState.BesideWordsPoint)
            {
                gameManager.ShowWords();
                Destroy(playerState.WordsPoint);
            }
            else if (!gameManager.IsPlayMode)
            {
                AudioManager.instance.StartSE();
                gameManager.HideStartPanel();
                gameManager.DefaultBGM();
            }
            else if (playerState.IsEnd)
            {
                gameManager.LastBGM();
                gameManager.LastWords();
                gameManager.End();
                Destroy(playerState.WordsPoint);
            }
            else if (gameManager.IsTitleBack)
            {
                gameManager.TitleBack();
            }
        }
    } //実行コマンド:宝箱,武器取得:E
    #endregion


    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * moveSpeed, myRb2D.velocity.y);
        myRb2D.velocity = playerVelocity;

        //顔の向き変更
        piCha.Facing = (int)moveInput.x;
        //走るアニメーション
        piCha.MovingBlend = Mathf.Abs(moveInput.x);
    }

    







    /*
    [SerializeField] ContactFilter2D filter2D = default;
     
    void ExampleJump()
    {
        float jumpPower = 50f;
        bool ground = myRb2D.IsTouching(filter2D);
        //Debug.Log(ground);
        if (ground)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                myRb2D.velocity = new Vector3();

                myRb2D.velocity = new Vector2(0, jumpPower);
            }
        }
    }
    */
}
