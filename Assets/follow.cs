using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class follow : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float MinPlayerX = 0f;
    [SerializeField] float MaxPlayerX = 1f;
    [SerializeField] float MinPlayerY = 0f;
    [SerializeField] float MaxPlayerY = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.position.x > MinPlayerX && player.position.x < MaxPlayerX)
        {
            transform.position = new Vector3(player.position.x, transform.position.y, transform.position.z);
        }
        if (player.position.y > MinPlayerY && player.position.y < MaxPlayerY)
        {
            transform.position = new Vector3(transform.position.x, player.position.y, transform.position.z);
        }
    }
}
