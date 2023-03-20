using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    playerManager playerManager;
    // Start is called before the first frame update
    void Start()
    {
        playerManager = GameObject.FindGameObjectWithTag("Player").GetComponent<playerManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {//checking to see if player collides with circle 
        if (collision.gameObject.tag=="Player")
        {
            playerManager.takeDamage();
        }
    }
}
