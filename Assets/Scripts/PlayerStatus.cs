using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField] public int playerHp;
    

    private void Awake()
    {
        playerHp = 3;
    }

    private void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MonsterBullet"))
        {
            PlayerTakenDamage();
            if (playerHp <= 0)
            {
                PlayerDead();
            }

        }
        
    }

    private void PlayerTakenDamage()
    {
        playerHp--;
    }

    private void PlayerDead()
    {
        Destroy(gameObject);
    }
}
