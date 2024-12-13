using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatCRotation : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime, 0f, 0f);
    }
}
