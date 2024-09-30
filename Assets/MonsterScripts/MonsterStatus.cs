using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStatus : MonoBehaviour
{
    [SerializeField] public int monsterHP;

    private void Awake()
    {
        monsterHP = 3;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            MonsterTakedDamage();
            MonsterDead();
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("PlayerBullet"))
    //    {
    //        MonsterTakedDamage();
    //        MonsterDead();
    //    }
    //}
    
    private void MonsterTakedDamage()
    {
        monsterHP--;
    }

    private void MonsterDead()
    {
        if (monsterHP <= 0)
        {
            Destroy(gameObject);
        }
    }
}
