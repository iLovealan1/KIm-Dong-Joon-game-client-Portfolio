using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace luna_sportshop.Playable002
{
    public class Box : MonoBehaviour
    {
        [Header("=====Box SerializeField=====")]
        [Space]
        [SerializeField] private Animator _anim = null;
        [SerializeField] private Quaternion _defaultRotation = Quaternion.identity;

        //===============================================================
        //Properties
        //===============================================================
        public Animator BoxAnim => _anim;
        public event Action<Box> OnRelease
        {
            add {_onRelease += value;}
            remove {_onRelease -= value;}
        }

        public List<Item> CurrItemList => _currItemList;
        //===============================================================
        //Fields
        //===============================================================
        private Action<Box> _onRelease = null;
        private List<Item> _currItemList = new List<Item>();

        //===============================================================
        //Functions
        //===============================================================
        private void Awake()    => this.gameObject.SetActive(false);

        private void OnEnable() => this.transform.rotation = _defaultRotation;

        public void Release()  => _onRelease.Invoke(this);
    }
}