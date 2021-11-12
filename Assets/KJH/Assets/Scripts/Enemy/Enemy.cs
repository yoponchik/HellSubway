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
        Damage, // �÷��̾�� �������� �޾�����
        Aggro,   // ��ź�� �¾�����
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
    // ��׷� ��ź�� �ݰ� �� ���� �ȿ� �͠��� ��� ���񿡰� ��׷ΰ� ������.



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
