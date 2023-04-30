using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vessel : Breakable
{
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public override void Smashed()
    {
        anim.SetBool("Smashed", true);
        MakeLoot();
        StartCoroutine(breakCo());
    }
}
