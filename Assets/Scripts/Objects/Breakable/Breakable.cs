using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    [Header("Loot")]
    public LootTable thisLoot;

    public virtual void Smashed()
    {
        MakeLoot();
        StartCoroutine(breakCo());
    }

    public IEnumerator breakCo()
    {
        yield return new WaitForSeconds(0.3f);
        this.gameObject.SetActive(false);
    }

    public void MakeLoot()
    {
        if (thisLoot != null)
        {
            PickUp current = thisLoot.LootPickUp();
            if (current != null)
            {
                Instantiate(current.gameObject, transform.position, Quaternion.identity);
            }
        }
    }
}
