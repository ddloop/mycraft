using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField]
    private ObjectPoolManager poolManager;

    [SerializeField]
    private List<GameObject> initItems;

    void Start()
    {
        StartCoroutine(SpawnInitialItems());
    }

    IEnumerator SpawnInitialItems() 
    {
        while (!poolManager.Initialized)
            yield return 0;

        foreach (var pool in poolManager.objectPools)
        {
            if (!initItems.Contains(pool.Key))
                continue;

            for (int i = 0; i < 4; i++)
            {
                GameObject pooledObject = pool.Value.GetPooledObject();
                pooledObject.transform.position = transform.position;
                pooledObject.GetComponent<Item>().Enable(true);
                yield return 0;
            }
        }
    }
}
