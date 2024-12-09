using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropController : MonoBehaviour
{
    GameObject floor;
    GameObject floors;
    public ObjectPool floorObjectPool;

    private int floorNumber = 0;
    private int floorHeight = -5;

    GameObject Enemy;
    GameObject Enemies;
    public ObjectPool EnemyObjectPool;

    void Start()
    {
        floor = Resources.Load<GameObject>("Prefabs/prop/floor");
        floors = GameObject.FindGameObjectWithTag("floors");
        floorObjectPool = new ObjectPool(floor, Global.InitialFloorCount);

        Enemy = Resources.Load<GameObject>("Prefabs/prop/Enemy");
        Enemies = GameObject.FindGameObjectWithTag("Enemies");
        EnemyObjectPool = new ObjectPool(Enemy, Global.InitialEnemyCount);
    }

    // Update is called once per frame
    void Update()
    {
        if (Global.IsGo)
        {
            SetFloorActived("floor", floors, floorObjectPool);
            SetActived("Enemy", Enemies, EnemyObjectPool);
        }
        
        
        
    }

    //
    void SetActived(string name, GameObject Prefabs, ObjectPool objectPool)
    {
        if (objectPool != null)
        {
            if (objectPool.GetCount() > 0)
            {
                GameObject newObject = objectPool.GetObject();
                newObject.name = name;
                newObject.transform.SetParent(Prefabs.transform);
                newObject.transform.position = new Vector3(Random.Range(-Screen.width/100f/2f + 0.4f, Screen.width / 100f / 2f - 0.4f), 
                                                           Random.Range(3f,498f), 0);
            }
        }
        else
        {
            Debug.Log(objectPool + "=null floor");
        }

    }

    //floor
    void SetFloorActived(string name, GameObject Prefabs, ObjectPool objectPool)
    {
        if (objectPool != null)
        {
            if (objectPool.GetCount() > 0)
            {
                floorNumber += 1;
                floorHeight += 5;
                GameObject newObject = objectPool.GetObject();
                newObject.name = name;
                newObject.GetComponent<Floor>().number = floorNumber;
                newObject.transform.SetParent(Prefabs.transform);
                newObject.transform.position = new Vector3(0, floorHeight,0);
            }
        }
        else
        {
            Debug.Log(objectPool + "=null floor");
        }

    }
}
