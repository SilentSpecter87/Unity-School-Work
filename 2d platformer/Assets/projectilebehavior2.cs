using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectilebehavior2 : MonoBehaviour
{
   Transform player;
   Transform boss;
    Vector2 direction;
    Rigidbody2D rb;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        boss = GameObject.FindGameObjectWithTag("Boss").transform;

        if (boss.position.x >= player.position.x)
        {
            direction = new Vector2(-1, 1.5f);

        }
        else
        {
            direction = new Vector2(1, 1.5f);
        }

    }
    private void FixedUpdate()
    {
        rb.AddForce(direction * speed, ForceMode2D.Impulse);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Boss")
        {
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
