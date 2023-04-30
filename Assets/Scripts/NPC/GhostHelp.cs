using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostHelp : Sign
{
    [Header("LayerOrder")]
    [SerializeField] private Transform player;
    private SpriteRenderer render;
    private Transform myTransform;
    [SerializeField] private BoolValue spawn;
    [SerializeField] private bool startEnabled;

    void Start()
    {
        render = GetComponent<SpriteRenderer>();
        myTransform = GetComponent<Transform>();

        if (spawn.currentValue || !startEnabled)
        {
            gameObject.SetActive(false);
        }
    }

    public override void Update()
    {
        SetRenderer();
        base.Update();
    }

    public void SetEnable()
    {
        gameObject.SetActive(true);
    }

    private void SetRenderer()
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
