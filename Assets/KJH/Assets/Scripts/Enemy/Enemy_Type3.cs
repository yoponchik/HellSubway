using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

// 무한히 쫓아오게??
public class Enemy_Type3 : Enemy
{
    /*public GameObject target;*/
    //NavMeshAgent agent;
    public float chaseDistance = 5;  // 추격사거리

    public float radius = 1f;  // 랜덤 반경
    Vector3 origin;

    //public Animator anim;



    // Time
    /*public float waittingTime = 1;
    float currentTime;*/

    // bool
    bool isWakeUP = false;
    public bool isHit = false;


    public bool isAggro = false;
    public GameObject key;


    /*public  enum State
    {
        Chase,
        Patrol,
        Damage, // 플레이어에게 데미지를 받았을때
        Aggro,  // 폭탄을 맞았을때,
        Idle,
        Laying
    }*/

    /*public State state;*/

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = 2f;
        //
        state = State.Laying;

        origin = transform.position;
        SetRandomTarget();
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
                UpdatePatrol();
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
            case State.Laying:
                Laying();
                break;
            default:
                break;
        }

    }

    void Laying()
    {

        Collider[] cols = Physics.OverlapSphere(transform.position, chaseDistance);
        for (int i = 0; i < cols.Length; i++)
        {
            if (cols[i].gameObject.name.Contains("Player"))
            {
                target = cols[i].gameObject;
                anim.SetTrigger("Detect");
                break;
            }
        }


    }

    internal void OnWakeUP()
    {
        state = State.Idle;
    }


    // 가만히 있는 상태
    void UpdateIdle()
    {
         // 플레이어 감지

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


        waitCurrentTime += Time.deltaTime;
        if (waitCurrentTime > waitTIme)
        {
            state = State.Patrol;
            anim.SetTrigger("Walk");
        }

    }

    void SetRandomTarget()
    {
        Vector2 r = Random.insideUnitCircle * radius;

        movePatrolDestination = origin + new Vector3(r.x, 0, r.y);
    }

    public Vector3 movePatrolDestination;

    void UpdatePatrol()
    {
        Vector3 dir = movePatrolDestination - transform.position;
        dir.Normalize();
        // 이동하고싶다. P = P0 + vt
        transform.position += dir * 2f * Time.deltaTime;

        Quaternion targetRotation = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 5);

        float goalDistance = Vector3.Distance(transform.position, movePatrolDestination);
        if (goalDistance < 2)
        {
            waitCurrentTime = 0;
            SetRandomTarget();
            SetWaitTime();
            state = State.Idle;
            anim.SetTrigger("Idle");
        }

;    }

    float waitMin = 2;
    float waitMax = 3;
    float waitTIme;
    float waitCurrentTime;

    void SetWaitTime()
    {
        waitTIme = Random.Range(waitMin, waitMax);
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

            if (distance > 10)
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

    // 어그로 폭탄이 반경 몇 미터 안에 터졋을 경우 좀비에게 어그로가 끌린다.
    protected  void UpdateAggro()
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

    //Vector3 moveTargetDestination;
    public override void SetMoveTarget(Vector3 newDestination)
    {
        isAggro = true;
        if (state == State.Laying)
        {
            anim.SetTrigger("Detect");
            moveTargetDestination = newDestination;
        }
        else
        {
            moveTargetDestination = newDestination;
            state = State.Aggro;
            anim.SetTrigger("Walk");
        }
        
    }


    internal void AggroWakeUp()
    {
        state = State.Aggro;
        isAggro = false;
        anim.SetTrigger("Walk");
    }

    // 플레이어가 공격했을 경우

    private  void UpdateDamage()
    {
        Vector3 dir = target.transform.position - transform.position;
        dir.Normalize();

        float speed = 20f;
        Quaternion rotation = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, speed * Time.deltaTime);

        agent.destination = target.transform.position;
        float distance = Vector3.Distance(transform.position, target.transform.position);
        agent.stoppingDistance = 1.5f;
        

        if (distance > 10f)
        {
            // 4. 순찰상태로 전이하고싶다.
            state = State.Idle;
            anim.SetTrigger("Idle");

        }
        if (distance <= 10f)
        {
            state = State.Chase;
            anim.SetTrigger("Walk");
        }
    }

    public override void SetChaseTarget(GameObject newDestination)
    {
        isHit = true;
        if (state == State.Laying)
        {
            anim.SetTrigger("Detect");
            target = newDestination;
        }
        else
        {
            target = newDestination;
            state = State.Damage;
            anim.SetTrigger("Walk");
        }
    }


    internal void OnHitWakeUP()
    {
        state = State.Damage;
        isHit = false;
        anim.SetTrigger("Walk");
    }

    // Player를 떄렷을떄..
    /*internal void OnHit()
    {
        HitManager.instance.Hit();
    }*/

    public override void OnDamaged(int damage)
    {
        //base.OnDamaged(damage);
        enemyHP.currentHP -= damage;
        //anim.SetTrigger("Damage");
        if (enemyHP.currentHP <= 0)
        {
            key.transform.parent = null;
            key.SetActive(true);
            Destroy(gameObject);
        }
    }


}
