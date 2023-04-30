using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthRection : MonoBehaviour
{
    public FloatValue health;
    public FloatValue heartContainers;
    public Signal healthSignal;

    public void Use(int amountToRestore)
    {
        health.currentValue += amountToRestore;
        if (health.currentValue > heartContainers.currentValue * 2)
        {
            health.currentValue = heartContainers.currentValue * 2;
        }
        healthSignal.Raise();
    }
}
