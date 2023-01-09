using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Player :  MonoBehaviour, IPointerUpHandler, IDragHandler
{
    public FixedJoystick _joystick;
    public  Rigidbody playerBody;

    private Vector3 inputvector;
    private float moveSpeed = 0.0015f;
    //mettre les attribut : e qui permet de caractériser les objets
    private Animator animator;
        private String name = "ali";
        private double life = 5;
        private double attack = 1;



    //  public float timer = 1000000;
    public static bool isCheckIdle = false;
    private bool isCollision = false;
    private  bool isAttack= false; 
    private bool isCheckDie = false;
    private bool isCheckBehitted = false;

    //button
    private Button btn;




    //timer
    // private TMP_InputField inputtext;
    //private TMP_InputField inputsecond;

    private void Start()
    {

        // inputtext = GameObject.Find("input").GetComponent<TMP_InputField>();
        // inputsecond = GameObject.Find("inputsc").GetComponent<TMP_InputField>();
       

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
           //  this.isAttack = sool;
             this.isCheckBehitted = sool;
        }

    public bool getIsAttack()
    {
     // return  this.isAttack ;
      return this.isCheckBehitted;
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



     //**********************************************move et damage ***************************************************


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



        // inputtext.text = "life   " + this.life.ToString();
        //isCheckIdle = true;
        isCheckBehitted = true;
        animator = this.GetComponent<Animator>();
        animator.Play("behitted");
        yield return new WaitForSeconds(5f);
        //isCheckIdle = false;
        this.life -= damage;
        isCheckBehitted = false;




    }

    IEnumerator WaiterBeforedie(float damage)
    {

        isCheckDie = true;
        animator = this.GetComponent<Animator>();
        animator.Play("dead");
      
        yield return new WaitForSeconds(5f);
      

        Destroy(this.gameObject);
    }



   
    //****moovement****

    float timerIdle = -1000;
    float diffIdle = 0;
    void UpdateMoov()
    {
        float horizontal = _joystick.Horizontal;
        float vertical = _joystick.Vertical;

      
            if ((horizontal == 0) && (vertical == 0))
            {

                //if ((isCheckIdle == false))
                if ((isCheckIdle == false) && (isCheckBehitted == false))
                {
                    animator = this.GetComponent<Animator>();
                    animator.Play("idle");
                    diffIdle = 0;
                    timerIdle = Time.time;

                }


            }
            else
            {

                if (Mathf.Abs(_joystick.Horizontal) > 0.5f && Mathf.Abs(vertical) < 0.5f)
                {
                    animator = this.GetComponent<Animator>();
                    animator.Play("run");
                    moveSpeed = 0.0015f;

                }
                else if (Mathf.Abs(horizontal) < 0.5f && Mathf.Abs(vertical) > 0.5f)
                {
                    animator = this.GetComponent<Animator>();
                    animator.Play("run");
                    moveSpeed = 0.0020f;

                }
                else
                {
                    animator = this.GetComponent<Animator>();
                    animator.Play("walk");
                    moveSpeed = 0.0015f;

                }

            }

        
            //pour le deplacement du joueur
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

   
    private void Update()
    {
       if(isCheckDie == false)
        {
            UpdateMoov();
        }
     
    }

    //*******************************collision****************************

    void OnCollisionEnter(Collision collision)
    {


        if (collision.transform.tag == "ball")
        {
         
            Destroy(collision.gameObject);
            damageattack(1f);


        }

    }

   

    public void OnDrag(PointerEventData eventData)
    {
        // UpdateMoov();
       // animator = this.GetComponent<Animator>();
       
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // throw new NotImplementedException();
        // UpdateMoov();
            animator = this.GetComponent<Animator>();
            animator.Play("idle");
           // diffIdle = 0;
           // timerIdle = Time.time;
            //  isCheckIdle = false;

        
    }


    ///*********************************************Attack **************************************************************

   




    // Start is called before the first frame update

}
