using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiabloHouse : MonoBehaviour
{
    [Header("LayerOrder")]
    public Transform player;
    private SpriteRenderer render;
    private Transform myTransform;

    void Start()
    {
        render = GetComponent<SpriteRenderer>();
        myTransform = GetComponent<Transform>();
    }

    void Update()
    {
        if (myTransform.position.y < player.position.y)
        {
            render.sortingOrder = 1;
        }
        else
        {
            render.sortingOrder = -1;
        }
    }
}
