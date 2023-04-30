using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDoorManager : MonoBehaviour
{
    public LockedDoor door;

    void Start()
    {
        door.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            if (!door.opened.currentValue)
            {
                door.gameObject.SetActive(true);
                door.playerInRange = true;
            }
        }
    }
}
