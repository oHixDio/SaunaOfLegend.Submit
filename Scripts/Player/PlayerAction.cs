using Cainos.CustomizablePixelCharacter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class PlayerAction : MonoBehaviour
{
    [SerializeField] Transform weaponSlot;

    PlayerState playerState;

    void Awake()
    {
        playerState = GetComponent<PlayerState>();
    }

    public void ChestOpen() //チェストを開けるAction
    {
        Weapon weapon = playerState.Chest.WeaponPrefab.GetComponent<Weapon>();
        Image weaponImage = playerState.Chest.ChestUI.transform.GetChild(1).GetComponent<Image>();

        playerState.BesideChest = false;


        if(weapon.Type() == Weapon.ItemType.Weapon)
        {
            playerState.BesideWeapon = true;
        }
        

        playerState.Chest.ChestAnime();
        AudioManager.instance.ChestOpenSE();




        weaponImage.sprite = weapon.WeaponSprite();
        weaponImage.color = new Color(1, 1, 1, 1);

        playerState.Chest.ChestUI.SetActive(true);


    }

    public void ChestClose() //チェストを閉めるAction
    {

        //playerState.Chest.IsOpen = false;
        playerState.Chest.ChestAnime();
        AudioManager.instance.ChestCloseSE();

        playerState.Chest.ChestUI.SetActive(false);

        playerState.BesideWeapon = false;

    }

    public void DropWeapon() //持っている武器を落とす
    {
        if (weaponSlot.childCount <= 0) return;
        Transform weapon = weaponSlot.GetChild(0);

        var c = weapon.GetComponent<Collider2D>();
        if (!c) return;
        c.isTrigger = false;
        weapon.gameObject.layer = LayerMask.NameToLayer("NotWepon");

        var r = weapon.GetComponent<Rigidbody2D>();
        if (!r) return;
        r.bodyType = RigidbodyType2D.Dynamic;                   //これ活用する

        weapon.transform.parent = null;
    }

    public void ClearWeapon() //武器スロットを空にする
    {
        for (int i = 0; i < weaponSlot.childCount; i++)
        {
            var w = weaponSlot.GetChild(i);
            Destroy(w.gameObject);
        }
    }

    public void AddWeapon(GameObject weaponPrefab, bool clearOld = true) //武器を拾う
    {
        if (weaponPrefab == null) return;
        DropWeapon();
        if (clearOld) ClearWeapon();

        AudioManager.instance.WeaponSE();

        weaponPrefab.layer = LayerMask.NameToLayer("HasWepon");

        var weapon = Instantiate(weaponPrefab);
        weapon.transform.parent = weaponSlot;
        weapon.transform.localPosition = Vector3.zero;
        weapon.transform.localRotation = Quaternion.identity;
    }

    public void Attack() //攻撃をする処理
    {
        
    }

    public void Damage() //ダメージを食らう処理
    {
        //Damage処理
    }
    
    public void Dead()  //体力が０になったら実行
    {

    }

    
    public void PlayerATAmount()
    {
        Weapon weapon = playerState.GetWeapon.GetComponent<Weapon>();
        
        playerState.CurrentAT = playerState.PlayerAT + weapon.AttackPoint();
    }

}
