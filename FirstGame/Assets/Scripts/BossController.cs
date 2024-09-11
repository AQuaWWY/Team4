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
        PlayerStatController.instance.LoadPlayerStats();

        PlayerStatController.instance.LoadMoveSpeed();
        PlayerStatController.instance.LoadHealth();
        PlayerStatController.instance.LoadPickupRange();
        PlayerStatController.instance.LoadMaxWeapons();
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

    public void LoadData()
    {
        PlayerStatController.instance.LoadMoveSpeed();
        PlayerStatController.instance.LoadHealth();
        PlayerStatController.instance.LoadPickupRange();
        PlayerStatController.instance.LoadMaxWeapons();
        //GameManager.instance.LoadWeapons();
    }
}
