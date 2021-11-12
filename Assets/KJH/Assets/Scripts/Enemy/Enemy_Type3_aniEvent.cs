using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Type3_aniEvent : MonoBehaviour
{
    public Enemy_Type3 enemy;
    // Start is called before the first frame update
    public void WakeUp()
    {
        if (enemy.isAggro)
        {
            enemy.AggroWakeUp();
        }
        if (enemy.isHit)
        {
            enemy.OnHitWakeUP();
        }
        else
        {
            enemy.OnWakeUP();
        }
    }

    public void Attack()
    {
        enemy.OnHit();
    }


}
