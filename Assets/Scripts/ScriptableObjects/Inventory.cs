using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Inventory")]
[System.Serializable]
public class Inventory : ScriptableObject
{
    public Item currentItem;
    public List<Item> items = new List<Item>();
    [Header("Location")]
    public string scene;
    public Vector2 position;
    [Header("Count Items")]
    public int coins;
    public int numberOfKeys;
    [Header("Unique Items")]
    public bool masterKey;
    public bool ironSword;
    public bool burningSword;
    public bool freezingSword;
    public bool explosion;
    public bool fireSpell;
    public bool iceSpell;
    public bool estus;
    public bool cinders;

    public void AddItem(Item itemToAdd)
    {
        switch (itemToAdd.item)
        {
            case itemFind.smallKey:
                numberOfKeys++;
                break;
            case itemFind.masterKey:
                masterKey = true;
                break;
            case itemFind.ironSword:
                ironSword = true;
                break;
            case itemFind.fireSword:
                burningSword = true;
                break;
            case itemFind.iceSword:
                freezingSword = true;
                break;
            case itemFind.explosion:
                explosion = true;
                break;
            case itemFind.fireSpell:
                fireSpell = true;
                break;
            case itemFind.iceSpell:
                iceSpell = true;
                break;
            case itemFind.estus:
                estus = true;
                break;
            case itemFind.cinders:
                cinders = true;
                break;
            default:
                break;
        }
    }

    public void FillAll()
    {
        ironSword = true;
        burningSword = true;
        freezingSword = true;
        explosion = true;
        fireSpell = true;
        iceSpell = true;
        estus = true;
        cinders = true;
    }

    public void EmptyAll()
    {
        ironSword = false;
        burningSword = false;
        freezingSword = false;
        explosion = false;
        fireSpell = false;
        iceSpell = false;
        estus = false;
        cinders = false;
    }

    public void Reset()
    {
        scene = "OpeningCutscene";
        position = Vector2.zero;
        coins = 0;
        numberOfKeys = 0;
        masterKey = false;
        ironSword = false;
        burningSword = false;
        freezingSword = false;
        explosion = false;
        fireSpell = false;
        iceSpell = false;
        estus = false;
        cinders = false;
    }
}
