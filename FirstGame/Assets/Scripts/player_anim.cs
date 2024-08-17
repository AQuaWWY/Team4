using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_anim : MonoBehaviour
{
    public float moveSpeed;

    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveInput = new Vector3(0f, 0f, 0f);
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        moveInput.Normalize();
        transform.position+=moveInput*moveSpeed*Time.deltaTime;


        if(moveInput !=Vector3.zero){ //no input
             anim.SetBool("isMoving",true);
        }else{
            anim.SetBool("isMoving",false);
        }
    }
}
