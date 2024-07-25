using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;//所有敌人共用一个健康控制系统，不会在unity的inspetor中检查出来，因为不可以手动操作

    private void Awake() //before start
    {
        instance = this;//启动玩家健康控制器
    }

    public float currentHealth, maxHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))//检测T键被按下就启动函数扣除伤害
        {
            TakeDamage(10f);
        }
    }

    public void TakeDamage(float damageToTake)//可以从其他脚本中调用
    {
        currentHealth -= damageToTake;

        if(currentHealth <= 0)
        {
            gameObject.SetActive(false);//deactivate the player
        }
    }
}
