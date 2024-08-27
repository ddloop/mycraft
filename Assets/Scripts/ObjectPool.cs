using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPool : MonoBehaviour
{
    private List<GameObject> pooledObjects;
    public GameObject objectToPool;
    public int amountToPool;

    public void GeneratePool() 
    {
        pooledObjects = new List<GameObject>();
        GameObject tmp;

        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(objectToPool);
            Item item = tmp.GetComponent<Item>();
            item.Pool = this;
            item.Enable(false);
            tmp.transform.position = Vector3.one * 100;
            pooledObjects.Add(tmp);
        }
    }

    public GameObject GetPooledObject()
    {
        if (pooledObjects.Count > 0)
        {
            var returnObject = pooledObjects.First();
            pooledObjects.Remove(returnObject);
            return returnObject;
        }

        return null;
    }

    public void ReturnToPool(GameObject returnObject)
    {
        pooledObjects.Add(returnObject);
        returnObject.transform.position = Vector3.one * 100;
    }
}
