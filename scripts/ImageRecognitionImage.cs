using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using TMPro;
public class ImageRecognitionImage : MonoBehaviour
{
    // Start is called before the first frame update
    private ARTrackedImageManager aRTrackedImageManager;
    TMP_InputField inputtext;
    TMP_InputField inputsecond;


    private void Start()
    {
        inputtext = GameObject.Find("input").GetComponent<TMP_InputField>();
        inputsecond = GameObject.Find("inputsc").GetComponent<TMP_InputField>();
    }
    void Awake()
    {
        aRTrackedImageManager = FindObjectOfType<ARTrackedImageManager>();
    }

    public void OnEnable()
    {
        inputtext.text = "OnEnable";
        aRTrackedImageManager.trackedImagesChanged += OnImageChange;
    }
    public void OnDisable()
    {
        inputtext.text = "OnDisable";
        aRTrackedImageManager.trackedImagesChanged += OnImageChange;
    }

    private void OnImageChange(ARTrackedImagesChangedEventArgs obj)
    {
        inputtext.text = "OnImageChange";
        foreach (var  trackedImage in obj.added)
        {
            Debug.Log(trackedImage.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        inputtext.text = "Update";
    }
}
