using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Type2_aniEvent : MonoBehaviour
{
    public Enemy_Type2 enemy;
    public void Attack()
    {
        enemy.OnHit();
    }

    public void EndDamaged()
    {
        enemy.EndDamaged();
    }
}
