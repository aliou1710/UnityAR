using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Ennemi1 : MonoBehaviour
{
    // Start is called before the first frame update
    private String name = "ennemi1";
    private double life = 20;
    private double attack = 1;
    private float speed=0.5f;
    public static Player player;
    private Animator animator;
    private List<Vector2> listnodes = null;

    //***IA***
    public bool isPlayerinSightRange, isPlayerInAttackRange;
    private bool isWalkpoinSet;
    public Vector3 walkPoint;
    public float walkPointRange=2f ;
    public bool isCheckIfBFSfinish = false;


    //constructeur
    public Ennemi1()
    {

    }


    public Ennemi1(String name, double attack, double life)
    {
        //cvd ce qu'on va entrer en paramètre dans ce constructeur correspondra au nouveau name , nouveau life et au nive attack de ce jnouveau joueur
        this.name = name;
        this.attack = attack;
        this.life = life;

    }

    //fonction pour modifier le nom et pour le recuperer:
    //getteur: permet de recuperer le nom et le setteur permet de le modifier 

    public String Getname()
    {
        return name;
    }


    // on cree une methode qui va permettre d'enlever les point de vie d'un joueur


    public void Setname(string name)
    {
        this.name = name;
    }

    public double Getlife()
    {
        return life;
    }

    public void Setlife(double life)
    {
        this.life = life;
    }

    public double Getattack()
    {
        return attack;
    }

    public void Setattack(double attack)
    {
        this.attack = attack;
    }

   

    public void walkingAnimator()
    {

       
    }

    void bfs_walk()
    {
        int index = 0;
        if (listnodes != null)
        {
            if (((Vector2)transform.position != listnodes[index]))
            {
               // inputtext.text = listnodes[index].ToString();

                var steps = 0.1f * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, listnodes[index], steps);
                transform.LookAt(listnodes[index]);
                animator = this.GetComponent<Animator>();
                animator.Play("walkee1");

            }
            else
            {
                index += 1;
                if (index == listnodes.Count)
                {
                    listnodes = null;
                    isCheckIfBFSfinish = false;
                }
            }
        }
    }



    bool isSeenPlayer = false;
    bool isWalking = false;
   

    void checkPositionBetweenEnnemiAndPlayer()
    {
        /*
         inputtext.text = "";
        BFS bfs = new BFS();
        inputtext.text += bfs.getNameBFS();
        bfs.checkupDfs(transform.position, player.transform.position);
        listnodes = bfs.getPathsDfs();

         */

        //patrouille après une certaine distance
        if ((player.transform.position - transform.position).sqrMagnitude < 0.5)
        {
            isPlayerinSightRange = true;
        }
        else
        {
            isPlayerinSightRange = false;
        }

        //aatacker à une certaine distance
        if ((player.transform.position - transform.position).sqrMagnitude < 0.1)
        {
            isPlayerInAttackRange = true;
           
        }
        else
        {
            isPlayerInAttackRange = false;
        }

    }

    private void Start()
    {
  
    }
    private void Update()
    {

        checkPositionBetweenEnnemiAndPlayer();
        if (!isPlayerinSightRange && !isPlayerInAttackRange)
        {
            Patroling();
 
        }
        else if (isPlayerinSightRange && !isPlayerInAttackRange)
        {
            ChasePlayer();
     
        }
        else if (isPlayerinSightRange && isPlayerInAttackRange)
        {
            //AttackPlayer();
            ChasePlayer();

        }
       

    }

 

   

    public void checkPositionIntermediair()
    {
        Vector3 positionPlayer = player.transform.position;
        var step = 0.1 * Time.deltaTime; // calculate distance to move
        //on calcule la norme : cad la distance entre les deux joueurs
        if (((player.transform.position - transform.position).sqrMagnitude < 0.5) || (isSeenPlayer == true))
        {
            isSeenPlayer = true;
            if (!isWalking)
            {
                isWalking = true;
               // animator = transform.GetComponent<Animator>();
               // animator.Play("rune1");
            }
            float steps = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, positionPlayer, steps);
            transform.LookAt(positionPlayer);

        }
    }
    //*******************IA*************************
    //
    void Patroling()
    {
       
        if (!isWalkpoinSet)
        {
        
            SearchWalkPointEnnemi();
        }
        else
        {
            var steps = 0.1f * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, walkPoint, steps);
            transform.LookAt(walkPoint);
            animator = this.GetComponent<Animator>();
            animator.Play("walkee1");
           

            Vector3 distanceToWalkpoint = transform.position - walkPoint;
           
            //walpoint reached
            if (distanceToWalkpoint.magnitude < 0.0009f)
            {
                isWalkpoinSet = false;
            }
        }
    }

    void SearchWalkPointEnnemi()
    {
        //int alpha = UnityEngine.Random.Range(1, 3);
        int alpha = 1;
        //renvoie 0 ou 1
        int beta = UnityEngine.Random.Range(0, 2);
        
        //si c'est 0
        if (beta < 1)
        {
            alpha *= -1;
        }


        walkPoint = new Vector3(transform.position.x-alpha ,transform.position.y,  transform.position.z+alpha );

        isWalkpoinSet = true;
        
    }

    private void AttackPlayer()
    {
        
        transform.LookAt(player.transform);
        
       
    }

    private void ChasePlayer()
    {
        float steps = 0.4f * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, steps);
        transform.LookAt(player.transform);
        
        animator = this.GetComponent<Animator>();
        animator.Play("attacke1");
    }

    public void damageattack(double damage)
    {
        
        if(life > 0)
        {
            animator = transform.GetComponent<Animator>();
            animator.Play("behitted1");
            life -= damage;
        }
        else if (life <= 0)
        {
            animator = this.GetComponent<Animator>();
            animator.Play("diee1");
            
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "ball")
        {
            damageattack(1f);
            Destroy(collision.gameObject);
        }
    }

}

   
