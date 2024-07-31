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
        }
    }
}

[System.Serializable]
public class WeaponStats
{
    public float speed, damage, range, timeBetweenAttacks, amount, duration;
    public string upgradeText;
}
