using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Weapon[] weapons;

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
    //将所有武器放入传输池中
    public void PutAllInArry()
    {
        weapons = FindObjectsOfType<Weapon>();
    }

    // 保存武器数据
    public void SaveWeapons()
    {
        WeaponInfoSingleton.instance.SaveData(weapons);
    }

    // 加载武器数据
    public void LoadWeapons()
    {
        WeaponInfoSingleton.instance.LoadData(weapons);
    }

    public void GotoBossScene()
    {
            //PutAllInArry();//将所有武器放入传输池中

            PlayerStatController.instance.SavePlayerStats();//保存数据

            //GameManager.instance.SaveWeapons();

            SceneManager.LoadScene("Boss Scene");
    }
}