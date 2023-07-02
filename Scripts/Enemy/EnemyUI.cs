using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
    [Header("EnemyUI")]
    [SerializeField] GameObject enemyUIPanel;
    [SerializeField] Text enemyNameText;
    [SerializeField] Text enemyATText;
    [SerializeField] Text enemyHPText;
    [SerializeField] Image enemyImage;
    [SerializeField] Color32 highHealthColor; //
    [SerializeField] Color32 mediumHealthColor; //
    [SerializeField] Color32 lowHealthColor; //

    public Image EnemyHPGaugeImage
    {
        get { return enemyImage; }
        set { enemyImage = value; }
    }
    [SerializeField] Image enemyHPGaugeImage;

    EnemyUI enemyUI;

    

    public void ShowEnemyUI(GameObject myObj)
    {
        EnemyState enemyState = myObj.GetComponent<EnemyState>();
        if (enemyState.PlayerField && !enemyState.IsDead)
        {
            Debug.Log("!!");

            SetUI(myObj);
            enemyUIPanel.SetActive(true);
            Debug.Log(enemyHPGaugeImage.fillAmount);
        }
        else
        {
            Debug.Log("??");
            enemyUIPanel.SetActive(false);
            myObj = null;
        }
    }

    void SetUI(GameObject myObj)
    {
        EnemyState enemyState = myObj.GetComponent<EnemyState>();


        if (enemyState.EnemyHP == enemyState.DefaultHP)
        {
            enemyHPGaugeImage.fillAmount = 1f;
        }



        enemyImage.sprite = enemyState.EnemySprite;

        enemyNameText.text = enemyState.EnemyName;

        //AT表示
        if (enemyState.EnemyAT >= 10)
        {
            enemyATText.text = "AT : " + enemyState.EnemyAT;
        }
        else if (enemyState.EnemyAT < 10)
        {
            enemyATText.text = "AT : 0" + enemyState.EnemyAT;
        }

        //HP表示
        if (enemyState.EnemyHP >= 10)
        {
            enemyHPText.text = "HP : " + enemyState.EnemyHP;
        }
        else if (enemyState.EnemyHP < 10 && enemyState.EnemyHP > 0)
        {
            enemyHPText.text = "HP : 0" + enemyState.EnemyHP;
        }
        else if (enemyState.EnemyHP <= 0)
        {
            enemyHPText.text = "HP : 00";
            enemyState.IsDead = true;
        }

        HPBarColor();
    }

    private void HPBarColor()
    {
        //HPカラー変更、体力０処理
        if (enemyHPGaugeImage.fillAmount > 0.7f)
        {
            enemyHPGaugeImage.color = highHealthColor;
        }
        else if (enemyHPGaugeImage.fillAmount > 0.3f)
        {
            enemyHPGaugeImage.color = mediumHealthColor;
        }
        else if (enemyHPGaugeImage.fillAmount > 0)
        {
            enemyHPGaugeImage.color = lowHealthColor;
        }
    }


    public void CurrentFillAmount(GameObject myObj, PlayerState playerState)
    {
        EnemyState enemyState = myObj.GetComponent<EnemyState>();
        enemyState.DamageAmount = playerState.CurrentAT / enemyState.DefaultHP * 100f;

        if (enemyState.DamageAmount > enemyState.MaxHP)
        {
            enemyState.GaugeFillAmount = 1f;
        }
        else
        {
            enemyState.GaugeFillAmount = 1f - ((enemyState.MaxHP - enemyState.DamageAmount) / 100f);
        }
        enemyHPGaugeImage.fillAmount -= enemyState.GaugeFillAmount;

        
    }
}
