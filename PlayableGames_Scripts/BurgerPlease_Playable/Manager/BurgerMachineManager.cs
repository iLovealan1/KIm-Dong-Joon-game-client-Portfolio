using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LunaBurger.Playable010
{
    public class BurgerMachineManager : MonoBehaviour
    {
        [SerializeField] private int[]                   _machinePriceArr = {0, 220,   0,   0};
        
        [SerializeField] private BurgerMachineController _burgerMachine;
        [SerializeField] private float                   _timer;
        [SerializeField] private AnimationCurve          _burgerAnimCurve;
        [SerializeField] private AnimationCurve          _burgerMoveAnimCurve;

        public static System.Func<int ,bool>                                         OnCheckBurgerEnough  {get; private set;}
        public static System.Func<int>                                               OnReciveUpgradePrice {get; private set;}
        public static System.Action<int, Transform, PickupController, System.Action> OnBurgerToManager    {get; private set;}

        public void Init()
        {
            _burgerMachine.Init();  

            OnCheckBurgerEnough = (amount) => {
                var currBurgerAmount = _burgerMachine.CurrBurgersCount;
                var isEnough = amount <= currBurgerAmount;
                return isEnough;
            };

            OnBurgerToManager = (amount, handPos,pickup,onBackToWork) => {   
                StartCoroutine(SetupBurgerForPickUp(amount,handPos,pickup,onBackToWork));                          
            };
            
            OnReciveUpgradePrice = () => {
                var price = _machinePriceArr[1];
                return price;   
            };
        }

        private IEnumerator SetupBurgerForPickUp(int amount, Transform handTrans, PickupController pickup, System.Action onBackToWork)
        {
            List<Transform> burgerTransList = null;

            while(true)
            {
                burgerTransList = _burgerMachine.GiveBurgerForManager(amount);   
                if(burgerTransList != null)
                {
                    burgerTransList.ForEach( trans => {trans.SetParent(null);});   
                    break;
                }
                yield return null;
            }

            pickup.OnChangeState(eUnitAnimState.STACKIDLE);
            StartCoroutine(CoSendBurgerToHands(burgerTransList,handTrans,onBackToWork));
        }

        private IEnumerator CoSendBurgerToHands(List<Transform> burgerTransList,Transform targetTrans, System.Action onBackToWork, int idx = 0)
        {
            var isBurgerLeft = idx == burgerTransList.Count;
            if(isBurgerLeft) 
            {
                onBackToWork();
                yield break;
            }

            var burgerTrans = burgerTransList[idx];

            var sec = _timer;
            var startSec = Time.time;
            var endSec = startSec + sec;
            var childCount = targetTrans.childCount;
            var startPos = burgerTrans.position;
            var targetPos = targetTrans.position;

            if(targetTrans.childCount != 0)
            {
                var lastPos = targetTrans.GetChild(childCount-1).position;
                targetPos = lastPos + Vector3.up * 0.6f;
            }

            while (Time.time < endSec)
            {
                var ratio = (Time.time - startSec) / sec;

                burgerTrans.position = Vector3.Lerp(startPos, targetPos, _burgerMoveAnimCurve.Evaluate(ratio));
                burgerTrans.position = burgerTrans.position + Vector3.up * _burgerAnimCurve.Evaluate(ratio);
                yield return null;
            }

            burgerTrans.position = targetPos;
            burgerTrans.SetParent(targetTrans);           
            ++idx;

            StartCoroutine(CoSendBurgerToHands(burgerTransList,targetTrans,onBackToWork,idx));
        }

        private void OnDestroy() 
        {
            OnBurgerToManager = null;
            OnCheckBurgerEnough = null;
            OnReciveUpgradePrice = null; 
        }
    }
}

