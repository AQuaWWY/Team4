using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CoinController : MonoBehaviour
{
    public static CoinController instance;

    public int currentCoins;

    public CoinPickup coin;

    private void Awake()
    {
        // 确保只有一个 CoinController 实例
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);

            // 注册场景加载事件
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject); // 防止重复实例
            return;
        }
    }

    private void OnDestroy()
    {
        // 注销场景加载事件，防止内存泄漏
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // 场景加载后的回调函数
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 检查当前场景是否为 BossScene 或 wwyScene
        if (scene.name != "BossScene" && scene.name != "wwyScene")
        {
            Destroy(gameObject); // 如果不是 BossScene 或 wwyScene，则销毁CoinController
        }
    }

    public void AddCoins(int coinsToAdd)
    {
        currentCoins += coinsToAdd;

        UIController.instance.UpDateCoins();//UI更新金币
    }

    public void DropCoin(Vector3 position, int value)
    {
        CoinPickup newCoin = Instantiate(coin, position + new Vector3(.2f, .1f, 0f), Quaternion.identity);//生成金币，位置相较经验偏移
        newCoin.coinAmount = value;
        newCoin.gameObject.SetActive(true);
    }

    public void SpendCoins(int coinsToSpend)//花费金币
    {
        currentCoins -= coinsToSpend;

        UIController.instance.UpDateCoins();
    }

    public void ResetCoins()
    {
        currentCoins = 0;
        UIController.instance.UpDateCoins();
    }
}
