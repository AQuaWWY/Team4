using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeachController : MonoBehaviour
{
    public static PeachController instance;
    private void Awake()
    {
        instance = this;
    }

    //public int currentCoins;

    public PeachPickup peach;

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
