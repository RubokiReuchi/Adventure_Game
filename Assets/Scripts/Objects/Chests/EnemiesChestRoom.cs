using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesChestRoom : MonoBehaviour
{
    [Header("EnemiesToUnlock")]
    public Enemies[] enemies;
    private int nEnemies;
    private bool inRoom = false;
    public BoolValue isOpen;
    public Signal thisSignal;

    void Start()
    {
        nEnemies = enemies.Length;
        if (!isOpen.currentValue)
        {
            transform.GetChild(0).transform.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (inRoom)
        {
            CheckEnemies();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inRoom = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inRoom = false;
        }
    }

    private void CheckEnemies()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i].gameObject.activeInHierarchy == false)
            {
                nEnemies--;
            }
        }

        if (nEnemies == 0)
        {
            transform.GetChild(0).transform.gameObject.SetActive(true);
            if (thisSignal != null)
            {
                thisSignal.Raise();
            }
        }
        else
        {
            nEnemies = enemies.Length;
        }
    }
}
