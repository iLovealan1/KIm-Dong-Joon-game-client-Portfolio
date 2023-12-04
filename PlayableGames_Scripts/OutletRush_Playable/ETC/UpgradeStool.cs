using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Supercent.Util;
using TMPro;

namespace luna_sportshop.Playable002
{
    using Params = Supercent.Util.TweenUtil.Params;
    using Token = Supercent.Util.TweenUtil.Token;
    using TimeType = Supercent.Util.TweenUtil.TimeType;

    public enum EUpgradeStoolType
    {
        None = -1,
        Counter_UpgradeStool,
        Dipslay_Shoe1_UpgradeStool,
        Dipslay_Shoe2_UpgradeStool,
        Dipslay_Clothes_UpgradeStool,
        Counter_Employee_UpgradeStool,
        Storage_Shoe_UpgradeStool,
        Storage_Clothes_UpgradeStool
    }

    public class UpgradeStool : MonoBehaviour
    {
        [Header("======UpgradeStool SerializeField======")]
        [Space]
        [SerializeField] private EUpgradeStoolType _type = EUpgradeStoolType.None;
        [SerializeField] private GameObject _stoolObejct = null;
        [SerializeField] private MoneyPool _ownPool = null;
        [SerializeField] private Transform _targetBoingTrans = null;
        [SerializeField] private bool  _isDefaultStool = false;
        [SerializeField] private int   _upgradePrice = 0;
        [SerializeField] private Vector3 _targetYoYoScale = Vector3.zero;
        [SerializeField] private float _secDuration = 0.5f;

        [Space]
        [Header("===========애니메이션 세부 시간 조절==========")]
        [Space]
        [Header("돈을 가져오는 간격 (스택 버전)")]
        [SerializeField] private float _stackInterval = 0.1f;
        [Header("돈 이동시 목적지까지 걸리는 시간 (스택 버전)")]
        [SerializeField] private float _stackTimelimit = 0.2f;
        [Header("돈을 가져오는 간격 (논 스택 버전)")]
        [SerializeField] private float _noneStackInterval = 0.05f;
        [Header("돈 이동시 목적지까지 걸리는 시간 (논 스택 버전)")]
        [SerializeField] private float _noneStackTimelimit = 1f;

        [Space]
        [Header("===========애니메이션 커브 조절==========")]
        [Space]
        [Header("돈 이동 커브 (스택 버전)")]
        [SerializeField] private AnimationCurve _stackMoveCurve = null;
        [Header("돈 점프 커브 (스택 버전)")]
        [SerializeField] private AnimationCurve _stackJumpCurve = null;
        [Header("돈 이동 커브 (논 스택 버전)")]
        [SerializeField] private AnimationCurve _noneStackMoveCurve = null;
        [Header("돈 점프 커브 (논 스택 버전)")]
        [SerializeField] private AnimationCurve _noneStackJumpCurve = null;

        [SerializeField] private Image _imgFill = null;
        [SerializeField] private TextMeshProUGUI _txtUpgradePrice = null;

        //===============================================================
        //Properties
        //===============================================================
        public static bool IsMoneyStackingMode {get; set;}
        public int UpgradePrice { set => _upgradePrice = value; }
        public event Action OnGaugeFull {add {_onGaufeFull += value;} remove {_onGaufeFull -= value;}}
        public event Action<EGuideArrowState> OnGaugeFulGuideCall {add {_onGagudeFullGuideCall += value;} remove {_onGagudeFullGuideCall -= value;}}
        public event Action<EEventCamType> OnGaugeFulCamCall {add {_onGaugeFulCamCall += value;} remove {_onGaugeFulCamCall -= value;}}

        //===============================================================
        //Fields
        //===============================================================
        private Action _onGaufeFull = null;
        private Action<EGuideArrowState> _onGagudeFullGuideCall = null;
        private Action<EEventCamType> _onGaugeFulCamCall = null;
        private Coroutine _takeMoneyCoroutine = null;
        private Token _tweenToken;
        private bool _isOkToTakeMoney = true;
        private bool _isPlayerIn = false;
        private float _minFillAmount = 0f;
        private int _needMoneyObjectAmount = 0;
        private float _noStackTimer = 1f;

        //===============================================================
        //Functions
        //===============================================================
        private void Start()
        {
            _txtUpgradePrice.text = _upgradePrice.ToString();

            if (IsMoneyStackingMode)
            {
                _minFillAmount =  (float)Money.Price / _upgradePrice;
                _needMoneyObjectAmount = (_upgradePrice / Money.Price);
            }
            else
            {
                _minFillAmount = 1f / (float)_upgradePrice;
            }

            if (!_isDefaultStool)
                this.gameObject.SetActive(false);

        }

        private void OnEnable() 
        {
            if (_tweenToken.IsValid())
                return;

            _tweenToken = TweenUtil.LoopScale(_targetBoingTrans, _targetYoYoScale, true,_secDuration);          
        }

        private void OnDisable()  
        {
            if (!_tweenToken.IsValid())
                return;

            _tweenToken.Stop();
        }

        private void OnTriggerEnter(Collider other)
        {
            var type = (ELayerName)other.gameObject.layer;
            
            if (type != ELayerName.Player)
                return;

            _isPlayerIn = true;

            if (IsMoneyStackingMode)
            {
                if (other.TryGetComponent<IMoneyStackReturner>(out IMoneyStackReturner returner))
                {
                    var takenStack = returner.GetMoneyStack(out Action doneCallback);
                    
                    if (takenStack != null)
                        _takeMoneyCoroutine = this.StartCoroutine(CoTakeMoney(takenStack,other.transform, doneCallback));

                }
            }
            else
            {
                if (_takeMoneyCoroutine == null)
                    _takeMoneyCoroutine = this.StartCoroutine(CoTakeMoney_NoStackVer(other.transform));
            }      
        }

        private void OnTriggerExit(Collider other) 
        {
            var type = (ELayerName)other.gameObject.layer;

            if (type != ELayerName.Player)
                return;

            _isPlayerIn = false;

            if (IsMoneyStackingMode)
            {
                if (_takeMoneyCoroutine != null)
                {
                    this.StopCoroutine(_takeMoneyCoroutine);
                    _takeMoneyCoroutine = null;
                }
            }
            else
            {
                 if (_takeMoneyCoroutine != null)
                {
                    _takeMoneyCoroutine = null;
                }
            }

        }

        private IEnumerator CoTakeMoney(Stack<Money> takenMoneyStack,Transform playerTrans , Action doneCallback)
        {            
            while(_needMoneyObjectAmount > 0)
            {
                if (takenMoneyStack.Count == 0)
                    yield break;

                _isOkToTakeMoney = false;

                var money = takenMoneyStack.Pop();
                _needMoneyObjectAmount--;

                AudioManager.NullableInstance.PlaySFX(EAudioName.StackSound,true,false,0.15f);

                if (_needMoneyObjectAmount == 0)
                {
                    this.StartCoroutine(CoJumpMoney(money, doneCallback));
                }
                else
                {
                    this.StartCoroutine(CoJumpMoney(money));
                }

                yield return CoroutineUtil.WaitForSeconds(_stackInterval);
            }
        }

        private IEnumerator CoJumpMoney(Money money, Action doneCallback = null)
        {
            var moneyTrans = money.transform;
    
            var startSec = Time.time;
            var endSec = startSec + _stackTimelimit;
            var startPos = moneyTrans.position;

            while (Time.time < endSec)
            {
                var ratio = (Time.time - startSec) / _stackTimelimit;
                moneyTrans.position = Vector3.Lerp(startPos, this.transform.position , _stackMoveCurve.Evaluate(ratio));
                moneyTrans.position = moneyTrans.position + Vector3.up * _stackJumpCurve.Evaluate(ratio);
                yield return CoroutineUtil.WaitForFixedUpdate;
            }

            _upgradePrice -= Money.Price;
            _txtUpgradePrice.text = _upgradePrice.ToString();
            MoneyManager.UpdateCurrentMoney(-Money.Price);
            moneyTrans.rotation = Quaternion.identity;
            yield return null;

            money.Release();

            var currFill = _imgFill.fillAmount;
            var taregtFill = currFill + _minFillAmount;

            while(_imgFill.fillAmount < taregtFill)
            {
                _imgFill.fillAmount += 0.03f;

                if (_imgFill.fillAmount >= 0.99f)
                {
                    _imgFill.fillAmount = 1;
                    break;
                }
                
                yield return null;
            }

            if (doneCallback != null)
            {
                _imgFill.fillAmount = 1;

                yield return CoroutineUtil.WaitForFixedUpdate;

                AudioManager.NullableInstance.PlaySFX(EAudioName.UpgradeSound);

                  if (_stoolObejct != null)
                    _stoolObejct.SetActive(false);

                _onGaufeFull?.Invoke();

                if ( _type == EUpgradeStoolType.Dipslay_Clothes_UpgradeStool || _type == EUpgradeStoolType.Dipslay_Shoe2_UpgradeStool)
                    yield return CoroutineUtil.WaitForSeconds(1.1f);
                else if (_type == EUpgradeStoolType.Counter_Employee_UpgradeStool)
                    yield return CoroutineUtil.WaitForSeconds(1.1f);

                CallEventByType();

                _takeMoneyCoroutine = null;

                AudioManager.NullableInstance.ResetPitch(EAudioName.StackSound,true);

                doneCallback.Invoke();             
                this.gameObject.SetActive(false);
            }

            _isOkToTakeMoney = true;
        }

        private IEnumerator CoTakeMoney_NoStackVer(Transform playerTrans)
        {
            var timer = 0f;

            while ( _upgradePrice > 0 )
            {
                if ( MoneyManager.CurrentMoney <= 0 )
                {
                    MoneyManager.SetCurrentMoney(0);
                    yield break;
                }

                if ( !_isPlayerIn )
                {
                    yield break;
                }

                MoneyManager.UpdateCurrentMoney(-1);
                _upgradePrice -= 1;
                _txtUpgradePrice.text = _upgradePrice.ToString();
                _imgFill.fillAmount += _minFillAmount;
                timer += Time.fixedDeltaTime;

                if ( _upgradePrice == 0 )
                {
                    var money = _ownPool.GetMoney();
                    money.transform.position = playerTrans.position;
                    yield return null;

                    this.StartCoroutine(CoJumpMoney_NoStackVer(money,true));
                }
                else if ( timer > _noneStackInterval ) 
                {
                    AudioManager.NullableInstance.PlaySFX(EAudioName.StackSound,true,false,0.1f);

                    var money = _ownPool.GetMoney();
                    money.transform.position = playerTrans.position;
                    timer = 0;
                    yield return null;
  
                    this.StartCoroutine(CoJumpMoney_NoStackVer(money));
                }
                
                yield return CoroutineUtil.WaitForFixedUpdate;
            }
        }

        private IEnumerator CoJumpMoney_NoStackVer( Money money, bool isDone = false )
        {
            var moneyTrans = money.transform;            

            var startSec = Time.time;
            var endSec = startSec + _noneStackTimelimit;
            Vector3 startPos = moneyTrans.position;

            while ( Time.time < endSec )
            {
                var ratio = ( Time.time - startSec ) / _noneStackTimelimit;
                moneyTrans.position = Vector3.Lerp( startPos, this.transform.position, _stackMoveCurve.Evaluate( ratio ) );
                moneyTrans.position = moneyTrans.position + Vector3.up * _noneStackJumpCurve.Evaluate( ratio );
                
                yield return CoroutineUtil.WaitForFixedUpdate;
            }

            moneyTrans.rotation = Quaternion.identity;
            yield return null;

            money.Release();

            if ( isDone )
            {
                _imgFill.fillAmount = 1;

                yield return CoroutineUtil.WaitForFixedUpdate;
                
                AudioManager.NullableInstance.PlaySFX(EAudioName.UpgradeSound);

                if (_stoolObejct != null)
                    _stoolObejct.SetActive(false);

                _onGaufeFull?.Invoke();

                if ( _type == EUpgradeStoolType.Dipslay_Clothes_UpgradeStool || _type == EUpgradeStoolType.Dipslay_Shoe2_UpgradeStool)  
                    yield return CoroutineUtil.WaitForSeconds(1.1f);
                else if (_type == EUpgradeStoolType.Counter_Employee_UpgradeStool)
                    yield return CoroutineUtil.WaitForSeconds(1.1f);

        
                CallEventByType();

                _takeMoneyCoroutine = null;

                AudioManager.NullableInstance.ResetPitch( EAudioName.StackSound,true );

                this.gameObject.SetActive( false );
            }
        }

        private void CallEventByType()
        {
            var arrowTaregt = EGuideArrowState.None;
            var camTarget = EEventCamType.None;

            switch ( _type )
            {
                case EUpgradeStoolType.Counter_UpgradeStool : 
                { arrowTaregt = EGuideArrowState.DisplayShelf_Shoe1_Upgrade; break; }

                case EUpgradeStoolType.Dipslay_Shoe1_UpgradeStool : 
                { arrowTaregt = EGuideArrowState.StorageShelf_Shoe_Upgrade; break; }

                case EUpgradeStoolType.Storage_Shoe_UpgradeStool : 
                { arrowTaregt = EGuideArrowState.StorageSHelf_Shoe_Take; break; }
            }

            if ( arrowTaregt != EGuideArrowState.None )
                _onGagudeFullGuideCall?.Invoke( arrowTaregt );

            switch ( _type )
            {
                case EUpgradeStoolType.Dipslay_Shoe1_UpgradeStool : 
                {camTarget = EEventCamType.StorageShelf_Shoe; break; }

                case EUpgradeStoolType.Dipslay_Shoe2_UpgradeStool : 
                {camTarget = EEventCamType.CounterEmployee; break; }

                case EUpgradeStoolType.Counter_Employee_UpgradeStool : 
                {camTarget = EEventCamType.DisplayCloathes; break; }

                case EUpgradeStoolType.Dipslay_Clothes_UpgradeStool : 
                {camTarget = EEventCamType.StorageShelf_Clothes; break; }
            }

            if ( camTarget != EEventCamType.None )
                _onGaugeFulCamCall?.Invoke( camTarget );
        }

        public void ActivateStool() => this.gameObject.SetActive(true);
    }
}