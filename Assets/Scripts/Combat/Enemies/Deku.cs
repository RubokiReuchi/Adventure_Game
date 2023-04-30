using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deku : Enemies 
{
    private Rigidbody2D rb;
    [Header("Deku")]
    public Transform target;
    public float detectionRadius;
    public float attackRadius;
    public Animator anim;
    public Transform homePos;
    public float homeRadius;

    // Start is called before the first frame update
    void Start()
    {
        currentState = EnemyState.idle;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckDistance();
    }

    void CheckDistance()
    {
        if (!freeze)
        {
            if (Vector3.Distance(target.position, transform.position) <= detectionRadius
            && Vector3.Distance(target.position, transform.position) > attackRadius
            && boundary.bounds.Contains(target.transform.position)
            && agro.currentValue)
            {
                if (currentState == EnemyState.idle || currentState == EnemyState.walk)
                {

                    Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.fixedDeltaTime);
                    ChangeAnimation(temp - transform.position);
                    rb.MovePosition(temp);
                    currentState = EnemyState.walk;
                    anim.SetBool("WakeUp", true);
                }
            }
            else if (Vector3.Distance(target.position, transform.position) > detectionRadius
                || !boundary.bounds.Contains(target.transform.position)
                || !agro.currentValue)
            {
                if (Vector3.Distance(homePos.position, transform.position) > homeRadius)
                {
                    Vector3 temp = Vector3.MoveTowards(transform.position, homePos.position, moveSpeed * Time.fixedDeltaTime);
                    ChangeAnimation(temp - transform.position);
                    rb.MovePosition(temp);
                }
                else
                {
                    currentState = EnemyState.idle;
                    anim.SetBool("WakeUp", false);
                }
            }
        }
    }

    private void SetAnimationFloat(Vector2 setVector)
    {
        anim.SetFloat("MoveX", setVector.x);
        anim.SetFloat("MoveY", setVector.y);
    }

    private void ChangeAnimation(Vector2 direction)
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
