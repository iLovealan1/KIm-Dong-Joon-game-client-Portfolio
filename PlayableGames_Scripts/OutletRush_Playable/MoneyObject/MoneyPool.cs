using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace luna_sportshop.Playable002
{
    public class MoneyPool : MonoBehaviour
    {
        [Header("=====Pool SerializeField=====")]
        [Space]
        [SerializeField] private Money      _moneyPrefab       = null;
        [SerializeField] private int        _defaultGenAmount  = 0;
  
        //===============================================================
        //Fields
        //===============================================================
        private Queue<Money> _moneyQ                           = null;

        //===============================================================
        //Functions
        //===============================================================
        private void Awake()
        {
            _moneyQ = new Queue<Money>();
            Init();
        }

        private void Init()
        {
            for (int i = 0; i < _defaultGenAmount; i++ )           
                GenerateMoney(_moneyPrefab, true);
        }

        private Money GenerateMoney(Money _moneyPrefab , bool isInit)
        {       
            var money = Instantiate(_moneyPrefab);

            if (isInit)
                _moneyQ.Enqueue(money);

            money.transform.parent = this.transform;
            money.OnRelease += RetrunMoney;
            money.gameObject.SetActive(false);

            return money;
        }

        public Money GetMoney()
        {
            Money money = null;

            if (_moneyQ.Count == 0)        
                money = GenerateMoney(_moneyPrefab, false);                              
            else        
                money = _moneyQ.Dequeue();
            
            money.gameObject.SetActive(true);   
            
            return money;
        }

        public void RetrunMoney(Money returnedMoney)
        {
            _moneyQ.Enqueue(returnedMoney);
            returnedMoney.transform.parent = this.transform;
            returnedMoney.gameObject.SetActive(false);
        }
    }
}


