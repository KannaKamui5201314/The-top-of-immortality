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

    void Start()
    {
        floor = Resources.Load<GameObject>("Prefabs/prop/floor");
        floors = GameObject.FindGameObjectWithTag("floors");
        floorObjectPool = new ObjectPool(floor, Global.InitialFloorCount);
    }

    // Update is called once per frame
    void Update()
    {
        SetFloorActived("floor", floors, floorObjectPool);
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
