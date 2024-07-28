using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class EnemyDamager : MonoBehaviour
{
    public float damageAmount;//伤害值

    public float lifeTime,growSpeed = 5f;//生命周期,生长速度
    private Vector3 targetSize;//目标大小

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
        transform.localScale = Vector3.MoveTowards(transform.localScale,targetSize,growSpeed * Time.deltaTime);//物体大小从0到目标大小

        lifeTime -= Time.deltaTime;//生命周期减少

        if(lifeTime <= 0)//生命周期结束
        {
            targetSize = Vector3.zero;//目标大小为0

            if(transform.localScale.x == 0f)//物体大小为0
            {
                Destroy(gameObject);//销毁物体
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) //触发when两个collider碰撞
    {
        if(collision.tag == "Enemy")//如果碰撞到的是敌人
        {
            collision.GetComponent<EnemyController>().TakeDamage(damageAmount);//调用敌人的受伤函数
        }
    }
}
