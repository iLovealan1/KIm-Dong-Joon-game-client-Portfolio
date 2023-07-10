using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ContentGrid : MonoBehaviour, IDragHandler
{
    [SerializeField]
    private UIInventory inventory;
    public void OnDrag(PointerEventData eventData)
    {
        this.inventory.onSetOffpopup();
        this.inventory.onSetOffSelect(0);
    }
}
