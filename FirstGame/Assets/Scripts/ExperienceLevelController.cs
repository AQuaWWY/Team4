using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceLevelController : MonoBehaviour
{
    public static ExperienceLevelController instance;

    private void Awake()
    {
        instance = this;
    }

    public int currentExperience;

    public ExpPickup pickup;//在inspector中填经验预制体

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetExp(int amountToGet)
    {
        currentExperience += amountToGet;
    }

    public void SpawnExp(Vector3 position,int expValue)
    {
        Instantiate(pickup,position,Quaternion.identity).expValue = expValue;//实例化预制体，方位，不旋转,将掉落的经验值设置为怪物自带的预设
    }
}
