using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chest : Interactable
{
    [Header("ItemManagment")]
    public Item content;
    public Inventory playerInventory;
    public BoolValue UI_Link;
    public EstusValue keyNum_Link;
    [Header("State")]
    public BoolValue isOpen;
    public Signal raiseItem;
    [Header("Signal & Dialogs")]
    public GameObject dialogBox;
    public Text dialogText;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        if (isOpen.currentValue)
        {
            anim.SetBool("isOpen", true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Interact") && playerInRange)
        {
            if (!isOpen.currentValue)
            {
                OpenChest();
            }
            else
            {
                ChestOpened();
            }
        }
    }

    public void OpenChest()
    {
        dialogBox.SetActive(true);
        dialogText.text = content.itemDescription;

        playerInventory.AddItem(content);
        if (UI_Link != null)
        {
            UI_Link.currentValue = true;
        }
        if (keyNum_Link != null)
        {
            keyNum_Link.currentValue++;
        }
        playerInventory.currentItem = content;

        raiseItem.Raise();
        context.Raise();
        isOpen.currentValue = true;
        anim.SetBool("isOpen", true);
    }

    public void ChestOpened()
    {
        dialogBox.SetActive(false);
        raiseItem.Raise();
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger && !isOpen.currentValue)
        {
            context.Raise();
            playerInRange = true;
        }
    }

    public override void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            if (!isOpen.currentValue)
            {
                context.Raise();
            }
            playerInRange = false;
        }
    }
}
