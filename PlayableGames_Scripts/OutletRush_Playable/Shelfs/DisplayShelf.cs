using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Supercent.Util;
// using Luna.Unity;

namespace luna_sportshop.Playable002
{
    using Action = System.Action;

    public class DisplayShelf : Shelf, IDIsplayItemReturner
    {
        private enum EDisplayShelfType
        {
            None = -1,
            Shoe_1 = 0,
            Shoe_2 = 1,
            Clothes = 2
        }

        [Header("=====DisPlayShelf SerializeField=====")]
        [Space]

        [Space]
        [Header("===========애니메이션 커브 조절==========")]
        [Space]
        [SerializeField] private EDisplayShelfType _type = EDisplayShelfType.None;
        [Header("아이템 이동 커브")] 
        [SerializeField] protected AnimationCurve _moveItemCurve = null;
        [Header("아이템 이동시 높이 커브")]
        [SerializeField] protected AnimationCurve _jumpItemCurve = null;

        [Space]
        [Header("===========애니메이션 세부 시간 조절==========")]
        [Space]
        [Header("아이템을 가져가는 간격")]
        [SerializeField] private float _itemTakeInterval = 0.1f;
        [Header("아이템 이동시 목적지까지 걸리는 시간")]
        [SerializeField] protected float _itemMovetimelimit = 0.3f;

        //===============================================================
        //Properties
        //===============================================================
        public event Action OnFirstDisplaying { add => _onFirstDisplaying += value; remove =>_onFirstDisplaying -= value; }
        public event Action OnClothesDisplaying {add => _onClothesDisplaying += value; remove => _onClothesDisplaying -= value;}

        //===============================================================
        //Fields
        //===============================================================
        private Action _onFirstDisplaying = null;
        private Action _onClothesDisplaying = null;
        private Queue<Collider> _waitCustomerQueue = null;
        private Coroutine _takeItemCoroutine = null;
        private bool _isItemDisplaying = false;

        //===============================================================
        //Functions
        //===============================================================
        protected override void Awake()
        {
            base.Awake();
            _waitCustomerQueue = new Queue<Collider>();
            this.gameObject.SetActive(false);
        }

        private void OnTriggerEnter(Collider other) 
        {
            var type = (ELayerName)other.gameObject.layer;

            switch(type)
            {
                case ELayerName.Player : 
                {
                    if (_type == EDisplayShelfType.Shoe_2 && _isPrepared == false)
                        return;

                    if (_takeItemCoroutine != null)
                        return;
                        
                    if (_currItemList.Count == _pointList.Count)
                        return;

                    if (other.TryGetComponent<IItemListReturner>(out IItemListReturner returner))
                    {
                        var takenItemList = returner.GetItemList(out Action doneCallBack);

                         if (takenItemList == null)
                            return;

                        if (!CheckItemList(_type, takenItemList, out int availAmount))
                            return;

                        _takeItemCoroutine = this.StartCoroutine(TakeItems(takenItemList,availAmount,doneCallBack));
                    }

                    return;
                }

                case ELayerName.Customer :
                {
                    if (_type == EDisplayShelfType.Clothes)
                        return;

                    _waitCustomerQueue.Enqueue(other);
                    return;
                }
            }
                
        }

        private bool CheckItemList(EDisplayShelfType type, List<Item> takenItemList, out int availAmount)
        {
            var count = takenItemList.Count;

            var isShoe = false;
            var isClothes = false;
            availAmount = 0;

            if (type == EDisplayShelfType.Clothes)
            {
                for (int i = 0; i < count; i++ )
                {
                    var itemType = takenItemList[i].Type;
                    if (itemType == EItemType.Clothes)
                    {
                        isClothes = true;
                        ++availAmount;
                    }
                    else
                    {
                        isShoe = true;
                    }
                }

                if (!isShoe || (isShoe && isClothes))
                    return true;
                else   
                    return false;
            }
            else
            {
                for (int i = 0; i < count; i++ )
                {
                    var itemType = takenItemList[i].Type;
                    if (itemType == EItemType.Shoe)
                    {
                        isShoe = true;
                        ++availAmount;
                    }
                    else
                    {
                        isClothes = true;
                    }
                }

                if (!isClothes || (isShoe && isClothes))
                    return true;
                else   
                    return false;
            }
        }

        private IEnumerator TakeItems(List<Item> takenItemList, int availAmount, Action doneCallback = null)
        {
            if (_onFirstDisplaying != null)
            {
                _onFirstDisplaying.Invoke();
                _onFirstDisplaying = null;
            }
            
            _isItemDisplaying = true;
            var emptySpotAmount = _pointList.Count - _currItemList.Count;
            var targetAmount = 0;

            if (availAmount > emptySpotAmount)
                targetAmount = emptySpotAmount;
            else 
                targetAmount = availAmount;

            var idx = takenItemList.Count - 1;

            while(targetAmount > 0 || idx > -1)
            {
                var item = takenItemList[idx];
                
                AudioManager.NullableInstance.PlaySFX(EAudioName.StackSound, false, true,0.3f);

                if (_type == EDisplayShelfType.Clothes)
                {
                    if (item is Clothes)
                    {
                        targetAmount--;
                        takenItemList.Remove(item);
                        _currItemList.Add(item);

                        if (targetAmount == 0)
                        {                      
                            // Playable.InstallFullGame();
                            // LifeCycle.GameEnded();
                            _onClothesDisplaying.Invoke();
                            this.StartCoroutine(CoJumpItem(item,doneCallback));                                       

                        }                       
                        else 
                            this.StartCoroutine(CoJumpItem(item));
                    }
                }
                else
                {
                    if (item is Shoe)
                    {
                        targetAmount--;
                        takenItemList.Remove(item);
                        _currItemList.Add(item);

                        if (targetAmount == 0)
                        {
                            this.StartCoroutine(CoJumpItem(item,doneCallback));                                       
                            yield break;
                        }                       
                        else 
                            this.StartCoroutine(CoJumpItem(item));
                    }
                }

                idx--;

                yield return CoroutineUtil.WaitForSeconds(_itemTakeInterval);
            }
        }

        private IEnumerator CoJumpItem(Item item ,Action doneCallBack = null)
        {
            var itemTrans = item.transform;
            itemTrans.parent = FindEmptyPoint();
            yield return null;

            var startSec = Time.time;
            var endSec = startSec + _itemMovetimelimit;
            Vector3 startPos = item.transform.localPosition;

            if (_type == EDisplayShelfType.Clothes)
            {             
                var clothes = item as Clothes;
                clothes.ChangeForm(EItemFormState.Display);
                TweenUtil.TweenLocalRotation(itemTrans,Quaternion.Euler(new Vector3(0f,90f,0f)),false,_itemMovetimelimit);
            }           
            else
            {
                if (_type == EDisplayShelfType.Shoe_1)
                    TweenUtil.TweenLocalRotation(itemTrans,Quaternion.Euler(new Vector3(0f,90f,0f)),false,_itemMovetimelimit);
                else
                    TweenUtil.TweenLocalRotation(itemTrans,Quaternion.Euler(new Vector3(0f,-90f,0f)),false,_itemMovetimelimit);

                itemTrans.localScale = item.DisplaySize;
            }

            while (Time.time < endSec)
            {
                var ratio = (Time.time - startSec) / _itemMovetimelimit;
                 itemTrans.localPosition = Vector3.Lerp(startPos, Vector3.zero , _moveItemCurve.Evaluate(ratio));
                 itemTrans.localPosition =  itemTrans.localPosition + Vector3.up * _jumpItemCurve.Evaluate(ratio);
                yield return CoroutineUtil.WaitForFixedUpdate;
            }

            itemTrans.localPosition = Vector3.zero;

            if (_type == EDisplayShelfType.Clothes)
                itemTrans.localEulerAngles = new Vector3(0f,90f,0f);
            else if (_type == EDisplayShelfType.Shoe_1)
            {
                itemTrans.localEulerAngles = new Vector3(0f,90f,0f);
                var shoe = item as Shoe;
                shoe.ChangeForm(EItemFormState.Display);
            }
            else
            {
                itemTrans.localEulerAngles = new Vector3(0f,-90f,0f);
                var shoe = item as Shoe;
                shoe.ChangeForm(EItemFormState.Display);
            }

            if (doneCallBack != null)
            {
                AudioManager.NullableInstance.ResetPitch(EAudioName.StackSound, false, true);

                yield return CoroutineUtil.WaitForFixedUpdate;
                _isItemDisplaying = false;
                _takeItemCoroutine = null;
                doneCallBack?.Invoke();
            }

            Transform FindEmptyPoint() // 빈곳 할당
            {
                Transform pointTrans = null;
                for (int i = 0; i < _pointList.Count; i++)
                {
                    pointTrans = _pointList[i];
                    
                    if (pointTrans.childCount == 0)
                        break;
                }

                return pointTrans;
            }
        }

        public List<Item> GetItemsFromDisplayShelf(Collider customerCol, out Action doneCallback)
        {
            Collider targetCustomer = null;
            List<Item> itemList = null;
            doneCallback = null;

            if (_isItemDisplaying)
                return itemList;

            if (_currItemList.Count == 0)
                return itemList;

            if (_waitCustomerQueue.Count != 0)
            {
                targetCustomer = _waitCustomerQueue.Peek();   

                if (targetCustomer == customerCol)
                {
                    doneCallback = () => {
                        _waitCustomerQueue.Dequeue();
                    };
                    itemList = _currItemList;
                }
            }
            
            return itemList;
        }
    }
}