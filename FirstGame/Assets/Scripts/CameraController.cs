using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//追踪玩家
public class CameraController : MonoBehaviour
{
    private Transform target;//private在unity中不显示窗口

    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<PlayerController>().transform;
    }

    // Update is called once per frame after other objects update
    void LateUpdate()
    {
        transform.position = new Vector3(target.position.x,target.position.y,transform.position.z);//只有x和y跟随玩家位置
    }
}
