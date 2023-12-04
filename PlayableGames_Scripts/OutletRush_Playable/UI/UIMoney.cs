using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace luna_sportshop.Playable002
{
    public class UIMoney : MonoBehaviour
    {
        [Header("=====UIMoney Fields=====")]
        [Space] 
        [SerializeField] private TextMeshProUGUI _txtMoney = null;

        //===============================================================
        //Functions
        //===============================================================
        private void Start()
        {
            var currentMoney = MoneyManager.CurrentMoney;
            _txtMoney.text = currentMoney.ToString();
        }

        public void UpdateTextMoney()
        {
            var currentMoney = MoneyManager.CurrentMoney;
            _txtMoney.text =  currentMoney.ToString();
        }
    }
}

