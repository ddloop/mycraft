using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    [SerializeField]
    private bool initialized = false;
    public bool Initialized { get { return initialized; } }

    [SerializeField]
    private List<GameObject> objectsToPool;
    [SerializeField]
    private int amountToPool;

    public Dictionary<GameObject,ObjectPool> objectPools;

    void Start()
    {
        objectPools = new Dictionary<GameObject, ObjectPool>();
        for (int i = 0; i < objectsToPool.Count; i++) 
        {
            var newPool = gameObject.AddComponent<ObjectPool>();
            newPool.objectToPool = objectsToPool[i];
            newPool.amountToPool = amountToPool;
            newPool.GeneratePool();
            objectPools.Add(objectsToPool[i], newPool);
        }

        initialized = true;
    }
}
