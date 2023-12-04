using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace luna_sportshop.Playable002
{
    public enum EItemFormState
    {
        None = -1,
        Storage = 0,
        Display = 1,
    }

    public enum EItemType
    {
        None = -1,
        Shoe = 0,
        Clothes = 1,
    }

    public abstract class Item : MonoBehaviour
    {

        [Header("=====Item SerializeField=====")]
        [Space]
        [SerializeField] protected Vector3            _defaultSize        = Vector3.one;
        [SerializeField] protected Quaternion         _defaultRotation    = Quaternion.identity;
        [SerializeField] protected Vector3            _targetShrinkSize   = Vector3.one;
        [SerializeField] protected List<Material>     _matList            = null;
        [SerializeField] protected List<MeshRenderer> _meshRanList        = null;
        [SerializeField] protected GameObject         _disableTargetObjet = null;
        [SerializeField] protected EItemType          _type               = EItemType.None; 

        //===============================================================
        //Static Fields
        //===============================================================
        public  event  Action<Item> OnDisable { add => _onDisable += value;  remove => _onDisable -= value; } 
        private static int          _price            = 10;  

        //===============================================================
        //Properties
        //===============================================================
        public static int Price { set => _price = value; get => _price; }
        public Vector3    DefaultSize     => _defaultSize;
        public Vector3    DisplaySize     => _displaySize;
        public Quaternion DeFaultRotation => _defaultRotation;
        public EItemType  Type            => _type;
          
        //===============================================================
        //Fields
        //===============================================================
        private Action<Item>     _onDisable     = null;
        private Vector3          _displaySize  = new Vector3(0.5f,0.8f,0.5f);
        protected EItemFormState _currFormState = EItemFormState.Storage;
        protected int            _matListCount  = 0;
        protected int            _meshListCount = 0;

        //===============================================================
        //Functions
        //===============================================================
        protected virtual void Awake()
        {
            this.transform.localScale = _defaultSize;
            this.transform.rotation = _defaultRotation;
            _currFormState = EItemFormState.Storage;

            _matListCount = _matList.Count;
            _meshListCount = _meshRanList.Count;        

            var ranIdx = Random.Range(0,_matListCount);

            for (int i = 0; i < _meshListCount; i++)
            {
                _meshRanList[i].material = _matList[ranIdx];
            } 
                    
        }        

        public void Disable() => this.StartCoroutine(CoDisable());

        private IEnumerator CoDisable()
        {
            _disableTargetObjet.SetActive(true);
            _currFormState = EItemFormState.Storage;

            this.transform.parent = null;
            yield return null;

            this.transform.localScale = _defaultSize;
            this.transform.rotation = _defaultRotation;

            var ranIdx = Random.Range(0,_matListCount);

            for (int i = 0; i < _meshListCount; i++)
            {
                _meshRanList[i].material = _matList[ranIdx];
            } 

            if (_onDisable != null)
                _onDisable.Invoke(this);
        }

        public abstract void ChangeForm(EItemFormState state = EItemFormState.Storage);
        
        public abstract void Shrink();
    }
}