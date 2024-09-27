using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NavMover : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform target;
    [SerializeField] BoxCollider boxCollider;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.isStopped = true;
    }

    private void Update()
    {
        NavPatrollMonster();
    }

    private void NavPatrollMonster()
    {
        if (boxCollider.CompareTag("Player"))
        {
            agent.isStopped = false;
            agent.destination = target.position;

            if (boxCollider == null)
            {
                agent.isStopped = true;
            }
        }
    }
}
