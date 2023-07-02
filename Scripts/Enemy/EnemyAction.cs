using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAction : MonoBehaviour
{
    void Attack()
    {

    }

    public void Damage(EnemyState enemyState, PlayerState playerState)
    {
        if (enemyState.EnemyHP > 0)
        {
            enemyState.EnemyHP -= playerState.CurrentAT;
        }
        AudioManager.instance.DamageSE();
    }
}
