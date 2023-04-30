using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : PickUp
{
    public Inventory playerInventory;
    public int coins;

    void Start()
    {
        pickUpSignal.Raise();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.isTrigger)
        {
            playerInventory.coins += coins;
            if (playerInventory.coins > 9999)
            {
                playerInventory.coins = 9999;
            }
            pickUpSignal.Raise();
            Destroy(this.gameObject);
        }
    }
}
