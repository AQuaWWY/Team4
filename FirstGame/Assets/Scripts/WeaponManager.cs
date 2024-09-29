using UnityEngine;
using UnityEngine.SceneManagement;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager instance;
    public Transform target;

    private void Awake()
    {
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
            Debug.Log("Destroying Weapons and its children.");

            foreach (Transform child in transform)
            {
                Debug.Log("Destroying child: " + child.name);
                Destroy(child.gameObject);
            }

            Destroy(gameObject);
        }
        else
        {
            // 在新的场景中找到玩家
            FindPlayer();
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
