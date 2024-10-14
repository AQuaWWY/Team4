using UnityEngine;

public class HealthBarFollow : MonoBehaviour
{
    public Transform bossTransform; // 引用BOSS的Transform
    public Vector3 offset = new Vector3(0, 0.0f, 0); // 调整HealthBar相对BOSS的偏移量

    void Update()
    {
        //bossTransform = BossBehaviorController.instance.transform;
        if (bossTransform != null)
        {
            Vector3 healthBarPosition = bossTransform.position + offset;
            transform.position = healthBarPosition;
        }
    }
}
