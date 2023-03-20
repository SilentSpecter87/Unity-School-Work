using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehavior : MonoBehaviour
{
    //create a variable called boss health
    public int bossHealth;
    public GameObject projectile;
    public GameObject projectile2;
    
    public float speed=10;
    // create a series of bool that will help us transition to different phases
    public bool Phase2 = false;
    public bool Phase3 = false;
    public bool isDead = false;

    //Create storage for transform
    Transform player;
    //create storage for our player manager script
    playerManager playerManager;
    //create a storage location for a bool to check if boss is flipped
    public bool isFlipped = false;
    public float attackRange = 0.2f;
    //create a location for our bullet to start from
    public Transform shotLocation;
    //create timer/cooldown
    public float timer;
    public float coolDown;
    //
    // Start is called before the first frame update
    void Start()
    {
        //sets and grabs the information we need to function
        playerManager = GameObject.FindGameObjectWithTag("Player").GetComponent<playerManager>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        //Create a series of if else statements that will check to see if the boss

        //is below 7 and above 3, below 3 and above 1, and less than or equal to 0
        if (bossHealth < 7 && bossHealth > 3)
        {
            speed = 20;
            attackRange = 6;
            Phase2 = true;
            Debug.Log("Phase 2");
            projectileShoot();
        }
        else if (bossHealth < 3 && bossHealth > 1)
        {
            Phase2 = false;
            speed = 15;
            attackRange = 8;
            Phase3 = true;
            Debug.Log("Phase 3");
        }
        else if (bossHealth <= 0)
        {
            Phase3 = false;
            isDead = true;
            Debug.Log("Dead");
            
        }

        
    }
    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;
        //boss position
        if (transform.position.x > player.position.x && isFlipped) 
        {
            transform.localScale = flipped;
            transform.Rotate(0, 180, 0);
            isFlipped = false;
        }
        else if (transform.position.x < player.position.x && !isFlipped)

        {
            transform.localScale = flipped;
            transform.Rotate(0, 180, 0);
            isFlipped = true;
        }
    }
    public void projectileShoot()
    {
        if(timer > coolDown)
        {
            if(Phase2)
            {
                GameObject clone = Instantiate(projectile, shotLocation.position, Quaternion.identity);

                timer = 0;
            }
            else if (Phase3)
            {
                GameObject clone = Instantiate(projectile2, shotLocation.position, Quaternion.identity);

                timer = 0;
            }




         
        }
    }
    public void BossTakeDamage()
    {
        bossHealth -= 1;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            BossTakeDamage();
            Debug.Log("boss took damage");
        }
    }

}
