using System.Collections.Generic;
using UnityEngine;

namespace LunaBurger.Playable010
{  
    public class CounterManager : MonoBehaviour, IUpgradeHandler
    {  
        [SerializeField] private List<CounterController> _counterList;
        [SerializeField] private int _burgerPrice;
        [SerializeField] private int[] _counterPriceArr                                      = {0,  60, 100, 180};

        public static System.Func<eUnitLevel>                            OnRequestUpgrade      {get; private set;}
        public static System.Func<eUnitLevel,int>                        OnReciveUpgradePrice  {get; private set;}
        public static System.Func<eCustomerLevel,Transform>              OnRequestWaitLineSpot {get; private set;}
        public static System.Func<eCustomerLevel,WaitLineSpot,Transform> OnRequestMidLineSpot  {get; private set;}
        public static System.Action<int, int>                            OnOrderBurger         {get; private set;}
        public static System.Action<eCustomerLevel>                      OnCounterChageState   {get; private set;}
            
        public void Init()
        {
            OnRequestUpgrade = () => {
                SoundManager.NullableInstance.OnPlaySFX(eSoundName.UPGRADE);
                var currUnitLevel = Upgrade();
                return currUnitLevel;
            };

            OnReciveUpgradePrice = (unitLevel) => {
                var price = _counterPriceArr[(int)unitLevel];
                return price;   
            };

            OnRequestWaitLineSpot = (CustomerLevel) =>{
                var waitLineTrans = FindWaitLineSpotForCustomer(CustomerLevel);
                return waitLineTrans;
            };

            OnCounterChageState = (level) =>{
                _counterList[(int)level - 1].ChangeAnimState(eUnitAnimState.WORK);
            };  

            OnRequestMidLineSpot = (customerLevel,waitLineSpotComp) =>{
                var waitLineTrans = FindMidLineSpotForCustomer(customerLevel,waitLineSpotComp);
                return waitLineTrans;
            };

            OnOrderBurger = (hash, amount) => {
                CompareHashAndOrder(hash, amount);
            };

            _counterList.ForEach(counter => counter.Init()); 
        }

        public eUnitLevel Upgrade()
        {
            eUnitLevel currLevel = eUnitLevel.NONE;
            foreach (var counter in _counterList)
            {
                if(!counter.gameObject.activeSelf)
                {
                    counter.gameObject.SetActive(true);
                    counter.PlayUpgradeParticles();
                    currLevel = (eUnitLevel)counter.UnitLevel;       
                    break;
                } 
            }
            return currLevel;  
        }

        private void CompareHashAndOrder(int hash, int amount)
        {
            foreach(var counter in _counterList)
            {
                var isSameHash =  hash == counter.transform.GetHashCode();
                if(isSameHash)
                {
                    var totlaMoney = amount * _burgerPrice;
                    counter.CollectMoney(totlaMoney);
                    break;
                }
            }
        }

        private Transform FindWaitLineSpotForCustomer(eCustomerLevel customerLevel)
        {
            Transform nullableWaitLineSpot = null;
            var idx = (int)customerLevel;
            nullableWaitLineSpot = _counterList[idx - 1].FindEmptySpot();

            return nullableWaitLineSpot;
        }

        private Transform FindMidLineSpotForCustomer(eCustomerLevel customerLevel, WaitLineSpot waitLineSpotComp)
        {
            Transform nullableWaitLineSpot = null;
            var idx = (int)customerLevel;
            nullableWaitLineSpot = _counterList[idx - 1].FindMidEmptySpot(waitLineSpotComp);        
            return nullableWaitLineSpot;
        }
        
        private void OnDestroy()
        {
            OnRequestWaitLineSpot = null;
            OnOrderBurger = null;
            OnRequestUpgrade = null;
        }
    }
}

