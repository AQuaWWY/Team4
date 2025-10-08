using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool gameActive;
    public float timer;

    public float waitToEndScreen = 1f;
    public float waitToWinningScreen = 1f;

    //private int once = 1;

    // Start is called before the first frame update
    void Start()
    {
        gameActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        // if (once == 1)
        // {
        //     gameActive = true;
        //     once = 0;
        // }

        if (gameActive == true)
        {
            timer += Time.deltaTime;
            UIController.instance.UpdateTimer(timer);
            //Debug.Log("timer = " + timer);
        }
    }

    public void EndLevel()
    {
        gameActive = false;

        StartCoroutine(EndLevelCo());
    }

    IEnumerator EndLevelCo()
    {
        yield return new WaitForSeconds(waitToEndScreen);

        float minutes = Mathf.FloorToInt(timer / 60f);
        float seconds = Mathf.FloorToInt(timer % 60f);

        UIController.instance.endTimeText.text = minutes.ToString() + " mins " + seconds.ToString("00" + " secs");
        UIController.instance.levelEndScreen.SetActive(true);//显示关卡结束面板
    }

    public void Winning()
    {
        gameActive = false;

        StartCoroutine(WinningCo());
    }

    IEnumerator WinningCo()
    {
        yield return new WaitForSeconds(waitToWinningScreen);

        float minutes = Mathf.FloorToInt(timer / 60f);
        float seconds = Mathf.FloorToInt(timer % 60f);

        UIController.instance.winTimeText.text = minutes.ToString() + " mins " + seconds.ToString("00" + " secs");
        UIController.instance.winningScreen.SetActive(true);//显示关卡结束面板
    }
}
