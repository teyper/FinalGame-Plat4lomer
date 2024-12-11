using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crowfly : MonoBehaviour
{
    //position
    [SerializeField] float Left = -8f;
    [SerializeField] float Right = 8f;
    [SerializeField] float Top = 5f;
    [SerializeField] float Bottom = -5f;
    //boundaries 
    [SerializeField] float Lb = -8f;
    [SerializeField] float Rb = 8f;
    [SerializeField] float Tb = 5f;
    [SerializeField] float Bb = -5f;
    //speed movement 
    [SerializeField] float MaxVelocity = 4.5f;
    [SerializeField] float MaxRotationVelocity = 0f;

    GameManager gameManager;

    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {

        gameManager = FindObjectOfType<GameManager>();
        //bird placement
        transform.position = new Vector3(Random.Range(Lb, Rb), Random.Range(Tb, Bb), 0f);
        Rigidbody2D rigidbody2;
        rigidbody2 = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // speed/travel of birds
        float RandomXVelocity = Random.Range(-MaxVelocity, MaxVelocity);
        float RandomYVelocity = Random.Range(-MaxVelocity, MaxVelocity);

        rigidbody2.velocity = new Vector3(RandomXVelocity, RandomYVelocity, 0f);
        //rotation
        rigidbody2.angularVelocity = Random.Range(-MaxRotationVelocity, MaxRotationVelocity);


    }

    // Update is called once per frame
    void Update()
    {
       /* if (gameManager.gameOver == true)
        {
            Rigidbody2D rigidbody2;
            rigidbody2 = GetComponent<Rigidbody2D>();
            rigidbody2.velocity = Vector3.zero;
            rigidbody2.angularVelocity = 0;
        }*/
        //past left --> Wrap to right

        if (transform.position.x < Left)
        {
            transform.position = new Vector3(Right, transform.position.y, transform.position.z);
            spriteRenderer.flipX = false;
        }
        // past right --> Wrap to left
        if (transform.position.x > Right)
        {
            transform.position = new Vector3(Left, transform.position.y, transform.position.z);
            spriteRenderer.flipX = true;
        }
        // past top --> Wrap to bottom
        if (transform.position.y > Top)
        {
            transform.position = new Vector3(transform.position.x, Bottom, transform.position.z);
        }
        //past botton --> Wrap to top
        if (transform.position.y < Bottom)
        {
            transform.position = new Vector3(transform.position.x, Top, transform.position.z);
        }
    }

}