using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField]
    private GridLayoutGroup inventoryGrid;
    [SerializeField]
    private GameObject inventoryPanel;
    [SerializeField]
    private GameObject itemsPanel;
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

    public void RearrengeInventory() 
    {
        LayoutRebuilder.MarkLayoutForRebuild(inventoryGrid.GetComponent<RectTransform>());
    }

    public void AddItemInventory(ItemElement item) 
    {
        var newItem = Instantiate(itemReference,itemsPanel.transform);
        ItemUI itemUI = newItem.GetComponent<ItemUI>();
        itemUI.thumbnail.sprite = item.icon;
        itemUI.itemElement = item;
    }

    public void RemoveItemInventory(ItemUI item) 
    {
        Destroy(item.gameObject);
    }

    public void ToggleUI() 
    {
        inventoryPanel.SetActive(!inventoryPanel.activeInHierarchy);
    }
}
