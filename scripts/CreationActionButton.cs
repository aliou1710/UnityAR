using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreationActionButton : MonoBehaviour
{
    // Start is called before the first frame update
    private Button btn;
    
    private Animator animator;
    public static Player player;
    public static Ball projectileEnnemi;
    public Rigidbody rig;
    void Start()
    {
        btn = GameObject.Find("Button01").GetComponent<Button>();
        btn.onClick.AddListener(KickFunction);
       // btnsecond = GameObject.Find("Button02").GetComponent<Button>();
        //btnsecond.onClick.AddListener(UppercutFunction);

    }

  

    public void KickFunction()
    {
        StartCoroutine(WaiterBeforedamageEnnemiKick());
    }

 

    IEnumerator WaiterBeforedamageEnnemiKick()
    {
        Player.isCheckIdle = true;
       

        //position de l'ennemi
        Vector3 playerPos = new Vector3(player.transform.position.x, player.transform.position.y + 0.3f, player.transform.position.z);
        //on utilise this.transform.forward qui nous donne la direction de l'ennemi
        Vector3 playerDirection = player.transform.forward;
        //on utilise  this.transform.rotation  qui nous donne la rotation de l'ennemi
        Quaternion playerRotation = player.transform.rotation;
        //distance à partir duquel la balle se crée par rapport à l'ennemi
        float spawnDistance = 0.3f;
        Vector3 spawnPos = playerPos + playerDirection * spawnDistance;
        animator = player.GetComponent<Animator>();
        animator.Play("attackpower");
        yield return new WaitForSeconds(1);
        Ball cb = Instantiate(projectileEnnemi, spawnPos, Quaternion.identity);
        cb.transform.tag = "ball";
        //position de depart de l'ennemi
        Ball.positiondepart = cb.transform.position;
        rig = cb.GetComponent<Rigidbody>();
        rig.AddForce((player.transform.forward) * 110f);
        yield return new WaitForSeconds(5);



        Player.isCheckIdle = false;

    }


    public void UppercutFunction()
    {
        player.SetIsAttack(true);
         animator = JoysticMovementControl.spawnedOject.GetComponent<Animator>();
         animator.Play("uppercut");

        StartCoroutine(WaiterBeforedamageEnnemiUppercut());
    }


    IEnumerator WaiterBeforedamageEnnemiUppercut()
    {
        Player.isCheckIdle = true;
        animator = JoysticMovementControl.spawnedOject.GetComponent<Animator>();
        animator.Play("uppercut");
        player.SetIsAttack(true);

        yield return new WaitForSeconds(5);

        // player.SetIsAttack(false);

        // Player.isCheckIdle = false;

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
