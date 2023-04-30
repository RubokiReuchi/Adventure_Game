using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Letter : PickUp
{
    [SerializeField] private BoolValue letter;
    [SerializeField] private Inventory playerInventory;
    [SerializeField] private SpriteRenderer render;
    public Item content;
    [Header("SceneTransition")]
    public string sceneToLoad;
    public Vector2 playerPos;
    public VectorValue playerStorage;
    public Animator anim;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            playerInventory.currentItem = content;
            StartCoroutine(LetterCo());
        }
    }

    private IEnumerator LetterCo()
    {
        pickUpSignal.Raise();
        letter.currentValue = true;
        render.enabled = false;
        yield return new WaitForSeconds(1.5f);
        playerStorage.currentValue = playerPos;
        playerInventory.position = playerPos;
        FadeToLevel();
    }

    public void FadeToLevel()
    {
        anim.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        playerInventory.scene = sceneToLoad;
        SceneManager.LoadScene(sceneToLoad);
        pickUpSignal.Raise();
    }
}
