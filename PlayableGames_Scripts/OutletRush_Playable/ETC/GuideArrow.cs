using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Supercent.Util;

namespace luna_sportshop.Playable002
{
    using Params = Supercent.Util.TweenUtil.Params;
    using Token = Supercent.Util.TweenUtil.Token;
    using TimeYpe = Supercent.Util.TweenUtil.TimeType;

    public enum EGuideArrowState
    {
        None = -1,
        Counter_Upgrade = 0,
        DisplayShelf_Shoe1_Upgrade = 1,
        StorageShelf_Shoe_Upgrade = 2,
        StorageSHelf_Shoe_Take = 3,
        DisplayShelf_Shoe1_Take = 4,
        Counter_CheckOut = 5,
        DisplayShelf_Shoe2_Upgrade = 6,
    }

    public class GuideArrow : MonoBehaviour
    { 
        [Header("======GuideArrow Fields=====")]
        [Space]
        [SerializeField] private Transform _guideArrow = null;
        [SerializeField] private Transform _guideArrowForMove = null;
        [SerializeField] private Transform _guideArrowPlayer = null;
        [SerializeField] private List<Transform> _arrowPosList = null;
        [SerializeField] private Vector3 _targetArrowDownPos = Vector3.one;
        [SerializeField] private float _upAndDownDuration = 1f;
        [SerializeField] private float _minDist = 3f;

        //===============================================================
        //Fields
        //===============================================================
        private Token _UpAndDownMovementToken;
        private EGuideArrowState _lastState = EGuideArrowState.None;
        private Vector3 _defaultPos = Vector3.zero;
        
        //===============================================================
        //Functions
        //===============================================================
        private void Awake()
        {
            _defaultPos = _guideArrowForMove.transform.localPosition;

            this._guideArrow.gameObject.SetActive(false);
            this._guideArrowPlayer.gameObject.SetActive(false);
        }

        public void Update()
        {
            if (_guideArrow.gameObject.activeSelf)
            {
                var dist = Vector3.Distance(_guideArrowPlayer.position,_guideArrow.position);
                if (dist < _minDist)
                {
                    _guideArrowPlayer.gameObject.SetActive(false);
                }
                else
                {
                    _guideArrowPlayer.gameObject.SetActive(true);
                    var targetPos = new Vector3(_guideArrow.position.x, _guideArrowPlayer.position.y, _guideArrow.position.z);
                    _guideArrowPlayer.LookAt(targetPos);
                }
            }
        }

        public void StartGuide(EGuideArrowState state)
        {
            if (_lastState == state)
                return;
            
            if (_UpAndDownMovementToken.IsValid())
                _UpAndDownMovementToken.Stop();
            
            _guideArrowForMove.transform.localPosition = _defaultPos;
            _lastState = state;
            var idx = (int)state;
            var targetTrans = _arrowPosList[idx];

            this._guideArrow.gameObject.SetActive(true);
            this._guideArrowPlayer.gameObject.SetActive(true);

            _guideArrow.position = targetTrans.position;
        
            _UpAndDownMovementToken = TweenUtil.LoopLocalPosition(_guideArrowForMove,_targetArrowDownPos,true,_upAndDownDuration);

        }

        public void StopGuide()
        {
            if (_UpAndDownMovementToken.IsValid())
                _UpAndDownMovementToken.Stop();

            this._guideArrow.gameObject.SetActive(false);
            this._guideArrowPlayer.gameObject.SetActive(false);
        }

    }
}

