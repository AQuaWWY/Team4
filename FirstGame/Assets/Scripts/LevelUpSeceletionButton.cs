using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelUpSelectionButton : MonoBehaviour
{
    public TMP_Text upgradeDescText, nameLevelText;//升级描述，名称等级
    public Image weaponIcon;//武器图标

    private Weapon assignedWeapon;

    public void UpdateButtonDisplay(Weapon theWeapon)//用于点击的对应武器的升级按钮
    {
        if (theWeapon.gameObject.activeSelf == true)//启用的武器
        {
            upgradeDescText.text = theWeapon.stats[theWeapon.weaponLevel].upgradeText;
            weaponIcon.sprite = theWeapon.icon;

            nameLevelText.text = theWeapon.name + " - Lvl " + theWeapon.weaponLevel;
        }
        else//未启用的武器，先要解锁
        {
            upgradeDescText.text = "Unlock " + theWeapon.name;
            weaponIcon.sprite = theWeapon.icon;

            nameLevelText.text = theWeapon.name;
        }

        assignedWeapon = theWeapon;//赋值
    }

    public void SelectUpgrade()
    {
        if (assignedWeapon != null)//已分配的武器不为空
        {
            if (assignedWeapon.gameObject.activeSelf == true)//武器是启用状态
            {
                assignedWeapon.LevelUp();//执行升级
            }
            else
            {
                PlayerController.instance.AddWeapon(assignedWeapon);//添加武器
            }

            UIController.instance.levelUpPanel.SetActive(false);//关闭面板
            Time.timeScale = 1f;//时间继续
        }
    }

}
