using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace LunaBurger.Playable010
{
    public class UICashController : MonoBehaviour
    {
        [SerializeField] private TMP_Text _txtCashAmount;

        public void Init()
        {
            var defaultCash = CashManager.Instance.CurrentCash.ToString(); 
            _txtCashAmount.text = defaultCash.ToString();
        }

        public void UpdateCashText()
        {
            var txtTotalCash = CashManager.Instance.CurrentCash;
            ChangeToKiloAndUpdateText(txtTotalCash);
        }

        private void ChangeToKiloAndUpdateText(int amount)
        {
            if (amount >= 10000)
            {
                float displayAmount = amount / 1000.0f;
                _txtCashAmount.text = displayAmount.ToString("F1") + "K";
            }
            else
            {
                _txtCashAmount.text = amount.ToString();
            }
        }
    }
}

