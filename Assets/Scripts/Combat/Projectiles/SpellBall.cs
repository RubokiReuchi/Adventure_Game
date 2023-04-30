using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellBall : Projectile
{
    public void Setup(Vector2 vel, Vector3 direction)
    {
        rb.velocity = vel.normalized * speed;
        transform.rotation = Quaternion.Euler(direction);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("Breakable")
            || collision.CompareTag("BreakableLog"))
        {
            Destroy(this.gameObject);
        }
    }
}
