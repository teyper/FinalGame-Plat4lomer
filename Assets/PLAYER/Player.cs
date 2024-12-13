using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float Speed = 2f;
    [SerializeField] float jumpF = 50f;
    [SerializeField] float ControlSensX = 0.05f;
    //  [SerializeField] float ControlSensY = 0.3f;





    Animator animator;
    SpriteRenderer spriteRenderer;
    Rigidbody2D Rbody;

    bool Ground = true;
   // bool air = true;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Rbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        //float y;
        float x;


        //y = Input.GetAxis("Vertical");
        x = Input.GetAxis("Horizontal");

        if (!Ground)
        { x = x * 0.5f; }
        Rbody.velocity = new Vector3(Speed * x, Rbody.velocity.y, 0f);


        //transform.Translate(0f, y * Speed * Time.deltaTime, 0f);
        transform.Translate(x * Speed * Time.deltaTime, 0f, 0f);

        //jumping

        if (Input.GetKeyDown(KeyCode.UpArrow) && Ground)
        {
            //jump
            animator.SetBool("walk", false);
           // air = true;
            Rbody.AddForce(jumpF * Vector3.up);
            Ground = false;
            Debug.Log("yes");
        }

        /*if (Input.GetKeyUp(KeyCode.UpArrow) && Ground)
        {
            //jump
            animator.SetBool("jump", true);
            rigidbody2D.AddForce(jumpF * Vector3.up);
            Ground = true;
        }*/




        //walk right 
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            //runs when up arrow is presssed
            if (Ground == true)
            { 
                animator.SetBool("walk", true);
        }
            spriteRenderer.flipX = false;
            //laserDirection = Vector3.right;
        }
       /* if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            //runs when up arrow is released
            animator.SetBool("walk", false);
            spriteRenderer.flipX = false;
        }*/

        //walk left 
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //runs when up arrow is presssed
            if (Ground == true)
            {
                animator.SetBool("walk", true);
            }
            spriteRenderer.flipX = true;
            //laserDirection = Vector3.left;
        }
        /*if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            //runs when up arrow is released
            animator.SetBool("walk", false);
           // animator.SetBool("idle", true);
            spriteRenderer.flipX = true;
        }*/

        //attack
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //runs when up arrow is presssed

            animator.SetBool("attack", true);
            
            
        }

            //spriteRenderer.flipX = false;
            //laserDirection = Vector3.right;
        

        if (x < -ControlSensX)
        {
            spriteRenderer.flipX = true;
            //animator.SetBool("walk", true);
        }
        // negative min level
        if (x > ControlSensX)
        {
            spriteRenderer.flipX = false;
        }
        //positive min level
        if (x < -ControlSensX || x > ControlSensX)
        {
            animator.SetBool("walk", true);

        }
        else
        {
            animator.SetBool("walk", false);
        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            Ground = true;
            animator.SetBool("walk", true);
            
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            //OnGround = false;
            animator.SetBool("walk", false);
            
        }
    }

}