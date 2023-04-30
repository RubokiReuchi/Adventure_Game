using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossDoor : Interactable
{
    public GameObject dialogBox;
    public Text dialogText;
    public Inventory playerInvetory;
    public BoolValue opened;
    public string dialogNoKey;
    public string dialogKey;
    private bool closed;
    public Signal UpdateUI;
    [SerializeField] private GameObject door;
    private Transform target;
    [SerializeField] private EstusValue masterKey;
    [SerializeField] private Enemies boss; 

    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        closed = false;
        if (opened.currentValue)
        {
            door.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Interact") && playerInRange)
        {
            if (dialogBox.activeInHierarchy)
            {
                dialogBox.SetActive(false);
                if (opened.currentValue)
                {
                    door.SetActive(false);
                }
            }
            else
            {
                if (playerInvetory.masterKey)
                {
                    playerInvetory.masterKey = false;
                    masterKey.currentValue = 0;
                    UpdateUI.Raise();
                    dialogBox.SetActive(true);
                    dialogText.text = dialogKey;
                    opened.currentValue = true;
                }
                else
                {
                    dialogBox.SetActive(true);
                    dialogText.text = dialogNoKey;
                }
            }
        }

        CheckBoss();
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger && !closed)
        {
            context.Raise();
            playerInRange = true;
        }
    }

    public override void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            if (opened.currentValue && target.position.y > this.transform.position.y)
            {
                door.SetActive(true);
                if (!closed)
                {
                    context.Raise();
                    playerInRange = false;
                    dialogBox.SetActive(false);
                }
                closed = true;
            }
            if (!closed)
            {
                context.Raise();
                playerInRange = false;
                dialogBox.SetActive(false);
            }
        }
    }

    private void CheckBoss()
    {
        if (!boss.gameObject.activeInHierarchy && closed)
        {
            this.gameObject.SetActive(false);
        }
    }
}
