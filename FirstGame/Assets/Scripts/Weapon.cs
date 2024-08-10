using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public List<WeaponStats> stats;//武器状态列表
    public int weaponLevel;//武器等级

    [HideInInspector]//在侧边栏中隐藏
    public bool statsUpdated;//状态更新

    public Sprite icon;//图标

    public void LevelUp()//升级时调用
    {
        if (weaponLevel <= stats.Count - 1)//武器当前等级小于最大等级（合理等级时）
        {
            weaponLevel++;//等级+1

            statsUpdated = true;//以更新状态

            if (weaponLevel == stats.Count - 1)//如果等级达到最大等级
            {
                PlayerController.instance.fullyLevelUpWeapons.Add(this);//在满级列表中添加
                PlayerController.instance.assignedWeapons.Remove(this);//在注册列表中删除
            }
        }
    }
}

[System.Serializable]
public class WeaponStats//武器状态
{
    public float speed,//速度
    damage, //伤害
    range, //射程/范围
    timeBetweenAttacks, //攻击间隔 
    amount, //数量
    duration; //持续时间
    public string upgradeText; //下一级的升级描述
}
