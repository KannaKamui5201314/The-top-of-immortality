using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    private GameObject prefab;
    private List<GameObject> pool;

    //��ʼ����
    public ObjectPool(GameObject prefab, int initialCount)
    {
        this.prefab = prefab;
        pool = new List<GameObject>();
        for (int i = 0; i < initialCount; i++)
        {
            GameObject obj = GameObject.Instantiate(prefab);
            obj.SetActive(false);
            pool.Add(obj);
        }
    }

    //�ͷ�
    public GameObject GetObject()
    {
        if (pool.Count > 0)
        {
            GameObject obj = pool[pool.Count - 1];
            pool.RemoveAt(pool.Count - 1);
            obj.SetActive(true);
            return obj;
        }
        else
        {
            return GameObject.Instantiate(prefab);
        }
    }

    //����
    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        pool.Add(obj);
    }

    public int GetCount()
    {
        return pool.Count;
    }

}
