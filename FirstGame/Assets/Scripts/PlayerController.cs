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

    public Weapon activeWeapon;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveInput = new Vector3(0f, 0f, 0f);
        moveInput.x = Input.GetAxisRaw("Horizontal");//横向输入
        moveInput.y = Input.GetAxisRaw("Vertical");//纵向输入

        //Debug.Log(moveInput);

        moveInput.Normalize();//保持斜向速度一致

        transform.position += moveInput * moveSpeed * Time.deltaTime;//坐标加移动 deltaTime:帧数越高，数值越小，使所有玩家都有相同的移动速度
    }
}
