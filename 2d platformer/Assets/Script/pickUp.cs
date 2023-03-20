using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickUp : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (collision.gameObject.CompareTag("Player"))
        {
            //creates a location and reference for our player manager script
            playerManager manager = collision.GetComponent<playerManager>();

            
            if (manager)
            {
                bool pickedUp = manager.Pickupitem(gameObject);
                if (pickedUp)
                {
                    Destroy(gameObject);
                }
            }
        }
    }

}
