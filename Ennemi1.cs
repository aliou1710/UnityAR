using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Threading;
using UnityEngine.UI;

public class Ennemi1 : MonoBehaviour
{
    // Start is called before the first frame update
    private String name = "ennemi1";
    private double life = 50;
    public static int damageValueOnPlayer = 3;
    private int damageValue = 3; 
    private double attack = 1;
    private float speed=0.5f;
    public static Player player;
    private Animator animator;
   // private List<Vector2> listnodes = null;

    //***IA***
    public bool isPlayerinSightRange, isPlayerInAttackRange;
    private bool isWalkpoinSet;
    public Vector3 walkPoint;
    public float walkPointRange=2f ;
    public bool isCheckIfBFSfinish = false;

    TMP_InputField inputtext;
    TMP_InputField inputsecond;

    private Button btnsecond;

    private Thread Thread;
    //constructeur
    public static List<Vector2Int> listnodes = null;


    //boudaries
    private int boundaryXinf = -500;
    private int boundaryYinf = -500;
    private int boundaryXsup = 500;
    private int boundaryYsup = 500;

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
                Vector3 vlist = new Vector3(listnodes[index].x, 0,listnodes[index].y) ;
                var steps = 0.1f * Time.deltaTime;
                // transform.position = Vector2.MoveTowards(transform.position, listnodes[index], steps);
                transform.position = Vector3.MoveTowards(transform.position, vlist, steps);
                transform.LookAt(vlist);
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

        //patrouille après une certaine distance
        if ((player.transform.position - transform.position).sqrMagnitude < 0.5)
        //if ((player.transform.position - transform.position).sqrMagnitude < MainMenu.valuePatrouille)
        {
            isPlayerinSightRange = true;
        }
        else
        {
            isPlayerinSightRange = false;
        }

        //aatacker à une certaine distance
        if ((player.transform.position - transform.position).sqrMagnitude < 0.1)
        //if ((player.transform.position - transform.position).sqrMagnitude < MainMenu.valueAttack)
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
        
     

      //  inputtext = GameObject.Find("input").GetComponent<TMP_InputField>();
      //  inputtext.text = "start"+ "   "+ Grid.Matrix[10,10].ToString();

        // btnsecond = GameObject.Find("Button02").GetComponent<Button>();
        // btnsecond.onClick.AddListener(func);
       



    }
    private void Update()
    {

        ConnexionAsync.startpos.x = (int)this.transform.position.x;
        ConnexionAsync.startpos.y = (int)this.transform.position.y;
        ConnexionAsync.endpos.x = (int)player.transform.position.x;
        ConnexionAsync.endpos.y = (int)player.transform.position.y;
      //  func();
      //  inputtext.text = msg;
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


        boundariesMethod();
    }

 
    public void boundariesMethod()
    {
        if (transform.position.x < boundaryXinf)
        {
            transform.position = new Vector3(boundaryXinf, transform.position.y, transform.position.z);
        }
        else if (transform.position.y < boundaryYinf)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, boundaryYinf);
        }

        if (transform.position.x > boundaryXsup)
        {
            transform.position = new Vector3(boundaryXsup, transform.position.y, transform.position.z);
        }
        else if (transform.position.y > boundaryYsup)
        {
            transform.position = new Vector3(boundaryYsup, transform.position.y, transform.position.z);
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

    //poursuivre le player
    private void ChasePlayer()
    {
        float steps = 0.4f * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, steps);
        transform.LookAt(player.transform);
        
        animator = this.GetComponent<Animator>();
        animator.Play("attacke1");
    }



    //reduire la vie du l'ennemi1
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



    //entrer en collsions 
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "ball")
        {
            damageattack(1f);
            Destroy(collision.gameObject);
        }
    }

}

   
