using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    // Start is called before the first frame update
    //l'objet crée
    //private GameObject spawnedOject;
    private Player spawnedOject;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnedOject == null) {

            JoysticMovementControl.spawnedOject = spawnedOject;
        }
       
    }

   
}
