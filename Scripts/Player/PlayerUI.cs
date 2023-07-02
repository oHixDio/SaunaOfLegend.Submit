using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerUI : MonoBehaviour
{
    [Header("EkeyUI")]
    [SerializeField] GameObject playerUI;
    [SerializeField] Color32 chestColor;
    [SerializeField] Color32 weaponColor;
    [SerializeField] Color32 wordsColor;
    Image playerUIImage;

    [Header("PlayerUI")]
    [SerializeField] Text playerATText; //
    [SerializeField] Text playerHPText; //
    [SerializeField] Text weaponNameText; //
    [SerializeField] Text weaponATText; //
    [SerializeField] Text currentATText; //
    [SerializeField] Text playerHPGaugeText;
    [SerializeField] Image weaponImage; //
    [SerializeField] Image playerHPGaugeImage; //
    [SerializeField] Color32 highHealthColor; //
    [SerializeField] Color32 mediumHealthColor; //
    [SerializeField] Color32 lowHealthColor; //
    [SerializeField] GameObject weaponUI; //
    [SerializeField] float gaugeAmount; //


    

    float totonoiAmount;

    PlayerState playerState;
    GameManager gameManager;

    void Awake()
    {
        playerState = GetComponent<PlayerState>();
        playerUIImage = playerUI.transform.GetChild(0).GetComponent<Image>();
        gameManager = FindObjectOfType<GameManager>();
    }
    void Update()
    {
        if (gameManager.IsPlayMode)
        {
            PopupPlayerUI();
            PopupPlayerUIColor();
            SetUI();
        }
    }

    void PopupPlayerUIColor() //頭の上のEキーUIの色を変える
    {
        if (playerState.BesideChest)
        {
            playerUIImage.color = chestColor;
        }
        else if (playerState.BesideWeapon)
        {
            playerUIImage.color = weaponColor;
        }
        else if(playerState.BesideWordsPoint || playerState.IsEnd)
        {
            playerUIImage.color = wordsColor;
        }
    }

    void PopupPlayerUI() //頭の上にEキーUI表示
    {
        if (playerState.BesideChest || playerState.BesideWeapon || playerState.BesideWordsPoint || playerState.IsEnd)
        {
            playerUI.SetActive(true);
        }
        else
        {
            playerUI.SetActive(false);
        }
    }

    void SetUI()
    {
        playerATText.text = "POW:" + playerState.PlayerAT;
        playerHPText.text = "整い率:" + playerState.PlayerHP;

        //WeaponUIチェンジャー
        if (playerState.HaveWeapon != null)
        {
            weaponUI.SetActive(true);
            Weapon weapon = playerState.HaveWeapon.GetComponent<Weapon>();
            weaponImage.sprite = weapon.WeaponSprite();
            weaponNameText.text = weapon.WeaponName();
            weaponATText.text = "POW:" + weapon.AttackPoint().ToString();

            playerState.CurrentAT = playerState.PlayerAT + weapon.AttackPoint();
            currentATText.text = "AT : " + playerState.CurrentAT;
        }
        else if (playerState.HaveWeapon == null)
        {
            weaponUI.SetActive(false);
            currentATText.text = "AT : 0" + playerState.PlayerAT;
        }

        //整うゲージ減少
        if (playerState.EntrySauna)
        {
            if (playerHPGaugeImage.fillAmount <= 1f)
            {
                playerHPGaugeImage.fillAmount += 0.04f * Time.deltaTime;
            }
        }
        else if (!playerState.EntrySauna)
        {

            if (playerHPGaugeImage.fillAmount > 0f)
            {
                playerHPGaugeImage.fillAmount -= gaugeAmount * Time.deltaTime;
            }
        }


        //HPbar変色
        if (playerHPGaugeImage.fillAmount > 0.7f)
        {
            playerHPGaugeImage.color = highHealthColor;
        }
        else if (playerHPGaugeImage.fillAmount > 0.3f)
        {
            playerHPGaugeImage.color = mediumHealthColor;
        }
        else
        {
            playerHPGaugeImage.color = lowHealthColor;
        }

        totonoiAmount = playerHPGaugeImage.fillAmount * 100f;
        playerHPGaugeText.text = "整 : " + (int)totonoiAmount;
    }



    



}
