using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class MonsterMover : MonoBehaviour
{
    [SerializeField] float monsterSpeed;
    [SerializeField] float monsterRotateSpeed;
    
    [SerializeField] Transform player;


    private void Update()
    {
        // MonsterMove();
    }


    private void MonsterMove()
    {
        transform.position += new Vector3(0.1f,0,0)*Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            transform.LookAt(player);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, monsterSpeed * Time.deltaTime);
        }
    }
}
