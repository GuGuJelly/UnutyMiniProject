using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FireMonster : MonoBehaviour
{
    [SerializeField] PlayerStatus playerStatus;

    [SerializeField] GameObject monsterBulletPrefab;
    [SerializeField] GameObject playerObject;
    [SerializeField] Transform monsterMuzzlePoint;
    [SerializeField] int monsterBulletQuantity;
    [SerializeField] float fireRange;
    [SerializeField] float repeatTime;
    [SerializeField] float reLoadTime;
    
    private Coroutine fireMonsterRoutine;

    private void Awake()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        
    }

    private void Start()
    {
        
    }
    private void Update()
    {
        if (playerStatus.playerHp > 0)
        {
            if (fireMonsterRoutine == null)
            {
                fireMonsterRoutine = StartCoroutine(FireRoutine());
            }
        }
        if (playerStatus.playerHp <= 0)
        {
            if (fireMonsterRoutine != null)
            {
                StopCoroutine(fireMonsterRoutine);
                fireMonsterRoutine = null;
            }
        }
    }

    //private void StopMonsterFire()
    //{
    //    if (playerStatus.playerHp <= 0)
    //    {
    //        StopCoroutine(fireMonsterRoutine);
    //    }
    //    
    //}

    private IEnumerator FireRoutine()
    {
        while (true)
        {
            yield return null;
            Instantiate(monsterBulletPrefab, monsterMuzzlePoint.position, monsterMuzzlePoint.rotation);
            yield return null;
            monsterBulletQuantity++;
            yield return new WaitForSeconds(repeatTime);
            
            if(monsterBulletQuantity > 7)
            {
                yield return null;
                monsterBulletQuantity = 0;
                yield return new WaitForSeconds(reLoadTime);
            }
            
        }
    }
}
 

    




