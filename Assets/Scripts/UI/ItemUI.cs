using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour, IPointerClickHandler
{
    public Image thumbnail;
    public ItemElement itemElement;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (GameManager.Instance.inventoryManager.AddItemCraftingBuffer(itemElement))
                UIManager.Instance.AddItemCrafting(itemElement);
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            GameManager.Instance.inventoryManager.RemoveItem(this);
        }
    }
}
