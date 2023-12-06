using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace LunaBurger.Playable010
{
    public class OrderBalloonManager : MonoBehaviour
    {
        [SerializeField] private List<Image>                _imgLoadingList;
        [SerializeField] private RectTransform[]            _loadingRectArr;
        [SerializeField] private Transform[]                _orderPosArr;

        public static System.Func<eCustomerLevel,Vector3,Image> OnRequestLoadingUI   {get; private set;}
        public static System.Action<eCustomerLevel>             OnResetLoadingUI     {get; private set;}     
        public static System.Action<eCustomerLevel>             OnChangeLoadingUIPos {get; private set;}

        public void Init()
        {
            OnRequestLoadingUI = (customerLevel, customerPos) => {
                var targetUI = SetUpForOrderLoading(customerLevel,customerPos);
                return targetUI;
            };

            OnResetLoadingUI = (customerLevel) =>{
                ResetOrderLoagdingUI(customerLevel);
            };

            var cnt = _imgLoadingList.Count;    

            for(int i = 0; i < cnt; i++)
            {
                var targetPos = _orderPosArr[i].position;
                var pos = Camera.main.WorldToScreenPoint(targetPos);
                var loadingRect = _loadingRectArr[i];
                loadingRect.position = pos; 
                loadingRect.gameObject.SetActive(false);              
            }
        } 

        private Image SetUpForOrderLoading(eCustomerLevel customerLevel, Vector3 customerPos)
        {
            var idx = (int)customerLevel - 1;
            var targetUI = _imgLoadingList[idx];
            var pos = Camera.main.WorldToScreenPoint(customerPos);
            var loadingRect = _loadingRectArr[idx];

            targetUI.transform.parent.gameObject.SetActive(true);  
            loadingRect.position = pos; 

            return targetUI;
        }

        private void ResetOrderLoagdingUI(eCustomerLevel customerLevel)
        {
            var idx = (int)customerLevel - 1;
            var targetUI = _imgLoadingList[idx];
            targetUI.fillAmount = 1;
            targetUI.transform.parent.gameObject.SetActive(false);  
        }
    }
}

