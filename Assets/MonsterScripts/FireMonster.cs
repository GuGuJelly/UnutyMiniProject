using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FireMonster : MonoBehaviour
{
    [SerializeField] GameObject monsterBulletPrefab;
    [SerializeField] GameObject playerObject;
    [SerializeField] Transform monsterMuzzlePoint;
    [SerializeField] int monsterBulletQuantity;
    [SerializeField] float fireRange;
    [SerializeField] float repeatTime;
    [SerializeField] float reLoadTime;
    
    private Coroutine fireMonsterRoutine;

    private void Start()
    {
        StartCoroutine(FireRoutine());
    }

    private void FixedUpdate()
    {
        
    }

    private IEnumerator FireRoutine()
    {
        while (true)
        {
            //yield return null;
            //Physics.Raycast(monsterMuzzlePoint.position, monsterMuzzlePoint.forward, out RaycastHit hitInfo, fireRange);
            //yield return null;
            //if (hitInfo.collider.tag == "Player" && hitInfo.collider != null)
            
                //yield return null;
                //for (int i = 0; i < 7; i++ )
                //{
                    //yield return null;
                    Instantiate(monsterBulletPrefab, monsterMuzzlePoint.position, monsterMuzzlePoint.rotation);
                    yield return null;
                    monsterBulletQuantity++;
                    yield return new WaitForSeconds(repeatTime);
                //}
            if(monsterBulletQuantity > 7)
            {
                monsterBulletQuantity = 0;
                yield return new WaitForSeconds(reLoadTime);
            }
                
        }
    }
}
 

    




