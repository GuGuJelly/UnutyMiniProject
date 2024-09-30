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
        // ��ũ�� �̵��ӵ��� �ʹ� ���� ������ �����󰡸� 
        // ��ũ�� ������������ �̵������� �����İ�������
        // ���������ϱ� ������ agent�� ������ ����� ���� ���ָ� �ذ�ȴ�
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
