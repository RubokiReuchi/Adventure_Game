using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rat : Enemies
{
    private Rigidbody2D rb;
    [Header("Rat")]
    public Transform target;
    public float detectionRadius;
    public float attackRadius;
    public Animator anim;

    public Transform[] path;
    public int currentPoint;
    public Transform currentGoal;
    public float roundingDistance;
    [Header("Boss")]
    public bool isBoss;
    public BoolValue alreadyDead;

    // Start is called before the first frame update
    void Start()
    {
        currentState = EnemyState.idle;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform;

        if (isBoss)
        {
            if (alreadyDead.currentValue)
            {
                gameObject.SetActive(false);
            }
        }
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
                    ChageState(EnemyState.walk);
                }
            }
            else if (Vector3.Distance(target.position, transform.position) > detectionRadius
                || !boundary.bounds.Contains(target.transform.position)
                || !agro.currentValue)
            {
                if (Vector3.Distance(transform.position, path[currentPoint].position) > roundingDistance)
                {
                    Vector3 temp = Vector3.MoveTowards(transform.position, path[currentPoint].position, moveSpeed * Time.fixedDeltaTime);
                    ChangeAnimation(temp - transform.position);
                    rb.MovePosition(temp);
                }
                else
                {
                    ChangeGoal();
                }
            }
        }
    }

    private void ChangeGoal()
    {
        if (currentPoint == path.Length - 1)
        {
            currentPoint = 0;
            currentGoal = path[0];
        }
        else
        {
            currentPoint++;
            currentGoal = path[currentPoint];
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

    private void ChageState(EnemyState newState)
    {
        if (currentState != newState)
        {
            currentState = newState;
        }
    }
}
