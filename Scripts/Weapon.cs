using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    #region WEAPON STATUS
    [SerializeField] string weaponName = " ";
    [SerializeField] int attackPoint = 0;
    [SerializeField] Sprite weaponSprite = null;

    public enum ItemType
    {
        Weapon,
        other,
    }
    [SerializeField] ItemType itemType;
    #endregion

    #region METHOD
    public string WeaponName()
    {
        return weaponName;
    }

    public int AttackPoint()
    {
        return attackPoint;
    }

    public Sprite WeaponSprite()
    {
        return weaponSprite;
    }

    public ItemType Type()
    {
        return itemType;
    }
    #endregion
}
