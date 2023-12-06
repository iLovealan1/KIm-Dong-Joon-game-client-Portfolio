using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LunaBurger.Playable010
{
    public enum ePrefabType
    {
        None = -1,
        BURGER,
        CUSTOMER
    }
    public class ObjectPoolManager : MonoBehaviour
    {
        [Header("Burger,Customer")]
        [Space]
        [SerializeField] private GameObject[] _prefabArr;

        public static ObjectPoolManager NullableInstance => _instance;
        private static ObjectPoolManager _instance       = null;

        private List<GameObject> _burgerList   = new List<GameObject>(); //컴포넌트로 바꾸자       
        private List<GameObject> _customerList = new List<GameObject>();        
        private const int DEFAULTAMOUNT = 30;

        private void Awake() 
        {
            _instance = this;   
            for(int idx = 0; idx < _prefabArr.Length; idx++)
                MakeObjectsForPool(_prefabArr[idx], idx);                           
        }

        private void MakeObjectsForPool(GameObject prefab , int idx, int cnt = DEFAULTAMOUNT) //종류별로 일단 다 만들자
        {
            for (int i = 0; i < cnt; i++) 
                CreateInstance(prefab, idx);
        }

        private void CreateInstance(GameObject prefab, int idx)
        {
            var obj = Instantiate(prefab, this.transform);
            var type = (ePrefabType)idx;
            switch (type)
            {
                case ePrefabType.BURGER :
                _burgerList.Add(obj);   
                break;
                case ePrefabType.CUSTOMER :
                _customerList.Add(obj); 
                break;
            }
            obj.SetActive(false);
        }

        public GameObject GetObjFromPool(ePrefabType type)
        {
            var obj = FindObjectFromPool(type);
            return obj;
        }

        private GameObject FindObjectFromPool(ePrefabType type)
        {
            List<GameObject> list = null;

            switch (type)
            {
                case ePrefabType.BURGER :
                list = _burgerList;   
                break;

                case ePrefabType.CUSTOMER :
                list = _customerList;   
                break;
            }

            var cnt = list.Count;
        
            for (int i = 0; i < cnt; i++)
            {
                if (list[i].activeSelf == false)
                {
                    list[i].SetActive(true);
                    list[i].transform.SetParent(null);
                    return list[i];
                }
            }

            return MakeAndAddNewObj(type);
        }

        private GameObject MakeAndAddNewObj(ePrefabType type)
        {
            GameObject newObj = null;
            var idx = (int)type;

            switch (type)
            {
                case ePrefabType.BURGER :
                newObj = Instantiate(_prefabArr[idx]);
                _burgerList.Add(newObj);    
                break;

                case ePrefabType.CUSTOMER :
                newObj = Instantiate(_prefabArr[idx]);
                _customerList.Add(newObj);    
                break;
            }
            return newObj;  
        }

        public void RetrunObject(GameObject returnedObj)
        {
            returnedObj.transform.SetParent(this.transform);
            returnedObj.SetActive(false);
        }

        private void OnDestroy() 
        {
            _instance = null;   
            _burgerList.Clear();
            _customerList.Clear();
        } 
    }
}

