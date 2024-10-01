using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NavMover : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform target;
    [SerializeField] Transform returnPoint;

    [SerializeField] Transform patrollPoint1;
    [SerializeField] Transform patrollPoint2;
    [SerializeField] Transform patrollPoint3;

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

    private void Start()
    {
        PatollMonsterRoutine = StartCoroutine(PatrollMonster());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (PatollMonsterRoutine != null)
            {
                StopCoroutine(PatollMonsterRoutine);
            }

            transform.LookAt(target);
            agent.speed = 5;
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
            if (PatollMonsterRoutine != null)
            {
                StopCoroutine(PatollMonsterRoutine);
            }
            
            transform.LookAt(target);
            agent.speed = 5;
            agent.stoppingDistance = 8;
            agent.isStopped = false;
            agent.destination = target.position;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            agent.speed = 3;
            transform.LookAt(returnPoint);
            agent.stoppingDistance = 0;
            agent.destination = returnPoint.position;
            if (PatollMonsterRoutine == null)
            {
                PatollMonsterRoutine = StartCoroutine(PatrollMonster());
            }
        }
        agent.isStopped = false;
    }

    Coroutine PatollMonsterRoutine;

    private IEnumerator PatrollMonster()
    {
        while (true)
        {
            yield return null;
            agent.speed = 3;
            yield return null;
            agent.destination = patrollPoint2.position;
            yield return new WaitForSeconds(10f);
            agent.destination = patrollPoint3.position;
            yield return new WaitForSeconds(10f);
            agent.destination = patrollPoint1.position;
            yield return new WaitForSeconds(10f);
        }
    }
}
