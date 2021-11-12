using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Enemy_Base : MonoBehaviour
{
    GameObject target;
    NavMeshAgent agent;

    public enum State
    {
        Chase,
        Patrol
    }

    public State state;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        state = State.Patrol;
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
            default:
                break;
        }

    }

    private void UpdatePatrol()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hitInfo;
        int layer = 1 << LayerMask.NameToLayer("Player");

        if (Physics.Raycast(ray, out hitInfo, float.MaxValue, layer))
        {
            target = hitInfo.transform.gameObject;
            state = State.Chase;

        }

        /*        if (Physics.CheckSphere(transform.position, 10, layer))
                {
                    target = GameObject.Find("Player");
                    state = State.Chase;
                }*/

    }

    private void UpdateChase()
    {
        if (target != null)
        {
            agent.SetDestination(target.transform.position);

        }
        else
        {
            state = State.Patrol;
        }
    }
}
