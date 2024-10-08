using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviorController : MonoBehaviour
{
    public Transform player; // 玩家对象的Transform
    public float moveSpeed = 2.0f; // BOSS的移动速度
    public float attackRange = 3.0f; // BOSS的攻击范围
    public float attackCooldown = 2.0f; // BOSS攻击的冷却时间
    public float maxHealth = 100f; // BOSS的最大生命值

    private Animator animator; // BOSS的Animator组件
    private float distanceToPlayer; // BOSS与玩家的距离
    private bool isAttacking = false; // 用来判断BOSS是否正在攻击
    private bool isDead = false; // 判断BOSS是否死亡
    private float currentHealth; // 当前生命值

    private float attackTimer = 0; // 攻击冷却计时器

    void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = maxHealth; // 初始化BOSS的生命值
    }

    void Update()
    {
        // 如果BOSS已经死亡，不再执行任何操作
        if (isDead)
        {
            return;
        }

        // 计算BOSS和玩家之间的距离
        distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // 如果BOSS不在攻击范围内，则向玩家移动
        if (distanceToPlayer > attackRange && !isAttacking)
        {
            MoveTowardsPlayer();
        }
        else
        {
            // 如果BOSS在攻击范围内，并且冷却时间已过，则攻击
            if (!isAttacking && attackTimer <= 0)
            {
                StartCoroutine(Attack());
            }
        }

        // 更新攻击计时器
        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
        }
    }

    // BOSS移动到玩家位置
    void MoveTowardsPlayer()
    {
        // 设置动画状态为移动
        animator.SetBool("isMoving", true);

        // 计算移动方向并进行移动
        Vector2 direction = (player.position - transform.position).normalized;
        transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
    }

    // BOSS发动攻击
    IEnumerator Attack()
    {
        // 停止移动
        animator.SetBool("isMoving", false);

        // 设置攻击动画
        animator.SetTrigger("Attack");

        // 攻击中，设置isAttacking为true
        isAttacking = true;

        // 攻击完成后冷却
        yield return new WaitForSeconds(1.0f); // 假设攻击动画持续1秒

        // 攻击完成，进入冷却
        attackTimer = attackCooldown;
        isAttacking = false;
    }

    // BOSS受到伤害
    public void TakeDamage(float damage)
    {
        if (isDead) return;

        // 减少生命值
        currentHealth -= damage;

        // 触发受击动画
        animator.SetTrigger("TakeHit");

        // 如果生命值小于等于0，触发死亡动画
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // BOSS死亡
    void Die()
    {
        // 触发死亡动画
        animator.SetTrigger("Death");

        // 标记BOSS为死亡状态，防止后续动作
        isDead = true;

        // 停止一切移动或攻击行为
        StopAllCoroutines();
    }
}
