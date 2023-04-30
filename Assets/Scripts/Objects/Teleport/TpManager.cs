using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum tpPosition
{
    start,
    medium,
    final
}

public class TpManager : MonoBehaviour
{
    [SerializeField] private Teleport start;
    [SerializeField] private Teleport medium;
    [SerializeField] private Teleport final;
    public PlayerMovement player;
    private Animator anim;
    private bool allOperative;
    public FloatValue howManyActives;

    private void Start()
    {
        anim = GetComponent<Animator>();
        if (howManyActives.currentValue == 0)
        {
            start.gameObject.SetActive(false);
            medium.gameObject.SetActive(false);
            final.gameObject.SetActive(false);
        }
        else if (howManyActives.currentValue == 1)
        {
            start.gameObject.SetActive(true);
            medium.gameObject.SetActive(true);
            final.gameObject.SetActive(false);
        }
        else
        {
            start.gameObject.SetActive(true);
            medium.gameObject.SetActive(true);
            final.gameObject.SetActive(true);
        }
    }

    public void Teleport(tpPosition position)
    {
        switch (position)
        {
            case tpPosition.start:
                player.TpAction(medium.transform.position);
                StartCoroutine(ColliderCo(medium));
                break;
            case tpPosition.medium:
                if (allOperative)
                {
                    player.TpAction(final.transform.position);
                    StartCoroutine(ColliderCo(final));
                }
                else
                {
                    player.TpAction(start.transform.position);
                    StartCoroutine(ColliderCo(start));
                }
                break;
            case tpPosition.final:
                player.TpAction(start.transform.position);
                StartCoroutine(ColliderCo(start));
                break;
        }
    }

    private IEnumerator ColliderCo(Teleport tp)
    {
        tp.col.enabled = false;
        yield return new WaitForSeconds(2f);
        tp.col.enabled = true;
    }

    public void FirstActive()
    {
        start.gameObject.SetActive(true);
        medium.gameObject.SetActive(true);
        howManyActives.currentValue = 1;
    }

    public void LastActive()
    {
        final.gameObject.SetActive(true);
        allOperative = true;
        howManyActives.currentValue = 2;
    }
}
