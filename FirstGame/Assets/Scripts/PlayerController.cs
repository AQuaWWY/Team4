using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public PlayerStatData playerStatData;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        // 检查 playerStatData 是否已赋值
        if (playerStatData == null)
        {
            Debug.LogError("playerStatData is not assigned.");
        }
    }

    public float moveSpeed;

    public float pickupRange = 1.5f;

    //public Weapon activeWeapon;

    public List<Weapon> unassignedWeapons, assignedWeapons;//分配的武器和未分配的武器

    public int maxWeapons = 3;

    //[HideInInspector]
    public List<Weapon> fullyLevelUpWeapons = new List<Weapon>();//满级武器列表

    //游戏开始时随机一把武器
    void Start()
    {
        if (assignedWeapons.Count == 0)
        {
            AddWeapon(Random.Range(0, unassignedWeapons.Count));
            //使用int不会随机到最大的数
        }

        moveSpeed = PlayerStatController.instance.moveSpeed[0].value;
        pickupRange = PlayerStatController.instance.pickupRange[0].value;
        maxWeapons = Mathf.RoundToInt(PlayerStatController.instance.maxWeapons[0].value);
    }

    //玩家移动
    void Update()//移动实现
    {
        Vector3 moveInput = new Vector3(0f, 0f, 0f);
        moveInput.x = Input.GetAxisRaw("Horizontal");//横向输入
        moveInput.y = Input.GetAxisRaw("Vertical");//纵向输入

        //Debug.Log(moveInput);

        moveInput.Normalize();//保持斜向速度一致

        transform.position += moveInput * moveSpeed * Time.deltaTime;//坐标加移动 deltaTime:帧数越高，数值越小，使所有玩家都有相同的移动速度
    }

    //将武器列表中的一个武器放入到注册武器中，只用于第一次随机武器
    public void AddWeapon(int weaponNumber)
    {
        if (weaponNumber < unassignedWeapons.Count)
        {
            assignedWeapons.Add(unassignedWeapons[weaponNumber]);

            unassignedWeapons[weaponNumber].gameObject.SetActive(true);//启动
            unassignedWeapons[weaponNumber].isEnable = true;//设为启用
            unassignedWeapons.RemoveAt(weaponNumber);//在未注册的武器列表中删除
        }
    }

    //将武器列表中的一个武器放入到注册武器中
    public void AddWeapon(Weapon weaponToAdd)
    {
        weaponToAdd.gameObject.SetActive(true);
        weaponToAdd.isEnable = true;

        assignedWeapons.Add(weaponToAdd);
        unassignedWeapons.Remove(weaponToAdd);
    }

    public void SaveWeapons()
    {
        playerStatData.SaveWeapons(this);
    }

    public void LoadWeapons()
    {
        playerStatData.LoadWeapons(this);
    }

    public void Activate()
    {
        foreach (Weapon weapon in assignedWeapons)
        {
            weapon.gameObject.SetActive(true);
            weapon.isEnable = true;
        }

        foreach (Weapon weapon in fullyLevelUpWeapons)
        {
            //weapon.gameObject.SetActive(true);
            //weapon.isEnable = true;
            Debug.Log("Activate" + weapon.name);
        }
    }
    
}
