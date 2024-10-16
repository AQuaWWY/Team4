using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpPickup : MonoBehaviour
{
    public int expValue;

    private bool movingToPlayer;
    public float moveSpeed;

    public float timeBetweenChecks = 0.2f;
    private float checkCounter;//距离检查消耗较大，所以设置一个计时器，每隔一段时间检查一次

    private PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        player = PlayerHealthController.instance.GetComponent<PlayerController>();//获取PC组件
    }

    // Update is called once per frame
    void Update()
    {
        if (movingToPlayer == true)//如果要向玩家移动
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
        }
        else
        {
            checkCounter -= Time.deltaTime;//减少时间间隔
            if (checkCounter <= 0)//如果时间间隔小于等于0
            {
                checkCounter = timeBetweenChecks;//重新设置时间间隔

                if (Vector3.Distance(transform.position, PlayerHealthController.instance.transform.position) < player.pickupRange)
                {
                    movingToPlayer = true;

                    moveSpeed += player.moveSpeed;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {//碰撞检测，如果经验检测到玩家标签，经验值增加，删除经验物体
        if (collision.tag == "Player")
        {
            ExperienceLevelController.instance.GetExp(expValue);

            Destroy(gameObject);

            SFXManger.instance.PlaySFXPitch(1);//经验拾取音效
        }
    }
}
