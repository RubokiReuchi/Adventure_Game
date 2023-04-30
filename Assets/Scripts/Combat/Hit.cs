using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackType
{
    normal,
    fire,
    ice
}

public class Hit : MonoBehaviour
{
    [SerializeField] private float thrust;
    [SerializeField] private float knockTime;
    public AttackType attackType;
    [SerializeField] private float damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Breakable") && this.gameObject.CompareTag("PlayerWeapon"))
        {
            collision.GetComponent<Breakable>().Smashed();
        }
        else if (collision.gameObject.CompareTag("BreakableLog") && this.gameObject.CompareTag("PlayerWeapon"))
        {
            collision.GetComponent<ObstacleLog>().Smashed(attackType);
        }
        else if (collision.gameObject.CompareTag("Enemy") && collision.isTrigger)
        {
            Rigidbody2D hitted = collision.GetComponent<Rigidbody2D>();
            if (hitted)
            {
                Vector2 difference = hitted.transform.position - transform.position;
                difference = difference.normalized * thrust;
                hitted.AddForce(difference, ForceMode2D.Impulse);

                hitted.GetComponent<Enemies>().currentState = EnemyState.stagger;
                collision.GetComponent<Enemies>().Knock(hitted, knockTime, attackType, damage);
            }
        }
        else if (collision.gameObject.CompareTag("Player") && collision.isTrigger)
        {
            Rigidbody2D hitted = collision.GetComponent<Rigidbody2D>();
            if (hitted)
            {
                Vector2 difference = hitted.transform.position - transform.position;
                difference = difference.normalized * thrust;
                hitted.AddForce(difference, ForceMode2D.Impulse);

                if (collision.GetComponent<PlayerMovement>().currentState != PlayerState.stagger)
                {
                    hitted.GetComponent<PlayerMovement>().currentState = PlayerState.stagger;
                    collision.GetComponent<PlayerMovement>().Knock(knockTime, damage);
                }
            }
        }
    }
}
