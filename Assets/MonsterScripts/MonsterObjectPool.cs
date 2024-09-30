using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterObjectPool : MonoBehaviour
{
    [SerializeField] List<MonsterPooledObject> monsterPool = new List<MonsterPooledObject>();
    [SerializeField] MonsterPooledObject prefab;
    [SerializeField] int size;

    private void Awake()
    {
        for (int i = 0; i < size; i++)
        {
            MonsterPooledObject instance = Instantiate(prefab);
            instance.gameObject.SetActive(false);
            instance.transform.parent = transform;
            //instance.monsterReturnPool = this;
            monsterPool.Add(instance);
        }


    }

    public MonsterPooledObject GetMonsterPool(Vector3 position, Quaternion rotation)
    {
        if (monsterPool.Count > 0)
        {
            MonsterPooledObject instance = monsterPool[monsterPool.Count - 1];
            instance.transform.position = position;
            instance.transform.rotation = rotation;
            instance.transform.parent = null;
            instance.gameObject.SetActive(true);
            //instance.monsterReturnPool = this;
            monsterPool.RemoveAt(monsterPool.Count - 1);

            return instance;
        }
        else
        {
            MonsterPooledObject instance = null;
            // instance.returnPool = this;
            return instance;
        }
    }

    public void MonsterReturnPool(MonsterPooledObject instance)
    {
        instance.gameObject.SetActive(false);
        instance.transform.parent = transform;
        monsterPool.Add(instance);
    }
}
