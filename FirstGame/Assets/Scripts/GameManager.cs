using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    //public Weapon[] weapons;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void GotoBossScene()
    {

        PlayerStatController.instance.SavePlayerStats();//保存数据
        PlayerController.instance.SaveWeapons();//保存武器数据

        SceneManager.LoadScene("Boss Scene");
    }

}