using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : PickUp
{
    public FloatValue playerHealth;
    public FloatValue heartContainers;
    public float amountToRegen;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.isTrigger)
        {
            playerHealth.currentValue += amountToRegen;
            if (playerHealth.currentValue > heartContainers.currentValue * 2)
            {
                playerHealth.currentValue = heartContainers.currentValue * 2;
            }
            pickUpSignal.Raise();
            Destroy(this.gameObject);
        }
    }
}
