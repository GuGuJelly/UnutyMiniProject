using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPooledObject : MonoBehaviour
{
    public ObjectPool monsterReturnPool;
    [SerializeField] float monsterReturnTime = 3;

    private float curTime;

    private void OnEnable()
    {
        curTime = monsterReturnTime;
    }
    private void Update()
    {
        //curTime -= Time.deltaTime;
        //if (curTime < 0)
        //{
        //    monsterReturnPool.ReturnPool(this);
        //}
    }


}
