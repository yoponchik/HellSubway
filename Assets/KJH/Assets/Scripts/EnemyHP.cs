using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 플레이어 HP를
public class EnemyHP : MonoBehaviour
{
    
    public int currentHP;
    public int maxHP = 100;

    public int CURRENTHP
    {
        get
        {
            return currentHP;
        }
        set
        {
            currentHP = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        CURRENTHP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage(int damage)
    {
        CURRENTHP -= damage;
    }
}
