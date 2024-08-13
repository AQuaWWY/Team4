using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance;//创建一个静态的UIController实例
    private void Awake()//在Awake中初始化instance
    {
        instance = this;
    }

    public Slider expLvlSlider;//经验条
    public TMP_Text expLvlText;//经验值文本

    public LevelUpSelectionButton[] levelUpButtons;//不会改变

    public GameObject levelUpPanel;//升级面板

    public TMP_Text coinText;//金币文本

    public PlayerStatUpgradeDisplay moveSpeedUpgradeDisplay, healthUpgradeDisplay, pickupRangeUpgradeDisplay, maxWeaponsUpgradeDisplay;

    public TMP_Text timeText;//时间文本


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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
}
