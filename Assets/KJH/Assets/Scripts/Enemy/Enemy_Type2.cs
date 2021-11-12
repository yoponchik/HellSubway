using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// ������ �Ѿƿ���??
public class Enemy_Type2 : Enemy
{
    /*public GameObject target;*/
    //NavMeshAgent agent;

    

    public float chaseDistance = 5;  // �߰ݻ�Ÿ�
    //public Animator anim;


    // Time
    /*public float waittingTime = 1;
    float currentTime;*/

    /*public enum State
    {
        Chase,
        Patrol,
        Damage, // �÷��̾�� �������� �޾�����
        Aggro,  // ��ź�� �¾�����,
        Idle
    }

    public State state;*/

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = 2f;
        state = State.Idle;


    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Chase:
                UpdateChase();
                break;
            case State.Patrol:
                break;
            case State.Damage:
                UpdateDamage();
                break;
            case State.Aggro:
                UpdateAggro();
                break;
            case State.Idle:
                UpdateIdle();
                break;
            default:
                break;
        }

    }

    // ��׷� ��ź�� �ݰ� �� ���� �ȿ� �͠��� ��� ���񿡰� ��׷ΰ� ������.
    private void UpdateAggro()
    {
        // 1. ��������� ���ؼ� �̵��ϰ�ʹ�.
        agent.destination = moveTargetDestination;
        // 2. ���� ������ ���� �Ÿ��� �����ؼ�
        float dist = Vector3.Distance(transform.position, moveTargetDestination);
        // 3. ���� �Ÿ��� 5���� ũ�ٸ� (5M���� �ָ� ������ �ִٸ�)
        if (dist < 2f)
        {
            currentTime += Time.deltaTime;
            if (currentTime >= waittingTime)
            {
                currentTime = 0;
                // 4. �������·� �����ϰ�ʹ�.
                state = State.Idle;

            }
        }
    }

    // �÷��̾ �������� ���

    private void UpdateDamage()
    {
        Vector3 dir = target.transform.position - transform.position;
        dir.Normalize();

        float speed = 20f;
        Quaternion rotation = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, speed * Time.deltaTime);

        agent.destination = target.transform.position;
        float distance = Vector3.Distance(transform.position, target.transform.position);
        agent.stoppingDistance = 1.5f;

        if (distance > 15f)
        {
            // 4. �������·� �����ϰ�ʹ�.
            state = State.Idle;

        }
        if (distance <= 15f)
        {
            state = State.Chase;
        }
    }

    // ������ �ִ� ����
    void UpdateIdle()
    {
        
        Collider[] cols = Physics.OverlapSphere(transform.position, chaseDistance);
        for (int i = 0; i < cols.Length; i++)
        {
            if (cols[i].gameObject.name.Contains("Player"))
            {
                target = cols[i].gameObject;
                state = State.Chase;
                break;
            }
        }


    }

    private void UpdateChase()
    {
        if (target != null)
        {
            float distance = Vector3.Distance(transform.position, target.transform.position);
            float maxChaseDistance = chaseDistance * 2f;

            Vector3 dir;
            float speed = 5;

            dir = target.transform.position - transform.position;
            dir.Normalize();

            Quaternion rotation = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, speed * Time.deltaTime);

            agent.SetDestination(target.transform.position);


            if (distance < agent.stoppingDistance)
            {
                anim.SetTrigger("Attack");
            }
            else
            {
                anim.SetTrigger("Walk");
            }

            if (distance > 6)
            {
                target = null;
                anim.SetTrigger("Idle");
                state = State.Idle;
                //transform.position = transform.position;
            }

        }
        else
        {
            state = State.Idle;
        }
        


    }


    /*internal void OnHit()
    {
        HitManager.instance.Hit();
    }*/
}
