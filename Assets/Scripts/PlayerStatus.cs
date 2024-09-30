using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField] public int playerHp;
    

    private void Awake()
    {
        playerHp = 10;
    }

    private void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MonsterBullet"))
        {
            PlayerTakenDamage();
            PlayerDead();

        }
        
    }

    private void PlayerTakenDamage()
    {
        playerHp--;
    }

    private void PlayerDead()
    {
        if (playerHp <= 0)
        {
            Destroy(gameObject);
        }
        
    }
}
