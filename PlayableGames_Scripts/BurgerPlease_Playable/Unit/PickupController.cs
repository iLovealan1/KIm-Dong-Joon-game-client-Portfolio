using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LunaBurger.Playable010
{
    public class PickupController : Unit
    {
        [SerializeField] private float            _workerSpeed = 10f;

        [SerializeField] private Transform        _burgerPickupTrans;
        [SerializeField] private Transform        _burgerBoxTrans;
        [SerializeField] private Transform        _workerTrans;
        [SerializeField] private Transform        _workerHandTrans;
        [SerializeField] public  ParticleSystem   _footParticle;
        [SerializeField] private ParticleSystem[] _upgradeParticleArr;

        public System.Action<int>            OnBurgerCheck    {get; private set;}
        public System.Action<eUnitAnimState> OnChangeState    {get; private set;} 
        public bool                          IsWaitAvailable  {get; set;}
        public bool                          IsPickupAvaiable {get; set;}

        private Vector3 PickupDir
        {
            get
            {
                var targetPos = _burgerPickupTrans.position;
                var startPos = _defaultPos;
                var dir = (targetPos - startPos).normalized;      
                return dir;              
            }
        } 

        private Vector3 WorkerSpotDir
        {
            get
            {
                var targetPos = _defaultPos;
                var startPos = _burgerPickupTrans.position;
                var dir = (targetPos - startPos).normalized;
                return dir;      
            }
        }

        private System.Action _onBackToWork;
        private Vector3       _defaultPos;     
        private int           _objectHash;
        private float         _timLimit       = 1.5f; 
        private float         _checkInterval  = 0.1f;
        private int           _spotCount;    
        private int           _lastWeight     = 1;
        private bool          _isCustormer;

        private const float   MINDIST         = 0.5f;

        public override void Init()
        {
            _defaultPos = _workerTrans.position;
            _objectHash = this.transform.GetHashCode();  
            _spotCount  = _lineSpotTransList.Count;  

            OnBurgerCheck = (amount) =>{
                StartCoroutine(CoCheckBurgerMachine(amount));
            };

            _onBackToWork = () => {
                StartCoroutine(CoGoBackToWorkerSpot());
            };

            OnChangeState = (state) => {
                ChangeState(state);
            };    

            if (_unitLevel == DEFAULTLEVEL)
            {
                IsPickupAvaiable = false;
                IsWaitAvailable = true;
            }
            else
            {
                IsPickupAvaiable = true;

            }

            var defaultIdx = 0;
            var spotComp = _lineSpotCompList[defaultIdx].GetComponent<WaitLineSpot>();
            spotComp.ParentObjTransHash = _objectHash;
        
            if(!_isDefaultUnit) 
                this.gameObject.SetActive(false); 
        }

        public void PlayUpgradeParticles()
        {
            foreach(var particle in _upgradeParticleArr)
            {
                particle.Play();
            }
        }

        private void ChangeState(eUnitAnimState state = eUnitAnimState.NONE)
        {
            switch(state)
                {
                    case eUnitAnimState.NONE:
                    _workerAnimComp.Play("Idle");   
                    Debug.LogError("입력된 state 값이 없습니다.");
                    break;
                    case eUnitAnimState.IDLE:
                    _workerAnimComp.Play("Idle");
                    break;
                    case eUnitAnimState.RUN:
                    _workerAnimComp.Play("player_run");
                    break;
                    case eUnitAnimState.STACKIDLE:
                    _workerAnimComp.Play("Stack_Idle");
                    break;
                    case eUnitAnimState.STACKRUN :
                    _workerAnimComp.Play("Stack_Run");
                    break;         
                }
        }

        private IEnumerator CoCheckBurgerMachine(int burgerAmount)
        {
            while(true)
            {
                var isEnough = BurgerMachineManager.OnCheckBurgerEnough(burgerAmount);
                if(isEnough && IsPickupAvaiable)
                {
                    StartCoroutine(CoMoveWorkerToPickup(burgerAmount));
                    yield break;    
                }
                yield return CoroutineUtil.WaitForSeconds(_checkInterval);
            }          
        }

        private IEnumerator CoMoveWorkerToPickup(int amount)
        {
            var startPos = _defaultPos;
            var targetPos = _burgerPickupTrans.position;
            _workerTrans.LookAt(targetPos);      
            _workerAnimComp.Play("player_run");
            var dist = Vector3.Distance(startPos,targetPos);
            var timer = 0f;
            yield return null;

            while(true)
            {
                dist = Vector3.Distance(_workerTrans.position,targetPos);
                timer += Time.fixedDeltaTime;
                if(dist <= MINDIST || timer >= _timLimit)
                {
                    _workerAnimComp.Play("Idle");
                    _workerTrans.LookAt(_burgerBoxTrans.position);
                    _workerTrans.position = targetPos;  
                    BurgerMachineManager.OnBurgerToManager(amount, _workerHandTrans, this, _onBackToWork);                    
                    yield break;
                }
                _workerTrans.Translate(PickupDir *_workerSpeed * Time.fixedDeltaTime,Space.World);  
                yield return CoroutineUtil.WaitForFixedUpdate;
            }
        }   

        private IEnumerator CoGoBackToWorkerSpot()
        {
            var startPos = _burgerPickupTrans.position;   
            var targetPos = _defaultPos;
            _workerTrans.LookAt(targetPos);      
            _workerAnimComp.Play("Stack_Run");
            var dist = Vector3.Distance(startPos,targetPos);
            var timer = 0f;
            yield return null;

            while(true)
            {
                dist = Vector3.Distance(_workerTrans.position,targetPos);
                timer += Time.fixedDeltaTime;
                if(dist < MINDIST || timer > _timLimit)
                {
                    _workerAnimComp.Play("Stack_Idle");
                    _workerTrans.localRotation = Quaternion.Euler(Vector3.zero); 
                    _workerTrans.position = targetPos;  
                    PrepareParametersAndRequest();
                    yield break;
                }
                _workerTrans.Translate(WorkerSpotDir *_workerSpeed * Time.fixedDeltaTime,Space.World);  
                yield return CoroutineUtil.WaitForFixedUpdate;
            }
        }

        private void PrepareParametersAndRequest()
        {
            var burgerList = new Stack<Transform>();
            foreach(Transform burgerTrans in _workerHandTrans) burgerList.Push(burgerTrans);
            var defautlIdx = 0;
            var customerTrans = _lineSpotTransList[defautlIdx].GetChild(defautlIdx);
            var isCompExist = customerTrans.TryGetComponent<CustomerController>(out CustomerController customerComp);

            if(isCompExist) PickupManager.onBurgerPickupToManager(burgerList,customerComp,OnChangeState);
            else Debug.LogError("손님 컴포넌트가 존재하지 않습니다.");     
        }

        public Transform FindEmptySpot()
        {
            if (!IsWaitAvailable) 
                return null;     

            Transform emptySpot = null;
            bool isFull;
            List<WaitLineSpot> emptySpotCompList = CheckEmptySpotsAndMakeList(out isFull); // 현재 줄 상태 리스트

            if(isFull) // 빈 곳이 없다면
            {
                return emptySpot;   
            }       

            if(!emptySpotCompList.Contains(null)) // 전부 빈 곳이라면
            {
                emptySpot = emptySpotCompList[0].transform;
       
                emptySpotCompList.Clear();
                emptySpotCompList = null;

                return emptySpot;   
            } 

            var emptyMinWeight = 0;
            var fullMaxWeight = 0;
            var isEmptyFound = false;
            var isCutLinePossible = false;

            for (int i = 0; i < _spotCount; i++) // 새치기 가능여부 확인 부분
            {
                var targetSpot = emptySpotCompList[i];
                if (targetSpot == null) // 스팟에 손님이 있다면
                {
                    if(isEmptyFound) // 스팟에 손님이 없는곳 뒤에 손님이 있다면
                    {
                        isCutLinePossible = true;
                    }
                    fullMaxWeight = i+1;
                }   
                else // 스팟에 손님이 없다면
                {           
                    isEmptyFound = true;
                    var targetWeight = targetSpot.Weight;   

                    if (emptyMinWeight == 0)
                    {
                        emptyMinWeight = targetWeight;
                    }
                }
            }    

            if(isCutLinePossible) // 대기라인 스팟 확정
            {
                if (fullMaxWeight != _spotCount)
                    emptySpot = _lineSpotTransList[fullMaxWeight];
            }
            else
            {
                var idx = emptyMinWeight - 1;
                emptySpot = _lineSpotTransList[idx];    
            }

            return emptySpot;             
        }    

        private List<WaitLineSpot> CheckEmptySpotsAndMakeList(out bool isFull)
        {
            List<WaitLineSpot> tempTransList = new List<WaitLineSpot>();
            isFull = true;

            for (int i = 0; i < _spotCount; i++)
            {
                if(_lineSpotTransList[i].childCount == 0)
                {
                    tempTransList.Add(_lineSpotCompList[i]);    
                    isFull = false;
                }
                else
                {
                    tempTransList.Add(null); 
                }
            }
            
            if (isFull) 
            {
                tempTransList.Clear();
                tempTransList = null;
                return tempTransList;
            }

            return tempTransList;      
        }


        // if (_isCustormer)
            // {
            //     foreach (var spot in _lineSpotCompList)
            //     {
            //         if(spot.Weight == _spotCount && spot.transform.childCount == 0) 
            //         {
            //             emptySpotList = spot.transform;
            //             break;
            //         }          
            //     } 
            // }
            // else
            // {
            //     foreach (var spot in _lineSpotCompList)
            //     {              
            //         if(spot.transform.childCount == 0 && spot.Weight != _lasWeight) 
            //         {
            //             emptySpotList = spot.transform;
            //             _lasWeight = spot.Weight;
            //             break;
            //         }          
            //     } 

            //     if (emptySpotList == null)
            //     {
            //         _isCustormer = true;           
            //     }
            // }

        public Transform FindMidEmptySpot(WaitLineSpot spotComp) // 2 ,3 ,4 ,5 ,6, 7 가중치
        {
            Transform emptySpot = null;
            var currWeight = spotComp.Weight; 
            var targetWeight = --currWeight;      
            foreach (var comp in _lineSpotCompList)
            {
                if (comp.Weight == targetWeight)
                {
                    if(comp.transform.childCount == 0)
                    {
                        emptySpot = comp.transform;
                        break;
                    }
                    else 
                        break;
                }                      
            }            
            return emptySpot;             
        }

        private void OnDestroy() 
        {
            OnBurgerCheck = null;
            OnChangeState = null;
        }
    }
}

