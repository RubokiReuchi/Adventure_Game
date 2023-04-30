using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum itemFind
{
    smallKey,
    masterKey,
    ironSword,
    fireSword,
    iceSword,
    explosion,
    fireSpell,
    iceSpell,
    estus,
    cinders,
    nothing
}

[CreateAssetMenu]
[System.Serializable]
public class Item : ScriptableObject
{
    public Sprite itemSprite;
    public string itemDescription;
    public itemFind item;
}
