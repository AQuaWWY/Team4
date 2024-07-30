using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance;//创建一个静态的UIController实例
    private void Awake()//在Awake中初始化instance
    {
            instance = this;
    }

    public Slider expLvlSlider;//经验条
    public TMP_Text expLvlText;//经验值文本

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpDateExperience(int currentExp, int levelExp ,int currentLevel)
    {
        expLvlSlider.maxValue = levelExp;//设置经验条最大值,不同等级对应的经验值不同
        expLvlSlider.value = currentExp;//设置经验条当前值

        expLvlText.text = "Level: " + currentLevel ;//设置经验值文本
    }
}
