using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    [SerializeField]
    private List<ItemElement> craftingBuffer = new List<ItemElement>();

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

    public void RemoveItem(ItemUI itemUIRef, bool destroy = false) 
    {
        foreach (var item in itemElements)
        {
            if (item == itemUIRef.itemElement)
            {
                if(!destroy) FindAndSpawnObject(item);
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

    public bool AddItemCraftingBuffer(ItemElement item) 
    {
        if (craftingBuffer.Count < 2)
        {
            craftingBuffer.Add(item);
            return true;
        }

        return false;
    }

    public void ClearCraftingBuffer() 
    {
        craftingBuffer.Clear();
        UIManager.Instance.ClearCraftingBuffer();
    }

    public void CraftItem() 
    {
        if (craftingBuffer.Count != 2)
            return;

        Recipe? foundRecipe = FindRecipe();

        if (foundRecipe == null)
            return;

        Recipe recipe = foundRecipe.Value;

        //Clear Inventory
        UIManager.Instance.RemoveItemInventory(recipe.ingredients);

        if (IsSuccess(recipe.chanceOfSuccess))
        {
            AddItem(recipe.product);
        }

        ClearCraftingBuffer();
    }

    private Recipe? FindRecipe() 
    {
        foreach (Recipe recipe in recipeList)
        {
            var checkOne = craftingBuffer.Except(recipe.ingredients);
            var checkTwo = recipe.ingredients.Except(craftingBuffer);

            if (!checkOne.Any() && !checkTwo.Any())
                return recipe;
        }

        return null;
    }

    bool IsSuccess(float chanceOfSuccess)
    {
        float randomValue = Random.value;
        return randomValue < chanceOfSuccess;
    }
}

[System.Serializable]
struct Recipe 
{
    public ItemElement product;
    public List<ItemElement> ingredients;
    public float chanceOfSuccess;
}