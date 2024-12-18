using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bb : MonoBehaviour
{
    GameManager gMan;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
 private void OnTriggerEnter2D(Collider2D other)

        {
            if (other.CompareTag("shooter") || other.CompareTag("crow"))
            {
                Debug.Log("Player hit by projectile!");
                gMan.UpdateHealth(other.CompareTag("shooter") ? -5 : -10); // Lose health
                Destroy(other.gameObject); // Destroy the projectile
            }
        }
    }

