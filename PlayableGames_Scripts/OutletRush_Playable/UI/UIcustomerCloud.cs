using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIcustomerCloud : MonoBehaviour
{    
    [SerializeField] private List<Sprite> _iconList = null;
    [SerializeField] private Image _imgIcon = null;

    private void Awake()
    {
        ChangeRanIcon();
        this.gameObject.SetActive(false);
    }
    
    private void OnDisable() => ChangeRanIcon();
    
    private void ChangeRanIcon()
    {
        if (_iconList.Count != 0)
        {
            var ran = UnityEngine.Random.Range(0,_iconList.Count);
            _imgIcon.sprite = _iconList[ran];
        }
    }
}
