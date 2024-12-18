using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shipflight : MonoBehaviour
{
    float Speed = 3f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0f, Speed * Time.deltaTime, 0f);
        Destroy(gameObject, 10f);
    }
}
