using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundedNPC : Sign
{
    [Header("LayerOrder")]
    public Transform player;
    private SpriteRenderer render;
    [Header("Movement")]
    private Vector3 directionVector;
    private Transform myTransform;
    public float speed;
    private Rigidbody2D rb;
    private Animator anim;
    public Collider2D bounds;
    private float timeToMove = 0f;
    private float moveTime;

    void Start()
    {
        render = GetComponent<SpriteRenderer>();
        moveTime = Random.Range(0.5f, 1f);
        anim = GetComponent<Animator>();
        myTransform = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        ChangeDirection();
    }

    public override void Update()
    {
        SetRenderer();
        base.Update();

        if (!anim.GetBool("PlayerInRange"))
        {
            moveTime -= Time.deltaTime;
            if (moveTime <= 0)
            {
                ChangeDirection();
                moveTime = Random.Range(0.5f, 1f); ;
            }
        }

        if (!playerInRange)
        {
            if (timeToMove <= 0)
            {
                anim.SetBool("PlayerInRange", false);
                Move();
            }
            else
            {
                timeToMove -= Time.deltaTime;
            }
        }
        else
        {
            timeToMove = 0.5f;
            anim.SetBool("PlayerInRange", true);
        }
    }

    private void Move()
    {
        Vector3 temp = myTransform.position + directionVector * speed * Time.fixedDeltaTime;
        if (bounds.bounds.Contains(temp))
        {
            rb.MovePosition(temp);
        }
        else
        {
            ChangeDirection();
        }
    }

    private void ChangeDirection()
    {
        int direction = Random.Range(0, 4);
        moveTime = Random.Range(0.5f, 1f); ;

        switch (direction)
        {
            case 0:
                directionVector = Vector3.down;
                break;
            case 1:
                directionVector = Vector3.left;
                break;
            case 2:
                directionVector = Vector3.right;
                break;
            case 3:
                directionVector = Vector3.up;
                break;
            default:
                break;
        }

        anim.SetFloat("MoveX", directionVector.x);
        anim.SetFloat("MoveY", directionVector.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector3 temp = directionVector;
        ChangeDirection();
        int loops = 0;
        while (temp == directionVector && loops < 100)
        {
            ChangeDirection();
            loops++;
        }
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
