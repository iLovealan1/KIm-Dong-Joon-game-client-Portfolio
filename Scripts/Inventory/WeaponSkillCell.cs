using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class WeaponSkillCell : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    private UIWeaponCharacteristic uIWeaponCharacteristic;
 
    public void OnPointerDown(PointerEventData eventData)
    {
        var weaponSkillIcon = this.transform.GetChild(0);
        var skillName = weaponSkillIcon.GetComponent<Image>().sprite.name.Replace("(Clone)","");  
        Debug.Log(skillName);    
        this.uIWeaponCharacteristic.onSkillPopup(this.transform.localPosition, skillName);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        this.uIWeaponCharacteristic.onSkillPopupClose();
    }
}
