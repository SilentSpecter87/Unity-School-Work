using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Chase : StateMachineBehaviour
{

    // box to store player's transform information
    Transform Player;
    // box that stores the bosses rigid body
    Rigidbody2D rb;
    
   
    //create a box that will hold the boss behavior
    BossBehavior bossBehavior;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //set the reference for the player
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        // set the reference for the bosses rb
        rb = animator.GetComponent<Rigidbody2D>();
        //set the reference for our boss behavior
        bossBehavior = animator.GetComponent<BossBehavior>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //call our look at function
        bossBehavior.LookAtPlayer();
     //declaring and sending the player to target for our boss, locking the y axis
        Vector2 Target = new Vector2(Player.position.x, rb.position.y);
        //sets a new position for our boss to move towards
        Vector2 newPos = Vector2.MoveTowards(rb.position, Target, bossBehavior.speed * Time.deltaTime);
        // tell our rb to move to the newPos
        rb.MovePosition(newPos);
        //check the distance between the boss and the player set a trigger to start an attack
        float distance = Vector2.Distance(Player.position,rb.position);
        if (distance < bossBehavior.attackRange && !bossBehavior.Phase2 && !bossBehavior.Phase3 && !bossBehavior.isDead)
        {
            animator.SetTrigger("MeleeAttack");
            Debug.Log("attack");
        }
        else if (distance < bossBehavior.attackRange && bossBehavior.Phase2 && !bossBehavior.Phase3 && !bossBehavior.isDead)
        {
            animator.SetTrigger("Phase2Attack");
        }
        else if (distance < bossBehavior.attackRange && !bossBehavior.Phase2 && bossBehavior.Phase3 && !bossBehavior.isDead)
        {
            animator.SetTrigger("Phase3Attack");
        }
        else if (bossBehavior.isDead)
        {
            animator.SetTrigger("Death");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }


}
