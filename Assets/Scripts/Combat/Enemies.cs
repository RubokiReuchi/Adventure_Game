using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    idle,
    walk,
    attack,
    stagger
}

public enum EnemyType
{
    normal,
    wood,
    fire,
    rock
}

public class Enemies : MonoBehaviour
{
    [Header("StateMachine")]
    public EnemyState currentState;
    public EnemyType enemyType;
    [Header("Health")]
    public float maxHealth;
    public float health;
    public string enemyName;
    public float thrustRes;
    public float moveSpeed;
    //[HideInInspector]
    public bool freeze = false;
    public BoolValue agro;
    [Header("Spawn")]
    public Vector2 spawnPos;
    [Header("MovementRestrict")]
    public Collider2D boundary;
    [Header("Loot")]
    public LootTable thisLoot;

    private void Awake()
    {
        health = maxHealth;
    }

    private void OnEnable()
    {
        transform.position = spawnPos;
        health = maxHealth;
    }

    private void TakeDamage(float damage, AttackType attackType)
    {
        if (attackType == AttackType.fire && enemyType == EnemyType.wood)
        {
            health -= damage * 2;
        }
        else if (attackType == AttackType.ice && enemyType == EnemyType.fire)
        {
            health -= damage * 2;
        }
        else if (attackType == AttackType.fire && enemyType == EnemyType.fire)
        {
            health -= damage * 0;
        }
        else if ((attackType == AttackType.fire || attackType == AttackType.ice) && enemyType == EnemyType.rock)
        {
            health -= damage * 0;
        }
        else
        {
            health -= damage;
        }

        if (health <= 0)
        {
            this.GetComponent<Animator>().SetBool("Destroyed", true);
            freeze = true;
            this.GetComponent<PolygonCollider2D>().enabled = false;
            this.GetComponent<BoxCollider2D>().enabled = false;
            MakeLoot();
            StartCoroutine(DeadCo());
        }
    }

    private void MakeLoot()
    {
        if (thisLoot != null)
        {
            PickUp current = thisLoot.LootPickUp();
            if (current != null)
            {
                Instantiate(current.gameObject, transform.position, Quaternion.identity);
            }
        }
    }

    public void Knock(Rigidbody2D hitted, float knockTime, AttackType attackType, float damage)
    {
        StartCoroutine(KnockCo(hitted, knockTime));
        TakeDamage(damage, attackType);
    }

    private IEnumerator KnockCo(Rigidbody2D hitted, float knockTime)
    {
        if (hitted != null)
        {
            yield return new WaitForSeconds(knockTime - thrustRes);
            hitted.velocity = Vector2.zero;
            currentState = EnemyState.idle;
        }
    }

    public virtual IEnumerator DeadCo()
    {
        yield return null;
        this.GetComponent<Animator>().SetBool("Destroyed", false);
        yield return new WaitForSeconds(1f);
        this.gameObject.SetActive(false);
    }
}
