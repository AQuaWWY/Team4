using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceLevelController : MonoBehaviour
{
    public static ExperienceLevelController instance;

    private void Awake()
    {
        instance = this;
    }

    public int currentExperience;

    public ExpPickup pickup;//在inspector中填经验预制体

    public List<int> expLevels;
    public int currentLevel = 1, levelCount = 100;

    public List<Weapon> weaponsToUpgrade;

    // Start is called before the first frame update
    void Start()
    {
        while (expLevels.Count < levelCount)//使用循环将等级升到100
        {
            expLevels.Add(Mathf.CeilToInt(expLevels[expLevels.Count - 1] * 1.1f));//调用数组加一个元素的函数，经验值为上一个的一点一倍再取整
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GetExp(int amountToGet)
    {
        currentExperience += amountToGet;

        if (currentExperience >= expLevels[currentLevel])//如果当前经验值超过此等级的经验值，升级
        {
            LevelUp();
        }

        UIController.instance.UpDateExperience(currentExperience, expLevels[currentLevel], currentLevel);//更新UI
    }

    public void SpawnExp(Vector3 position, int expValue)
    {
        Instantiate(pickup, position, Quaternion.identity).expValue = expValue;//实例化预制体，方位，不旋转,将掉落的经验值设置为怪物自带的预设
    }

    private void LevelUp()//只需要再此类中调用，不用使用public
    {
        currentExperience -= expLevels[currentLevel];//保留溢出的等级到下一级

        currentLevel++;

        if (currentLevel >= expLevels.Count)//如果等级超出当前设置，减掉一个回到最高等级
        {
            currentLevel = expLevels.Count - 1;
        }

        //PlayerController.instance.activeWeapon.LevelUp();//将等级升级与武器升级链接在一起

        UIController.instance.levelUpPanel.SetActive(true);

        Time.timeScale = 0f;

        //UIController.instance.levelUpButtons[0].UpdateButtonDisplay(PlayerController.instance.activeWeapon);
        //UIController.instance.levelUpButtons[0].UpdateButtonDisplay(PlayerController.instance.assignedWeapons[0]);
        //UIController.instance.levelUpButtons[1].UpdateButtonDisplay(PlayerController.instance.unassignedWeapons[0]); 
        //UIController.instance.levelUpButtons[2].UpdateButtonDisplay(PlayerController.instance.unassignedWeapons[1]);

        weaponsToUpgrade.Clear();//清空列表

        List<Weapon> availableWeapons = new List<Weapon>();
        availableWeapons.AddRange(PlayerController.instance.assignedWeapons);

        if (availableWeapons.Count > 0)//如果有武器
        {
            int selected = Random.Range(0, availableWeapons.Count);//随机选择一个武器
            weaponsToUpgrade.Add(availableWeapons[selected]);//添加到升级列表
            availableWeapons.RemoveAt(selected);//删除已经添加的武器
        }

        if (PlayerController.instance.assignedWeapons.Count + PlayerController.instance.fullyLevelUpWeapons.Count < PlayerController.instance.maxWeapons)
        {
            availableWeapons.AddRange(PlayerController.instance.unassignedWeapons);//添加未注册的武器
        }

        for (int i = weaponsToUpgrade.Count; i < 3; i++)//使列表中至少有三种武器
        {
            if (availableWeapons.Count > 0)//如果有武器
            {
                int selected = Random.Range(0, availableWeapons.Count);//随机选择一个武器
                weaponsToUpgrade.Add(availableWeapons[selected]);//添加到升级列表
                availableWeapons.RemoveAt(selected);//删除已经添加的武器
            }
        }

        for (int i = 0; i < weaponsToUpgrade.Count; i++)//更新升级按钮
        {
            UIController.instance.levelUpButtons[i].UpdateButtonDisplay(weaponsToUpgrade[i]);//更新按钮
        }

        for (int i = 0; i < UIController.instance.levelUpButtons.Length; i++)//显示升级按钮
        {
            if (i < weaponsToUpgrade.Count)
            {
                UIController.instance.levelUpButtons[i].gameObject.SetActive(true);
            }
            else
            {
                UIController.instance.levelUpButtons[i].gameObject.SetActive(false);
            }
        }

        PlayerStatController.instance.UpdateDisplay();//更新显示
    }
}
