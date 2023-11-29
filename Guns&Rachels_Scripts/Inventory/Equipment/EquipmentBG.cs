using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipmentBG : MonoBehaviour, IPointerDownHandler
{
    [SerializeField]
    private GameObject EquipmentSelected;

    private UIInventory inventory;
    private Image imgBG;

    private void Awake()
    {
        //초기값 셋팅
        this.inventory = GameObject.FindObjectOfType<UIInventory>();
        
        this.EquipmentSelected.SetActive(false); 
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        var source = this.transform.GetComponentInParent<AudioSource>();
        AudioManager.instance.PlaySFXOneShot(AudioManager.eSFXMusicPlayList.UI_Click, source);  
        this.EquipmentSelected.SetActive(true);
        var EquipmentName = this.transform.GetChild(1).name;
        var hesh = this.transform.parent.GetHashCode();
        this.inventory.onSelected(EquipmentName, hesh);   
    }
}
