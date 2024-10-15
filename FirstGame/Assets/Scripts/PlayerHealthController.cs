using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//调整UI的权限

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;//所有敌人共用一个健康控制系统，不会在unity的inspector中检查出来，因为不可以手动操作

    private void Awake() //before start
    {
        instance = this;//启动玩家健康控制器
    }

    public float currentHealth, maxHealth;

    public Slider healthSlider;//Slider对象

    public GameObject deathEffect;//死亡特效

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = PlayerStatController.instance.health[0].value;

        //设置总血量和当前血量
        currentHealth = maxHealth;

        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        // if(Input.GetKeyDown(KeyCode.T))//检测T键被按下就启动函数扣除伤害
        // {
        //     TakeDamage(10f);
        // }
    }

    public void TakeDamage(float damageToTake)//可以从其他脚本中调用
    {
        currentHealth -= damageToTake;

        SFXManger.instance.PlaySFXPitch(7);//受伤音效

        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);//deactivate the player

            LevelManager.instance.EndLevel();//end the level

            Instantiate(deathEffect, transform.position, transform.rotation);//生成死亡特效

            SFXManger.instance.PlaySFX(6);//死亡音效
        }

        healthSlider.value = currentHealth;//更新Slider当前血量
    }

    public void HealPlayer(int healAmount)//治疗玩家
    {
        currentHealth += healAmount;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        healthSlider.value = currentHealth;//更新Slider当前血量

        SFXManger.instance.PlaySFXPitch(8);//治疗音效
    }
}
