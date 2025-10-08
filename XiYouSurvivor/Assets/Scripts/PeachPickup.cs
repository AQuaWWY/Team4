using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeachPickup : MonoBehaviour
{
    public int healAmount;//一个桃子所回复的血量

    private bool movingToPlayer;
    public float moveSpeed;

    public float timeBetweenChecks = 0.2f;
    private float checkCounter;//距离检查消耗较大，所以设置一个计时器，每隔一段时间检查一次

    private PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        player = PlayerController.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (movingToPlayer == true)//向玩家移动
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
        }
        else//不向玩家移动，隔段时间检查距离
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
    {
        if (collision.tag == "Player")
        {
            PeachController.instance.Heal(healAmount);

            Destroy(gameObject);

            //SFXManger.instance.PlaySFXPitch(5);
        }
    }
}
