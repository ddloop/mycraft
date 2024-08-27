using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private ObjectPool pool;
    public ObjectPool Pool { get { return pool; } set { pool = value; } }

    public void Enable(bool enable) 
    {
        GetComponent<Rigidbody>().isKinematic = !enable;

        foreach (var renderer in GetComponentsInChildren<MeshRenderer>())
            renderer.enabled = enable;
    }

    public void Return() 
    {
        pool.ReturnToPool(gameObject);
    }
}
