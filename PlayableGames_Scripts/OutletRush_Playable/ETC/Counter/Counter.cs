using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace luna_sportshop.Playable002
{
    public class Counter : MonoBehaviour, IBoxReturner
    {
        [Header("=====Counter Serialize Fields=====")]
        [Space] 
        [SerializeField] private List<Box> _boxList = null;
        [SerializeField] private Transform _boxPointTrans = null;
        [SerializeField] private GameObject _employee = null;
        [SerializeField] private BoxCollider _col = null;
        [SerializeField] private CounterInteractibleArea _interctibleArea = null;

        [Space]
        [Header("===========애니메이션 세부 시간 조절==========")]
        [Space]
        [Header("아이템을 가져가는 간격")]
        [SerializeField] private float _itemTakeInterval =  0.1f;
        [Header("아이템 이동시 목적지까지 걸리는 시간")]
        [SerializeField] protected float _itemTakeTimeLimit = 0.3f;

        [Space]
        [Header("==============애니메이션 커브==============")]
        [Space]
        [Header("아이템 이동 커브")]
        [SerializeField] private AnimationCurve _moveAnimCurve = null;
        [Header("아이템 이동시 높이 커브")]
        [SerializeField] private AnimationCurve _jumpAnimCurve = null;

        //===============================================================
        //Properties
        //===============================================================
        public event Action<int> OnFinnishGetBox { add {_onFinnishGetBox += value;} remove {_onFinnishGetBox -= value;} }
        public event Action OnFinishCheckOut { add {_onFinishCheckOut += value;} remove {_onFinishCheckOut -= value;} }

        //===============================================================
        //Fields
        //===============================================================
        private IItemListReturner _currentCustomer = null;
        private Transform _playerTrans = null;
        private Action<int> _onFinnishGetBox = null;
        private Action _onFinishCheckOut = null;
        private Coroutine _employeeCoroutine = null;
        private Coroutine _playerCounterCoroutine = null;
        private Coroutine _pakagingCoroutine = null;
        private Box _pakgingDoneBox = null;
        private bool _isEmployeeOn = false;
        private bool _isPlayerIn = false;

        //===============================================================
        //Functions
        //===============================================================
        private void Awake()
        {
            _employee.SetActive(false);
            _col.enabled = false;

            var cnt = _boxList.Count;
            for (int i = 0; i < cnt; i++)
                _boxList[i].OnRelease += SetupBoxForDiable;

            this.gameObject.SetActive(false);
        }

        private void OnTriggerEnter(Collider other) 
        {
            var type = (ELayerName)other.gameObject.layer;
            switch(type)
            {
                case ELayerName.Player : 
                {
                    if (_playerTrans == null)
                        _playerTrans = other.transform;

                    if (other.transform.position.z > -2.5f)
                        return;
                        
                    if (_isEmployeeOn)
                        return;

                    _isPlayerIn = true;

                    _playerCounterCoroutine = this.StartCoroutine(CoStartPlayerWork());

                    return; 
                }

                case ELayerName.Customer : 
                {
                    if (_currentCustomer != null)
                    {
                        Debug.Log($"현재 예약된 손님 : {_currentCustomer}  중복된 손님 : {other.gameObject.name}!!!!!!중복!!!!!");

                        if (other.transform.TryGetComponent<Customer>(out Customer dupleCus))
                        {
                            dupleCus.StopAllAndStartCoroutine(dupleCus.CoMoveForHome(null));
                            return;
                        }                  
                    }

                    if (other.transform.TryGetComponent<IItemListReturner>(out IItemListReturner returner))
                        _currentCustomer = returner;

                    return;        
                }        
            }
        }

        private void OnTriggerExit(Collider other)
        {
            var type = (ELayerName)other.gameObject.layer;

            if (type != ELayerName.Player)
                return;

            if (_playerCounterCoroutine != null)
                this.StopCoroutine(_playerCounterCoroutine);   
                
            _isPlayerIn = false;
        }

        private IEnumerator CoStartPlayerWork()
        {
            while(_isPlayerIn)
            {
                if (_isEmployeeOn)
                    yield break;
                    
                if (_currentCustomer != null && _pakagingCoroutine == null)
                {
                    var takenItemList = _currentCustomer.GetItemList(out Action doneCallback);
                    _pakagingCoroutine = this.StartCoroutine(CoTakeItemAndPakaging(takenItemList,doneCallback));
                }
                yield return CoroutineUtil.WaitForFixedUpdate;
            }
        }

        private IEnumerator CoStartEmployeeWork()
        {
            yield return CoroutineUtil.WaitUntil (() => {
                return _pakagingCoroutine == null;
            });

            while(this.gameObject.activeSelf)
            {
                if (_currentCustomer != null)
                {
                    if (_employeeCoroutine == null)
                    {
                        var takenItemList = _currentCustomer.GetItemList(out Action doneCallback);
                    
                        if (takenItemList != null)                 
                            _employeeCoroutine = this.StartCoroutine(CoTakeItemAndPakaging(takenItemList,doneCallback));
                    }
                }

                yield return CoroutineUtil.WaitForSeconds(0.1f);
            }
        }

        private IEnumerator CoTakeItemAndPakaging(List<Item> takenItemList, Action doneCallback)
        {
            Box targetBox = null; 

            foreach(Box box in _boxList)
            {
                if (box.gameObject.activeSelf == false)
                {
                    targetBox = box;
                    break;
                }
            }

            var dist = Vector3.Distance(this.transform.position, _playerTrans.position);

            var volume = 1f;

            if (dist > 6.2f)
                volume = 0.5f;

            AudioManager.NullableInstance.PlaySFX(EAudioName.CheckOutSound,false,false,0f,volume);

            targetBox.gameObject.SetActive(true);
            targetBox.transform.parent = _boxPointTrans;
            yield return null;
            targetBox.transform.localPosition = Vector3.zero;
            targetBox.BoxAnim.Play("AppearBox");
            yield return CoroutineUtil.WaitForSeconds(0.5f);

            var lastIdx = takenItemList.Count - 1; // Take all items from Customer

            for (int i = lastIdx; i > -1; i--)
            {
                var item  = takenItemList[i];
                
                if (i == 0) // Last One
                    this.StartCoroutine(this.CoJumpItem(item,targetBox,doneCallback));
                else    
                    this.StartCoroutine(this.CoJumpItem(item,targetBox));

                yield return CoroutineUtil.WaitForSeconds(_itemTakeInterval);
            }

        }

        private IEnumerator CoJumpItem(Item item, Box targetBox,Action doneCallBack = null)
        {
            var itemTrans = item.transform;
            itemTrans.parent = targetBox.transform;
            targetBox.CurrItemList.Add(item);
            yield return null;
            TweenUtil.TweenRotation(itemTrans,item.DeFaultRotation,false,_itemTakeTimeLimit);
            yield return null;
            var shoe = item as Shoe;
            shoe.Shrink();

            var startSec = Time.time;
            var endSec = startSec + _itemTakeTimeLimit;
            Vector3 startPos = itemTrans.localPosition;
            
            while (Time.time < endSec)
            {
                var ratio = (Time.time - startSec) / _itemTakeTimeLimit;
                itemTrans.localPosition = Vector3.Lerp(startPos, Vector3.zero , _moveAnimCurve.Evaluate(ratio));
                itemTrans.localPosition = itemTrans.localPosition + Vector3.up * _jumpAnimCurve.Evaluate(ratio);
                yield return CoroutineUtil.WaitForFixedUpdate;
            }
        
            itemTrans.localPosition = Vector3.zero;
            itemTrans.localEulerAngles = new Vector3 (0f,-90f,0f);

            if (doneCallBack != null)
            {
                doneCallBack?.Invoke();
                targetBox.BoxAnim.Play("BoxAnimation");
                yield return CoroutineUtil.WaitForSeconds(0.5f);
                _pakgingDoneBox = targetBox;
                _currentCustomer = null;

                if (_isEmployeeOn)
                _employeeCoroutine = null;
            }
        }

        public void Upgrade()
        {
            this.gameObject.SetActive(true);
            this.StartCoroutine(CoWaitForColEneable());
        }

        private IEnumerator CoWaitForColEneable()
        {
            yield return CoroutineUtil.WaitForSeconds(2f);
            _col.enabled = true;
        }

        public void ActiveEmployee()
        {
            _isEmployeeOn = true;
            _isPlayerIn = false;
            _interctibleArea.IsEmployeeOn = true;
            _employee.SetActive(true);
            this.StartCoroutine(CoStartEmployeeWork());
        }

        public Box GetBoxObject(out Action doneCallback)
        {
            doneCallback = null;
            Box box = null;

            if(_pakgingDoneBox == null)
                return box;
           
            box = _pakgingDoneBox;
            doneCallback = () => {
                if (_pakgingDoneBox == null)
                    return;
                    
                var count = _pakgingDoneBox.CurrItemList.Count;
                var totalPrice = count * Item.Price;
                _onFinnishGetBox.Invoke(totalPrice);
                _pakagingCoroutine = null;
                _pakgingDoneBox = null;

                if (_onFinishCheckOut != null)
                {
                    _onFinishCheckOut.Invoke();
                    _onFinishCheckOut = null;
                }
            };
            return box;
            
        }

        private void SetupBoxForDiable(Box returnedBox)
        {
            returnedBox.transform.parent = _boxPointTrans;

            var itemList = returnedBox.CurrItemList;

            foreach (var item in itemList)
                item.Disable();

            itemList.Clear();

            returnedBox.gameObject.SetActive(false);
        }
    }
}
