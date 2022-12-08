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
    public static Ball projectileEnnemi;
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

    private float timerAnimator = 0;
    private float diff_Animator = 0;

    TMP_InputField inputtext;
    TMP_InputField inputsecond;
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

  

    //***IA****

    void checkPositionBetweenEnnemiAndPlayer()
    {
        //patrouille après une certaine distance
        if ((player.transform.position - transform.position).sqrMagnitude < 0.8 )
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

            lookPlayer();
            //ShootBullet(250f);
     
        }
        else if (isPlayerinSightRange && isPlayerInAttackRange)
        {

            lookPlayer();
          //  ShootBullet(100f);
        }

    }

  
   

    //shooting
     IEnumerator WaiterBeforeShoot(float valueforce)
    {
        if (alreadyAttacked == false)
        {
            Vector3 v3 = new Vector3(this.transform.position.x, this.transform.position.y + 0.3f, this.transform.position.z );



            animator = this.GetComponent<Animator>();
            animator.Play("magicattack");
            
            yield return new WaitForSeconds(1);
            //Ball cb = Instantiate(projectileEnnemi, , Quaternion.identity);
            Ball cb = Instantiate(projectileEnnemi,v3,this.transform.rotation);
            Rigidbody rig = cb.GetComponent<Rigidbody>();
            rig.AddForce(this.transform.forward * valueforce) ;
            // rig.AddForce(new Vector3(1000f, 0f ,0f));
            alreadyAttacked = true;
            yield return new WaitForSeconds(2);

            
            alreadyAttacked = false;
           
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
        inputtext.text = diff.ToString();
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


        this.life -= damage;
        yield return new WaitForSeconds(0.8f);






    }

    IEnumerator WaiterBeforedie(float damage)
    {




        animator = this.GetComponent<Animator>();
        animator.Play("diee2");

        yield return new WaitForSeconds(3);

        Destroy(this.gameObject);





    }
    private void Start()
    {
        inputtext = GameObject.Find("input").GetComponent<TMP_InputField>();
       // inputsecond = GameObject.Find("inputsc").GetComponent<TMP_InputField>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        //Destroy(this.gameObject);
        if (collision.transform.tag == "Player" )
        {
            inputtext.text = "collsion ennemi";
            animator = transform.GetComponent<Animator>();
            animator.Play("behitted2");

            if(player.getIsAttack() == true)
            {
                inputtext.text = "damage condition ennemi ";
                damageattack(0.2f);
                player.SetIsAttack(false);
            }
        }
    

    }

}
