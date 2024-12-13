using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zommieGUY : MonoBehaviour
{
    bool idle;
    bool run;
    bool attack;

    Animator animator;
    AudioSource audioS;
    // peanut
    GameManager gameManager;


    // Start is called before the first frame update
    void Start()
    {
        run = GameObject.FindWithTag("moon");
        attack = true; //assume moon is found
        animator = GetComponent<Animator>();
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log()
        //check to see if we hit moon
        if (collision.gameObject.tag == "moon")
        {
            //do nothing  if alr on a moon
            if (idle == true) return;
            //audioSource = GetComponent<AudioSource>();
            //audioSource.Play();

            run = collision.gameObject;
            attack = true;
            //gameManager.LandedOnMoon();
            //audioSource = GetComponent<AudioSource>();
            //audioSource.Play();

        }
        if (collision.gameObject.tag == "asteroid")
        {
            if (idle == false) //only happens if youre flying 
            {
                animator.SetBool("attack", true);
                Destroy(gameObject, 1f);
                audioS = GetComponent<AudioSource>();
                audioS.Play();
            }
        }
    }
}
