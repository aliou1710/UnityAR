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
    //ball
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

    //bloc
    public GameObject bloc;
    public static  GameObject blocObject;

    //boudaries
    private int boundaryXinf = -500;
    private int boundaryYinf = -500;
    private int boundaryXsup = 500;
    private int boundaryYsup = 500;

    private TMP_InputField inputsecond;
    void Start()
    {
        aRRaycastManager = FindObjectOfType<ARRaycastManager>();
        //inputtext = GameObject.Find("input").GetComponent<TMP_InputField>();
        inputsecond = GameObject.Find("inputscc").GetComponent<TMP_InputField>();
       // placementPosesec = GameObject.Find("PlacementIndic").GetComponent<Pose>();
        ennemi2s = new List<Ennemi2>();
        ennemi1s = new List<Ennemi1>();
     



    }

    // Update is called once per frame
   
    int countsec = 0;//compteur pour l'ennemi 2
    int countfs = 0;//compteur pour l'ennemi 1


    public Vector3 generateRandomPosition(Vector3 position,float radius)
    {
       // int radius = 1;
        Vector3 center = PlacementPose.position;
        Vector3 randomPos = center + Random.insideUnitSphere * radius;

        return randomPos;
    }
    void Update()
    {
        //player
        if (spawnedOject == null && placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
       {

            if (spawnedOject == null)
            {
                ARPlaceObject();
                //spawnedOject.gameObject.SetActive(false);
                //Destroy(spawnedOject.gameObject);
                JoysticMovementControl.spawnedOject = spawnedOject;
                Ennemi1.player = spawnedOject;
                Ennemi2.player = spawnedOject;
                CreationActionButton.player = spawnedOject;
            }

            // int radius = 1;
            //Vector3 center = PlacementPose.position;
            // Vector3 center = new Vector3(PlacementPose.position.x, 100, PlacementPose.position.z);
            // Vector3 randomPos = center + Random.insideUnitSphere * radius;
            if(firstEnnemi == null)
            {
                Vector3 vector_enone = generateRandomPosition(PlacementPose.position, 1);
                firstEnnemi = Instantiate(arObjectTOSpawnEnnemifirst, vector_enone, PlacementPose.rotation);
            }
            
            if(secEnnemi == null)
            {
                Vector3 vector_ensecond = generateRandomPosition(PlacementPose.position, 1.1f);
                secEnnemi = Instantiate(arObjectTOSpawnEnnemisec, vector_ensecond, PlacementPose.rotation);
            }
            
           
            
            inputsecond.text = PlacementPose.position.ToString();

            if (treasure == null) {
                //si le tresor n'a pas été instancié
                if (secEnnemi != null && firstEnnemi != null )
                {   
                    //si les deux ennemis ont été instancié , alors le tresor sera instancié aux alentours du second ennemi
                    Vector3 vector_treasure = generateRandomPosition(secEnnemi.transform.position, 0.6f);
                    treasure = Instantiate(treasurehint, vector_treasure, PlacementPose.rotation);
                } 
                else if (secEnnemi != null && firstEnnemi == null)
                {
                    //si les 1er  ennemis n'a ps  été instancié , alors le tresor sera instancié aux alentours du second ennemi
                    Vector3 vector_treasure = generateRandomPosition(secEnnemi.transform.position, 0.6f);
                    treasure = Instantiate(treasurehint, vector_treasure, PlacementPose.rotation);
                }
                else if(secEnnemi == null && firstEnnemi != null)
                {  
                    //si les 2eme  ennemis n'a ps  été instancié , alors le tresor sera instancié aux alentours du 1er ennemi
                    Vector3 vector_treasure = generateRandomPosition(firstEnnemi.transform.position, 0.6f);
                    treasure = Instantiate(treasurehint, vector_treasure, PlacementPose.rotation);
                }
            }
            // treasure = Instantiate(treasurehint, vector_ensecond, PlacementPose.rotation);
            Ennemi2.projectileEnnemi = projectile;
            CreationActionButton.projectileEnnemi = projectile;

            // blocObject = Instantiate(bloc, PlacementPose.position, PlacementPose.rotation);
            //position du bloc 
        }
        //ennemi 2
        /* else if (spawnedOject != null && secEnnemi == null && placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && countfs < nbreEnnemi)
          // if (secEnnemi == null && placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && countfs < nbreEnnemi)
         {
          //   ARPlaceObject();
               secEnnemi = Instantiate(arObjectTOSpawnEnnemisec, PlacementPose.position, PlacementPose.rotation);
               ennemi2s.Add(secEnnemi);
          //   spawnedOject=null;

                   secEnnemi = null;
                   countfs++;

           }*/
        //ennemi 1
        /*else if (spawnedOject != null  && firstEnnemi == null && placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && countfs < nbreEnnemi)
        {

           // firstEnnemi = Instantiate(arObjectTOSpawnEnnemifirst, PlacementPose.position, PlacementPose.rotation);
            //ennemi1s.Add(firstEnnemi);


            firstEnnemi = null;
            countfs++;




        }*/
        //projectile et treasure
        /*else if(spawnedOject != null && secEnnemi == null && treasure == null && placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && countfs == nbreEnnemi)
        
        {
            //float rx = Random.Range(boundaryXinf, boundaryXsup);
            //float ry = Random.Range(boundaryYinf, boundaryYsup);
            //Vector3 v = new Vector3(rx, spawnedOject.transform.position.y, ry);
            //treasure = Instantiate(treasurehint,v,Quaternion.identity) ;
            //treasure = Instantiate(treasurehint, PlacementPose.position, PlacementPose.rotation);

           // blocObject = Instantiate(bloc, PlacementPose.position, PlacementPose.rotation);
            //position du bloc 
            


            Ennemi2.projectileEnnemi = projectile;
            CreationActionButton.projectileEnnemi = projectile;
            //treasure = null;
            countfs++;




        }*/


        //position du bloc apres la condition(dans l'update)




        UpdatePlacementPose();
        UpdatePlacementIndicator();

    }

    public void boundariesMethod()
    {
    
        if (transform.position.x < boundaryXinf)
        {
            transform.position = new Vector3(boundaryXinf, transform.position.y, transform.position.z);
        }
        else if (transform.position.y < boundaryYinf)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, boundaryYinf);
        }

        if (transform.position.x > boundaryXsup)
        {
            transform.position = new Vector3(boundaryXsup, transform.position.y, transform.position.z);
        }
        else if (transform.position.y > boundaryYsup)
        {
            transform.position = new Vector3(boundaryYsup, transform.position.y, transform.position.z);
        }
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
