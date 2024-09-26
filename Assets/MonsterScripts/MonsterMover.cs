using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMover : MonoBehaviour
{
    [SerializeField] float monsterSpeed;
    [SerializeField] float monsterRotateSpeed;

    [SerializeField] Rigidbody rigid;


    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
    }

    private void MonsterMove()
    {
        rigid.velocity = new Vector3(-1, 0, -1);
    }
}
