using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Inventory/Item")]
public class InventoryItem : ScriptableObject
{
    public string itemName;
    public string itemDescription;
    public Sprite noItemSprite;
    public Sprite itemSprite;
    public bool usable;
    public bool unique;
    public UnityEvent thisEvent;
    [Header("Item values")]
    public BoolValue itemObtained;
    public EstusValue currentUses;

    public void Use()
    {
        thisEvent.Invoke();
    }
}
