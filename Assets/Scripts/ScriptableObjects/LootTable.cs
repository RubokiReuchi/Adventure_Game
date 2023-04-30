using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Loot
{
    public PickUp thisLoot;
    public int lootChange;
}

[CreateAssetMenu]
public class LootTable : ScriptableObject
{
    public Loot[] loots;

    public PickUp LootPickUp()
    {
        int acumProb = 0;
        int currentProb = Random.Range(0, 100);

        for (int i = 0; i < loots.Length; i++)
        {
            acumProb += loots[i].lootChange;
            if (currentProb <= acumProb)
            {
                return loots[i].thisLoot;
            }
        }
        return null;
    }
}
