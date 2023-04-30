using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Movement")]
    public Rigidbody2D rb;
    public float speed;
    public Vector2 direction;
    [Header("LifeTime")]
    public float inicialLifeTime;
    private float currentLifeTime;

    // Start is called before the first frame update
    void Start()
    {
        currentLifeTime = inicialLifeTime;
    }

    // Update is called once per frame
    void Update()
    {
        currentLifeTime -= Time.deltaTime;
        if (currentLifeTime <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void Lauch(Vector2 inicialVel)
    {
        rb.velocity = inicialVel * speed;
    }
}
