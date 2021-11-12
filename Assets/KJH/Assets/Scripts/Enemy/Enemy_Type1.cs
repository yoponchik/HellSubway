using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Enemy_Type1 : Enemy
{
    /*public GameObject target;*/
    //NavMeshAgent agent;

    

    FieldOfView fov;
    public bool canSeePlayer;


    //public Animator anim;
    public PathManager path;
    int pathIndex;
    public float goalPoint = 2.5f;

    // Test
    Transform currentPosition;
    bool detecting;

    /*// Time
    public float waittingTime = 1;
    float currentTime;*/

/*    public enum State
    {
        Chase,
        Patrol,
        Damage, // 플레이어에게 데미지를 받았을때
        Aggro   // 폭탄을 맞았을때
    }



    public State state;*/

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        agent = GetComponent<NavMeshAgent>();
        state = State.Patrol;
        fov = GetComponent<FieldOfView>();
        anim.SetTrigger("Walk");
        //anim = GetComponent<Animator>();
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
                state = State.Patrol;

            }
        }
    }

    private void UpdateDamage()
    {
        //Vector3 dir = fov.playerRef.transform.position - transform.position;
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
            state = State.Patrol;

        }
        if (distance <= 15f)
        {
            state = State.Chase;
        }
    }


    bool isFirst = true;

    private void UpdatePatrol()
    {

        // 시야 안에 들어 왔을경우
        target = fov.playerRef;
        //canSeePlayer = fov.canSeePlayer;
        //anim.SetTrigger("Walk");
        detecting = false;

        // 시야안에 들어 오지 않더라도 근접했을경우 플레이어에게로 방향을 회전한다.
        float distance = Vector3.Distance(transform.position, target.transform.position);
        if (distance < 5)
        {
            Vector3 dir = target.transform.position - transform.position;
            dir.Normalize();

            float speed = 20f;
            Quaternion rotation = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, speed * Time.deltaTime);
            detecting = true;
        }

        canSeePlayer = fov.canSeePlayer;

        if (canSeePlayer)
        {
            state = State.Chase;
        }
        else if (!canSeePlayer && !detecting)     // 순찰모드
        {
            agent.stoppingDistance = 0.5f;

            if (isFirst)
            {
                FindFirstDestination();
            }
            agent.destination = path.info[pathIndex].position;
            
            //agent.SetDestination(path.info[pathIndex].position);
            float distance2 = Vector3.Distance(transform.position, path.info[pathIndex].position);
            if (distance2 < goalPoint)
            {
                SetNextDestination();
            }
        }

    }

    private void SetNextDestination()
    {
        pathIndex++;
        if (pathIndex >= path.info.Length)
        {
            pathIndex = 0;
        }
    }

    void FindFirstDestination()
    {
        int tempIndex = -1;
        float temp = float.MaxValue;
        float distance = 0;

        for (int i = 0; i < path.info.Length; i++)
        {
            distance = Vector3.Distance(transform.position, path.info[i].position);
            if (distance < temp)
            {
                temp = distance;
                tempIndex = i;
            }
            
        }
        pathIndex = tempIndex;
        isFirst = false;

    }

    public bool isAttack = false;

    private void UpdateChase()
    {
        canSeePlayer = fov.canSeePlayer;
        agent.stoppingDistance = 2.5f;
        float distance = 0;

        if (target != null)
        {
            Vector3 dir = target.transform.position - transform.position;
            dir.Normalize();

            float speed = 20f;
            Quaternion rotation = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, speed * Time.deltaTime);

            agent.SetDestination(target.transform.position);
            distance = Vector3.Distance(transform.position, target.transform.position);
            if (distance < agent.stoppingDistance)
            {
                anim.SetTrigger("Attack");
                isAttack = true;
                /*if (!isAttack)
                {
                    anim.SetTrigger("Attack");
                    isAttack = true;
                }*/
            }
            else
            {
                if (isAttack)
                {
                    anim.SetTrigger("Walk");
                    isAttack = false;
                }
            }
            //anim.SetTrigger("Walk");


            if (distance > 6)
            {
                anim.SetTrigger("Walk");
                target = fov.playerRef;
                state = State.Patrol;
            }
        }

        // 선택 필요 1. 안보이면 포기 할지 거리가 멀어지면 포기할지

        /*if (!canSeePlayer)
        {
            anim.SetTrigger("Walk");
            target = fov.playerRef;
            state = State.Patrol;
        }*/

    }


/*    internal void OnHit()
    {
        
    }

    internal void EndDamaged()
    {
        
    }*/


}
