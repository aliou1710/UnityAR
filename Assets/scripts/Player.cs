using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Player :  MonoBehaviour, IEndDragHandler
{
    public FixedJoystick _joystick;
    public Rigidbody playerBody;

    private Vector3 inputvector;
    private float moveSpeed = 0.0015f;
    //mettre les attribut : e qui permet de caractériser les objets
    private Animator animator;
        private String name = "ali";
        private double life = 5;
        private double attack = 1;


      //  public float timer = 1000000;
        private bool isCollision = false;
        private  bool isAttack= false; 
    //timer
    private TMP_InputField inputtext;
    private TMP_InputField inputsecond;

    private void Start()
    {
        //playerBody = GetComponent<Rigidbody>();
       // inputtext = GameObject.Find("input").GetComponent<TMP_InputField>();
       // inputsecond = GameObject.Find("inputsc").GetComponent<TMP_InputField>();
    }
    void UpdateMoov()
    {

        animator = this.GetComponent<Animator>();
        float horizontal = _joystick.Horizontal;
        float vertical = _joystick.Vertical;
        if (horizontal == 0 && vertical==0)
        {
            animator = this.GetComponent<Animator>();
            animator.Play("idle");
        }
        else if (Mathf.Abs(_joystick.Horizontal) > 0.5f && Mathf.Abs(vertical) < 0.5f)
        {
            animator.Play("run");
            moveSpeed = 0.0015f;
        }
        else if (Mathf.Abs(horizontal) < 0.5f && Mathf.Abs(vertical) > 0.5f)
        {
            animator.Play("run");
            moveSpeed = 0.0020f;
        }
        else
        {
            animator.Play("walk");
            moveSpeed = 0.0015f;
        }
        var rigibody_ = GetComponent<Rigidbody>();
        // rigibody_.velocity = new Vector3(_joystick.Horizontal * movespeed,0, _joystick.Vertical * movespeed);
        playerBody.velocity = inputvector;
        inputvector = new Vector3(_joystick.Horizontal * 1f, 0, _joystick.Vertical * 1f);
        this.transform.LookAt(transform.position + new Vector3(inputvector.x, 0, inputvector.z));
        this.transform.position += new Vector3(rigibody_.velocity.x * moveSpeed, 0, rigibody_.velocity.z * moveSpeed);

        if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
        {
            transform.rotation = Quaternion.LookRotation(playerBody.velocity);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        animator = this.GetComponent<Animator>();
        animator.Play("idle");
    }
    private void Update()
        {

        

         UpdateMoov();
        //  UpdateMoov();
        /*   var diff = Time.time - timer;
          if ((diff > 1) && (isCollision))
          {
              this.life -= 1000;
             if (this.life <= 0 )
             {
             Destroy(this.gameObject);
             }    
 }*/

    }

        

    //constructeur
    public Player()
        {
            
        }


        public Player(String name, double attack, double life)
        {
            //cvd ce qu'on va entrer en paramètre dans ce constructeur correspondra au nouveau name , nouveau life et au nive attack de ce jnouveau joueur
            this.name = name;
            this.attack = attack;
            this.life = life;

        }


        public  void SetIsAttack(bool sool)
        {
             this.isAttack = sool;
        }

    public bool getIsAttack()
    {
      return  this.isAttack ;
    }


    //fonction pour modifier le nom et pour le recuperer:
    //getteur: permet de recuperer le nom et le setteur permet de le modifier 

    public String Getname()
        {
            return name;
        }

      
        
        // on cree une methode qui va permettre d'enlever les point de vie d'un joueur
      
   
    

    public  void Setname(string name)
    {
        this.name = name;
    }

    public  double Getlife()
    {
        return life;
    }

    public  void Setlife(double life)
    {
        this.life = life;
    }

    public  double Getattack()
    {
        return attack;
    }

    public  void Setattack(double attack)
    {
        this.attack = attack;
    }

    public  void damageattack(float damage)
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
       // inputtext.text = "life   " + this.life.ToString();
        animator = this.GetComponent<Animator>();
        animator.Play("behitted");
        yield return new WaitForSeconds(0.8f);
      





    }

    IEnumerator WaiterBeforedie(float damage)
    {


       
       
        animator = this.GetComponent<Animator>();
        animator.Play("dead");
        
        yield return new WaitForSeconds(3);

        Destroy(this.gameObject);





    }

    private void OnCollisionEnter(Collision collision)
    {


        if (collision.transform.tag == "Ennemi1" || collision.transform.tag == "Ennemi2")
        {
            animator = transform.GetComponent<Animator>();
            animator.Play("behitted");
           
          //  transform.position = collision.transform;
        }

        if (collision.transform.tag == "ball")
        {
            damageattack(0.2f);


        }

    }

  




    // Start is called before the first frame update

}
