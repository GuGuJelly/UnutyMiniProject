using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolMonster : MonoBehaviour
{
    public Transform[] points;
    private int current;
    public float speed;

    private void Start()
    {
        current = 0;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, points[current].position, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, points[current].position) < 0.1)
        {
            IncreaseTargetInt();
        }
    }
    private void IncreaseTargetInt()
    {
        current++;
        if (current >= points.Length)
        {
            current = 0;
        }
    }
}
