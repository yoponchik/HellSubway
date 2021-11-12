using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 플레이어 HP를 보여 줄 UI나 특수처리가 필요할것같아서..
public class PlayerHP : MonoBehaviour
{
    public static PlayerHP instance;

    private void Awake()
    {
        instance = this;
    }


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

    public void Heal(int healHP)
    {
        CURRENTHP += healHP;
    }

    public void Damage(int damage)
    {
        CURRENTHP -= damage;
    }
}
