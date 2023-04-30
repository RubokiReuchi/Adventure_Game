using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bench : Interactable
{
    private Animator anim;
    public FloatValue playerHealth;
    public FloatValue magic;
    public FloatValue heartContainers;
    public Signal hidePlayer;
    public Signal healthSignal;
    public Signal magicSignal;
    public BoolValue agro;
    public Inventory playerInventory;
    [Header("Estus")]
    public Signal estusSignal;
    public EstusValue estus;
    public EstusValue cinders;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Interact") && playerInRange)
        {
            hidePlayer.Raise();
            context.Raise();
            if (!anim.GetBool("Ocuped"))
            {
                anim.SetBool("Ocuped", true);
                playerHealth.currentValue = heartContainers.currentValue * 2;
                magic.currentValue = 999;
                estus.currentValue = estus.currentMaxValue;
                cinders.currentValue = cinders.currentMaxValue;
                healthSignal.Raise();
                magicSignal.Raise();
                estusSignal.Raise();
                agro.currentValue = false;
                playerInventory.position = new Vector2(this.transform.position.x, this.transform.position.y - 1.7f);
                playerInventory.scene = SceneManager.GetActiveScene().name;
            }
            else
            {
                anim.SetBool("Ocuped", false);
                agro.currentValue = true;
            }
        }
    }
}
