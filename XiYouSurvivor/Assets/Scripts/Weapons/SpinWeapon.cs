using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinWeapon : Weapon//继承自Weapon类
{
    public float rotateSpeed;

    public Transform holder, fireballToSpawn;

    public float timeBetweenSpawn;
    private float spawnCounter;

    public EnemyDamager damager;

    // Start is called before the first frame update
    void Start()
    {
        SetStats();//武器参数初始化

        //UIController.instance.levelUpButtons[0].UpdateButtonDisplay(this);
    }

    // Update is called once per frame
    void Update()
    {
        //holder.rotation = Quaternion.Euler(0f, 0f, holder.rotation.eulerAngles.z + rotateSpeed * Time.deltaTime);//只有z轴旋转

        holder.rotation = Quaternion.Euler(0f, 0f, holder.rotation.eulerAngles.z + (rotateSpeed * Time.deltaTime * stats[weaponLevel].speed));//只有z轴旋转

        spawnCounter -= Time.deltaTime;//一段时间后生成火球
        if (spawnCounter <= 0)
        {
            spawnCounter = timeBetweenSpawn;
            //Instantiate(fireballToSpawn, fireballToSpawn.position, fireballToSpawn.rotation, holder).gameObject.SetActive(true);

            for (int i = 0; i < stats[weaponLevel].amount; i++)//将火球平均分布在360度范围内
            {
                float rot = (360f / stats[weaponLevel].amount) * i;

                Instantiate(fireballToSpawn, fireballToSpawn.position, Quaternion.Euler(0f, 0f, rot), holder).gameObject.SetActive(true);
            }
        }

        if (statsUpdated == true)
        {
            statsUpdated = false;

            SetStats();
        }
    }

    public void SetStats()//武器参数设置
    {
        damager.damageAmount = stats[weaponLevel].damage;//设置EnemyDamager的伤害值

        transform.localScale = Vector3.one * stats[weaponLevel].range;//设置范围

        timeBetweenSpawn = stats[weaponLevel].timeBetweenAttacks;

        damager.lifeTime = stats[weaponLevel].duration;

        spawnCounter = 0f;
    }
}
