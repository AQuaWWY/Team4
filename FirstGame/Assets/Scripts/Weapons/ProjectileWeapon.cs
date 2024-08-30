using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : Weapon
{
    public EnemyDamager damager;
    public Projectile projectile;

    private float shotCounter;

    public float weaponRange;
    public LayerMask whatIsEnemy;

    // Start is called before the first frame update
    void Start()
    {
        SetStats();
    }

    // Update is called once per frame
    void Update()
    {
        if (statsUpdated == true)//状态更新
        {
            statsUpdated = false;

            SetStats();
        }

        shotCounter -= Time.deltaTime;//射击计时器减少

        if (shotCounter <= 0)
        {
            shotCounter = stats[weaponLevel].timeBetweenAttacks;//重置

            Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, weaponRange * stats[weaponLevel].range, whatIsEnemy);//检测范围内的敌人

            if (enemies.Length > 0)//如果有敌人在范围内
            {
                for (int i = 0; i < stats[weaponLevel].amount; i++)
                {
                    Vector3 targetPosition = enemies[Random.Range(0, enemies.Length)].transform.position;//随机选择一个敌人，获得他的位置

                    Vector3 direction = targetPosition - transform.position;//方向:目标位置-当前位置
                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;//设置角度
                    angle -= 90f;//角度偏移
                    projectile.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);//重新设置正向

                    Instantiate(projectile, transform.position, projectile.transform.rotation).gameObject.SetActive(true);//生成子弹

                    SFXManger.instance.PlaySFX(3);//射击音效

                }
            }
        }
    }

    void SetStats()
    {
        damager.damageAmount = stats[weaponLevel].damage;
        damager.lifeTime = stats[weaponLevel].duration;

        damager.transform.localScale = Vector3.one * stats[weaponLevel].range;

        shotCounter = 0f;
        projectile.moveSpeed = stats[weaponLevel].speed;
    }
}
