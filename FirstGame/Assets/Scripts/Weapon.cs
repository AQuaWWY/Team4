using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public List<WeaponStats> stats;
    public int weaponLevel;

    [HideInInspector]//在侧边栏中隐藏
    public bool statsUpdated;

    public Sprite icon;

    public void LevelUp()//升级时调用
    {
        if (weaponLevel <= stats.Count - 1)//武器升级
        {
            weaponLevel++;

            statsUpdated = true;

            if (weaponLevel == stats.Count - 1)
            {
                PlayerController.instance.fullyLevelUpWeapons.Add(this);//在满级列表中添加
                PlayerController.instance.assignedWeapons.Remove(this);//在注册列表中删除
            }
        }
    }
}

[System.Serializable]
public class WeaponStats
{
    public float speed, damage, range, timeBetweenAttacks, amount, duration;
    public string upgradeText;
}
