using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManeger : MonoBehaviour
{
    public GameSaveManager gameSave;
    [HideInInspector]
    public bool isPaused;
    [HideInInspector]
    public bool inInventory;
    [HideInInspector]
    public bool isDebuging;
    [HideInInspector]
    public bool isDead;
    public GameObject pausePanel;
    public GameObject inventoryPanel;
    public GameObject debugPanel;
    public GameObject gameOverPanel;
    [Header("Respawn")]
    public Animator anim;
    public VectorValue playerPos;
    public Inventory inventory;
    private string level;
    [Header("Debug")]
    public Signal moneySignal;

    void Start()
    {
        isPaused = false;
        inInventory = false;
        isDebuging = false;
        isDead = false;
    }

    private void OnDisable()
    {
        gameSave.SaveScriptables();
    }

    void Update()
    {
        if (Input.GetButtonDown("Pause") && !isDead)
        {
            isPaused = !isPaused;
            inInventory = false;
            inventoryPanel.GetComponent<InventoryManager>().SetInfoNull();
            inventoryPanel.SetActive(false);
            isDebuging = false;
            debugPanel.SetActive(false);

            if (isPaused)
            {
                pausePanel.SetActive(true);
                Time.timeScale = 0f;
            }
            else
            {
                pausePanel.SetActive(false);
                Time.timeScale = 1f;
            }
        }

        if (Input.GetButtonDown("Inventory") && !isDead)
        {
            inInventory = !inInventory;
            isPaused = false;
            pausePanel.SetActive(false);
            isDebuging = false;
            debugPanel.SetActive(false);

            if (inInventory)
            {
                inventoryPanel.SetActive(true);
                Time.timeScale = 0f;
            }
            else
            {
                inventoryPanel.GetComponent<InventoryManager>().SetInfoNull();
                inventoryPanel.SetActive(false);
                Time.timeScale = 1f;
            }
        }

        if (Input.GetButtonDown("Debug") && !isDead)
        {
            isDebuging = !isDebuging;
            isPaused = false;
            pausePanel.SetActive(false);
            inInventory = false;
            inventoryPanel.GetComponent<InventoryManager>().SetInfoNull();
            inventoryPanel.SetActive(false);
            

            if (isDebuging)
            {
                debugPanel.SetActive(true);
                Time.timeScale = 0f;
            }
            else
            {
                debugPanel.SetActive(false);
                Time.timeScale = 1f;
            }
        }
    }

    // Pause Buttons
    public void Continue()
    {
        isPaused = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void SaveGame()
    {
        gameSave.SaveScriptables();
    }

    public void ExitToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    // Debug Buttons
    public void All_Objects()
    {
        gameSave.All_Items(); // UI
        inventory.FillAll(); // real objects
        inventoryPanel.GetComponent<InventoryManager>().estus.currentUses.currentValue =
            inventoryPanel.GetComponent<InventoryManager>().estus.currentUses.currentMaxValue;
        inventoryPanel.GetComponent<InventoryManager>().cinders.currentUses.currentValue =
            inventoryPanel.GetComponent<InventoryManager>().cinders.currentUses.currentMaxValue;
    }

    public void No_Objects()
    {
        gameSave.No_Items(); // UI
        inventory.EmptyAll(); // real objects
        inventoryPanel.GetComponent<InventoryManager>().CleanItemInformation();
    }

    public void maxMoney()
    {
        inventory.coins = 9999;
        moneySignal.Raise();
    }

    // GameOver Buttons
    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void LastSave()
    {
        gameSave.LoadScriptables();
        FadeToLevel();
        level = inventory.scene;
        playerPos.currentValue = inventory.position;
        Time.timeScale = 1f;
    }

    public void FadeToLevel()
    {
        anim.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(level);
    }
}
