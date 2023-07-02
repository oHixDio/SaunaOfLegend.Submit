using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{

    public GameObject WeaponPrefab　//チェストに入っているアイテム
    {
        get { return weaponPrefab; }
        set { weaponPrefab = value; }
    }
    [SerializeField] GameObject weaponPrefab;

    public bool IsOpen //チェストが空いているか否か
    {
        get { return isOpen; }
        set { isOpen = value; }
    }
    bool isOpen;

    public BoxCollider2D BoxCollider2D
    {
        get { return boxCollider2D; }
        set { boxCollider2D = value; }
    }
    BoxCollider2D boxCollider2D;

    public GameObject ChestUI
    {
        get { return chestUI; }
        set { chestUI = value; }
    }
    GameObject chestUI;

    

    Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        chestUI = transform.GetChild(1).gameObject;
    }
    void Start()
    {
        isOpen = false;
    }

    public void ChestAnime()
    {
        if (isOpen)
        {
            animator.SetBool("isOpen", true);
        }
        else if(!isOpen)
        {
            animator.SetBool("isOpen", false);
        }
        
    }


}
