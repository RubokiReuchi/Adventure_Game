using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public string sceneToLoad;
    public Vector2 playerPos;
    public VectorValue playerStorage;
    public Animator anim;
    public Inventory playerInventory;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            playerStorage.currentValue = playerPos;
            playerInventory.position = playerPos;
            FadeToLevel();
        }
    }

    public void FadeToLevel()
    {
        anim.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        playerInventory.scene = sceneToLoad;
        SceneManager.LoadScene(sceneToLoad);
    }

}
