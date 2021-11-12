using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Type1_aniEvent1 : MonoBehaviour
{
    public Enemy_Type1 enemy;
    public void Attack()
    {
        enemy.OnHit();
    }

    public void EndDamaged()
    {
        enemy.EndDamaged();
    }
}
