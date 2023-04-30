using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    idle,
    walk,
    attack,
    cast,
    interact,
    stagger
}

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public PlayerState currentState;
    public float speed;
    private Rigidbody2D rb;
    private Vector3 change;
    private Animator anim;
    public bool freeze = false;
    private bool isMovHor;
    private bool isMovVer;
    [Header("PlayerInteractions")]
    public FloatValue currentHealth;
    public Signal playerHealthSignal;
    public VectorValue startingPos;
    public Inventory playerInventory;
    public SpriteRenderer recievedItemSprite;
    public BoolValue agro;
    public PauseManeger pauseManag;
    [Header("Class Managment")]
    private ClassManager cManag;
    [Header("Audio Managment")]
    private AudioManager aManag;
    [Header("Spells")]
    public Projectile projectile;
    private float castTime;
    private int spellCharge;
    public FloatValue magic;
    public Signal magicSignal;
    [Header("IFrame")]
    public Collider2D triggerCollider;
    public SpriteRenderer render;
    private Color flashColor = new Color(255, 0, 0, 150);
    private Color regularColor = new Color(255, 255, 255, 255);
    private float flashDuration = 0.1f;
    private int numOfFlashes = 4;
    

    // Start is called before the first frame update
    void Start()
    {
        currentState = PlayerState.walk;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        anim.SetFloat("MoveX", 0);
        anim.SetFloat("MoveY", -1);

        transform.position = startingPos.currentValue;
        castTime = 0;
        spellCharge = 0;
        cManag = GetComponent<ClassManager>();
        aManag = GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!freeze && !pauseManag.isPaused && !pauseManag.inInventory
            && !pauseManag.isDebuging && !pauseManag.isDead)
        {
            if (currentState == PlayerState.interact)
            {
                return;
            }
            else if (currentState == PlayerState.stagger)
            {
                castTime = 0;
                spellCharge = 0;
                anim.SetBool("Casting", false);
                anim.SetInteger("CastTime", 0);
            }
            change = Vector3.zero;

            if (Input.GetAxisRaw("Vertical") != 0 && Input.GetAxisRaw("Horizontal") != 0)
            {
                if (isMovHor)
                {
                    change.y = Input.GetAxisRaw("Vertical");
                }
                else if (isMovVer)
                {
                    change.x = Input.GetAxisRaw("Horizontal");
                }
            }
            else
            {
                isMovHor = Input.GetAxisRaw("Horizontal") != 0;
                change.x = Input.GetAxisRaw("Horizontal");
                isMovVer = Input.GetAxisRaw("Vertical") != 0;
                change.y = Input.GetAxisRaw("Vertical");
            }

            if (Input.GetButtonDown("Sword") && currentState != PlayerState.attack
                && currentState != PlayerState.cast && currentState != PlayerState.stagger)
            {
                if ((cManag.attackSet == AttackSet.iron && playerInventory.ironSword)
                    || (cManag.attackSet == AttackSet.fire && playerInventory.burningSword)
                    || (cManag.attackSet == AttackSet.ice && playerInventory.freezingSword))
                {
                    StartCoroutine(AttackCo());
                    aManag.swordFx.Play();
                }
            }
            else if (currentState == PlayerState.idle || currentState == PlayerState.walk)
            {
                UpdateAnimationAndMove();
            }

            // Spell Managment
            if ((cManag.attackSet == AttackSet.iron && playerInventory.explosion)
                    || (cManag.attackSet == AttackSet.fire && playerInventory.fireSpell)
                    || (cManag.attackSet == AttackSet.ice && playerInventory.iceSpell))
            {
                if (magic.currentValue > 0)
                {
                    if (currentState == PlayerState.idle || currentState == PlayerState.walk)
                    {
                        if (Input.GetButtonDown("Spell"))
                        {
                            anim.SetBool("Casting", true);
                            currentState = PlayerState.cast;
                        }
                    }
                    else if (currentState == PlayerState.cast)
                    {
                        if (Input.GetButton("Spell"))
                        {
                            if (spellCharge < 2)
                            {
                                castTime += Time.deltaTime;
                            }

                            if (castTime >= 0.7)
                            {
                                spellCharge++;
                                castTime = 0;
                                anim.SetInteger("CastTime", spellCharge);
                            }
                        }

                        if (Input.GetButtonUp("Spell"))
                        {
                            if (cManag.attackSet == AttackSet.iron)
                            {

                            }
                            else if (cManag.attackSet == AttackSet.fire)
                            {
                                switch (spellCharge)
                                {
                                    case 0:
                                        projectile = cManag.fire0;
                                        break;
                                    case 1:
                                        projectile = cManag.fire1;
                                        break;
                                    case 2:
                                        projectile = cManag.fire2;
                                        break;
                                }
                            }
                            else if (cManag.attackSet == AttackSet.ice)
                            {
                                switch (spellCharge)
                                {
                                    case 0:
                                        projectile = cManag.ice0;
                                        break;
                                    case 1:
                                        projectile = cManag.ice1;
                                        break;
                                    case 2:
                                        projectile = cManag.ice2;
                                        break;
                                }
                            }

                            StartCoroutine(LauchSpellCo(spellCharge));
                            aManag.spellFx.Play();
                        }
                    }
                }
            }
        }
    }

    public bool CheckForItem(Item item)
    {
        if (playerInventory.items.Contains(item))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private IEnumerator AttackCo()
    {
        anim.SetBool("Attacking", true);
        currentState = PlayerState.attack;
        yield return null; // Wait 1 frame
        anim.SetBool("Attacking", false);
        yield return new WaitForSeconds(0.3f);
        if (currentState != PlayerState.interact)
        {
            currentState = PlayerState.walk;
        }
    }

    private IEnumerator LauchSpellCo(int charge)
    {
        MakeSpellBall();
        yield return null;
        switch (spellCharge)
        {
            case 0:
                magic.currentValue -= 10; // Magic cost
                break;
            case 1:
                magic.currentValue -= 12; // Magic cost
                break;
            case 2:
                magic.currentValue -= 14; // Magic cost
                break;
        }
        castTime = 0;
        spellCharge = 0;
        magicSignal.Raise();
        anim.SetBool("Casting", false);
        anim.SetInteger("CastTime", spellCharge);
        currentState = PlayerState.walk;
    }

    private void MakeSpellBall()
    {
        Vector2 temp = new Vector2(anim.GetFloat("MoveX"), anim.GetFloat("MoveY"));
        float temporal = Mathf.Atan2(anim.GetFloat("MoveY"), anim.GetFloat("MoveX")) * Mathf.Rad2Deg;
        SpellBall spellBall = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<SpellBall>();
        spellBall.Setup(temp, new Vector3(0, 0, temporal + 90));
    }

    public void RaiseItem()
    {
        if (playerInventory.currentItem != null)
        {
            if (currentState != PlayerState.interact)
            {
                anim.SetBool("RecieveItem", true);
                currentState = PlayerState.interact;
                recievedItemSprite.sprite = playerInventory.currentItem.itemSprite;
            }
            else
            {
                anim.SetBool("RecieveItem", false);
                currentState = PlayerState.idle;
                recievedItemSprite.sprite = null;
                playerInventory.currentItem = null;
            }
        }
    }

    void UpdateAnimationAndMove()
    {
        if (change != Vector3.zero)
        {
            MoveCharacter();
            anim.SetFloat("MoveX", change.x);
            anim.SetFloat("MoveY", change.y);
            anim.SetBool("Moving", true);
        }
        else
        {
            anim.SetBool("Moving", false);
        }
    }

    void MoveCharacter()
    {
        change.Normalize(); // make sure that player don't pass the speed
        rb.MovePosition(transform.position + change * speed * Time.fixedDeltaTime);
    }

    public void Knock(float knockTime, float damage)
    {
        currentHealth.currentValue -= damage;
        playerHealthSignal.Raise();
        if (currentHealth.currentValue > 0)
        {
            aManag.hitFx.Play();
            StartCoroutine(KnockCo(knockTime));
        }
        else
        {
            agro.currentValue = false;
            StartCoroutine(DeadCo());
        }
    }

    private IEnumerator KnockCo(float knockTime)
    {
        if (rb != null)
        {
            StartCoroutine(FlashCo());
            yield return new WaitForSeconds(knockTime);
            rb.velocity = Vector2.zero;
            currentState = PlayerState.idle;
        }
    }

    private IEnumerator FlashCo()
    {
        int temp = 0;
        triggerCollider.enabled = false;
        while (temp < numOfFlashes)
        {
            render.color = flashColor;
            yield return new WaitForSeconds(flashDuration);
            render.color = regularColor;
            yield return new WaitForSeconds(flashDuration);
            temp++;
        }
        triggerCollider.enabled = true;
    }

    private IEnumerator DeadCo()
    {
        aManag.deadFx.Play();
        anim.SetBool("Dead", true);
        freeze = true;
        rb.velocity = Vector2.zero;
        currentState = PlayerState.idle;
        yield return new WaitForSeconds(1f);
        pauseManag.GameOver();
        this.gameObject.SetActive(false);
    }
    
    public void HideAndFreeze()
    {
        if (!freeze)
        {
            this.GetComponent<Renderer>().enabled = false;
            freeze = true;
        }
        else
        {
            this.GetComponent<Renderer>().enabled = true;
            freeze = false;
        }
    }

    public void TpAction(Vector3 place)
    {
        transform.position = place;
    }
}
