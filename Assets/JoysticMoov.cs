using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JoysticMoov : MonoBehaviour ,IPointerUpHandler, IDragHandler, IPointerDownHandler
{
    // Start is called before the first frame update
    private Image imgJoysticsBackground;
    private static Image imgJoystic;
    private Vector2 positionInput;
    //drag = trainer or glisser,tant qu'on tient qlq chose exple : drag est drop = glisser et poser
    //while dragging , we need to measure the values of the x and y coordinates

    void Start()
    {
        imgJoysticsBackground = GetComponent<Image>();
        imgJoystic = transform.GetChild(0).GetComponent<Image>();
    }
    public void OnDrag(PointerEventData eventData)
    {
        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(imgJoysticsBackground.rectTransform,eventData.position,eventData.pressEventCamera, out positionInput))
        {
            positionInput.x = positionInput.x / (imgJoysticsBackground.rectTransform.sizeDelta.x);
            positionInput.y = positionInput.y / (imgJoysticsBackground.rectTransform.sizeDelta.y);
        
        }
        //normalize movement 
        if( positionInput.magnitude > 1.0f)
        {
            positionInput = positionInput.normalized;
        }
        //joystic movement aroud joysticbackground
        imgJoystic.rectTransform.anchoredPosition = new Vector2(
            positionInput.x * (imgJoysticsBackground.rectTransform.sizeDelta.x / 3),
            positionInput.y * (imgJoysticsBackground.rectTransform.sizeDelta.y / 3)
            );
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //on veut faire en sorte  que , lorsqu'on lache le jostics, il revient  à la position zero
        positionInput = Vector2.zero;
        imgJoystic.rectTransform.anchoredPosition = Vector2.zero;
    }

    public float inputHorizontal()
    {
        if(positionInput.x != 0)
        {
            return positionInput.x;
        }
        else
        {
            return Input.GetAxis("Horizontal");
        }
    }

    public float inputVertical()
    {
        if (positionInput.y != 0)
        {
            return positionInput.y;
        }
        else
        {
            return Input.GetAxis("Vertical");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
