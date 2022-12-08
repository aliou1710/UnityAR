using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;
public class PlacementPlayersAR : MonoBehaviour
{
    public Player arObjectToSpawn;
    //l'indicateur (le target qui montre où on doit placer le player )
    public GameObject placementIndicator;
    private Player spawnedOject;
    private Pose PlacementPose;
    private ARRaycastManager aRRaycastManager;
    private bool placementPoseIsValid = false;
    public Ball projectile;

    public Treasure treasurehint;
    private Treasure treasure =null;
    public int nbreEnnemi;
    // Start is called before the first frame update


    //TMP_InputField inputtext;
    //TMP_InputField inputsecond;

    //ennemi 2
    public Ennemi2 arObjectTOSpawnEnnemisec;
    private Ennemi2 secEnnemi = null ;
    public static List<Ennemi2> ennemi2s;

    //ennemi 1
    public Ennemi1 arObjectTOSpawnEnnemifirst;
    private Ennemi1 firstEnnemi = null;
    public static List<Ennemi1> ennemi1s;
    void Start()
    {
        aRRaycastManager = FindObjectOfType<ARRaycastManager>();
        //inputtext = GameObject.Find("input").GetComponent<TMP_InputField>();
        //inputsecond = GameObject.Find("inputsc").GetComponent<TMP_InputField>();
       // placementPosesec = GameObject.Find("PlacementIndic").GetComponent<Pose>();
        ennemi2s = new List<Ennemi2>();
        ennemi1s = new List<Ennemi1>();
     



    }

    // Update is called once per frame
   
    int countsec = 0;//compteur pour l'ennemi 2
    int countfs = 0;//compteur pour l'ennemi 1

    void Update()
    {
        
        if (spawnedOject == null && placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
       {
             ARPlaceObject();
            //spawnedOject.gameObject.SetActive(false);
           //Destroy(spawnedOject.gameObject);
            /*JoysticMovementControl.spawnedOject = spawnedOject;
            Ennemi1.player = spawnedOject;
            Ennemi2.player = spawnedOject;
            CreationActionButton.player = spawnedOject;*/
           


        

          }
          else if (spawnedOject != null && secEnnemi == null && placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && countfs < nbreEnnemi)
         // if (secEnnemi == null && placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && countfs < nbreEnnemi)
            {
         //   ARPlaceObject();
            secEnnemi = Instantiate(arObjectTOSpawnEnnemisec, PlacementPose.position, PlacementPose.rotation);
              ennemi2s.Add(secEnnemi);
         //   spawnedOject=null;

                  secEnnemi = null;
                  countfs++;




          }
        else if(spawnedOject != null && secEnnemi == null && treasure == null && placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && countfs == nbreEnnemi)
        
        {

            treasure = Instantiate(treasurehint, PlacementPose.position, PlacementPose.rotation);
            Ennemi2.projectileEnnemi = projectile;
            treasure = null;
            countfs++;




        }
        /* else if (spawnedOject != null && secEnnemi == null && firstEnnemi==null && placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && countfs < nbreEnnemi)
         {

             firstEnnemi = Instantiate(arObjectTOSpawnEnnemifirst, PlacementPose.position, PlacementPose.rotation);
             ennemi1s.Add(firstEnnemi);


             firstEnnemi = null;
             countfs++;




         }*/

        UpdatePlacementPose();
        UpdatePlacementIndicator();

    }
    void UpdatePlacementPose()
    {
        //ViewportToScreenPoint transforme la position dans lespace 3D en espace d'ecran 2D
        //  Debug.Log(Camera.current);
        var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));

        var hits = new List<ARRaycastHit>();
        aRRaycastManager.Raycast(screenCenter, hits, TrackableType.Planes);

        placementPoseIsValid = hits.Count > 0;
        if (placementPoseIsValid)
        {
            PlacementPose = hits[0].pose;
        }
    }

    void UpdatePlacementIndicator()
    {
        if (spawnedOject == null && placementPoseIsValid)
        {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(PlacementPose.position, PlacementPose.rotation);
        }
        else
        {
            placementIndicator.SetActive(false);
        }


    }
    //add a  method to place the AR Objet
    void ARPlaceObject()
    {

        spawnedOject = Instantiate(arObjectToSpawn, PlacementPose.position, PlacementPose.rotation);


    }



}
