using System.Collections;
using UnityEngine;
using UnityEngine.UI; // 引入UI组件
using System.Collections.Generic;

public class BossBehaviorController : MonoBehaviour
{
    public static BossBehaviorController instance; // 单例模式
    private void Awake()
    {
        instance = this;
    }


    public Transform player; // 玩家对象的Transform
    public float moveSpeed = 2.0f; // BOSS的移动速度
    public float attackRange = 3.0f; // BOSS的攻击范围
    public float attackCooldown = 2.0f; // BOSS攻击的冷却时间
    public float maxHealth = 100f; // BOSS的最大生命值
    public float currentHealth; // BOSS的当前生命值
    public Slider healthSlider; // 血条Slider
    public float damageAmount = 10f; // BOSS的攻击伤害

    public Collider2D attackCollider; // 攻击碰撞器
    public Collider2D objectCollider; // 物体碰撞器

    private Animator animator; // BOSS的Animator组件
    private float distanceToPlayer; // BOSS与玩家的距离
    private bool isAttacking = false; // 用来判断BOSS是否正在攻击
    private bool isDead = false; // 判断BOSS是否死亡

    private float attackTimer = 0; // 攻击冷却计时器
    private bool facingRight = true; // 用于记录BOSS当前的朝向
    private int a = 0;

    // 引用 BOSS 的 SpriteRenderer 和 PolygonCollider2D
    private SpriteRenderer spriteRenderer;
    private PolygonCollider2D polygonCollider;

    void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = maxHealth; // 初始化BOSS的生命值

        // 设置血条的最大值和当前值
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;

        // 获取组件
        spriteRenderer = GetComponent<SpriteRenderer>();
        polygonCollider = GetComponent<PolygonCollider2D>();

        // 初始时更新 Collider 形状
        UpdateCollider();
    }

    void Update()
    {
        // 每一帧检查并更新 Collider 形状（如果 Sprite 发生了变化）
        UpdateCollider();

        // 计算BOSS和玩家之间的距离
        distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // 始终面向玩家
        FacePlayer();

        // 如果BOSS未死亡且不在攻击中，处理BOSS的移动和攻击逻辑
        if (!isDead)
        {
            if (distanceToPlayer < attackRange)
            {
                // 如果在攻击范围内且BOSS不在攻击中并且冷却结束，开始攻击
                if (!isAttacking && attackTimer <= 0)
                {
                    //StartCoroutine(Attack());
                    Attack();
                }
            }
            else
            {
                // 如果不在攻击范围内，且BOSS不在攻击中，移动到玩家位置
                if (!isAttacking)
                {
                    MoveTowardsPlayer();
                }
            }

            // 更新攻击冷却计时器
            if (attackTimer > 0)
            {
                a = 1; // 冷却中，将 a 设置为 1
                attackTimer -= Time.deltaTime;
            }
            else if (attackTimer <= 0 && a == 1)
            {
                Debug.Log("Cool down finished");
                a = 0; // 冷却结束后重置 a
            }
        }
        //currentHealth = currentHealth - 0.01f;
        healthSlider.value = currentHealth;

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

    // 始终面向玩家
    void FacePlayer()
    {
        // 判断玩家在BOSS的左边还是右边
        if (player.position.x < transform.position.x && facingRight)
        {
            // 如果玩家在左边，而BOSS面朝右，则翻转
            Flip();
        }
        else if (player.position.x > transform.position.x && !facingRight)
        {
            // 如果玩家在右边，而BOSS面朝左，则翻转
            Flip();
        }
    }

    // 翻转BOSS的朝向
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    // BOSS发动攻击
    void Attack()
    {
        Debug.Log("Starting Attack"); // 调试信息

        // 停止移动
        animator.SetBool("isMoving", false);

        // 设置攻击动画
        animator.SetTrigger("Attack");

        // 攻击中，设置isAttacking为true
        isAttacking = true;

        // 等待攻击动画完成（假设攻击动画持续2秒）
        //yield return new WaitForSeconds(2.0f);

        // 攻击完成，进入冷却
        attackTimer = attackCooldown;
        isAttacking = false;
        Debug.Log("Attack finished, cooling down"); // 调试信息

    }


    // BOSS受到伤害
    public void TakeDamage(float damage)
    {
        if (isDead) return;  // 确保不会多次调用死亡逻辑

        DamageNumberController.instance.SpawnDamage(damage, transform.position); // 生成伤害数字

        SFXManger.instance.PlaySFXPitch(2); // 敌人受击音效

        // 减少生命值
        currentHealth -= damage;

        // 更新血条
        healthSlider.value = currentHealth;

        // 如果生命值小于等于 0，先播放受击动画再播放死亡动画
        if (currentHealth <= 0)
        {
            Debug.Log("Boss Died!");

            // 播放受击动画
            animator.SetTrigger("TakeHit");

            // 确保 TakeHit 结束后播放死亡动画
            Die();
        }
        else
        {
            // 如果没有死亡，播放普通的受击动画
            animator.SetTrigger("TakeHit");
        }
    }

    // 受击动画播放完后播放死亡动画
    void Die()
    {
        // 播放死亡动画
        animator.SetTrigger("Death");

        BossBehaviorController.instance.healthSlider.gameObject.SetActive(false); // 隐藏血条

        isDead = true; // 设置BOSS死亡状态

    }

    // 更新 PolygonCollider2D 形状以匹配当前 Sprite
    void UpdateCollider()
    {
        // 检查是否有 Sprite
        if (spriteRenderer.sprite == null)
            return;

        // 重置 PolygonCollider2D 的形状
        polygonCollider.pathCount = spriteRenderer.sprite.GetPhysicsShapeCount();
        for (int i = 0; i < polygonCollider.pathCount; i++)
        {
            // 获取当前 Sprite 的物理形状并应用到 PolygonCollider2D
            List<Vector2> shape = new List<Vector2>();
            spriteRenderer.sprite.GetPhysicsShape(i, shape);
            polygonCollider.SetPath(i, shape.ToArray());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) // 触发当两个碰撞器碰撞时
    {
        // 检查碰撞对象是否为 Player
        if (collision.gameObject.CompareTag("Player"))
        {
            // 检查 objectCollider 是否与碰撞对象接触
            if (objectCollider != null && objectCollider.IsTouching(collision.collider))
            {
                Debug.Log("super attack!!!");
                PlayerHealthController.instance.TakeDamage(damageAmount + 30.0f);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 检查碰撞对象是否为 Player
        if (collision.gameObject.CompareTag("Player"))
        {
            // 检查 attackCollider 是否与碰撞对象接触
            if (attackCollider != null && attackCollider.IsTouching(collision))
            {
                Debug.Log("normal attack!!!");
                PlayerHealthController.instance.TakeDamage(damageAmount);
            }
        }
    }

}