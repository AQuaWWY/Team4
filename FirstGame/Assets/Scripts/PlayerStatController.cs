using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatController : MonoBehaviour
{
    public static PlayerStatController instance;
    public PlayerStatData playerStatData;

    private void Awake()
    {
        instance = this;
    }

    public List<PlayerStatValue> moveSpeed, health, pickupRange, maxWeapons;
    public int moveSpeedLevelCount, healthLevelCount, pickupRangeLevelCount;

    public int moveSpeedLevel, healthLevel, pickupRangeLevel, maxWeaponsLevel;

    // Start is called before the first frame update
    void Start()//自动新建每个元素的等级数值
    {
        for (int i = moveSpeed.Count - 1; i < moveSpeedLevelCount; i++)
        {
            moveSpeed.Add(new PlayerStatValue(moveSpeed[i].cost + moveSpeed[1].cost, moveSpeed[i].value + (moveSpeed[1].value - moveSpeed[0].value)));
        }

        for (int i = health.Count - 1; i < healthLevelCount; i++)
        {
            health.Add(new PlayerStatValue(health[i].cost + health[1].cost, health[i].value + (health[1].value - health[0].value)));
        }

        for (int i = pickupRange.Count - 1; i < pickupRangeLevelCount; i++)
        {
            pickupRange.Add(new PlayerStatValue(pickupRange[i].cost + pickupRange[1].cost, pickupRange[i].value + (pickupRange[1].value - pickupRange[0].value)));
        }
    }

    // Update is called once per frame
    void Update()//如果UI启动，显示信息
    {
        if (UIController.instance.levelUpPanel.activeSelf == true)
        {
            UpdateDisplay();//重复显示正确的升级信息
        }
    }

    public void UpdateDisplay()//在UI中显示正确的升级信息
    {
        if (moveSpeedLevel < moveSpeed.Count - 1)
        {
            UIController.instance.moveSpeedUpgradeDisplay.UpdateDisplay(moveSpeed[moveSpeedLevel + 1].cost, moveSpeed[moveSpeedLevel].value, moveSpeed[moveSpeedLevel + 1].value);
        }
        else
        {
            UIController.instance.moveSpeedUpgradeDisplay.ShowMaxLevel();
        }

        if (healthLevel < health.Count - 1)
        {
            UIController.instance.healthUpgradeDisplay.UpdateDisplay(health[healthLevel + 1].cost, health[healthLevel].value, health[healthLevel + 1].value);
        }
        else
        {
            UIController.instance.healthUpgradeDisplay.ShowMaxLevel();
        }

        if (pickupRangeLevel < pickupRange.Count - 1)
        {
            UIController.instance.pickupRangeUpgradeDisplay.UpdateDisplay(pickupRange[pickupRangeLevel + 1].cost, pickupRange[pickupRangeLevel].value, pickupRange[pickupRangeLevel + 1].value);
        }
        else
        {
            UIController.instance.pickupRangeUpgradeDisplay.ShowMaxLevel();
        }

        if (maxWeaponsLevel < maxWeapons.Count - 1)
        {
            UIController.instance.maxWeaponsUpgradeDisplay.UpdateDisplay(maxWeapons[maxWeaponsLevel + 1].cost, maxWeapons[maxWeaponsLevel].value, maxWeapons[maxWeaponsLevel + 1].value);
        }
        else
        {
            UIController.instance.maxWeaponsUpgradeDisplay.ShowMaxLevel();
        }

    }

    public void PurchaseMoveSpeed()
    {
        moveSpeedLevel++;
        CoinController.instance.SpendCoins(moveSpeed[moveSpeedLevel].cost);
        UpdateDisplay();

        LoadMoveSpeed();
    }

    public void PurchaseHealth()
    {
        healthLevel++;
        CoinController.instance.SpendCoins(health[healthLevel].cost);
        UpdateDisplay();

        LoadHealth();
    }

    public void PurchasePickupRange()
    {
        pickupRangeLevel++;
        CoinController.instance.SpendCoins(pickupRange[pickupRangeLevel].cost);
        UpdateDisplay();

        LoadPickupRange();
    }

    public void PurchaseMaxWeapons()
    {
        maxWeaponsLevel++;
        CoinController.instance.SpendCoins(maxWeapons[maxWeaponsLevel].cost);
        UpdateDisplay();

        LoadMaxWeapons();
    }

    public void LoadMoveSpeed()
    {
        PlayerController.instance.moveSpeed = moveSpeed[moveSpeedLevel].value;
    }

    public void LoadHealth()
    {
        if (healthLevel < 0 || healthLevel >= health.Count)//如果生命值等级超出范围
        {
            Debug.LogError("healthLevel is out of range.");//显示错误信息
            return;
        }

        PlayerHealthController.instance.maxHealth = health[healthLevel].value;

        if (healthLevel > 0)
        {
            PlayerHealthController.instance.currentHealth += health[healthLevel].value - health[healthLevel - 1].value;
        }
        else
        {
            PlayerHealthController.instance.currentHealth = health[healthLevel].value;
        }
    }

    public void LoadPickupRange()
    {
        PlayerController.instance.pickupRange = pickupRange[pickupRangeLevel].value;
    }

    public void LoadMaxWeapons()
    {
        PlayerController.instance.maxWeapons = Mathf.RoundToInt(maxWeapons[maxWeaponsLevel].value);
    }

    // 保存数据
    public void SavePlayerStats()
    {
        playerStatData.SaveData(this);
    }

    // 加载数据
    public void LoadPlayerStats()
    {
        playerStatData.LoadData(this);
    }
}

[System.Serializable]
public class PlayerStatValue
{
    public int cost;
    public float value;

    public PlayerStatValue(int newCost, float newValue)
    {
        cost = newCost;
        value = newValue;
    }
}
