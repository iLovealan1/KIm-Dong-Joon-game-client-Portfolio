using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace luna_sportshop.Playable002
{
    using Action = System.Action;

    public abstract class Unit : MonoBehaviour
    {
        protected enum EUnitAnimationName
        {
            None = -1,
            CPI_Player_Idle,
            CPI_Player_run,
            CPI_Stack_Idle,
            CPI_Stack_Run
        }

        [Header("=====Unit SerializeField=====")]
        [Space]
        [SerializeField] protected List<Transform> _stackPointList = null;
        [SerializeField] protected Rigidbody _rigedBody = null;
        [SerializeField] protected float _speed = 2;
        [SerializeField] protected Animator _anim = null;

        [Space]
        [Header("===========애니메이션 세부 시간 조절==========")]
        [Space]

        [Header("아이템을 가져가는 간격")]
        [SerializeField] protected float _itemTakeTimeInterval =  0.1f;
        [Header("아이템 이동시 목적지까지 걸리는 시간")]
        [SerializeField] protected float _itemMoveTimeLimit = 0.3f;
        [Header("돈을 가져가는 간격")]
        [SerializeField] protected float _moneyTakeInterval = 0.1f;
        [Header("돈 이동시 목적지까지 걸리는 시간")]
        [SerializeField] protected float _moneyMoveTimeLimit = 0.1f;
        [Space]

        [Space]
        [Header("==============애니메이션 커브==============")]
        [Space]
        [Header("아이템 이동 커브")]
        [SerializeField] protected AnimationCurve _itemMoveCurve = null;
        [Header("아이템 이동시 높이 커브")]
        [SerializeField] protected AnimationCurve _itemJumpCurve = null;
        [Header("돈 이동 커브")]
        [SerializeField] protected AnimationCurve _moneyMoveCurve = null;
        [Header("돈 이동시 높이 커브")]
        [SerializeField] protected AnimationCurve _moneyJumpCurve = null;
        [Space]

        //===============================================================
        //Fields
        //===============================================================
        protected List<Item> _currItemList = null;
        protected Coroutine _takeItemCorutine = null;
        protected bool _isCarrying = false;

        //===============================================================
        //Functions
        //===============================================================
        protected virtual void Awake() => _currItemList = new List<Item>();
        protected abstract void ChangeAnimation(bool isStopped);
        protected abstract IEnumerator TakeItems(List<Item> takenItemStack, Action doneCallback = null);
    }
}