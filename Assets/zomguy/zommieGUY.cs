using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zommieGUY : MonoBehaviour
{
    bool idle;
    bool run;
//    bool attack;

    Animator animator;
    AudioSource audioS;
    
    GameManager gameManager;
   [SerializeField] float speed = 0.2f;
    GameObject Player;
    SpriteRenderer spriteR;



    // Start is called before the first frame update
    void Start()
    {
        //run = GameObject.FindWithTag("moon");
        //idle = true; 
        //attack = true; //assume moon is found
        animator = GetComponent<Animator>();
        animator.SetBool("idle", true);
        spriteR = GetComponent<SpriteRenderer>();
       // gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //activate animations based on distance from player 
        //Vector2.Distance(3f, 1f);
        transform.position = Vector2.Lerp(transform.position, Player.transform.position, speed * Time.deltaTime);

        //if(transform.position)
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log()
        //check to see if player is near by

       // if(animator.SetBool("idle", true))

        if (collision.gameObject.tag == "moon")
        {
            //activate animations based on distance from player 
            //Vector2.Distance(3f, 1f);

            //do nothing  if no player 
            if (idle == false) //return;

            //audioSource = GetComponent<AudioSource>();
            //audioSource.Play();

            run = collision.gameObject;
            //attack = false;

            //gameManager.LandedOnMoon();
            //audioSource = GetComponent<AudioSource>();
            //audioSource.Play();

        }
        if (collision.gameObject.tag == "player")
        {
            if (idle == false) //only happens if player is close by 
            {
                animator.SetBool("attack", true);
                Destroy(gameObject, 1f);
                audioS = GetComponent<AudioSource>();
                audioS.Play();
            }
        }

    }
}
