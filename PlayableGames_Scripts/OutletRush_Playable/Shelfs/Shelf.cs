using System.Collections;
using System.Collections.Generic;
using Supercent.Util;
using UnityEngine;

namespace luna_sportshop.Playable002
{
    public abstract class Shelf : MonoBehaviour
    {
        [Header("=====Shelf SerializeField=====")]
        [Space]
        [SerializeField] protected List<Transform> _pointList;
        [SerializeField] protected Animator _anim = null;

        //===============================================================
        //Fields
        //===============================================================
        protected List<Item> _currItemList = null;
        protected bool _isPrepared = false;

        //===============================================================
        //Functions
        //===============================================================
        protected virtual void Awake() =>_currItemList = new List<Item>();

        public virtual void Upgrade()
        {
            this.gameObject.SetActive(true);
            this.StartCoroutine(CoPrepareForActivation());
        }

        private IEnumerator CoPrepareForActivation()
        {
            yield return CoroutineUtil.WaitForSeconds(1f);;
            _isPrepared = true;
        }
    }
}