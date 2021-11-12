using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// 무한히 쫓아오게??
public class Enemy_Type2 : Enemy
{
    /*public GameObject target;*/
    //NavMeshAgent agent;

    

    public float chaseDistance = 5;  // 추격사거리
    //public Animator anim;


    // Time
    /*public float waittingTime = 1;
    float currentTime;*/

    /*public enum State
    {
        Chase,
        Patrol,
        Damage, // 플레이어에게 데미지를 받았을때
        Aggro,  // 폭탄을 맞았을때,
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

    // 어그로 폭탄이 반경 몇 미터 안에 터졋을 경우 좀비에게 어그로가 끌린다.
    private void UpdateAggro()
    {
        // 1. 추적대상을 향해서 이동하고싶다.
        agent.destination = moveTargetDestination;
        // 2. 나와 목적지 간의 거리를 측정해서
        float dist = Vector3.Distance(transform.position, moveTargetDestination);
        // 3. 만약 거리가 5보다 크다면 (5M보다 멀리 떨어져 있다면)
        if (dist < 2f)
        {
            currentTime += Time.deltaTime;
            if (currentTime >= waittingTime)
            {
                currentTime = 0;
                // 4. 순찰상태로 전이하고싶다.
                state = State.Idle;

            }
        }
    }

    // 플레이어가 공격했을 경우

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
            // 4. 순찰상태로 전이하고싶다.
            state = State.Idle;

        }
        if (distance <= 15f)
        {
            state = State.Chase;
        }
    }

    // 가만히 있는 상태
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
