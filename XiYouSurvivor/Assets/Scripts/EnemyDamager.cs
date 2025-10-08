using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class EnemyDamager : MonoBehaviour
{
    public float damageAmount;//伤害值

    public float lifeTime, growSpeed = 5f;//生命周期,生长速度
    private Vector3 targetSize;//目标大小
    public bool shouldKnockBack;//是否击退

    public bool destroyParent;

    public bool damageOverTime;//是否持续伤害
    public float timeBetweenDamage;//伤害间隔
    private float damageCounter;//伤害计时器
    private float bossDamageCounter;//伤害计时器

    private List<EnemyController> enemiesInRange = new List<EnemyController>();//敌人列表

    public bool destroyOnImpact;//是否在碰撞时销毁

    // Start is called before the first frame update
    void Start()
    {
        //Destroy(gameObject,lifeTime);//lifetime时间后销毁物体

        targetSize = transform.localScale;//目标大小为当前大小
        transform.localScale = Vector3.zero;//初始大小为0
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = Vector3.MoveTowards(transform.localScale, targetSize, growSpeed * Time.deltaTime);//物体大小从0到目标大小

        lifeTime -= Time.deltaTime;//生命周期减少

        if (lifeTime <= 0)//武器生命周期结束
        {
            targetSize = Vector3.zero;//目标大小为0

            if (transform.localScale.x == 0f)//物体大小为0
            {
                Destroy(gameObject);//销毁物体

                if (destroyParent)//销毁父物体
                {
                    Destroy(transform.parent.gameObject);
                }
            }
        }

        if (damageOverTime == true)//创建需要扣血的敌人列表
        {
            damageCounter -= Time.deltaTime;//伤害计时器减少

            if (damageCounter <= 0)//伤害计时器结束
            {
                damageCounter = timeBetweenDamage;//伤害计时器重置

                for (int i = 0; i < enemiesInRange.Count; i++)//遍历敌人列表
                {
                    if (enemiesInRange[i] != null)//如果敌人不为空
                    {
                        enemiesInRange[i].TakeDamage(damageAmount, shouldKnockBack);//敌人受伤
                    }
                    else
                    {
                        enemiesInRange.RemoveAt(i);//移除敌人
                        i--;
                    }
                }
            }
        }

        if (bossDamageCounter > 0)
        {
            bossDamageCounter -= Time.deltaTime; // 在每一帧减少计时器

            if (bossDamageCounter <= 0)
            {
                BossBehaviorController.instance.TakeDamage(damageAmount); // 对 BOSS 造成伤害
                bossDamageCounter = timeBetweenDamage; // 重置计时器
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) //触发when两个collider碰撞
    {
        if (damageOverTime == false)//单次伤害
        {
            if (collision.tag == "Enemy")//如果碰撞到的是敌人
            {
                collision.GetComponent<EnemyController>().TakeDamage(damageAmount, shouldKnockBack);//调用敌人的受伤函数

                if (destroyOnImpact == true)
                {
                    Destroy(gameObject);//销毁物体
                }
            }
            else if (collision.CompareTag("Boss") && collision == BossBehaviorController.instance.objectCollider) // 如果碰撞到的是BOSS
            {
                BossBehaviorController.instance.TakeDamage(damageAmount); // 调用BOSS的受伤函数
            }
        }
        else // 持续伤害
        {
            if (collision.tag == "Enemy")
            {
                enemiesInRange.Add(collision.GetComponent<EnemyController>());
            }
            else if (collision.CompareTag("Boss") && collision == BossBehaviorController.instance.objectCollider) // 如果碰撞到的是 BOSS
            {
                // 将 Boss 的持续伤害逻辑移到 Update 中处理
                // 只需要在进入碰撞时重置计时器（如果需要）
                bossDamageCounter = timeBetweenDamage; // 初始化计时器

            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)//当两个collider不再碰撞时
    {
        if (damageOverTime == true)
        {
            if (collision.tag == "Enemy")
            {
                enemiesInRange.Remove(collision.GetComponent<EnemyController>());
            }
        }
    }
}
