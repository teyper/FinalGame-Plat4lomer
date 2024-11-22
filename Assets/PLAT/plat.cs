using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plat : MonoBehaviour
{
    bool left = true;
    [SerializeField] float Speed = 3f;
    [SerializeField] float XLimit = 3.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Moving up or down based on goingUP boolean
        if (left)
        {
            transform.Translate(Speed * Time.deltaTime, 0f, 0f);  // Move up
            if (transform.position.x > XLimit)
            {
                left = false;  // Switch direction at upper limit
            }
        }
        else
        {
            transform.Translate(-Speed * Time.deltaTime, 0f, 0f);  // Move down
            if (transform.position.x < -XLimit)
            {
                left = true;  // Switch direction at lower limit
            }
        }
    }
}
