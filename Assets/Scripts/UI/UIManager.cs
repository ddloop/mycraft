using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField]
    private GameObject inventoryPanel;
    [SerializeField]
    private GameObject itemsPanel;
    [SerializeField]
    private GameObject craftingPanel;
    [SerializeField]
    private GameObject itemReference;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;
    }

    public void AddItemInventory(ItemElement item)
    {
        var newItem = Instantiate(itemReference, itemsPanel.transform);
        ItemUI itemUI = newItem.GetComponent<ItemUI>();
        itemUI.thumbnail.sprite = item.icon;
        itemUI.itemElement = item;
    }

    public void AddItemCrafting(ItemElement item)
    {
        var newItem = Instantiate(itemReference, craftingPanel.transform);
        ItemUI itemUI = newItem.GetComponent<ItemUI>();
        itemUI.thumbnail.sprite = item.icon;
        itemUI.itemElement = item;
    }

    public void RemoveItemInventory(ItemUI item)
    {
        Destroy(item.gameObject);
    }

    public void RemoveItemInventory(List<ItemElement> listItemElements) 
    {
        foreach (ItemElement _itemElement in listItemElements)
        {
            foreach (Transform item in itemsPanel.transform)
            {
                var itemUI = item.GetComponent<ItemUI>();
                if (itemUI.itemElement == _itemElement)
                {
                    DestroyImmediate(itemUI.gameObject);
                    break;
                }
            }
        }
    }


    public void ClearCraftingBuffer() 
    {
        foreach (Transform child in craftingPanel.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public List<GameObject> GetAllCraftingElements() 
    {
        var list = new List<GameObject>();
        foreach (Transform child in craftingPanel.transform)
        {
            list.Add(child.gameObject);
        }

        return list;
    }

    public void RemoveFromInventoryGrid(ItemElement itemElement) 
    {
        foreach (Transform item in inventoryPanel.transform) 
        {
            var itemUI = item.GetComponent<ItemUI>();
            if (itemUI.itemElement == itemElement)
                Destroy(itemUI.gameObject);
        }
    }

    public void ToggleUI() 
    {
        inventoryPanel.SetActive(!inventoryPanel.activeInHierarchy);
    }
}
