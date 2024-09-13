using System.Collections.Generic;
using UnityEngine;

public class WeaponInfoSingleton : MonoBehaviour
{
    // 静态实例
    public static WeaponInfoSingleton instance;

    // 武器数据类
    [System.Serializable]
    public class WeaponInfo
    {
        public string weaponName;
        public int weaponLevel;
        public bool isEnabled;
    }

    // 保存所有武器信息的列表
    public List<WeaponInfo> weaponsInfo = new List<WeaponInfo>();

    private void Awake()
    {
        // 初始化单例实例
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 保持在场景切换时不被销毁
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 保存武器数据
    public void SaveData(Weapon[] weaponControllers)
    {
        weaponsInfo.Clear();
        foreach (var weapon in weaponControllers)
        {
            WeaponInfo info = new WeaponInfo
            {
                weaponName = weapon.name,
                weaponLevel = weapon.weaponLevel,
                isEnabled = weapon.gameObject.activeSelf
            };
            weaponsInfo.Add(info);
        }
    }

    // 加载武器数据
    public void LoadData(Weapon[] weaponControllers)
    {
        foreach (var weapon in weaponControllers)
        {
            var info = weaponsInfo.Find(w => w.weaponName == weapon.name);
            if (info != null)
            {
                weapon.weaponLevel = info.weaponLevel;
                weapon.gameObject.SetActive(info.isEnabled);
            }
        }
    }
}