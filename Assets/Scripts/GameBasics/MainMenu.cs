using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameSaveManager gameSave;
    public Animator anim;
    public VectorValue playerPos;
    public Inventory inventory;
    private string level;

    void Start()
    {
        Screen.SetResolution(1440, 1080, true);
    }

    public void NewGame()
    {
        gameSave.ResetScriptables();
        gameSave.LoadScriptables();
        level = "OpeningCutscene";
        FadeToLevel();
    }

    public void Continue()
    {
        gameSave.LoadScriptables();
        FadeToLevel();
        level = inventory.scene;
        playerPos.currentValue = inventory.position;
    }

    public void ExitGame()
    {
        Application.Quit();
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
