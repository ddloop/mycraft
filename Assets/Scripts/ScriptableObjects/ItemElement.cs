using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ItemElement", order = 1)]
public class ItemElement : ScriptableObject 
{
    public GameObject GameObject;
    public string itemName;
    public Sprite icon;
}
