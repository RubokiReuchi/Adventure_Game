using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartContainer : PickUp
{
    public Signal containerSignal;
    public FloatValue playerHealth;
    public FloatValue heartContainers;
    public BoolValue alreadyPick;

    void Start()
    {
        if (alreadyPick.currentValue)
        {
            this.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.isTrigger)
        {
            heartContainers.currentValue++;
            playerHealth.currentValue = heartContainers.currentValue * 2;
            containerSignal.Raise();
            pickUpSignal.Raise();
            alreadyPick.currentValue = true;
            Destroy(this.gameObject);
        }
    }
}
