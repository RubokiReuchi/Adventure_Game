using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Enemies
{
    private Rigidbody2D rb;
    [Header("Turret")]
    public Transform target;
    public float detectionRadius;
    public GameObject projectile;
    public float fireDelay;
    private float remainFireDelay;
    public bool canFire = true;
    [Header("Boss")]
    public bool isBoss;
    public BoolValue alreadyDead;

    // Start is called before the first frame update
    void Start()
    {
        currentState = EnemyState.idle;
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player").transform;

        if (isBoss)
        {
            if (alreadyDead.currentValue)
            {
                gameObject.SetActive(false);
            }
        }
    }

    void Update()
    {
        remainFireDelay -= Time.deltaTime;
        if (remainFireDelay <= 0)
        {
            canFire = true;
            remainFireDelay = fireDelay;
        }
    }

    void FixedUpdate()
    {
        CheckDistance();
    }

    void CheckDistance()
    {
        if (!freeze)
        {
            if (Vector3.Distance(target.position, transform.position) <= detectionRadius
            && boundary.bounds.Contains(target.transform.position)
            && agro.currentValue)
            {
                if (canFire)
                {
                    currentState = EnemyState.attack;
                    Shoot();
                }
            }
            else if (Vector3.Distance(target.position, transform.position) > detectionRadius
                || !boundary.bounds.Contains(target.transform.position)
                || !agro.currentValue)
            {
                currentState = EnemyState.idle;
            }
        }
    }

    void Shoot()
    {
        Vector3 temp = target.transform.position - transform.position;
        GameObject current = Instantiate(projectile, transform.position, Quaternion.identity);
        canFire = false;
        current.GetComponent<TurretBullet>().Lauch(temp);
    }
}
