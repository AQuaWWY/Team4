using UnityEngine;
using UnityEngine.SceneManagement;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager instance;
    public Transform target;

    private void Awake()
    {
        // 确保只有一个 WeaponManager 实例
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject); // 防止重复实例
            return;
        }

        // 注册场景加载事件
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void LateUpdate()
    {
        // 确保 target 被正确设置
        if (target != null)
        {
            transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
        }
        else
        {
            Debug.LogWarning("Target is null, attempting to find player...");
            FindPlayer(); // 如果 target 丢失，尝试重新查找
        }
    }

    public void FindPlayer()
    {
        // 查找 PlayerController 来设置 target
        PlayerController player = FindObjectOfType<PlayerController>();
        if (player != null)
        {
            target = player.transform;
            Debug.Log("Player found: " + target.name);
        }
        else
        {
            Debug.LogWarning("Player not found.");
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Loaded Scene: " + scene.name);

        if (scene.name != "wwyScene" && scene.name != "Boss Scene")
        {
            // 销毁不需要保留的场景中的 Weapons 对象
            Destroy(gameObject);
        }
        else
        {
            // 重新加载场景后，确保找到玩家
            FindPlayer();

            // 确保 target 在重新加载场景后没有丢失
            if (target == null)
            {
                Debug.LogWarning("Target is null after scene load, attempting to find player again.");
                FindPlayer();
            }
        }
    }

    private void OnDestroy()
    {
        // 移除场景加载监听器，避免重复回调
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
