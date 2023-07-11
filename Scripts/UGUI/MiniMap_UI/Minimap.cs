using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Minimap : MonoBehaviour, IPointerDownHandler
{
    public System.Action onTouched;
    public void OnPointerDown(PointerEventData eventData)
    {
        this.onTouched();
    }
}
