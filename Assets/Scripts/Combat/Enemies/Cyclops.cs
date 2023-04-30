using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cyclops : MeleeEnemy
{

    void Start()
    {
        currentState = EnemyState.idle;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform;
    }

    public override IEnumerator AttackCo()
    {
        currentState = EnemyState.attack;
        anim.SetBool("Attacking", true);
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("Active", false);
        freeze = true;
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("Active", true);
        anim.SetBool("Attacking", false);
        freeze = false;
        currentState = EnemyState.walk;
    }
}
