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

    public void OpenUI() 
    {
        inventoryPanel.SetActive(true);
    }
}
