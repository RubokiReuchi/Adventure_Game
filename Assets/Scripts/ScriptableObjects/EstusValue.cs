using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Values/EstusValue")]
public class EstusValue : ScriptableObject
{
    public int initialValue;
    public int currentMaxValue;
    public int currentValue;

    public void Reset()
    {
        currentValue = initialValue;
        currentMaxValue = initialValue;
    }
}
