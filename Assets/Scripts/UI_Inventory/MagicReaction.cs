using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicReaction : MonoBehaviour
{
    public FloatValue magic;
    public Signal magicSignal;

    public void Use(int amountToRestore)
    {
        magic.currentValue += amountToRestore;
        if (magic.currentValue > magic.initialValue)
        {
            magic.currentValue = magic.initialValue;
        }
        magicSignal.Raise();
    }
}
