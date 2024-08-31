using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseAttackWeapon : Weapon
{
    public EnemyDamager damager;

    private float attackCounter, direction;

    // Start is called before the first frame update
    void Start()
    {
        SetStats();
    }

    // Update is called once per frame
    void Update()
    {
        if (statsUpdated == true)
        {
            statsUpdated = false;

            SetStats();
        }

        attackCounter -= Time.deltaTime;
        if (attackCounter <= 0f)
        {
            attackCounter = stats[weaponLevel].timeBetweenAttacks;

            direction = Input.GetAxisRaw("Horizontal");

            if (direction != 0)//水平输入是否为零AD键
            {
                if (direction > 0)//D
                {
                    damager.transform.rotation = Quaternion.identity;//武器默认摆放位置
                }
                else//A
                {
                    damager.transform.rotation = Quaternion.Euler(0f, 0f, 180f);//武器旋转180度
                }
            }

            Instantiate(damager, damager.transform.position, damager.transform.rotation, transform).gameObject.SetActive(true);

            for (int i = 1; i < stats[weaponLevel].amount; i++)//平均分布在360度范围内
            {
                float rot = (360f / stats[weaponLevel].amount) * i;

                Instantiate(damager, damager.transform.position, Quaternion.Euler(0f, 0f, damager.transform.rotation.eulerAngles.z + rot), transform).gameObject.SetActive(true);
            }

            SFXManger.instance.PlaySFXPitch(4);//近战音效
        }
    }

    void SetStats()
    {
        damager.damageAmount = stats[weaponLevel].damage;
        damager.lifeTime = stats[weaponLevel].duration;

        damager.transform.localScale = Vector3.one * stats[weaponLevel].range;
        attackCounter = 0f;
    }
}
