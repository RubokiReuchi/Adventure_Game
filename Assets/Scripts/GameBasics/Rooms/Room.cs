using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{   [Header("VirtualCamera")]
    public GameObject virtualCam;
    [Header("Respawn")]
    public Enemies[] enemies;
    public Breakable[] breakables;
    public bool startEnabled;
    public Signal thisSignal;

    void Start()
    {
        if (!startEnabled)
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                ChangeEnemyActivation(enemies[i], false);
            }

            for (int i = 0; i < breakables.Length; i++)
            {
                ChangeBreakableActivation(breakables[i], false);
            }
            virtualCam.SetActive(false);
        }
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                ChangeEnemyActivation(enemies[i], true);
            }

            for (int i = 0; i < breakables.Length; i++)
            {
                ChangeBreakableActivation(breakables[i], true);
            }
            virtualCam.SetActive(true);
            if (thisSignal != null)
            {
                thisSignal.Raise();
            }
        }
    }

    public virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                ChangeEnemyActivation(enemies[i], false);
            }

            for (int i = 0; i < breakables.Length; i++)
            {
                ChangeBreakableActivation(breakables[i], false);
            }
            virtualCam.SetActive(false);
        }
    }

    private void OnDisable()
    {
        virtualCam.SetActive(false);
    }

    void ChangeEnemyActivation(Enemies enemy, bool active)
    {
        enemy.gameObject.SetActive(active);
        if (!active)
        {
            enemy.GetComponent<Animator>().SetBool("Destroyed", false);
            enemy.GetComponent<Enemies>().freeze = false;
            enemy.GetComponent<PolygonCollider2D>().enabled = true;
            enemy.GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    void ChangeBreakableActivation(Breakable breakable, bool active)
    {
        breakable.gameObject.SetActive(active);
        if (!active)
        {
            breakable.GetComponent<Animator>().SetBool("Destroyed", false);
        }
    }
}
