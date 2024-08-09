using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    private void Awake()
    {
        instance = this;
    }

    public float moveSpeed;

    public float pickupRange = 1.5f;

    //public Weapon activeWeapon;

    public List<Weapon> unassignedWeapons, assignedWeapons;//分配的武器和未分配的武器

    public int maxWeapon = 3;

    [HideInInspector]
    public List<Weapon> fullyLevelUpWeapons = new List<Weapon>();

    // Start is called before the first frame update
    void Start()
    {
        if (assignedWeapons.Count == 0)
        {
            AddWeapon(Random.Range(0, unassignedWeapons.Count));
            //使用int不会随机到最大的数
        }
    }

    // Update is called once per frame
    void Update()//移动实现
    {
        Vector3 moveInput = new Vector3(0f, 0f, 0f);
        moveInput.x = Input.GetAxisRaw("Horizontal");//横向输入
        moveInput.y = Input.GetAxisRaw("Vertical");//纵向输入

        //Debug.Log(moveInput);

        moveInput.Normalize();//保持斜向速度一致

        transform.position += moveInput * moveSpeed * Time.deltaTime;//坐标加移动 deltaTime:帧数越高，数值越小，使所有玩家都有相同的移动速度
    }

    public void AddWeapon(int weaponNumber)//将武器列表中的一个武器放入到注册武器中
    {
        if (weaponNumber < unassignedWeapons.Count)
        {
            assignedWeapons.Add(unassignedWeapons[weaponNumber]);

            unassignedWeapons[weaponNumber].gameObject.SetActive(true);//启动
            unassignedWeapons.RemoveAt(weaponNumber);//在未注册的武器列表中删除
        }
    }

    public void AddWeapon(Weapon weaponToAdd)
    {
        weaponToAdd.gameObject.SetActive(true);

        assignedWeapons.Add(weaponToAdd);
        unassignedWeapons.Remove(weaponToAdd);
    }
}
