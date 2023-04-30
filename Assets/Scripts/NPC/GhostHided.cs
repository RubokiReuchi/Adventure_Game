using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostHided : MonoBehaviour
{
    public GhostHelp ghost;

    public void SetEnable()
    {
        ghost.gameObject.SetActive(true);
    }
}
