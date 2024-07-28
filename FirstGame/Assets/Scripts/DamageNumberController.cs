using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageNumberController : MonoBehaviour
{
    public static DamageNumberController instance;

    private void Awake()
    {
        instance = this;
    }

    public DamageNumber numberToSpawn;
    public Transform numberCanvas;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.U))
        {
            SpawnDamage(57f, new Vector3(4,3,0));
        }
    }

    public void SpawnDamage(float damageAmount,Vector3 location)
    {
        int rounded = Mathf.RoundToInt(damageAmount);//四舍五入

        DamageNumber newDamage = Instantiate(numberToSpawn,location,Quaternion.identity,numberCanvas);

        newDamage.SetUp(rounded);
        newDamage.gameObject.SetActive(true);
    }
}
