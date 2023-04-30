using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemies
{
    public Rigidbody2D rb;
    [Header("Melee Enemy")]
    public Transform target;
    public float detectionRadius;
    public float attackRadius;
    public Animator anim;
    [Header("Boss")]
    public bool isBoss;
    public BoolValue alreadyDead;

    void Start()
    {
        if (isBoss)
        {
            if (alreadyDead.currentValue)
            {
                gameObject.SetActive(false);
            }
        }
    }

    void FixedUpdate()
    {
        if (!freeze)
        {
            CheckDistance();
        }
    }

    void CheckDistance()
    {
        if (!isBoss)
        {
            if (Vector3.Distance(target.position, transform.position) <= detectionRadius
            && boundary.bounds.Contains(target.transform.position)
            && agro.currentValue)
            {
                if (Vector3.Distance(target.position, transform.position) > attackRadius)
                {
                    if (currentState == EnemyState.idle || currentState == EnemyState.walk)
                    {
                        Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.fixedDeltaTime);
                        ChangeAnimation(temp - transform.position);
                        rb.MovePosition(temp);
                        currentState = EnemyState.walk;
                        anim.SetBool("Active", true);
                    }
                }
                else if (Vector3.Distance(target.position, transform.position) < attackRadius)
                {
                    if (currentState == EnemyState.walk)
                    {
                        StartCoroutine(AttackCo());
                    }
                }
            }
            else if (Vector3.Distance(target.position, transform.position) > detectionRadius
                || !boundary.bounds.Contains(target.transform.position)
                || !agro.currentValue)
            {
                currentState = EnemyState.idle;
                anim.SetBool("Active", false);
            }
        }
        else
        {
            if (boundary.bounds.Contains(target.transform.position))
            {
                if (Vector3.Distance(target.position, transform.position) > attackRadius)
                {
                    if (currentState == EnemyState.idle || currentState == EnemyState.walk)
                    {
                        Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.fixedDeltaTime);
                        ChangeAnimation(temp - transform.position);
                        rb.MovePosition(temp);
                        currentState = EnemyState.walk;
                        anim.SetBool("Active", true);
                    }
                }
                else if (Vector3.Distance(target.position, transform.position) < attackRadius)
                {
                    if (currentState == EnemyState.walk)
                    {
                        StartCoroutine(AttackCo());
                    }
                }
            }
        }
    }

    public virtual IEnumerator AttackCo()
    {
        yield return null;
    }

    public virtual void SetAnimationFloat(Vector2 setVector)
    {
        anim.SetFloat("MoveX", setVector.x);
        anim.SetFloat("MoveY", setVector.y);
    }

    public virtual void ChangeAnimation(Vector2 direction)
    {
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
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
        else if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
        {
            if (direction.y > 0)
            {
                SetAnimationFloat(Vector2.up);
            }
            else if (direction.y < 0)
            {
                SetAnimationFloat(Vector2.down);
            }
        }
    }
}
