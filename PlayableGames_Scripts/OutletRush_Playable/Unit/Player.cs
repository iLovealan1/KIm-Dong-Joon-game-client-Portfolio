using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Supercent.Util;

namespace luna_sportshop.Playable002
{
    using Action = System.Action;
    using Params = Supercent.Util.TweenUtil.Params;
    using TimeType = Supercent.Util.TweenUtil.TimeType;

    public class Player : Unit , IPlayerMoveHandler, IItemListReturner, IMoneyStackReturner
    {
        [Header("=====Player SerializeField=====")]
        [Space]
        [SerializeField] private Transform _moneyStackPointForNoneStackMode = null;
        [SerializeField] private MoneyPool _moneyPool = null;
        [SerializeField] private Transform _itemStackPoint = null;
        [SerializeField] private Transform _moneyStackPoint = null;
        [SerializeField] private Transform _itemPointPrefab = null;
        [SerializeField] private float _rotationSpeed = 10;
        [SerializeField] private int _maxItemCarryAmount = 6;
        [SerializeField] private float _itemStackInterval = 0.5f;

        //===============================================================
        //Properties
        //===============================================================
        public event Action OnItemTakened { add {_onItemTakened += value;} remove {_onItemTakened -= value;} }
        public bool IsOKToMove { get; set; }
        public bool IsMoneyStackingMode {get; set;}
        public Vector3 PlayerCamAngles { get; set; }

        //===============================================================
        //Fields
        //===============================================================
        private IMoneyStackReturner _moneyReturner = null;
        private Action _onItemTakened = null;
        private Vector3 _cameraForward = Vector3.zero;
        private Vector3 _cameraRight = Vector3.zero;
        private Vector3 _moneyStackInterval = new Vector3(0f, 0.1f, 0f);
        private Stack<Money> _currMoneyStack = null;
        private Coroutine _checkMoneyGenCoroutine = null;
        private Coroutine _takeMoneyCoroutine = null;
        private bool _isPlayerInMoneyStacker = false;
        private bool _isPlayerNearShelf = false;
        private bool _isDuringStacking = false;
        
        //===============================================================
        //Functions
        //===============================================================
        protected override void Awake()
        {
            base.Awake();
            _currMoneyStack = new Stack<Money>();
            GenerateItemStackPoints();
        }

        private void Start()
        {
            if (IsMoneyStackingMode)
                InitMoneyOnBack();

            _cameraForward = Quaternion.Euler(0, PlayerCamAngles.y, 0) * Vector3.forward;
            _cameraRight = Quaternion.Euler(0, PlayerCamAngles.y, 0) * Vector3.right;
        }

        private void OnTriggerEnter(Collider other)
        {
            var type = (ELayerName)other.gameObject.layer;

            switch(type)
            {
                case ELayerName.Shelf : 
                {
                    _isPlayerNearShelf = true;

                    if (_isDuringStacking)
                        return;

                    if (_currItemList.Count == _maxItemCarryAmount)
                        return;

                    if (_takeItemCorutine != null)
                        return;

                    if (other.TryGetComponent<IItemListReturner>(out IItemListReturner returner))
                    {
                        _takeItemCorutine = this.StartCoroutine(CoCheckItemGen(returner));
                    }

                    return;
                }

                case ELayerName.MoneyStacker : 
                {
                    _isPlayerInMoneyStacker = true;

                    if (other.TryGetComponent<IMoneyStackReturner>(out IMoneyStackReturner returner))
                    {
                        _moneyReturner = returner;
                        var takenStack = _moneyReturner.GetMoneyStack(out Action doneCallBack);

                        if (takenStack == null)
                        {
                            _checkMoneyGenCoroutine = this.StartCoroutine(CoCheckMoneyGen());
                            return;
                        }

                        _takeMoneyCoroutine = this.StartCoroutine(CoTakeMoney(takenStack,doneCallBack));
                    }

                    return;
                }
            }
        }

        private void OnTriggerExit(Collider other) 
        {
            var type = (ELayerName)other.gameObject.layer;

            switch(type)
            {              
                case ELayerName.Shelf : 
                {
                    _isPlayerNearShelf = false;
                    return;
                }

                case ELayerName.MoneyStacker : 
                {
                    _isPlayerInMoneyStacker = false;
                    _moneyReturner = null;
                    
                    if (_checkMoneyGenCoroutine != null)
                    {
                        this.StopCoroutine(_checkMoneyGenCoroutine);
                        _checkMoneyGenCoroutine = null;
                    }
                    return;
                }
            }
        }

        public void MovePlayer(float vertical ,float horizontal)
        {
            if (IsOKToMove == false)
            {
                _rigedBody.velocity = Vector3.zero;
                ChangeAnimation(true);
                return;
            }
            
            if (vertical == 0 && horizontal == 0)
            {
                _rigedBody.velocity = Vector3.zero;
                ChangeAnimation(true);
                return;
            }

            Vector3 targetDirection = _cameraForward * vertical + _cameraRight * horizontal;
            targetDirection.Normalize();

            if (targetDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, targetRotation, _rotationSpeed);
            }

            ChangeAnimation(false);
            _rigedBody.velocity = targetDirection * _speed;
        }

        protected override void ChangeAnimation(bool isStopped)
        {
            var animName = string.Empty;

            if (isStopped)
            {
                if (_isCarrying)              
                    animName = EUnitAnimationName.CPI_Stack_Idle.ToString();             
                else    
                    animName = EUnitAnimationName.CPI_Player_Idle.ToString();                      
            }
            else
            {
                if (_isCarrying)              
                    animName = EUnitAnimationName.CPI_Stack_Run.ToString();             
                else    
                    animName = EUnitAnimationName.CPI_Player_run.ToString();  
            }

            if (_anim.GetCurrentAnimatorStateInfo(0).IsName(animName))
                return;

            _anim.Play(animName);
        }

        private void GenerateItemStackPoints()
        {
            for (int i = 0; i < _maxItemCarryAmount; i++)
            {
                var point = Instantiate(_itemPointPrefab);
                point.parent = _itemStackPoint;
                point.localPosition = new Vector3(0f, _itemStackInterval * i, 0f);
                _stackPointList.Add(point);
            }
        }

        private void InitMoneyOnBack()
        {
            var genAmount = MoneyManager.CurrentMoney / Money.Price;

            for (int i = 0; i < genAmount; i++)
            {
                var money = _moneyPool.GetMoney();
                var cnt = _moneyStackPoint.childCount;
                var targetPos = _moneyStackInterval * cnt;

                _currMoneyStack.Push(money);
                money.transform.parent = _moneyStackPoint;
                money.transform.localPosition = targetPos;
                money.transform.localRotation = Quaternion.Euler(new Vector3(0f, 90f,0f));
            }
        }

        protected override IEnumerator TakeItems(List<Item> takenItemList, Action doneCallback = null)
        {
            _isCarrying = true;
            _isDuringStacking = true;
            var needAmount = _maxItemCarryAmount - _currItemList.Count;

            for (int i = 0; i < needAmount; i++)
            {
                var lasIdx = takenItemList.Count - 1;
                var item = takenItemList[lasIdx];
                takenItemList.Remove(item);
                _currItemList.Add(item);

                var isClothes = item is Clothes;

                if (i == needAmount - 1) 
                {
                    // JumpItem(item,isClothes,doneCallback);
                    this.StartCoroutine(CoJumpItem(item,isClothes,doneCallback));
                }
                else 
                {
                    // JumpItem(item,isClothes);
                    this.StartCoroutine(CoJumpItem(item,isClothes));
                }

                yield return CoroutineUtil.WaitForSeconds(_itemTakeTimeInterval);
            }
        }

        private IEnumerator CoJumpItem(Item item, bool isClothes = false ,Action doneCallBack = null)
        {
            var itemTrans = item.transform;
            itemTrans.parent = null;
            yield return null;

            var targetTrans = FindEmptyPoint();
            var startSec = Time.time;
            var endSec = startSec + _itemMoveTimeLimit;
            var startPos = itemTrans.position;
  
            AudioManager.NullableInstance.PlaySFX(EAudioName.StackSound, false, false,0.25f);

            while (Time.time < endSec)
            {
                var ratio = (Time.time - startSec) / _itemMoveTimeLimit;

                if (isClothes)
                {
                    itemTrans.position = Vector3.Lerp(itemTrans.position, targetTrans.position, _itemMoveCurve.Evaluate(ratio));
                }
                else
                {
                    itemTrans.position = Vector3.Lerp(itemTrans.position, targetTrans.position, _itemMoveCurve.Evaluate(ratio));
                }
     
                itemTrans.position = itemTrans.position + Vector3.up * _itemJumpCurve.Evaluate(ratio);
                yield return CoroutineUtil.WaitForFixedUpdate;
            }

            itemTrans.parent = targetTrans;
            yield return null;

            itemTrans.localScale = item.DefaultSize;

            if (isClothes)
            {
                itemTrans.localPosition = Vector3.zero;
                itemTrans.localEulerAngles = new Vector3(0f,270f,0f);
            }           
            else
            {
                itemTrans.localPosition = Vector3.zero;
                TweenUtil.TweenLocalRotation(itemTrans,Quaternion.Euler(Vector3.zero),false,0.01f);
            }

            if (doneCallBack != null)
            {
                AudioManager.NullableInstance.ResetPitch(EAudioName.StackSound);
                doneCallBack.Invoke();
                _takeItemCorutine = null;
                _isDuringStacking = false;
            }

            Transform FindEmptyPoint() // 빈곳 할당
            {
                Transform pointTrans = null;

                var idx = _currItemList.Count - 1;
                pointTrans = _stackPointList[idx];

                return pointTrans;
            }
        }

        private IEnumerator CoTakeMoney(Stack<Money> takenMoneyStack , Action doneCallback)
        { 
            if (!IsMoneyStackingMode)
                AudioManager.NullableInstance.PlaySFX(EAudioName.MoneyTakeSound,true);
           
            while(takenMoneyStack.Count > 0)
            {
                var money = takenMoneyStack.Pop();
                _currMoneyStack.Push(money);

                var cnt = _moneyStackPoint.childCount;
                var targetPos = _moneyStackInterval * cnt;

                if (IsMoneyStackingMode)
                    money.transform.parent = _moneyStackPoint;
                else 
                    money.transform.parent = null;
                
    
                if (takenMoneyStack.Count == 0)
                {
                    doneCallback.Invoke();

                    if (IsMoneyStackingMode)
                        this.StartCoroutine(JumpMoney(money,targetPos,doneCallback));
                    else    
                        this.StartCoroutine(CoJumpMoney_NoStackVer(money,doneCallback));
                }
                else
                {
                   if (IsMoneyStackingMode)
                        this.StartCoroutine(JumpMoney(money,targetPos));
                    else    
                        this.StartCoroutine(CoJumpMoney_NoStackVer(money));
                }

                yield return CoroutineUtil.WaitForSeconds(_moneyTakeInterval);
            }
        }
        private IEnumerator JumpMoney(Money money, Vector3 targetPos, Action doneCallback = null)
        {
            var moneyTrans = money.transform;
            var startSec = Time.time;
            var endSec = startSec + _moneyMoveTimeLimit;
            Vector3 startPos = moneyTrans.localPosition;

            TweenUtil.TweenLocalRotation(moneyTrans,Quaternion.Euler(new Vector3(0f, 90f,0f)),false,_itemMoveTimeLimit);

            AudioManager.NullableInstance.PlaySFX(EAudioName.MoneyStackSound,true,false, 0.05f);

            while (Time.time < endSec)
            {
                var ratio = (Time.time - startSec) / _moneyMoveTimeLimit;
                moneyTrans.localPosition = Vector3.Lerp(moneyTrans.localPosition, targetPos , _moneyMoveCurve.Evaluate(ratio));
                moneyTrans.localPosition = moneyTrans.localPosition + Vector3.up * _moneyJumpCurve.Evaluate(ratio);
                yield return CoroutineUtil.WaitForFixedUpdate;
            }

            moneyTrans.localPosition = targetPos;

            if (doneCallback != null)
            {
                AudioManager.NullableInstance.ResetPitch(EAudioName.MoneyStackSound);
                _takeMoneyCoroutine = null;

                if (_isPlayerInMoneyStacker)
                    _checkMoneyGenCoroutine = this.StartCoroutine(CoCheckMoneyGen());
                
                doneCallback.Invoke();
            }

            MoneyManager.UpdateCurrentMoney(Money.Price);
        }

        private IEnumerator CoJumpMoney_NoStackVer(Money money,Action doneCallback = null)
        {
            var moneyTrans = money.transform;
            
            var startSec = Time.time;
            var endSec = startSec + _moneyMoveTimeLimit;
            Vector3 startPos = moneyTrans.position;
            
            while (Time.time < endSec)
            {
                var ratio = (Time.time - startSec) / _moneyMoveTimeLimit;
                moneyTrans.position = Vector3.Lerp(startPos, this.transform.position + Vector3.up, _moneyMoveCurve.Evaluate(ratio));
                moneyTrans.position = moneyTrans.position + Vector3.up * _moneyJumpCurve.Evaluate(ratio);
                yield return CoroutineUtil.WaitForFixedUpdate;
            }

            moneyTrans.rotation = Quaternion.identity;
            yield return null;

            if (doneCallback != null)
            {
                _takeMoneyCoroutine = null;

                if (_isPlayerInMoneyStacker)
                    _checkMoneyGenCoroutine = this.StartCoroutine(CoCheckMoneyGen());
                
                doneCallback.Invoke();
            }
            
            MoneyManager.UpdateCurrentMoney(Money.Price);
            money.Release();
        }

        private IEnumerator CoCheckItemGen(IItemListReturner returner)
        {
            Action doneCallBack = null;
            List<Item> takenItemList = null;

            yield return CoroutineUtil.WaitUntil( () => {
                if (!_isPlayerNearShelf)
                    return true;

                takenItemList = returner.GetItemList(out doneCallBack);
                return takenItemList != null;
            });

            if (!_isPlayerNearShelf)
            {
                _takeItemCorutine = null;
                yield break;
            }

            this.StartCoroutine(TakeItems(takenItemList,doneCallBack));
        }

        private IEnumerator CoCheckMoneyGen()
        {
            while (_isPlayerInMoneyStacker)
            {
                if (_takeMoneyCoroutine != null)
                {
                    yield return null;
                }
                else
                {
                    Action doneCallback = null;
                    Stack<Money> takenStack = null;

                    yield return CoroutineUtil.WaitUntil(() =>{
                        doneCallback = null;

                        if (_moneyReturner != null)
                            takenStack = _moneyReturner.GetMoneyStack(out doneCallback);

                        return takenStack != null;
                    }); 

                    _takeMoneyCoroutine = this.StartCoroutine(CoTakeMoney(takenStack,doneCallback));
                }
            }
        }

        public Vector3 GetPosition() => this.transform.position;

        public List<Item> GetItemList(out Action doneCallBack)
        {

            if (_isDuringStacking)
            {
                doneCallBack = null;
                return null;
            }

            if (_currItemList.Count == 0)
            {
                doneCallBack = null;
                return null;
            }
            else
            {
                doneCallBack = () => {

                    if (_currItemList.Count == 0)
                        _isCarrying = false;
                    else     
                        this.StartCoroutine(SortingCurrentItems());

                    _takeItemCorutine = null;
                    
                    if (_rigedBody.velocity == Vector3.zero)
                        ChangeAnimation(true);
                    else    
                        ChangeAnimation(false);

                    if (_onItemTakened != null)
                    {
                        _onItemTakened.Invoke();
                        _onItemTakened = null;
                    }
                };

                return _currItemList;
            }
        }

        private IEnumerator SortingCurrentItems()
        {
            var tempList = new List<Transform>();
            var cnt = _currItemList.Count;

            for (int i = 0; i < cnt; i++)
            {
                _currItemList[i].transform.parent = _stackPointList[i];     

                yield return null;

                _currItemList[i].transform.localPosition = Vector3.zero;
            }
        }

        public Stack<Money> GetMoneyStack(out Action doneCallback)
        {
            doneCallback = () => Debug.Log("Money has been Takened!");

            if (_currMoneyStack.Count == 0)
                return null;         
            else
                return _currMoneyStack;
        }
    }
}