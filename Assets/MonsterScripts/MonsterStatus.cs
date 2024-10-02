using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStatus : MonoBehaviour
{
    [SerializeField] public int monsterHP;
    [SerializeField] public int monsterCurHP;

    private void Awake()
    {
        monsterHP = 3;
        monsterCurHP = monsterHP;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            MonsterTakedDamage();
            MonsterDead();
        }
    }
    
    private void MonsterTakedDamage()
    {
        monsterCurHP--;
    }

    private void MonsterDead()
    {
        if (monsterCurHP <= 0)
        {
            Destroy(gameObject);
        }
    }
}
