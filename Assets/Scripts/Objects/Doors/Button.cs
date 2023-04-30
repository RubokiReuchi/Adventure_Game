using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    private SpriteRenderer currentSprite;
    public Sprite spriteOff;
    public Sprite spriteOn;
    public BoolValue isActive;
    public LockedDoor door;

    void Start()
    {
        currentSprite = GetComponent<SpriteRenderer>();
        if (isActive.currentValue)
        {
            currentSprite.sprite = spriteOn;
        }
        else
        {
            currentSprite.sprite = spriteOff;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            isActive.currentValue = true;
            currentSprite.sprite = spriteOn;
            door.CheckButtons();
        }
    }
}
