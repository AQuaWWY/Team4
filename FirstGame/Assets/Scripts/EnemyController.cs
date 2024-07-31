using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Rigidbody2D theRB;
    public float moveSpeed;
    private Transform target;
    public float damage;
    public float hitWaitTime = 1f;//受击间隔
    private float hitCounter;//用于倒计时
    public float health = 5f;

    public float knockBackTime = 0.5f;//击退时间
    private float knockBackCounter;//用于倒计时

    public int expToGive = 1;//敌人死亡后给予的经验值

    // Start is called before the first frame update
    void Start()
    {
        //target = FindAnyObjectByType<PlayerController>().transform;//让每个敌人都搜索一次玩家的位置
        target = PlayerHealthController.instance.transform;//让健康系统找到玩家位置，性能更高
    }

    // Update is called once per frame
    void Update()
    {
        if (knockBackCounter > 0)
        {
            knockBackCounter -= Time.deltaTime;

            if (moveSpeed > 0)
            {
                moveSpeed = -moveSpeed * 2f;
            }

            if (knockBackCounter <= 0)
            {
                moveSpeed = Math.Abs(moveSpeed * 0.5f);
            }
        }

        theRB.velocity = (target.position - transform.position).normalized * moveSpeed;//设置敌人移动的向量
        //normalize使向量的大小化为之前设计好的长度1
        if (hitCounter > 0)
        {
            hitCounter -= Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) //触发when两个collider碰撞
    {
        //1.将player加入tag，函数发现敌人碰撞到对应tag的时候执行
        if (collision.gameObject.tag == "Player" && hitCounter <= 0f)
        {
            PlayerHealthController.instance.TakeDamage(damage);

            hitCounter = hitWaitTime;
        }
    }

    public void TakeDamage(float damageToTake)
    {
        health -= damageToTake;

        if (health <= 0)
        {
            Destroy(gameObject);

            ExperienceLevelController.instance.SpawnExp(transform.position, expToGive);//生成在敌人死亡位置,获取需要掉落的经验数量
        }

        DamageNumberController.instance.SpawnDamage(damageToTake, transform.position);//生成伤害数字
    }

    public void TakeDamage(float damageToTake, bool shouldKnockBack)
    {
        TakeDamage(damageToTake);

        if (shouldKnockBack)
        {
            knockBackCounter = knockBackTime;
        }
    }
}
