using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> objectsToPool;
    [SerializeField]
    private int amountToPool;

    private Dictionary<GameObject,ObjectPool> objectPools;

    void Start()
    {
        objectPools = new Dictionary<GameObject, ObjectPool>();
        for (int i = 0; i < objectsToPool.Count; i++) 
        {
            var newPool = new ObjectPool();
            newPool.objectToPool = objectsToPool[i];
            newPool.amountToPool = 10;
            newPool.GeneratePool();
            objectPools.Add(objectsToPool[i], newPool);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
