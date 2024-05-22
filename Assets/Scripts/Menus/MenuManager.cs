using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance = null;
    internal  Menu ActiveMenu = null;
    private bool titleMenuShown = false;
    private void Start()
    {
        if (instance)
        {
            Debug.LogError("more than 1 menumanager");
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void SwitchToGameplayMenus()
    {
        SceneManager.LoadScene("PauseMenu", LoadSceneMode.Additive);
    }
    public void SwitchToMainMenuMenus()
    {
        SceneManager.LoadScene("MainMenuMenu", LoadSceneMode.Additive);
    }
}
