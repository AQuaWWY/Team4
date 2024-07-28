using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageNumber : MonoBehaviour
{
    public TMP_Text damageText;

    public float lifeTime;
    private float lifeCounter;

    public float floatSpeed = 1f;


    // Update is called once per frame
    void Update()
    {
        

        if(lifeCounter > 0)
        {
            lifeCounter -= Time.deltaTime;

            if(lifeCounter <= 0)
            {
                //Destroy(gameObject);

                DamageNumberController.instance.PlaceToPool(this);
            }
        }

        // if(Input.GetKeyDown(KeyCode.U))
        // {
        //     SetUp(45);
        // }

        transform.position += Vector3.up * floatSpeed * Time.deltaTime;//数字向上飘
    }

    public void SetUp(int damageToDisplay)
    {
        lifeCounter = lifeTime;

        damageText.text = damageToDisplay.ToString();
    }
}
