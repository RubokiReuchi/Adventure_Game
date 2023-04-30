using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minotaur : MeleeEnemy
{
    [Header("Attacks")]
    private int attack;
    [Header("Sprite")]
    private SpriteRenderer render;
    private Transform myTransform;

    void Start()
    {
        currentState = EnemyState.idle;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform;
        render = GetComponent<SpriteRenderer>();
        myTransform = GetComponent<Transform>();
    }

    void Update()
    {
        SetRenderer();

        if (health <= maxHealth/2 && !anim.GetBool("Rage"))
        {
            StartCoroutine(RageCo());
        }

        if (!anim.GetBool("Rage"))
        {
            attack = Random.Range(1, 3);
        }
        else
        {
            attack = Random.Range(1, 4);
        }
    }

    public override IEnumerator AttackCo()
    {
        currentState = EnemyState.attack;
        anim.SetInteger("Attack", attack);
        switch (anim.GetInteger("Attack"))
        {
            case 1:
                yield return new WaitForSeconds(0.9f);
                break;
            case 2:
                yield return new WaitForSeconds(0.7f);
                break;
            case 3:
                yield return new WaitForSeconds(1.5f);
                break;
        }
        anim.SetBool("Active", false);
        freeze = true;
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("Active", true);
        anim.SetInteger("Attack", 0);
        freeze = false;
        currentState = EnemyState.walk;
    }

    private IEnumerator RageCo()
    {
        anim.SetBool("Rage", true);
        freeze = true;
        yield return new WaitForSeconds(1f);
        freeze = false;
    }

    public override void SetAnimationFloat(Vector2 setVector)
    {
        anim.SetFloat("MoveX", setVector.x);
    }

    public override void ChangeAnimation(Vector2 direction)
    {
        if (direction.x > 0)
        {
            SetAnimationFloat(Vector2.right);
        }
        else if (direction.x < 0)
        {
            SetAnimationFloat(Vector2.left);
        }
    }

    private void SetRenderer()
    {
        if (myTransform.position.y < target.position.y)
        {
            render.sortingOrder = 1;
        }
        else
        {
            render.sortingOrder = -1;
        }
    }

    public override IEnumerator DeadCo()
    {
        yield return new WaitForSeconds(3f);
        this.gameObject.SetActive(false);
    }
}
