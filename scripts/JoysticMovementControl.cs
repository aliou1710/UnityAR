using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;


public class JoysticMovementControl : MonoBehaviour, IPointerUpHandler, IDragHandler,IPointerDownHandler, IEndDragHandler
{
    //public FixedJoystick joystick;
    public FixedJoystick joystick;

   
    //public Joystick joystick;
    public static Player spawnedOject;
    private Animator animator;
    private Rigidbody playerBody;
    private Vector3 inputvector;
    // Start is called before the first frame update
    // Start is called before the first frame update
    private RectTransform imgJoysticsBackground;
    
    private float moveSpeed =0;
     //drag = trainer or glisser,tant qu'on tient qlq chose exple : drag est drop = glisser et poser
    GameObject arObject;
    Vector3 moving;
    Vector3 Secondmoving;
    bool walking;
    //firt of all we need to click or touch the image to use the joystick

    private JoysticMoov mngJoystick;

    private Image imgBackgroundJoystics;
    private Image imgJoystics;
    //private Image imgJoystic;
    private Vector2 positionInput;

    float InputX ;
    float InputY ;

    private float time = -1;
    private float v = 0f;

    private bool isWalking = false;
    private bool isAttack1 = false;
    private bool isAttack2 = false;

    


    void Start()
    {
      // Player.joystick = joystick;



    }
   
   

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }


  
    void UpdateMoveJoystick()
    {
        //horizontal varie de -1 a 1  vertical aussi
        float horizontal = joystick.Horizontal;
        float vertical = joystick.Vertical;

      
        //rotation du personnage
        Vector3 v3 = new Vector3(horizontal * moveSpeed, spawnedOject.transform.rotation.x, vertical * moveSpeed) ; ;

        if (horizontal != 0 || vertical != 0)
        {
            spawnedOject.transform.rotation = Quaternion.LookRotation(v3);
        }
        //spawnedOject.transform.LookAt(spawnedOject.transform.position + new Vector3(v3.x, 0, v3.z));
       



        animator = spawnedOject.GetComponent<Animator>();
        

      if(Mathf.Abs(horizontal) > 0.5f && Mathf.Abs(vertical) < 0.5f)
        {
            animator.Play("run");
            moveSpeed = 0.8f;
        }
        else if (Mathf.Abs(horizontal) < 0.5f && Mathf.Abs(vertical) > 0.5f)
        {
            animator.Play("run");
            moveSpeed = 0.8f;
        }
        else {
            animator.Play("walk");
            moveSpeed = 0.3f;
        }

        //moovement en translation du personnage
        spawnedOject.transform.position = new Vector3(spawnedOject.transform.position.x + horizontal * moveSpeed * Time.deltaTime, spawnedOject.transform.position.y, spawnedOject.transform.position.z + vertical * 2f * Time.deltaTime);
       // spawnedOject.transform.position += new Vector3(spawnedOject.transform.position.x  * 0.0015F , 0, spawnedOject.transform.position.z*0.0015f);
       

    }





    //lorsqu'on tient le joycetics et qu'on le glisse
    public void OnDrag(PointerEventData eventData)
    {
        
     

        UpdateMoveJoystick();
      
    }
   
    
    
    public void OnEndDrag(PointerEventData eventData)
    {


        //isWalking = false;
        //animator = spawnedOject.GetComponent<Animator>();
        animator = spawnedOject.GetComponent<Animator>();
        animator.Play("idle");
    }



    public void OnPointerUp(PointerEventData eventData)
    {

        //  isWalking = false;
      
    }

   
    public void AttackButton01()
    {
        time = Time.time;

      

       // isAttack1 = true;
       // animator = spawnedOject.GetComponent<Animator>();
   //     animator.Play("attack1");
    }

    public void EndAttackButton1()
    {

    //    animator = spawnedOject.GetComponent<Animator>();
        isAttack1 = false;
      //  animator.Play("attack1");
   

    }
    public void EndAttackButton2()
    {
        
        isAttack2 = false;
     //   animator = spawnedOject.GetComponent<Animator>();
     //   animator.Play("attack2");

    }
    public void AttackButton02()
    {

      
        isAttack2 = true;
      //  animator = spawnedOject.GetComponent<Animator>();
      //  animator.Play("attack2");
    }


    // Update is called once per frame
    void Update()
    {
        
      /* 
        if (isAttack1)
        {
            animator.Play("attack1");
        }
        if (isAttack1)
        {
            animator.Play("attack2");
        }

*/


    
    }
}
