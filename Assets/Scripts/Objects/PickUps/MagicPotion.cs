using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicPotion : PickUp
{
    public FloatValue magic;
    public float amountToRegen;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.isTrigger)
        {
            magic.currentValue += amountToRegen;
            pickUpSignal.Raise();
            Destroy(this.gameObject);
        }
    }
}
