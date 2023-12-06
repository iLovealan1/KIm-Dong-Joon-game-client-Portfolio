using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

namespace LunaBurger.Playable010
{ 
    
    public class UIBtnUpgradeController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler // 전체적으로 리펙토링 필요한 클래스 (버튼 컴포넌트 상속 고려)
    {
        [SerializeField] private TextMeshProUGUI        _txtPrice;
        [SerializeField] private Image                  _imgBtn;
        [SerializeField] public  Transform              _guideHandTrans;
        [SerializeField] private Sprite                 _btnActiveSprite;
        [SerializeField] private Sprite                 _btnDeActiveSprite;
        [SerializeField] private Animator               _btnAnim;

        [Header("BurgerMachine Only Field")]
        [Space]
        [SerializeField] private UIBtnUpgradeController _counterUpgradeCon;
        [SerializeField] private UIBtnUpgradeController _pickupUpgradeCon;

        private eBtnType            _btnType;
        public  eBtnType            BtnType      => _btnType;    
        private eUpgradeBtnLevel    _btnLevel;
        public  eUpgradeBtnLevel    BtnLevel     => _btnLevel;
        private int                 _upgradePrice;
        public  int                 UpgradePrice => _upgradePrice; 

        private const string        TXTMAXLEVEL  = "MAX";
        private const float         CHECKINTERVAL = 0.1f;
        private bool                _isLandScape = false;
        private bool                _isAllUpgraded = false;

        public System.Action<bool>        _OnChangeBtnSize      {get; private set;}
        private System.Action<EGameState> _onRequestChangeState = null;

        public void Init(eBtnType type, eUpgradeBtnLevel btnLevel, System.Action<EGameState> OnstateChange = null) 
        {
            // _OnChangeBtnSize = (isLandScape) =>{
            //     ChangeAnimState(isLandScape);
            // };
            _btnLevel = btnLevel;  
            _btnType = type;

                
            CheckType(OnstateChange);
        }

        // private void ChangeAnimState(bool isLandScape)
        // {
        //     _isLandScape = isLandScape;
        //     if(_isLandScape) _btnAnim.Play("BtnRandScapeIdle");
        //     else _btnAnim.Play("BtnPortraitIdle");
        // }

        private void CheckType(System.Action<EGameState> OnstateChange = null)
        {
            var level = (int) _btnLevel;
            if (eBtnType.COUNTER == _btnType)
            {
                var price = CounterManager.OnReciveUpgradePrice?.Invoke((eUnitLevel)level);
                _upgradePrice = price ?? 0;
            }
            else if (eBtnType.PICKUP == _btnType)
            {
                var price = PickupManager.OnReciveUpgradePrice?.Invoke((eUnitLevel)level);
                _upgradePrice = price ?? 0;
            }
            else if (eBtnType.MACHINE == _btnType)
            {
                var price = BurgerMachineManager.OnReciveUpgradePrice?.Invoke();
                // var price = OnReciveUpgradePrice?.Invoke();  이렇게 되도록 짜보자
                _upgradePrice = price ?? 0;                    
                _onRequestChangeState += OnstateChange;
            }
            else
            {
                Debug.LogError("Initializing Required");
            }

            _txtPrice.text = _upgradePrice.ToString();      
            StartCoroutine(CoStartChecking(_btnType));
        }


        private IEnumerator CoStartChecking(eBtnType type)
        {
            while (true)
            {
                var currCash = CashManager.Instance.CurrentCash;
                var pickupLevel = _pickupUpgradeCon.BtnLevel;   

                if(type == eBtnType.COUNTER || type == eBtnType.PICKUP)
                {                      
                    if(_btnLevel == eUpgradeBtnLevel.LEVEL4)
                    {
                        if(_imgBtn.sprite != _btnDeActiveSprite)
                            _imgBtn.sprite = _btnDeActiveSprite;
                    }
                    else if (currCash < _upgradePrice)
                    {
                        if(_imgBtn.sprite != _btnDeActiveSprite)
                            _imgBtn.sprite = _btnDeActiveSprite;
                    }
                    else if (currCash >= _upgradePrice && pickupLevel != eUpgradeBtnLevel.None)
                    {
                        if(_imgBtn.sprite != _btnActiveSprite)
                            _imgBtn.sprite = _btnActiveSprite;
                    }
                }
                else if(type == eBtnType.MACHINE)
                {
                    var counterLevel = _counterUpgradeCon.BtnLevel;

                    if(currCash >= _upgradePrice)
                    {                
                        if (counterLevel == eUpgradeBtnLevel.LEVEL4 && pickupLevel == eUpgradeBtnLevel.LEVEL4)
                        {
                            _guideHandTrans.gameObject.SetActive(true);
                        }
                       
                       if (_imgBtn.sprite == _btnDeActiveSprite && _pickupUpgradeCon.BtnLevel != eUpgradeBtnLevel.None)
                            _imgBtn.sprite = _btnActiveSprite;
                    }
                    else
                    {
                        if(_imgBtn.sprite == _btnActiveSprite)
                            _imgBtn.sprite = _btnDeActiveSprite;
                    }

                    CompareAndActiveGuideHand(counterLevel,pickupLevel,currCash);           
                }   

                yield return CoroutineUtil.WaitForSeconds(CHECKINTERVAL);
            }
        }

        private void CompareAndActiveGuideHand(eUpgradeBtnLevel counterLevel , eUpgradeBtnLevel pickupLevel, int currCash)
        {

            if(_isAllUpgraded)
                return;

            if(counterLevel == eUpgradeBtnLevel.LEVEL4 && pickupLevel == eUpgradeBtnLevel.LEVEL4)
            {
                _onRequestChangeState(EGameState.STATE4);           
                _counterUpgradeCon._guideHandTrans.gameObject.SetActive(false);
                _pickupUpgradeCon._guideHandTrans.gameObject.SetActive(false);
                _isAllUpgraded = true;
                return;
            } 

            var currCounterLevel = (int)counterLevel; 
            var currPickupLevel = (int)pickupLevel; 

            // 게임 상태 체크 부분
            if (currCounterLevel != currPickupLevel && currPickupLevel != 0)
            {
                if (currCounterLevel > currPickupLevel )
                    _onRequestChangeState?.Invoke((EGameState)currCounterLevel);
                else if (currCounterLevel < currPickupLevel)
                    _onRequestChangeState?.Invoke((EGameState)currPickupLevel);
            } 

            if (currCounterLevel <= currPickupLevel)
            {
                if (currCash >= _counterUpgradeCon.UpgradePrice)
                {
                    _counterUpgradeCon._guideHandTrans.gameObject.SetActive(true);
                    _pickupUpgradeCon._guideHandTrans.gameObject.SetActive(false);
                }
                else
                {
                    _counterUpgradeCon._guideHandTrans.gameObject.SetActive(false);
                    _pickupUpgradeCon._guideHandTrans.gameObject.SetActive(false);
                }                 
            }
            else
            {
                if(currCash >= _pickupUpgradeCon.UpgradePrice)
                {
                    _pickupUpgradeCon._guideHandTrans.gameObject.SetActive(true);
                    _counterUpgradeCon._guideHandTrans.gameObject.SetActive(false);
                }
                else
                {
                    _pickupUpgradeCon._guideHandTrans.gameObject.SetActive(false);
                    _counterUpgradeCon._guideHandTrans.gameObject.SetActive(false);
                }                 
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {        
            if (_isLandScape)
                _btnAnim.Play("BtnLandScapePress");
            else 
                _btnAnim.Play("Pressed");

            if(_btnType != eBtnType.PICKUP && _pickupUpgradeCon.BtnLevel == eUpgradeBtnLevel.None)
            {
                SoundManager.NullableInstance?.OnPlaySFX(eSoundName.CLICKBLOCK);
                return;   
            }

            var currCash = CashManager.Instance?.CurrentCash;

            if (currCash < UpgradePrice || _btnLevel == eUpgradeBtnLevel.LEVEL4)
            {
                SoundManager.NullableInstance?.OnPlaySFX(eSoundName.CLICKBLOCK);
                return;    
            }      
            else
                SoundManager.NullableInstance?.OnPlaySFX(eSoundName.CLICK);

            eUnitLevel unitLevel = eUnitLevel.NONE; 
            var level = 0;
            var price = 0;
            var nextUpgradePrice = 0;
            var customerLevel = 0;
            var beforeUpgradeLevel = 0;

            switch(_btnType)
            {
                case eBtnType.COUNTER:
                unitLevel = CounterManager.OnRequestUpgrade?.Invoke() ?? eUnitLevel.NONE;
                level = (int)unitLevel;
                customerLevel = level;
                _btnLevel  = (eUpgradeBtnLevel)level;
                CustomerManager.OnSpawnCustomer((eCustomerLevel)customerLevel);
                beforeUpgradeLevel = level -1;
                price = CounterManager.OnReciveUpgradePrice((eUnitLevel)beforeUpgradeLevel);
                CashManager.Instance.UpdateCurrentCash(-price);     
                UIManager.OnUpdateUICash();
                if(_btnLevel != eUpgradeBtnLevel.LEVEL4)
                {
                    nextUpgradePrice = CounterManager.OnReciveUpgradePrice((eUnitLevel)level);
                    _txtPrice.text = nextUpgradePrice.ToString();
                }
                else
                {
                    _txtPrice.text = TXTMAXLEVEL;
                }      
                _upgradePrice = nextUpgradePrice;
                break;

                case eBtnType.PICKUP:
                unitLevel = PickupManager.OnRequestUpgrade?.Invoke() ?? eUnitLevel.NONE;
                level = (int)unitLevel;
                _btnLevel  = (eUpgradeBtnLevel)level;
                beforeUpgradeLevel = level -1;
                price = PickupManager.OnReciveUpgradePrice((eUnitLevel)beforeUpgradeLevel);
                CashManager.Instance.UpdateCurrentCash(-price);   
                UIManager.OnUpdateUICash();
                if(_btnLevel != eUpgradeBtnLevel.LEVEL4)
                {
                    nextUpgradePrice = PickupManager.OnReciveUpgradePrice((eUnitLevel)level);
                    _txtPrice.text = nextUpgradePrice.ToString();
                }
                else
                {
                        _txtPrice.text = TXTMAXLEVEL;
                }      
                _upgradePrice = nextUpgradePrice;
                break;

                case eBtnType.MACHINE:
                UIManager.OnEndcardPopup();
                break;
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {        
            if(_isLandScape)_btnAnim.Play("BtnLandScapeRelease");
            else _btnAnim.Play("Highlighted");
        }

        private void OnDestroy()
        {
            this.StopAllCoroutines();
        }
    }
}

