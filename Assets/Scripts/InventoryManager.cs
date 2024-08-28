using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField]
    ObjectPoolManager poolManager;

    private List<ItemElement> itemElements = new List<ItemElement>();

    [SerializeField]
    private List<ItemElement> referenceList;

    [SerializeField]
    private List<Recipe> recipeList;

    public void AddItem(ItemElement itemElementRef) 
    {
        foreach (var item in referenceList)
        {
            if (item == itemElementRef)
            {
                UIManager.Instance.AddItemInventory(item);
                itemElements.Add(item);
                break;
            }
        }
    }

    public void RemoveItem(ItemUI itemUIRef) 
    {
        foreach (var item in itemElements)
        {
            if (item == itemUIRef.itemElement)
            {
                FindAndSpawnObject(item);
                UIManager.Instance.RemoveItemInventory(itemUIRef);
                itemElements.Remove(item);                
                break;
            }
        }
    }

    public void FindAndSpawnObject(ItemElement item)
    {
        foreach (var pool in poolManager.objectPools)
        {
            if (pool.Key == item.GameObject) 
            {
                GameObject pooledObject = pool.Value.GetPooledObject();
                pooledObject.transform.position = GameManager.Instance.playerTransform.position + GameManager.Instance.playerTransform.forward * 0.3f;
                pooledObject.GetComponent<Item>().Enable(true);
                break;
            }
        } 
    }
}

[System.Serializable]
struct Recipe 
{
    public ItemElement product;
    public List<ItemElement> ingredients;
    public float chanceOfSuccess;
}