using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreationActionButton : MonoBehaviour
{
    // Start is called before the first frame update
    private Button btn;
    private Button btnsecond;
    private Animator animator;
    public static Player player;
    void Start()
    {
        btn = GameObject.Find("Button01").GetComponent<Button>();
        btn.onClick.AddListener(KickFunction);
        btnsecond = GameObject.Find("Button02").GetComponent<Button>();
        btnsecond.onClick.AddListener(UppercutFunction);

    }

    private void UppercutFunction()
    {
        animator = JoysticMovementControl.spawnedOject.GetComponent<Animator>();
        animator.Play("uppercut");
        player.SetIsAttack(true);
        //StartCoroutine(WaiterBeforedamageEnnemiUppercut());
    }

    private void KickFunction()
    {
        animator = JoysticMovementControl.spawnedOject.GetComponent<Animator>();
        animator.Play("kick");
        player.SetIsAttack(true);
        //StartCoroutine(WaiterBeforedamageEnnemiKick());
    }

    IEnumerator WaiterBeforedamageEnnemiUppercut()
    {

        animator = JoysticMovementControl.spawnedOject.GetComponent<Animator>();
        animator.Play("uppercut");
        player.SetIsAttack(true);

        yield return new WaitForSeconds(5);

        player.SetIsAttack(false);

    }

    IEnumerator WaiterBeforedamageEnnemiKick()
    {

        animator = JoysticMovementControl.spawnedOject.GetComponent<Animator>();
        animator.Play("kick");
        player.SetIsAttack(true);

        yield return new WaitForSeconds(5);

        player.SetIsAttack(false);

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
