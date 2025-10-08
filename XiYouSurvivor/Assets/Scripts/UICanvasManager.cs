using UnityEngine;
using UnityEngine.SceneManagement;

public class UICanvasManager : MonoBehaviour
{
    private static UICanvasManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded; // 注册场景加载事件
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // 在对象销毁时取消注册事件
    }

    // 场景加载后调用
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "Boss Scene" && scene.name != "wwyScene")
        {
            Destroy(gameObject); // 如果场景不是 BossScene 或 wwyScene，销毁 UI Canvas
        }
    }
}