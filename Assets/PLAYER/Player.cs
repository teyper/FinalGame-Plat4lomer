using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class Player : MonoBehaviour
{
    [SerializeField] float Speed = 2f;
    [SerializeField] float jumpF = 50f;
    [SerializeField] float ControlSensX = 0.05f;
    //  [SerializeField] float ControlSensY = 0.3f;


    //hit damage
   // [SerializeField] private int health = 8;
    bool hit = true;
    //private bool_onHit = false;

    [SerializeField] private GameObject attackZone; // Drag the AttackZone GameObject in the Inspector
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
        
        //attack
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //attacks when space key is pressed

            animator.SetBool("attack", true);
            
            
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            animator.SetBool("attack", false); // return to idle
            //animator.SetBool("walk",false);
        }

       

        if (Input.GetKeyDown(KeyCode.Space)) // When attack button is pressed
        {
            animator.SetBool("attack", true);
            attackZone.SetActive(true); // Activate attack hitbox
            Invoke("DisableAttackZone", 0.5f); // Deactivate after 0.5 seconds
        }

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
            Ground = true;// while on the ground
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

    




    private void DisableAttackZone()
    {
        attackZone.SetActive(false);
        animator.SetBool("attack", false); // Reset attack animation
    }

}