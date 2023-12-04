using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Supercent.Util;

namespace luna_sportshop.Playable002
{
    using Action = System.Action;

    public class StorageShelf : Shelf, IItemListReturner
    {
        private enum EStorageShelfType
        {
            None = -1,
            Shoe = 0,
            Clothes = 1
        }

        [Header("=====StorageShelf SerializeField=====")]
        [Space]
        [SerializeField] private ItemPool _ownPool = null;
        [SerializeField] private EStorageShelfType _type = EStorageShelfType.None;

        //===============================================================
        //Properties
        //===============================================================
        public event Action<EGuideArrowState> OnPlayerTakeItems { add {_onPlayerTakeItems += value;} remove {_onPlayerTakeItems -= value;} }

        //===============================================================
        //Fields
        //===============================================================
        private Action<EGuideArrowState> _onPlayerTakeItems = null;
        private bool _isItemSpawning = true;

        //===============================================================
        //Functions
        //===============================================================
        protected override void Awake()
        {
            base.Awake();        
        }
        
        private void Start()
        {
            this.StartCoroutine(CoGenerateItems(true));
            this.gameObject.SetActive(false);
        }

        private void GenerateItems() => this.StartCoroutine(CoGenerateItems(false));

        private IEnumerator CoGenerateItems(bool isSpawn)
        {
            _isItemSpawning = true;

            for (int i = 0; i < _pointList.Count; i++)
            {
                var pointTrans = _pointList[i];
                var isEmpty = pointTrans.childCount < 1;

                if (isEmpty)
                {
                    var item = _ownPool.GetItem();
                    item.transform.parent = pointTrans;
                    item.transform.localPosition = Vector3.zero;
                    _currItemList.Add(item);

                    if (_type == EStorageShelfType.Clothes)
                    {
                        Clothes clothes = item as Clothes;
                        clothes?.ChangeForm();
                    } 
                    
                    if (!isSpawn)
                        yield return CoroutineUtil.WaitForSeconds(0.05f);
                }
            }

            _isItemSpawning = false;
        }
        
        public List<Item> GetItemList(out Action doneCallBack)
        {
            if (_isItemSpawning)
            {
                doneCallBack = null;
                return null;
            }
            else
            {
                if (_onPlayerTakeItems != null)
                {
                    doneCallBack = () => {
                        _onPlayerTakeItems.Invoke(EGuideArrowState.DisplayShelf_Shoe1_Take);
                        _onPlayerTakeItems = null;
                        GenerateItems();
                    };
                }
                else
                {
                    doneCallBack = GenerateItems;
                }
            
                return _currItemList;
            }
        }
        
        public override void Upgrade()
        {
            base.Upgrade();
            // GenerateItems();
        }

    }
}