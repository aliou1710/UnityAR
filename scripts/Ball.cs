using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Ball : MonoBehaviour
{
    // Start is called before the first frame update
    
    private Animator animator;
    private TMP_InputField inputtext;
    private TMP_InputField inputsecond;
    private bool alreadyAttacked = false;
    private float timerAnimator = -1000;
    private float diff_Animator = 0;


  //  public Rigidbody rig;
  //  public Ball projectile;
  //  private  Ball spawnball;
  
    public static Ennemi2 ennemi;
    public static Vector3 positiondepart=new Vector3(0,0,0);
    Vector3 actualPosition  = new Vector3(0, 0, 0);
    void Start()
    {
       
       
        actualPosition = this.transform.position;
      
    }

    // Update is called once per frame
    void Update()
    {
        

        //on supprime la balle après une certaine distance 700 par rapport au point de depart  
        actualPosition = this.transform.position;
        float difference = actualPosition.sqrMagnitude - positiondepart.sqrMagnitude;
        if ( (actualPosition.sqrMagnitude.ToString() != "5353249") && (actualPosition.sqrMagnitude != 0) &&  Mathf.Abs(difference)> 1000f)
        {
            Destroy(this.gameObject);
        }
     



    }

    private void OnCollisionEnter(Collision collision)
    {
        if( collision.transform.tag == "ball")
        {
            Destroy(this.gameObject);
            Destroy(collision.gameObject);
        }
    }



}
