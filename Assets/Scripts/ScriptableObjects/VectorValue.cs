using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Values/VectorValue")]
[System.Serializable]
public class VectorValue : ScriptableObject
{
    public Vector2 initialValue;
    public Vector2 currentValue;

    public void Reset()
    {
        currentValue = initialValue;
    }
}
