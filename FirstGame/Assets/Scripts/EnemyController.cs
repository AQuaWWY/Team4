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
    public float hitWaitTime = 1f; // 受击间隔
    private float hitCounter; // 用于倒计时
    public float health = 5f;

    public float knockBackTime = 0.5f; // 击退时间
    private float knockBackCounter; // 用于倒计时

    public int expToGive = 1; // 敌人死亡后给予的经验值

    public int coinValue = 1; // 敌人死亡后给予的金币值
    public float coinDropRate = 0.5f; // 金币掉落概率
    public int peachHealthValue = 50; // 敌人死亡后给予的桃子的血量
    public float peachDropRate = 0.5f; // 桃子掉落概率

    private bool facingRight = true; // 记录敌人当前的朝向

    // Start is called before the first frame update
    void Start()
    {
        // target = FindAnyObjectByType<PlayerController>().transform; // 让每个敌人都搜索一次玩家的位置
        target = PlayerHealthController.instance.transform; // 让健康系统找到玩家位置，性能更高
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.instance.gameObject.activeSelf == true)
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

            // 设置敌人移动的向量
            theRB.velocity = (target.position - transform.position).normalized * moveSpeed;

            // 始终面向玩家
            FacePlayer();
        }
        else
        {
            theRB.velocity = Vector2.zero; // 玩家死亡后，敌人停止移动
        }

        if (hitCounter > 0)
        {
            hitCounter -= Time.deltaTime;
        }
    }

    // 始终面向玩家
    void FacePlayer()
    {
        // 判断玩家在敌人的左边还是右边
        if (target.position.x < transform.position.x && facingRight)
        {
            // 如果玩家在左边，而敌人面朝右，则翻转
            Flip();
        }
        else if (target.position.x > transform.position.x && !facingRight)
        {
            // 如果玩家在右边，而敌人面朝左，则翻转
            Flip();
        }
    }

    // 翻转敌人的朝向
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private void OnCollisionEnter2D(Collision2D collision) // 触发当两个碰撞器碰撞时
    {
        // 当敌人碰撞到玩家时执行
        if (collision.gameObject.tag == "Player" && hitCounter <= 0f)
        {
            PlayerHealthController.instance.TakeDamage(damage);
            hitCounter = hitWaitTime;
        }
    }

    public void TakeDamage(float damageToTake)
    {
        SFXManger.instance.PlaySFXPitch(2); // 敌人受击音效

        health -= damageToTake;

        if (health <= 0)
        {
            Destroy(gameObject);
            ExperienceLevelController.instance.SpawnExp(transform.position, expToGive); // 生成经验值

            if (UnityEngine.Random.value <= coinDropRate)
            {
                CoinController.instance.DropCoin(transform.position, coinValue); // 生成金币
            }
        }

        DamageNumberController.instance.SpawnDamage(damageToTake, transform.position); // 生成伤害数字
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
