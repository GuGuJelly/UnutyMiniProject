using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMover2 : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform target;
    [SerializeField] Transform returnPoint;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.isStopped = true;
    }


    //private void NavChaserMonster()
    //{
    //    if (boxCollider.CompareTag("Player"))
    //    {
    //        transform.LookAt(target);
    //        agent.isStopped = false;
    //        agent.destination = target.position;
    //
    //        if (boxCollider == null)
    //        {
    //            agent.isStopped = true;
    //        }
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            transform.LookAt(target);
            agent.stoppingDistance = 8;
            agent.isStopped = false;
            agent.destination = target.position;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // 탱크의 이동속도가 너무 빨라서 가속이 못따라가면 
        // 탱크가 목적지점까지 이동했을때 지나쳐가버려서
        // 갈팡질팡하기 때문에 agent의 가속을 충분히 높게 해주면 해결된다
        if (other.gameObject.CompareTag("Player"))
        {
            transform.LookAt(target);
            agent.stoppingDistance = 8;
            agent.isStopped = false;
            agent.destination = target.position;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            transform.LookAt(returnPoint);
            agent.stoppingDistance = 0;
            agent.destination = returnPoint.position;
        }
        agent.isStopped = false;
    }
}
