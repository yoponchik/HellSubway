using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class FieldOfView : MonoBehaviour
{
    // NavMeshAgent agent;

    public float radius;
    [Range(0,360)]
    public float angle;

    public GameObject playerRef;

    public LayerMask targetMask;    // Player
    public LayerMask obstructionMask; // 벽  Ray 차단 벽

    public bool canSeePlayer;

    // Start is called before the first frame update
    void Start()
    {
        //playerRef = GameObject.FindGameObjectWithTag("TargetPlayer");
        playerRef = GameObject.Find("Player");
        StartCoroutine(FOVRoutine());

        // agent = GetComponent<NavMeshAgent>();
    }

    IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);
        if (rangeChecks.Length!= 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle /2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                {
                    canSeePlayer = true;
                }
                else
                {
                    canSeePlayer = false;
                }
            }
            else
            {
                canSeePlayer = false;
            }

        }
        else if(canSeePlayer){
            canSeePlayer = false;
        }
    }


    // Update is called once per frame
    void Update()
    {

    }
}
