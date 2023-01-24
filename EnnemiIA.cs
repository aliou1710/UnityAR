using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnnemiIA : MonoBehaviour
{
    //lien de biblio: https://www.youtube.com/watch?v=UjkSFoLxesw
    // Start is called before the first frame update
    public NavMeshAgent agentPNG;
    public Player player;
    public static Player spawnplayer;
    public LayerMask WhatIsPlayer, WhatisGround;


    //projectile
    public GameObject projectileEnnemi;

    //patrouille
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //attack
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    //states
    public float sightRange, attackrange;
    public bool playerInSightRange, playerInAttackRange;



    private String name = "ali";
    private double life = 20;
    private double attack = 1;
    

    private void Awake()
    {

      agentPNG.GetComponent<NavMeshAgent>();
      // player=  GameObject.Find("Sword");
    }

    // Update is called once per frame
    void Update()
    {
        // Play something  if an object is within the sphere's radius.
        playerInSightRange =  Physics.CheckSphere(transform.position, sightRange, WhatIsPlayer );
        playerInAttackRange = Physics.CheckSphere(transform.position, attackrange, WhatIsPlayer);
        if(!playerInSightRange && !playerInAttackRange)
        {
            Patroling();
        }
        if (playerInSightRange && !playerInAttackRange)
        {
            ChasePlayer();
        }
        if (playerInSightRange && playerInAttackRange)
        {
            AttackPlayer();
        }

    }

    void Patroling()
    {
        if (!walkPointSet) {
            SearchWalkPoint();

            if (walkPointSet)
            {
                agentPNG.SetDestination(walkPoint);

            }
            Vector3 distanceToWalkpoint = transform.position - walkPoint;
            //walpoint reached
            if(distanceToWalkpoint.magnitude < 1f)
            {
                walkPointSet = false;
            }
        }
    }
    void ChasePlayer()
    {
        agentPNG.SetDestination(player.transform.position);
    }
    void AttackPlayer()
    {
        agentPNG.SetDestination(transform.position);
        transform.LookAt(player.transform);
        if (!alreadyAttacked)
        {
            ///attack code here

            Rigidbody rb = Instantiate(projectileEnnemi, transform.position,Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 35f, ForceMode.Impulse);
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);
            ///
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    void SearchWalkPoint()
    {
        float randomX = UnityEngine.Random.Range(-walkPointRange, walkPointRange);
        float randomZ = UnityEngine.Random.Range(-walkPointRange, walkPointRange);
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.z + randomZ);
        if (Physics.Raycast(walkPoint, -transform.up, 2f, WhatisGround))
        {
            walkPointSet = true;
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void Takedamageattack(double damage)
    {
        life -= damage;
        if(life <=0)
        {
           Invoke(nameof(DestroyEnnemi),0.5f);
        }
    }
    private void DestroyEnnemi()
    {
        Destroy(gameObject);
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackrange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

}
