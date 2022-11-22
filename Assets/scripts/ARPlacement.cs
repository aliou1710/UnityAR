using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class ARPlacement : MonoBehaviour
{
    //[SerializeField]
    //[Tooltip("Instantiates this prefab on a plane at the touch location.")]
    // c'est le gameobject qui represente l'arraigné ou autre element
    //public GameObject arObjectToSpawn;
    public Player arObjectToSpawn;


    //l'indicateur (le target qui montre où on doit placer le player )
    public GameObject placementIndicator;


    //l'objet crée
    //private GameObject spawnedOject;
    private Player spawnedOject;


    private Pose PlacementPose;
    private ARRaycastManager aRRaycastManager;
    private bool placementPoseIsValid = false;

    public GameObject joystickCanvas;
    /*
    //to delete
    GameObject m_PlacedPrefab;
    public GameObject placedPrefab
    {
        get { return m_PlacedPrefab; }
        set { m_PlacedPrefab = value; }
    }*/

    //public GameObject spawnedObject { get; private set; }

    /*private void Awake()
    {  
    }*/

    void Start()
    {
        aRRaycastManager = FindObjectOfType<ARRaycastManager>();
        joystickCanvas.SetActive(false);
    }

    // on a besoin de maj de l'indicateur de placement lorsqu'on bouge notre telephone
    void Update()
    {
      
        if( spawnedOject == null && placementPoseIsValid && Input.touchCount >0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            ARPlaceObject();

            JoysticMovementControl.spawnedOject = spawnedOject;
            EnnemiIA.spawnplayer = spawnedOject;

        }
        
        UpdatePlacementPose();
        UpdatePlacementIndicator();
        joystickCanvas.SetActive(true);

        
    }
   /* bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        if(Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }
        touc
    }
   */

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
