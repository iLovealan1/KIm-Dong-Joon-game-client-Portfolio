using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Supercent.Util;

namespace luna_sportshop.Playable002
{
    using Params = Supercent.Util.TweenUtil.Params;
    using Token = Supercent.Util.TweenUtil.Token;
    using TimeType = Supercent.Util.TweenUtil.TimeType;
    public enum EEventCamType
    {
        None = -1,
        StorageShelf_Shoe = 0,
        CounterEmployee = 1,
        DisplayCloathes = 2,
        StorageShelf_Clothes = 3,
    }

    public class PlayerCamera : MonoBehaviour, IPositionReturner
    {
        [Header("Serialize Fields")]
        [Space] 
        [SerializeField] private Vector3 _presetPosition = Vector3.zero;
        [SerializeField] private Quaternion _presetRotation = Quaternion.identity;
        [SerializeField] private List<Transform> _eventCamTransList = null;
        [SerializeField] private AnimationCurve _playerCamAnimCurve = null;
        // [SerializeField] private float _
        [SerializeField] private float _camMoveTimer = 0.5f;
        [SerializeField] private float _camWaitTimer = 0.5f;
        [SerializeField] private float _camComeBackTimer = 0.5f;

        //===============================================================
        //Properties
        //===============================================================
        public IPositionReturner PlayerPosistionReturner{ set { _playerPosReturner = value;} }
        public event Action OnFirstMoveEventDone {add => _onFirstMoveEventDone += value; remove => _onFirstMoveEventDone -= value;}

        //===============================================================
        //Fields
        //===============================================================
        private IPositionReturner _playerPosReturner = null;
        private Action _onFirstMoveEventDone = null;
        private Vector3 _targetPos = Vector3.zero;
        private Vector3 _playerPos = Vector3.zero;
        private Token _eventCamToken;
        private bool _isEvnet = false;

        //================================================================
        //Functions
        //================================================================
        private void Awake()
        {
            this.transform.localPosition = _presetPosition;
            this.transform.rotation = _presetRotation;     
        }

        private void Start()
        {
            _playerPos = _playerPosReturner.GetPosition();
            _targetPos = _playerPos - this.transform.position;
        }

        private void LateUpdate()
        {
            if (!_isEvnet)
            {
                _playerPos = _playerPosReturner.GetPosition();
                this.transform.position = _playerPos - _targetPos;
            }
        }

        public void StartEventCamYoyo(EEventCamType type)
        {
            var moveHandler =_playerPosReturner as IPlayerMoveHandler;
            
            if ( moveHandler == null )
                return;

            if ( _eventCamToken.IsValid() )
                return;

            _isEvnet = true;
            moveHandler.IsOKToMove = false;

            var defaultPos = this.transform.position;
            var idx = (int)type;
            var targetTrans = _eventCamTransList[idx];

            var camMoveTimer = _camMoveTimer;
            var camComebackTimer = _camComeBackTimer;

            if (type == EEventCamType.DisplayCloathes)
            {
                camMoveTimer -= 0.2f;
                camComebackTimer -= 0.2f;
            }

            TweenUtil.TweenPosition(
                this.transform,
                targetTrans,
                new Params(TimeType.Scale)
                {    
                    secDuration = camMoveTimer, 
                    timeModular = (t) => EaseUtil.SineIn(t) 
                }, 
                (done_MoveIn) =>{

                    var waitPos = new Vector3(
                        this.transform.position.x, 
                        this.transform.position.y + 0.0001f, 
                        this.transform.position.z);

                    TweenUtil.TweenPosition(
                        this.transform,
                        waitPos,
                        false,
                        _camWaitTimer,
                        (done_Wait) =>{
                            TweenUtil.TweenPosition(
                            this.transform,
                            defaultPos,
                            new Params(TimeType.Scale)
                            {    
                                secDuration = camComebackTimer, 
                                timeModular = (t) => EaseUtil.SineIn(t) 
                            }, 
                            (done_MoveOut) =>{

                                if (_onFirstMoveEventDone != null)
                                {
                                    _onFirstMoveEventDone.Invoke();
                                    _onFirstMoveEventDone = null;
                                }

                                _isEvnet = false;
                                moveHandler.IsOKToMove = true;
                            });
                    });
            });
        }

        public Vector3 GetPosition() => this.transform.position;
    }
}

