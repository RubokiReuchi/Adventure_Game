using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum AttackSet
{
    iron,
    fire,
    ice
}

public class ClassManager : MonoBehaviour
{
    [Header("Player")]
    public AttackSet attackSet;
    public Inventory playerInventory;
    public PlayerMovement playerMov;
    private Animator anim;
    [Header("Spells")]
    public Projectile ice0;
    public Projectile ice1;
    public Projectile ice2;
    public Projectile fire0;
    public Projectile fire1;
    public Projectile fire2;
    [Header("Image")]
    public Image moneyHolder;
    public Sprite ironImage;
    public Sprite fireImage;
    public Sprite iceImage;

    void Start()
    {
        attackSet = AttackSet.iron;
        anim = GetComponent<Animator>();
        ChangeAttackSet(attackSet);
    }

    void Update()
    {
        if (playerMov.currentState != PlayerState.cast)
        {
            if (Input.GetButtonDown("IronSet") && (playerInventory.ironSword || playerInventory.explosion))
            {
                attackSet = AttackSet.iron;
                ChangeAttackSet(attackSet);
            }
            else if (Input.GetButtonDown("FireSet") && (playerInventory.burningSword || playerInventory.fireSpell))
            {
                attackSet = AttackSet.fire;
                ChangeAttackSet(attackSet);
            }
            else if (Input.GetButtonDown("IceSet") && (playerInventory.freezingSword || playerInventory.iceSpell))
            {
                attackSet = AttackSet.ice;
                ChangeAttackSet(attackSet);
            }
        }
    }

    private void ChangeAttackSet(AttackSet attackSet)
    {
        switch (attackSet)
        {
            case AttackSet.iron:
                for (int i = 0; i < 4; i++)
                {
                    transform.GetChild(i).GetComponent<Hit>().attackType = AttackType.normal;
                }
                anim.SetInteger("AttackType", 0);
                moneyHolder.sprite = ironImage;
                break;
            case AttackSet.fire:
                for (int i = 0; i < 4; i++)
                {
                    transform.GetChild(i).GetComponent<Hit>().attackType = AttackType.fire;
                }
                anim.SetInteger("AttackType", 1);
                moneyHolder.sprite = fireImage;
                break;
            case AttackSet.ice:
                for (int i = 0; i < 4; i++)
                {
                    transform.GetChild(i).GetComponent<Hit>().attackType = AttackType.ice;
                }
                anim.SetInteger("AttackType", 2);
                moneyHolder.sprite = iceImage;
                break;
            default:
                break;
        }
    }
}
