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
        LevelManager.instance.timer = 0f; // 重置时间
        LevelManager.instance.gameActive = true; // 确保游戏重新开始后时间会继续更新
    }

    public void QuitGame()
    {
        Application.Quit();

        Debug.Log("I'm quitting the game");
    }
}

