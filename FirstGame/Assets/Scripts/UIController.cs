using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public static UIController instance;//创建一个静态的UIController实例
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
    public Slider expLvlSlider;//经验条
    public TMP_Text expLvlText;//经验值文本

    public LevelUpSelectionButton[] levelUpButtons;//不会改变

    public GameObject levelUpPanel;//升级面板

    public TMP_Text coinText;//金币文本

    public PlayerStatUpgradeDisplay moveSpeedUpgradeDisplay, healthUpgradeDisplay, pickupRangeUpgradeDisplay, maxWeaponsUpgradeDisplay;

    public TMP_Text timeText;//时间文本

    public GameObject levelEndScreen;//关卡结束面板
    public TMP_Text endTimeText;//关卡结束信息
    public TMP_Text winTimeText;//关卡结束金币信息

    public string mainMenuName;//主菜单场景名

    public GameObject pauseScreen;//暂停画布

    public GameObject winningScreen;//胜利画布

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnpause();
        }
    }

    public void UpDateExperience(int currentExp, int levelExp, int currentLevel)
    {
        expLvlSlider.maxValue = levelExp;//设置经验条最大值,不同等级对应的经验值不同
        expLvlSlider.value = currentExp;//设置经验条当前值

        expLvlText.text = "Level: " + currentLevel;//设置经验值文本
    }

    public void SkipLevelUp()
    {
        levelUpPanel.SetActive(false);//关闭升级面板
        Time.timeScale = 1f;//时间流速为1
    }

    public void UpDateCoins()
    {
        coinText.text = "Coins: " + CoinController.instance.currentCoins;//设置金币文本
    }

    public void PurchaseMoveSpeed()
    {
        PlayerStatController.instance.PurchaseMoveSpeed();//调用PlayerStatController中的升级函数
        SkipLevelUp();//关闭升级面板
    }

    public void PurchaseHealth()
    {
        PlayerStatController.instance.PurchaseHealth();
        SkipLevelUp();
    }

    public void PurchasePickupRange()
    {
        PlayerStatController.instance.PurchasePickupRange();
        SkipLevelUp();
    }

    public void PurchaseMaxWeapons()
    {
        PlayerStatController.instance.PurchaseMaxWeapons();
        SkipLevelUp();
    }

    public void UpdateTimer(float time)
    {
        float minutes = Mathf.FloorToInt(time / 60f);
        float seconds = Mathf.FloorToInt(time % 60f);

        timeText.text = "Time: " + minutes + ":" + seconds.ToString("00");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(mainMenuName);//加载主菜单场景
        Time.timeScale = 1f;//恢复游戏
    }

    public void Restart()
    {
        // 销毁 WeaponManager 实例
        if (WeaponManager.instance != null)
        {
            Destroy(WeaponManager.instance.gameObject); // 确保当前的 WeaponManager 被销毁
            WeaponManager.instance = null; // 重置实例引用
        }

        levelEndScreen.SetActive(false);//关闭关卡结束面板
        pauseScreen.SetActive(false);//关闭暂停画布
        winningScreen.SetActive(false); // 关闭胜利画布

        // 重新加载 wwyScene
        Debug.Log("LoadWWY  !!!");
        SceneManager.LoadScene("wwyScene");
        Debug.Log("Reset time  !");
        LevelManager.instance.timer = 0f;//重置时间
        LevelManager.instance.gameActive = true; // 确保游戏重新开始后时间会继续更新

        Time.timeScale = 1f; // 恢复游戏
    }


    public void QuitGame()
    {
        Application.Quit();
    }

    public void PauseUnpause()
    {
        //pauseScreen.SetActive(!pauseScreen.activeInHierarchy);//暂停画布的激活状态取反
        if (pauseScreen.activeSelf == false)//如果暂停画布激活
        {
            pauseScreen.SetActive(true);//激活暂停画布
            Time.timeScale = 0f;//暂停游戏
        }
        else
        {
            pauseScreen.SetActive(false);//关闭暂停画布
            if (levelUpPanel.activeSelf == false)//如果升级面板未激活
            {
                Time.timeScale = 1f;//恢复游戏
            }
        }
    }
}
