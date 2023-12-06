using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Supercent.Util;

namespace LunaBurger.Playable010
{
    public enum eCustomerState // 손님 이동 상태
    {
        NONE = -1,
        SPAWNTOSPOT, 
        COUNTERMIDLINESPOT, 
        COUNTERORDERSPOT, 
        COUNERTOCONNECTOR,
        CONNECTORTOPICKUP,
        PICKUPMIDSPOT, 
        PICKUPORDERSPOT,
        BACKTOHOME,
    }
    public enum eCustomerLevel
    {
        NONE = 0,
        LEVEL1 = 1,
        LEVEL2 = 2,
        LEVEL3 = 3,
        LEVEL4 = 4,
    }
    
    public class CustomerManager : MonoBehaviour
    {
        [SerializeField] private float           _customerSpeed;
        [SerializeField] private float           _customerSpawnTime;
        [SerializeField] private List<Transform> _customerSpawnPointList;
        [SerializeField] private List<Transform> _customerRetrunPointList;

        private float _defaultTimeLimit = 3f;
        private float _midLineTimeLimit = 1f;
        private float _minDist          = 0.5f;
        System.Random _rand             = new System.Random();  

        public static System.Action<CustomerController> OnChangePosition {get; private set;}
        public static System.Action<eCustomerLevel>     OnSpawnCustomer  {get; private set;}

        public void Init()
        {
            OnChangePosition = (customerComp) =>  {
                StartCoroutine(SetupByStateAndMoveCustomer(customerComp));
            }; 

            OnSpawnCustomer = (level) => {
                StartCoroutine(CoSpawnCustomer(level));
            };
            StartCoroutine(CoSpawnCustomer());
        }

        private IEnumerator CoSpawnCustomer(eCustomerLevel level = eCustomerLevel.LEVEL1)
        {           
            while (true)
            {
                var ranTime = UnityEngine.Random.Range(0f, 1.0f);
                yield return CoroutineUtil.WaitForSeconds(_customerSpawnTime+ranTime);

                var customer = ObjectPoolManager.NullableInstance.GetObjFromPool(ePrefabType.CUSTOMER);
                var customerComp = customer.GetComponent<CustomerController>(); // 런타임에서 X
                var posIdx = (int)level - 1;
                var spawnPos = _customerSpawnPointList[posIdx].position;

                customerComp.Level = level; 
                customer.transform.position = spawnPos;  
                customerComp.CustomerBurgerAmount = _rand.Next(1,4);          
                customerComp._state = eCustomerState.SPAWNTOSPOT;     
                
                StartCoroutine(SetupByStateAndMoveCustomer(customerComp));               
            }                        
        }

        private IEnumerator SetupByStateAndMoveCustomer(CustomerController customerComp)
        {
            var moveTimeLimit = 0f;
            var isFull = false;

            switch (customerComp._state)
            {
                case eCustomerState.SPAWNTOSPOT:               
                isFull = SetupSpawnToSpot(customerComp);
                if (isFull)
                    break;
                else
                    moveTimeLimit = customerComp.MoveTimeLimit;
                break;

                case eCustomerState.COUNTERMIDLINESPOT:    
                yield return StartCoroutine(CoSetupCounterMidlineSpot(customerComp));
                moveTimeLimit = customerComp.MoveTimeLimit;
                break;

                case eCustomerState.COUNTERORDERSPOT:
                yield return StartCoroutine(CoSetupCounterOrderSpot(customerComp));
                moveTimeLimit = customerComp.MoveTimeLimit;
                break;

                case eCustomerState.PICKUPMIDSPOT:
                yield return StartCoroutine(CoSetupPickupMidSpot(customerComp));
                moveTimeLimit = customerComp.MoveTimeLimit;
                break;

                case eCustomerState.BACKTOHOME:
                StartCoroutine(CoBackToHome(customerComp)); 
                yield break;
            }
            
            if (!isFull)
                StartCoroutine(CoMoveCustomerToSpot(customerComp,moveTimeLimit));
        } 

        private bool SetupSpawnToSpot(CustomerController customerComp)
        {
            var targetSpotTrans = CounterManager.OnRequestWaitLineSpot(customerComp.Level);
            var isFull = targetSpotTrans == null;
            if (isFull) 
            {
                Debug.Log($"진입 타겟 스팟컴프 상태 : {!isFull} 풀 객체를 반환합니다.");
                ObjectPoolManager.NullableInstance.RetrunObject(customerComp.gameObject);
                return isFull;
            }
            var targetComp = targetSpotTrans.GetComponent<WaitLineSpot>();   
            var targetWeight = targetComp.Weight;      
     
            customerComp.transform.SetParent(targetSpotTrans);
            customerComp.MoveTimeLimit = _defaultTimeLimit; 

            // 스폰 후 첫번쨰 분기
            var nextState = eCustomerState.NONE; 

            if(1 == targetComp.Weight) nextState = eCustomerState.COUNTERORDERSPOT;  
            else nextState = eCustomerState.COUNTERMIDLINESPOT;    

            customerComp._state = nextState;
            customerComp.TargetSpotComp = targetComp;  
            return isFull;           
        }

        private IEnumerator CoSetupCounterMidlineSpot(CustomerController customerComp)
        {
            var moveTimeLimit = _midLineTimeLimit;
            var nextState = eCustomerState.NONE;
            Transform targetTrans = null;

            while(true)
            {
                targetTrans = CounterManager.OnRequestMidLineSpot(customerComp.Level,customerComp.CurrSpotComp);   
                if(targetTrans != null)
                {
                    var targetComp = targetTrans.GetComponent<WaitLineSpot>();
                    customerComp.transform.SetParent(targetTrans);     
                    // 두번째 분기
                    if(targetComp.Weight == 1) nextState = eCustomerState.COUNTERORDERSPOT;  
                    else nextState = eCustomerState.COUNTERMIDLINESPOT;

                    customerComp._state = nextState;  
                    customerComp.TargetSpotComp = targetComp;   
                    customerComp.MoveTimeLimit = moveTimeLimit; 
                    yield break; 
                }              
                yield return null;
            }          
        }

        private IEnumerator CoSetupCounterOrderSpot(CustomerController customerComp)// 리펙토링 필요
        {
            var nextState = eCustomerState.NONE;
            var moveTimeLimit = 0f;
            while(true)
            {           
                var targetTrans = PickupManager.OnRequestWaitLineSpot();
                if(targetTrans == null) yield return CoroutineUtil.WaitForSeconds(0.1f);
                else
                {
                    customerComp.transform.SetParent(targetTrans);
                    var targetComp = targetTrans.GetComponent<WaitLineSpot>();
                    var tergetParentLevel = targetComp.ParentLevel;
                    var targetWeight = targetComp.Weight;
                    customerComp.TargetSpotComp = targetComp;
                    if(tergetParentLevel == eUnitLevel.LEVEL1)
                    {
                        if(targetWeight == 1)
                        {
                            nextState = eCustomerState.PICKUPORDERSPOT;
                            moveTimeLimit = 3f;
                        }
                        else
                        {
                            nextState = eCustomerState.PICKUPMIDSPOT;
                            moveTimeLimit = 3.5f;
                        }                        
                        customerComp._state = nextState;   
                        customerComp.MoveTimeLimit = moveTimeLimit;   
                        yield break;
                    }
                    else if(tergetParentLevel == eUnitLevel.LEVEL2)
                    {
                        if(targetWeight == 1)
                        {
                            nextState = eCustomerState.PICKUPORDERSPOT;
                            moveTimeLimit = 3f;
                        }
                        else
                        {
                            nextState = eCustomerState.PICKUPMIDSPOT;
                            moveTimeLimit = 3f;

                        }                        
                        customerComp._state = nextState;   
                        customerComp.MoveTimeLimit = moveTimeLimit;   
                        yield break;
                    }
                    else if (tergetParentLevel == eUnitLevel.LEVEL3)
                    {
                        if(targetWeight == 1)
                        {
                            nextState = eCustomerState.PICKUPORDERSPOT;
                            moveTimeLimit = 2.5f;
                        }
                        else
                        {
                            nextState = eCustomerState.PICKUPMIDSPOT;
                            moveTimeLimit = 3f;

                        }                        
                        customerComp._state = nextState; 
                        customerComp.MoveTimeLimit = moveTimeLimit;   
                        yield break;
                    }
                    else if(tergetParentLevel == eUnitLevel.LEVEL4)
                    {
                        if(targetWeight == 1)
                        {
                            nextState = eCustomerState.PICKUPORDERSPOT;
                            moveTimeLimit = 3f;
                        }
                        else
                        {
                            nextState = eCustomerState.PICKUPMIDSPOT;
                            moveTimeLimit = 3.5f;

                        }                        
                        customerComp._state = nextState; 
                        customerComp.MoveTimeLimit = moveTimeLimit;   
                        yield break;
                    }
                }                 
            } 
        }

        private IEnumerator CoSetupPickupMidSpot(CustomerController customerComp)
        {
            var moveTimeLimit = _midLineTimeLimit;
            var nextState = eCustomerState.NONE;
            var customerLevel = (int)customerComp.CurrSpotComp.ParentLevel;
            var currLevel = (eCustomerLevel)customerLevel;
            Transform targetTrans = null;

            while(true)
            {
                targetTrans = PickupManager.OnRequestMidLineSpot(currLevel,customerComp.CurrSpotComp);   
                if(targetTrans != null)
                {
                    customerComp.transform.SetParent(targetTrans);
                    var targetComp = targetTrans.GetComponent<WaitLineSpot>();
                    if(targetComp.Weight == 1) nextState = eCustomerState.PICKUPORDERSPOT;  
                    else nextState = eCustomerState.PICKUPMIDSPOT;
                    customerComp._state = nextState;  
                    customerComp.TargetSpotComp = targetComp;   
                    yield break; 
                }              
                yield return null;
            }          
        }

        //이동로직 
        private IEnumerator CoMoveCustomerToSpot(CustomerController customerComp, float moveTimeLimit)
        {
            var customerTrans = customerComp.transform;
            var targetComp = customerComp.TargetSpotComp;
            var startPos = customerTrans.position;
            var endPos = targetComp.transform.position;
            var dir = (endPos - startPos).normalized; 
            var dist = Vector3.Distance(startPos, endPos); 
            var timer = 0f;     

            customerTrans.LookAt(endPos);
            customerComp.OnChangeAnimState(eUnitAnimState.RUN);   

            while(true)
            {
                dist = Vector3.Distance(customerTrans.position,endPos);         
                timer += Time.fixedDeltaTime;
                if(dist < _minDist || timer > moveTimeLimit) 
                {
                    customerTrans.position = endPos;
                    customerComp.CurrSpotComp = targetComp; 
                    customerTrans.localRotation = Quaternion.Euler(Vector3.zero); 
                    customerComp.OnChangeAnimState(eUnitAnimState.IDLE);
                    customerComp.OnNextMove();
                    yield break;
                }
                customerTrans.Translate(dir * _customerSpeed * Time.fixedDeltaTime,Space.World);
                yield return CoroutineUtil.WaitForFixedUpdate;
            }
        }

        private IEnumerator CoBackToHome(CustomerController customerComp)
        {        
            var customerTrans = customerComp.transform;
            var startPos = customerComp.transform.position;
            var currLineInfo = customerComp.CurrSpotComp.ParentLevel;
            var endPos = _customerRetrunPointList[(int)currLineInfo-1].position;
            var dir = (endPos - startPos).normalized; 
            var dist = Vector3.Distance(startPos, endPos); 
            var moveTimeLimit = 4.0f;
            var timer = 0f;
            customerTrans.LookAt(endPos);
            customerTrans.SetParent(null);
            customerComp.OnChangeAnimState(eUnitAnimState.STACKRUN);   
            customerComp.PlayRanEmojiParticle();

            while(true)
            {
                dist = Vector3.Distance(customerTrans.position, endPos);         
                timer += Time.fixedDeltaTime;
                if(dist < _minDist || timer > moveTimeLimit) 
                {
                    customerTrans.position = endPos;
                    customerComp.OnNextMove();
                    yield break;
                }
                customerTrans.Translate(dir * _customerSpeed * Time.fixedDeltaTime,Space.World);
                yield return CoroutineUtil.WaitForFixedUpdate;
            }
        }
    }
}

