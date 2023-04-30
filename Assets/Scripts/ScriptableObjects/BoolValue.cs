using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Values/BoolValue")]
[System.Serializable]
public class BoolValue : ScriptableObject
{
    public bool initialValue;
    public bool currentValue;

    public void Reset()
    {
        currentValue = initialValue;
    }
}
