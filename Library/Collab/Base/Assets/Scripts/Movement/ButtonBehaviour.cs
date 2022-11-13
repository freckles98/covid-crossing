using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonBehaviour : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
 
    bool isPressed;
    

    public bool IsPressed()
    {
        return isPressed;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;
        
    }
    

    public void OnPointerUp(PointerEventData eventData)
    {
        isPressed = false;
    }
}
