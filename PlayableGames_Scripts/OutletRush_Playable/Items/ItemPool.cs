using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace luna_sportshop.Playable002
{
    using Random = UnityEngine.Random;

    public class ItemPool : MonoBehaviour
    {
        [Header("=====Pool SerializeField=====")]
        [Space]
        [SerializeField] private List<Item> _prefabsList       = null;
        [SerializeField] private int        _defaultGenAmount  = 0;

        //===============================================================
        //Fields
        //===============================================================
        private Queue<Item> _itemQ        = null;

        //===============================================================
        //Functions
        //===============================================================
        private void Awake()
        {
            _itemQ = new Queue<Item>();
            Init();
        }

        private void Init()
        {
            for (int i = 0; i < _defaultGenAmount; i++ )
            {
                var idx = Random.Range(0 , _prefabsList.Count);
                var itemPrefab = _prefabsList[idx];
                GenerateItem(itemPrefab,true);
            }
        }

        private Item GenerateItem(Item prefab, bool isInitGen = false)
        {       
            var item = Instantiate(prefab);

            if (isInitGen)
                _itemQ.Enqueue(item);
                
            item.transform.parent = this.transform;
            item.gameObject.SetActive(false);
            return item;
        }

        public Item GetItem()
        {
            Item item = null;

            if (_itemQ.Count == 0)
            {
                var idx = Random.Range(0 , _prefabsList.Count);
                item = GenerateItem(_prefabsList[idx]);
                                     
            }
            else
            {
                item = _itemQ.Dequeue();
            }

            item.OnDisable += RetrunItem;
            item.gameObject.SetActive(true);   
            
            return item;
        }

        public void RetrunItem(Item retrunedItem)
        {
            retrunedItem.OnDisable -= RetrunItem;
            _itemQ.Enqueue(retrunedItem);
            retrunedItem.transform.parent = this.transform;
            retrunedItem.gameObject.SetActive(false);
        }
    }
}