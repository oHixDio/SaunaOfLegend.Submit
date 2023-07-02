using Cainos.CustomizablePixelCharacter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TotonoiController : MonoBehaviour
{
    [SerializeField] GameObject sauner;
    [SerializeField] float gaugeAmount;

    Image myTPGauge;
    PlayerState playerCollision;


    void Start()
    {
        myTPGauge = GetComponent<Image>();
        playerCollision = sauner.GetComponent<PlayerState>();
    }
    void Update()
    {
        CurrentTP();
    }

    

    void CurrentTP()
    {
        if (playerCollision.EntrySauna)
        {
            if (myTPGauge.fillAmount < 1f)
            {
                myTPGauge.fillAmount += gaugeAmount;
            }
        }
        else if (!playerCollision.EntrySauna)
        {
            
            if (myTPGauge.fillAmount > 0f)
            {
                myTPGauge.fillAmount -= gaugeAmount;
            }
        }
    }
}
