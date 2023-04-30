using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum DoorType
{
    key,
    enemies,
    button
}

public class LockedDoor : Interactable
{
    public GameObject dialogBox;
    public Text dialogText;
    public Inventory playerInvetory;
    public DoorType doorType;
    public BoolValue opened;
    [Header("KeyType")]
    public string dialogNoKey;
    public string dialogKey;
    public EstusValue keys;
    public Signal UpdateUI;
    [Header("EnemiesType")]
    public string dialogNoEnemies;
    public bool closed;
    public Enemies[] enemies;
    private int nEnemies;
    [Header("ButtonType")]
    public string dialogNoButtons;
    public List<Button> switches = new List<Button>();
    private int buttons;

    void Start()
    {
        if (opened.currentValue)
        {
            gameObject.SetActive(false);
        }

        if (doorType == DoorType.key)
        {
            
        }
        else if (doorType == DoorType.enemies)
        {
            nEnemies = enemies.Length;
            closed = false;
        }
        else if (doorType == DoorType.button)
        {
            buttons = switches.Count;
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
                    this.gameObject.SetActive(false);
                }
            }
            else
            {
                if (doorType == DoorType.key)
                {
                    if (playerInvetory.numberOfKeys >= 1)
                    {
                        playerInvetory.numberOfKeys--;
                        keys.currentValue--;
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
                else if (doorType == DoorType.enemies)
                {
                    dialogBox.SetActive(true);
                    dialogText.text = dialogNoEnemies;
                }
                else if (doorType == DoorType.button)
                {
                    dialogBox.SetActive(true);
                    dialogText.text = dialogNoButtons;
                }
            }
        }

        if (doorType == DoorType.enemies)
        {
            CheckEnemies();
            if (playerInRange)
            {
                closed = true;
            }
        }
    }

    public override void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            context.Raise();
            playerInRange = false;
            dialogBox.SetActive(false);
            if (opened.currentValue)
            {
                this.gameObject.SetActive(false);
            }
        }
    }

    public void CheckButtons()
    {
        for (int i = 0; i < switches.Count; i++)
        {
            if (switches[i].isActive.currentValue == true)
            {
                buttons--;
            }
        }

        if (buttons == 0)
        {
            opened.currentValue = true;
            this.gameObject.SetActive(false);
        }
        else
        {
            buttons = switches.Count;
        }
    }

    private void CheckEnemies()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i].gameObject.activeInHierarchy == false && closed)
            {
                nEnemies--;
            }
        }

        if (nEnemies == 0)
        {
            opened.currentValue = true;
            this.gameObject.SetActive(false);
        }
        else
        {
            nEnemies = enemies.Length;
            this.gameObject.SetActive(true);
        }
    }
}
