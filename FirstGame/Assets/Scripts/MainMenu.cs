using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string firstLEvelName;

    public void StartGame()
    {
        SceneManager.LoadScene(firstLEvelName);
    }

    public void QuitGame()
    {
        Application.Quit();

        Debug.Log("I'm quitting the game");
    }
}

