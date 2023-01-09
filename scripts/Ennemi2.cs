using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ennemi2 : MonoBehaviour
{

    // Start is called before the first frame update
    private String name = "ennemi2";
    private double life = 20;
    private double attack = 1;
    public static Player player;
    private Animator animator;
   
    private float timeBetweenAttacks = 20f;
    private float speedbullet = 4f;
    //***IA***
    public bool isPlayerinSightRange, isPlayerInAttackRange;
    private bool isWalkpoinSet;
    public Vector3 walkPoint;
    public float walkPointRange;
    private bool alreadyAttacked = false;

    private bool checkup = false;
    private float timer = 0;
    private float diff = 0;

    private float timerAnimator = -1000;
    private float diff_Animator = 0;

    TMP_InputField inputtext;
    TMP_InputField inputsecond;


    //ball
    public static Ball projectileEnnemi;
    private Ball precedentProjectile;
    public static  Rigidbody rig;
    public  bool isCheck = false;

    //behitted
    private bool isCheckBehitted = false;

    Ball cb;
    //constructeur
    public Ennemi2()
    {
        
    }


    public Ennemi2(String name, double attack, double life)
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

  

    //***IA****

    void checkPositionBetweenEnnemiAndPlayer()
    {
        //patrouille après une certaine distance
        if ((player.transform.position - transform.position).sqrMagnitude < 0.7 )
        {
            isPlayerinSightRange = true;
        }
        else
        {
            isPlayerinSightRange = false;
        }

        //aatacker à une certaine distance
        if ((player.transform.position - transform.position).sqrMagnitude < 0.5)
        {
            isPlayerInAttackRange = true;

        }
        else
        {
            isPlayerInAttackRange = false;
        }

       


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
                //lance une ball vers le joueur
                lookPlayer();
                ShootBullet(110f);

            }
            else if (isPlayerinSightRange && isPlayerInAttackRange)
            {
                //lance une ball vers le joueur
                lookPlayer();
                ShootBullet(100f);
            }
        

    }

  
   

    //shooting
     IEnumerator WaiterBeforeShoot(float valueforce)
    {
        
       if (alreadyAttacked == false)
        {
            
            //position de l'ennemi
            Vector3 playerPos = new Vector3(this.transform.position.x, this.transform.position.y + 0.3f, this.transform.position.z);
           //on utilise this.transform.forward qui nous donne la direction de l'ennemi
            Vector3 playerDirection = this.transform.forward;
            //on utilise  this.transform.rotation  qui nous donne la rotation de l'ennemi
            Quaternion playerRotation = this.transform.rotation;
            //distance à partir duquel la balle se crée par rapport à l'ennemi
            float spawnDistance = 0.3f;

            Vector3 spawnPos = playerPos + playerDirection * spawnDistance;
     
          
            //timer permettant que l'animation se deroule completement
            diff = Time.time - timerAnimator;
            
            if (diff > 3)
            {
               
                timerAnimator = Time.time;
                animator = this.GetComponent<Animator>();
                animator.Play("magicattack");

                yield return new WaitForSeconds(1);
                Ball cb = Instantiate(projectileEnnemi,spawnPos  , Quaternion.identity);
                //position de depart de l'ennemi
                Ball.positiondepart = cb.transform.position;
                rig =  cb.GetComponent<Rigidbody>();
                
                rig.AddForce((this.transform.forward)* valueforce);
               
                alreadyAttacked = true;
                yield return new WaitForSeconds(2);


                alreadyAttacked = false;
            }
        }
        }
    
    void ShootBullet(float valueForce)
    {
        StartCoroutine(WaiterBeforeShoot(valueForce));
        


    }



    void letreset()
    {

        //alreadyAttacked = false;
        diff = Time.time - timer;
       // inputtext.text = diff.ToString();
        if( diff > 2)
        {
            alreadyAttacked = false;
            diff = 0;
         //   Destroy(cb);
        }
        
    
    }

  

    private void lookPlayer()
    {
        
        transform.LookAt(player.transform);

       // animator = transform.GetComponent<Animator>();
       // animator.Play("dance");
    }

    void Patroling()
    {

        animator = transform.GetComponent<Animator>();
        animator.Play("dance");

    }

    //damage
    public void damageattack(float damage)
    {

       
        if (life <= 0)
        {

            StartCoroutine(WaiterBeforedie(damage));

        }
        else
        {

            StartCoroutine(WaiterBeforedamage(damage));
        }
    }

    IEnumerator WaiterBeforedamage(float damage)
    {

        animator = transform.GetComponent<Animator>();
        animator.Play("behitted2");
        this.life -= damage;
        yield return new WaitForSeconds(3f);

        






    }

    IEnumerator WaiterBeforedie(float damage)
    {


        animator = this.GetComponent<Animator>();
        animator.Play("diee2");

        yield return new WaitForSeconds(2);

        Destroy(this.gameObject);


    }
    private void Start()
    {
       // inputtext = GameObject.Find("input").GetComponent<TMP_InputField>();
        // inputsecond = GameObject.Find("inputsc").GetComponent<TMP_InputField>();
       
    }
    private void OnCollisionEnter(Collision collision)
    {
        //Destroy(this.gameObject);
        if (collision.transform.tag == "ball" )
        {
            //  inputtext.text = "collsion ennemi";
               animator = transform.GetComponent<Animator>();
               animator.Play("behitted2");

          //  damageattack(0.2f);
            Destroy(collision.gameObject);
           
           
        }
    

    }

}
