using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : Menu
{
    public static PauseMenu instance = null;

    private void Start()
    {
        if (instance)
        {
            Debug.LogError("Multiple PauseMenu instances!");
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    public void OnResumeButton()
    {
        GameManager.Instance.TogglePause();
    }
    public void OnMainMenuButton()
    {
        Time.timeScale = 1;
        TurnOff(false);
        SceneManager.LoadScene("MainMenu");
    }
}
