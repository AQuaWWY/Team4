using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public static BossController instance;
    private int once = 1;
    // Start is called before the first frame update
    void Start()
    {
        PlayerStatController.instance.LoadPlayerStats();//加载玩家数据
        //PlayerController.instance.LoadWeapons();//加载武器数据

    }

    // Update is called once per frame
    void Update()
    {
        if (once == 1)//加载一次数据
        {
            LoadData();
            once = 0;
        }
    }

    public void LoadData()//将数据导入到玩家数据中
    {
        PlayerStatController.instance.LoadMoveSpeed();
        PlayerStatController.instance.LoadHealth();
        PlayerStatController.instance.LoadPickupRange();
        PlayerStatController.instance.LoadMaxWeapons();
        //PlayerController.instance.LoadWeapons();//加载武器数据
        //PlayerController.instance.Activate();
        WeaponManager.instance.FindPlayer();
    }
}
