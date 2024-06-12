using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class HomingProjectile : MonoBehaviour
{

    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatisGround, whatisPlayer;

    //States
    public float sightRange;
    public bool playerInSightRange;


    private Animator animator;


    private void Awake()
    {
      
        player = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();


    }


    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatisPlayer);  //Checks to see if player is within chase range

        if (playerInSightRange) ChasePlayer();
        
        //Fade over time
    }

    private void ChasePlayer()
    {
        //animator.SetBool("Moving", true);
        agent.SetDestination(player.position);
    }
   

    

  


}
