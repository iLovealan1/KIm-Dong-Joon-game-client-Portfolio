using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LunaBurger.Playable010
{
    public class PickupManager : MonoBehaviour, IUpgradeHandler
    {
        [SerializeField] private int[]                  _pickUpPriceArr = {40,  80, 140, 200};
        
        [SerializeField] private List<PickupController> _pickupList;  
        [SerializeField] private List<GameObject>       _imgLineList;
        [SerializeField] private AnimationCurve         _burgerAnimCurve;
        [SerializeField] private AnimationCurve         _burgerMoveAnimCurve;
        [SerializeField] private float                  _timer;

        public static System.Func<eUnitLevel>                              OnRequestUpgrade          {get; private set;}
        public static System.Func<eUnitLevel,int>                          OnReciveUpgradePrice      {get; private set;}
        public static System.Func<Transform>                               OnRequestWaitLineSpot     {get; private set;}
        public static System.Func<eCustomerLevel,WaitLineSpot,Transform>   OnRequestMidLineSpot      {get; private set;}
        public static System.Action<int,int,System.Action<eUnitAnimState>> OnBurgerCustomerToManager {get; private set;} // 2번째 인자는 손님이 보낸 픽업객체 해시
        public static System.Action<Stack<Transform>,CustomerController,System.Action<eUnitAnimState>> onBurgerPickupToManager {get; private set;}  
        private const float WAITTIME = 1.5f;
        
        public void Init()
        {
            OnRequestUpgrade = () =>{
                SoundManager.NullableInstance.OnPlaySFX(eSoundName.UPGRADE);
                var level = Upgrade();
                return level;
            };

            OnReciveUpgradePrice = (unitLevel) => {
                var level = (int)unitLevel;
                var price = _pickUpPriceArr[level];
                return price;   
            };

            OnRequestWaitLineSpot = () =>{
                var spoTrans = FindWaitLineSpotForCustomer();
                return spoTrans;
            };

            OnRequestMidLineSpot = (customerLevel,waitLineSpotComp) =>{
                var waitLineTrans = FindMidLineSpotForCustomer(customerLevel,waitLineSpotComp);
                return waitLineTrans;
            };

            OnBurgerCustomerToManager = (hash,burgerAmount,OnChangeCustomerAnim) => {
                StartCoroutine(CoCheckPickupHashAndAvailable(hash,burgerAmount,OnChangeCustomerAnim));          
            };
            
            onBurgerPickupToManager = (burgerTransStack,CustomerComp,workerAnimChange) =>{
                StartCoroutine(CoSendBurgerToCustomer(burgerTransStack, CustomerComp ,workerAnimChange));
            };

            _pickupList.ForEach(controller => controller.Init());    
        }

        public eUnitLevel Upgrade()
        {
            eUnitLevel currLevel = eUnitLevel.NONE;
            foreach (var pickup in _pickupList)
            {
                if(!pickup.gameObject.activeSelf)
                {
                    pickup.gameObject.SetActive(true);
                    currLevel = (eUnitLevel)pickup.UnitLevel;
                    pickup.PlayUpgradeParticles();
                    StartCoroutine(CoCeckPickupAvailable(pickup));
                    break;
                } 
            } 

            _imgLineList[(int)currLevel].SetActive(true);
            return currLevel;        
        }

        private IEnumerator CoCeckPickupAvailable(PickupController pickup)
        {
            yield return CoroutineUtil.WaitForSeconds(WAITTIME);
            pickup.IsWaitAvailable = true;  
            pickup.IsPickupAvaiable = true;
            pickup._footParticle.Play();
        }

        private Transform FindWaitLineSpotForCustomer()
        {
            Transform nullableWaitLineSpot = null;
            var transList = new List<Transform>(); 

            foreach(var pickup in _pickupList)
            {
                if(pickup.gameObject.activeSelf || pickup.UnitLevel == 1)
                {
                    var emptySpotTrans = pickup.FindEmptySpot();
                    transList.Add(emptySpotTrans);
                }
            }

            nullableWaitLineSpot = CompareWeight(transList);   
            return nullableWaitLineSpot;
        }
        
        private Transform FindMidLineSpotForCustomer(eCustomerLevel customerLevel, WaitLineSpot waitLineSpotComp)
        {
            Transform nullableWaitLineSpot = null;
            var idx = (int)customerLevel;
            nullableWaitLineSpot = _pickupList[idx - 1].FindMidEmptySpot(waitLineSpotComp);        
            return nullableWaitLineSpot;
        }

        private Transform CompareWeight(List<Transform> transList)
        {
            Transform finalSpot = null;
            var minWeight = 0;
            var defaultTransCnt = 1;
            var count = transList.Count;

            foreach (var spotTrans in transList)
            {
                if(count == defaultTransCnt)
                {
                    finalSpot = spotTrans;
                    return finalSpot;
                }              

                if(spotTrans != null)
                {
                    var comp = spotTrans.GetComponent<WaitLineSpot>(); 
                    if(minWeight == 0) 
                    {
                        minWeight = comp.Weight;
                        finalSpot = spotTrans;
                    }
                    else if(minWeight > comp.Weight)
                    {                      
                        minWeight = comp.Weight;
                        finalSpot = spotTrans;      
                    }
                    else continue;
                }
            }   
            return finalSpot;
        }

        private IEnumerator CoCheckPickupHashAndAvailable(int hashCode,int amount, System.Action<eUnitAnimState> OnChangeCustomerAnim)
        {
            var target = hashCode;
    
            while(true)
            {
                foreach (var pickUp in _pickupList)
                {
                    var hash = pickUp.transform.GetHashCode();
                    if(hash == target && pickUp.gameObject.activeSelf)
                    {
                        pickUp.OnBurgerCheck(amount);
                        OnChangeCustomerAnim(eUnitAnimState.STACKIDLE);
                        yield break;
                    } 
                }

                yield return CoroutineUtil.WaitForSeconds(0.1f);
            }       
        }

        private IEnumerator CoSendBurgerToCustomer(Stack<Transform> burgerTransStack, CustomerController customerComp, System.Action<eUnitAnimState> workerAnimChange)
        {
            var isBurgerLeft = 0 == burgerTransStack.Count;         
            if(isBurgerLeft) 
            {
                workerAnimChange(eUnitAnimState.IDLE);
                customerComp.OnGoBackToHome();
                yield break;
            }
            
            var burgerTrans = burgerTransStack.Pop();
            var targetTrans = customerComp._customerHandTrans;
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
            burgerTrans.localRotation = Quaternion.identity;    
            StartCoroutine(CoSendBurgerToCustomer(burgerTransStack,customerComp,workerAnimChange));
            yield return null;
        }

        private void OnDestroy() 
        {
            OnRequestUpgrade = null;
            OnRequestWaitLineSpot= null;
            OnBurgerCustomerToManager = null;
            onBurgerPickupToManager = null;
        }

        public void Upgrade(eUnitLevel level)
        {
        }
    }
}

