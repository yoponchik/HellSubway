using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Enemy : MonoBehaviour
{

    protected EnemyHP enemyHP;
    protected NavMeshAgent agent;

    public GameObject target;
    public Animator anim;

    // Time
    public float waittingTime = 1;
    protected float currentTime;

    public enum State
    {
        Chase,
        Patrol,
        Damage, // 플레이어에게 데미지를 받았을때
        Aggro,   // 폭탄을 맞았을때
        Idle,
        Laying

    }



    public State state;

    // Start is called before the first frame update
    public virtual void Start()
    {
        enemyHP = GetComponent<EnemyHP>();
        if (enemyHP == null)
        {
            print("enemyHP == null");
        }
    }

    // Update is called once per frame
    /*public virtual void Update()
    {
    }*/
    // 어그로 폭탄이 반경 몇 미터 안에 터졋을 경우 좀비에게 어그로가 끌린다.



    protected Vector3 moveTargetDestination;
    public virtual void SetMoveTarget(Vector3 newDestination)
    {
        moveTargetDestination = newDestination;
        state = State.Aggro;
        anim.SetTrigger("Walk");
    }



    public virtual void SetChaseTarget(GameObject newDestination)
    {
        target = newDestination;
        state = State.Damage;
        anim.SetTrigger("Walk");
    }


    public virtual void OnDamaged(int damage)
    {
        //base.OnDamaged(damage);
        enemyHP.currentHP -= damage;
        if (enemyHP.currentHP > 0)
        {
            anim.SetTrigger("Damage");
            agent.isStopped = true;
        }
        if (enemyHP.currentHP <= 0)
        {
            Destroy(gameObject);
        }
    }

    internal void OnHit()
    {
        HitManager.instance.Hit();
    }

    internal void EndDamaged()
    {
        agent.isStopped = false;
    }
}
