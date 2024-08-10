using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneWeapon : Weapon
{
    public EnemyDamager damager;

    private float spawnTime, spawnCounter;
    // Start is called before the first frame update
    void Start()
    {
        SetStats();
    }

    // Update is called once per frame
    void Update()
    {
        //升级
        if (statsUpdated == true)
        {
            statsUpdated = false;

            SetStats();
        }

        spawnCounter -= Time.deltaTime;//计数器减去时间

        //定时生成伤害区域
        if (spawnCounter <= 0f)//计数器小于等于0
        {
            spawnCounter = spawnTime;//重置计数器

            Instantiate(damager, damager.transform.position, Quaternion.identity, transform).gameObject.SetActive(true);//创建武器预制体的副本
        }
    }

    void SetStats()
    {
        damager.damageAmount = stats[weaponLevel].damage;
        damager.lifeTime = stats[weaponLevel].duration;
        damager.timeBetweenDamage = stats[weaponLevel].speed;
        damager.transform.localScale = Vector3.one * stats[weaponLevel].range;
        spawnTime = stats[weaponLevel].timeBetweenAttacks;
        spawnCounter = 0f;
    }
}
