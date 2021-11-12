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
        Damage, // �÷��̾�� �������� �޾�����
        Aggro   // ��ź�� �¾�����
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
            // 4. �������·� �����ϰ�ʹ�.
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

        // �þ� �ȿ� ��� �������
        target = fov.playerRef;
        //canSeePlayer = fov.canSeePlayer;
        //anim.SetTrigger("Walk");
        detecting = false;

        // �þ߾ȿ� ��� ���� �ʴ��� ����������� �÷��̾�Է� ������ ȸ���Ѵ�.
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
        else if (!canSeePlayer && !detecting)     // �������
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

        // ���� �ʿ� 1. �Ⱥ��̸� ���� ���� �Ÿ��� �־����� ��������

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
