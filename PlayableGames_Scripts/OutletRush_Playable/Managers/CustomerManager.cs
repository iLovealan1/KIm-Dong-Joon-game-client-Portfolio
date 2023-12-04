using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Supercent.Util;

namespace luna_sportshop.Playable002
{
    public enum ECustomerState
    {
        None = -1,
        PurchaseNode = 0,
        WaitNode1 = 1,
        WaitNode2 = 2,
        WaitNode3 = 3,
        WaitNode4 = 4,
        WaitNode5 = 5,
        Shopping,
        BeforeWaitNode,
    }

    public class CustomerManager : MonoBehaviour, IInitializeHandler
    {
        [SerializeField] private float _spwanDelay = 3f;
        [SerializeField] private List<Customer> _shelf_1_customerList = null;
        [SerializeField] private List<Customer> _shelf_2_customerList = null;
        [SerializeField] private List<Customer> _clothes_customerList = null;
        [SerializeField] private List<Transform> _waitNodeList = null;
        [SerializeField] private Customer  _extraCustomer = null;

        public void Init<T>(T value)
        {
            if (value is Action<EGuideArrowState>)
                _shelf_1_customerList[0].OnArrivedAtPurchase += value as Action<EGuideArrowState>; 

            var cnt = _shelf_1_customerList.Count;
            
            for (int i = 0; i < cnt; i++)
            {
                var shelf1_customer = _shelf_1_customerList[i];
                var shelf2_customer = _shelf_2_customerList[i];

                shelf1_customer.OnChangeState += ChangeCustomerState;
                shelf1_customer.OnRelease += ReleaseCustomer;      
                shelf1_customer._targetShelf = ECustomerShelfTarget.Shelf1;

                shelf2_customer.OnChangeState += ChangeCustomerState;
                shelf2_customer.OnRelease += ReleaseCustomer;      
                shelf2_customer._targetShelf = ECustomerShelfTarget.Shelf2;
            }

            foreach(var customer in _clothes_customerList)
                customer._targetShelf = ECustomerShelfTarget.Clotehs;
            

            _extraCustomer.OnChangeState += ChangeCustomerState;
            _extraCustomer.OnRelease += ReleaseCustomer;      
            _extraCustomer._targetShelf = ECustomerShelfTarget.Shelf1;
        }

        private IEnumerator CoSpawnShelf_1_Customers()
        {
            while (this.gameObject.activeSelf)
            {
                var cnt = _shelf_1_customerList.Count;

                for (int i = 0; i < cnt; i++)
                {
                    var customer = _shelf_1_customerList[i];
                    if (!customer.gameObject.activeSelf)
                    {   
                        customer.gameObject.SetActive(true);
                        customer.MoveCustomerToDisplayShelf();
                        break;
                    }
                }

                yield return CoroutineUtil.WaitForSeconds(_spwanDelay);
            }
        }

        private IEnumerator CoSpawnShelf_2_Customers()
        {
            var cnt = _shelf_2_customerList.Count;

            while (this.gameObject.activeSelf)
            {
                for (int i = 0; i < cnt; i++)
                {
                    var customer = _shelf_2_customerList[i];
                    if (!customer.gameObject.activeSelf)
                    {
                        customer.gameObject.SetActive(true);
                        customer.MoveCustomerToDisplayShelf();
                        break;
                    }
                }
                
                yield return CoroutineUtil.WaitForSeconds(_spwanDelay);
            }
        }

        private IEnumerator CoSpawnClothes_Customers()
        {
            yield return CoroutineUtil.WaitForSeconds(2f);
            
            var cnt = _clothes_customerList.Count;

            for (int i = 0; i < cnt; i++)
            {
                var customer = _clothes_customerList[i];
                customer.gameObject.SetActive(true);
                customer.MoveCustomerToDisplayShelf();
                
                yield return CoroutineUtil.WaitForSeconds(_spwanDelay);
            }
        }

        public void AddExtraCustomerToList()     => _shelf_1_customerList.Add(_extraCustomer); 
        public void StartSapwnShelf_1_Customer() => this.StartCoroutine(CoSpawnShelf_1_Customers());
        public void StartSapwnShelf_2_Customer() => this.StartCoroutine(CoSpawnShelf_2_Customers());
        public void StartSpawnClotehs_Customer() => this.StartCoroutine(CoSpawnClothes_Customers());

        private ECustomerState ChangeCustomerState(ECustomerState currState, Customer requestedCutomer)
        {
            var newState = ECustomerState.None;

            switch (currState)
            {
                case ECustomerState.Shopping : 
                {
                    newState = ECustomerState.BeforeWaitNode;
                    break;
                }
                case ECustomerState.BeforeWaitNode :
                {
                    newState = FindEmptyWaitNode(requestedCutomer);
                    break;
                }
                case ECustomerState.PurchaseNode : 
                {
                    break;
                }
                default : 
                {
                    var idx = (int)currState - 1;

                    if (_waitNodeList[idx].childCount == 0)
                    {
                        requestedCutomer._currTargetWaitNode = _waitNodeList[idx];
                        newState = (ECustomerState)idx;

                    }
                    else
                    {
                        requestedCutomer._currTargetWaitNode = null;
                        newState = requestedCutomer._currState;
                    }
                    break;
                }
            }

            return newState;
        }

        private ECustomerState FindEmptyWaitNode(Customer requestedCutomer)
        {
            var newState = ECustomerState.None;

            for (int i = 0; i< _waitNodeList.Count; i++ )
            {
                var node = _waitNodeList[i];

                if (node.childCount == 0)
                {
                    requestedCutomer._currTargetWaitNode = node;
                    newState = (ECustomerState)i;
                    break;
                }
            }
            
            if (newState == ECustomerState.None)
            {
                requestedCutomer._currTargetWaitNode = null;
                newState = requestedCutomer._currState;
            }

            return newState;
        }

        private void ReleaseCustomer(Customer returnedCustomer)
        {
            returnedCustomer._currState = ECustomerState.Shopping;
            returnedCustomer.transform.parent = null;
            returnedCustomer.gameObject.SetActive(false);
        }

        
    }
}

