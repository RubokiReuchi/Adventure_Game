using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Values/FloatValue")]
[System.Serializable]
public class FloatValue : ScriptableObject
{
    public float initialValue;
    public float currentValue;

    public void Reset()
    {
        currentValue = initialValue;
    }
}
