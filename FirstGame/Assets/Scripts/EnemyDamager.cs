using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class EnemyDamager : MonoBehaviour
{
    public float damageAmount;//伤害值

    public float lifeTime;//生命周期

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,lifeTime);//lifetime时间后销毁物体
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision) //触发when两个collider碰撞
    {
        if(collision.tag == "Enemy")//如果碰撞到的是敌人
        {
            collision.GetComponent<EnemyController>().TakeDamage(damageAmount);//调用敌人的受伤函数
        }
    }
}
