using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LunaBurger.Playable010
{
    public class CustomerController : MonoBehaviour
    {    
        [SerializeField] private float _orderTime;

        [SerializeField] private Animator _customerAnim;
        [SerializeField] public Transform _customerHandTrans;
        [SerializeField] public eCustomerState _state;
        [SerializeField] public ParticleSystem[] _emojiParticleArr;
        [SerializeField] private Transform OrderUIPos;
        
        public eCustomerLevel Level
        {
            get => _level;
            set => _level = value;  
        }
        private eCustomerLevel _level; 

        public int CustomerBurgerAmount
        {
            get => _customerBurgerAmount;
            set => _customerBurgerAmount = value;     
        }
        private int _customerBurgerAmount;

        public WaitLineSpot TargetSpotComp
        {
            get => _targetSpotComp;
            set => _targetSpotComp = value;     
        }
        private WaitLineSpot _targetSpotComp;

        public WaitLineSpot CurrSpotComp
        {
            get => _currSpotComp;
            set => _currSpotComp = value;     
        }
        private WaitLineSpot _currSpotComp;


        public float MoveTimeLimit
        {
            get => _moveTimeLimit;
            set => _moveTimeLimit = value;     
        }
        private float _moveTimeLimit;

        private List<Transform>  _customerBurgerList = new List<Transform>();
        private System.Random    _rand               = new System.Random();  
  
        public System.Action<eUnitAnimState> OnChangeAnimState      {get; private set;}  
        public System.Action                 OnNextMove             {get; private set;} 
        public System.Action                 OnGoBackToHome         {get; private set;} 
        public System.Action                 OnDoNextMoveForDefault {get; private set;} 

        private void Awake() 
        {
            OnChangeAnimState = (state) =>{
                ChangeAnimState(state);
            };
            
            OnNextMove = () =>{
                ChooseNextMove();
            };

            OnGoBackToHome = () => {
                var state = eCustomerState.BACKTOHOME;
                _state = state;
                var cnt = _customerHandTrans.childCount;
                for(int i = 0; i < cnt; i++)
                {
                    var burgerTrans = _customerHandTrans.GetChild(i);
                    _customerBurgerList.Add(burgerTrans);
                }
                CustomerManager.OnChangePosition(this);
            };

            OnDoNextMoveForDefault = () =>{
                ChooseNextMove();
            };

            if(_state == eCustomerState.COUNTERORDERSPOT || _state == eCustomerState.COUNTERMIDLINESPOT ) // 디폴트 스폰 손님
            {
                _level = eCustomerLevel.LEVEL1;
                _customerBurgerAmount = _rand.Next(1,4); 
                _currSpotComp = this.transform.parent.GetComponent<WaitLineSpot>();
            }
        }

        private void ChangeAnimState(eUnitAnimState animState)
        {
            switch(animState)
            {
                case eUnitAnimState.IDLE:
                _customerAnim.Play("Customer_Idle");
                break;
                case eUnitAnimState.RUN:
                _customerAnim.Play("Customer walk");
                break;
                case eUnitAnimState.STACKIDLE:
                _customerAnim.Play("Customer_Stack_Idle");
                break;
                case eUnitAnimState.STACKRUN :
                _customerAnim.Play("Customer carry_walk");
                break;         
            }
        }

        private void ChooseNextMove()
        {       
            ChangeAnimState(eUnitAnimState.IDLE);   
            switch(_state)
            {
                case eCustomerState.COUNTERMIDLINESPOT:
                CustomerManager.OnChangePosition(this);
                break;

                case eCustomerState.COUNTERORDERSPOT:
                StartCoroutine(CoOrderBurgerAndWait());
                break;

                case eCustomerState.PICKUPMIDSPOT:
                CustomerManager.OnChangePosition(this);
                break;

                case eCustomerState.PICKUPORDERSPOT:
                PickupBurger();
                break;

                case eCustomerState.BACKTOHOME:
                StartCoroutine(CoSetUpforReset());
                break;
            }
        }

        private IEnumerator CoOrderBurgerAndWait()
        {
            var timer = 0f;
            var targetPos = OrderUIPos.position;
            var imgLoading = OrderBalloonManager.OnRequestLoadingUI(_level,targetPos);       
            CounterManager.OnCounterChageState(_level);
            
            while (true)
            {
                imgLoading.fillAmount = Mathf.Clamp01(timer / _orderTime);
                timer += Time.deltaTime;
                OrderBalloonManager.OnRequestLoadingUI(_level,targetPos);      
                if(timer > _orderTime)
                {
                    SoundManager.NullableInstance.OnPlaySFX(eSoundName.CASH);
                    var counterHash = _currSpotComp.ParentObjTransHash;
                    CounterManager.OnOrderBurger(counterHash,_customerBurgerAmount);
                    CustomerManager.OnChangePosition(this);   
                    OrderBalloonManager.OnResetLoadingUI(_level);
                    yield break;
                }
                yield return null;
                
            }
        }

        public void PlayRanEmojiParticle()
        {
            var cnt = _emojiParticleArr.Length;
            var ran = UnityEngine.Random.Range(0, cnt); 
            _emojiParticleArr[ran].Play();
        }

        private void PickupBurger()
        {    
            this.transform.localRotation = Quaternion.Euler(Vector3.zero); 
            var pickupHash = _currSpotComp.ParentObjTransHash;
            PickupManager.OnBurgerCustomerToManager(pickupHash,_customerBurgerAmount,this.ChangeAnimState);
        }

        private IEnumerator CoSetUpforReset()
        {
            var burgerAmount = _customerBurgerList.Count;
            foreach(var burger in _customerBurgerList)
            {
               var BurgerObj = burger.gameObject;
               ObjectPoolManager.NullableInstance.RetrunObject(BurgerObj);   
                yield return null;
            }
            yield return null;
            _customerBurgerList.Clear();
            _level = eCustomerLevel.NONE;
            _customerBurgerAmount = 0;  
            _targetSpotComp = null;
            _currSpotComp = null;   
            _state = eCustomerState.NONE;   
            _moveTimeLimit = 0;
            yield return null;
            ObjectPoolManager.NullableInstance.RetrunObject(this.gameObject);           
        }
    }
}

