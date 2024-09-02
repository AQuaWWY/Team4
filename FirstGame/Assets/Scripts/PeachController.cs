using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeachController : MonoBehaviour
{
    public static PeachController instance;

    public float timeBetweenSpawn = 10f;

    private void Awake()
    {
        instance = this;
    }

    //public int currentCoins;

    public PeachPickup peach;

    // Update is called once per frame
    void Update()
    {
        timeBetweenSpawn -= Time.deltaTime;
        while (timeBetweenSpawn <= 0)
        {
            timeBetweenSpawn = 10f;
            DropPeach(new Vector3(Random.Range(-8f, 8f) + PlayerController.instance.transform.position.x, Random.Range(-4f, 4f) + PlayerController.instance.transform.position.y), 50);
            //相对玩家位置随机生成
        }
    }

    public void Heal(int healthToAdd)
    {
        PlayerHealthController.instance.HealPlayer(healthToAdd);
    }

    public void DropPeach(Vector3 position, int healValue)
    {
        PeachPickup newPeach = Instantiate(peach, position + new Vector3(.2f, .1f, 0f), Quaternion.identity);//生成金币，位置相较经验偏移
        newPeach.healAmount = healValue;
        newPeach.gameObject.SetActive(true);
    }
}
