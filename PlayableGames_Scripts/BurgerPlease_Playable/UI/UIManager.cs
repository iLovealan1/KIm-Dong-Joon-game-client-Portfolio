using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   
using Supercent.Util;

namespace LunaBurger.Playable010
{
    public enum eUpgradeBtnLevel
    {
        None = 0,
        LEVEL1 = 1,
        LEVEL2 = 2,
        LEVEL3 = 3,
        LEVEL4 = 4,
    }

    public enum eBtnType
    {
        None = 0,
        COUNTER,
        PICKUP,
        MACHINE
    }

    public class UIManager : MonoBehaviour
    {
        [SerializeField] private UICashController       _cashController;
        [SerializeField] private UIBtnUpgradeController _counterUpgradeButton;
        [SerializeField] private UIBtnUpgradeController _pickupUpgradeButton;
        [SerializeField] private UIBtnUpgradeController _machineUpgradeButton;
        [SerializeField] private CanvasGroup            _endCardCanvasGroup;
        [SerializeField] private Button                 _btnEndCard;  

        public static System.Action                             OnUpdateUICash { get; private set; }
        public static System.Action                             OnEndcardPopup { get; private set; }
        public static System.Action<bool,EGameState, eCamState> OnChangeUIPos  { get; private set; }

        //-----------------------------------------------------------------------------
        //화면 회전 대응
        //-----------------------------------------------------------------------------\
        [Header("For UIPos")]
        [Space]
        [SerializeField] private RectTransform _btnCounterRect;
        [SerializeField] private RectTransform _btnPickupRect;
        [SerializeField] private RectTransform _btnMachineRect;

        [System.Serializable]
        private class UIPosData
        {
            public EGameState state1;
            public Vector2 _btnPortraitCounterPos;
            public Vector2 _btnLandScapeCounterPos;
            public Vector2 _btnPortraitPickupPos;
            public Vector2 _btnLandScapePickupPos;
            public Vector2 _btnPortraitMachinePos;          
            public Vector2 _btnLandScapeMachinePos; 

            [Header("Starit Cam State Options")]
            [Space]
            
            public Vector2 _STRbtnPortraitCounterPos;
            public Vector2 _STRbtnLandScapeCounterPos;
            public Vector2 _STRbtnPortraitPickupPos;
            public Vector2 _STRbtnLandScapePickupPos;
            public Vector2 _STRbtnPortraitMachinePos;          
            public Vector2 _STRbtnLandScapeMachinePos;          
        }

        [SerializeField] private List<UIPosData> _uiPosDataList;

        public void Init(System.Action<EGameState> OnChangeState)
        {
            _cashController.Init(); 
            _counterUpgradeButton.Init(eBtnType.COUNTER,eUpgradeBtnLevel.LEVEL1);
            _pickupUpgradeButton.Init(eBtnType.PICKUP,eUpgradeBtnLevel.None); 
            _machineUpgradeButton.Init(eBtnType.MACHINE,eUpgradeBtnLevel.LEVEL1,OnChangeState);

            // CTA 버튼 팝업
            _btnEndCard.onClick.AddListener(() => {
                // Playable.InstallFullGame();
            });

            OnUpdateUICash = () => {
                _cashController.UpdateCashText();
            };

            OnEndcardPopup = () => {
                _endCardCanvasGroup.gameObject.SetActive(true);
                SoundManager.NullableInstance.OnStopBGMAndSFX();
                // LifeCycle.GameEnded();
                StartCoroutine(CoStartEndCardpopup());  
            };

            OnChangeUIPos = (isLandScape, sate, eCamState) => {
                SetBtnsPos(isLandScape,sate,eCamState);
            };
        }

        private IEnumerator CoStartEndCardpopup()
        {
            while (_endCardCanvasGroup.alpha < 1)
            {
                _endCardCanvasGroup.alpha += Time.fixedDeltaTime / 0.1f;
                yield return CoroutineUtil.WaitForFixedUpdate;
            }

        }

        private void SetBtnsPos(bool isLandScape , EGameState state, eCamState camState)
        {
            var idx = (int)state;
            var data = _uiPosDataList[idx];
            if (camState == eCamState.SDIE)
            {
                if(isLandScape)
                {
                    _btnCounterRect.anchoredPosition = data._btnLandScapeCounterPos;
                    _btnPickupRect.anchoredPosition = data._btnLandScapePickupPos;
                    _btnMachineRect.anchoredPosition = data._btnLandScapeMachinePos;                  
                }
                else 
                {     
                    _btnCounterRect.anchoredPosition = data._btnPortraitCounterPos;
                    _btnPickupRect.anchoredPosition = data._btnPortraitPickupPos;
                    _btnMachineRect.anchoredPosition = data._btnPortraitMachinePos; 
                }
            }
            else if (camState == eCamState.STRAIT)
            {
                if(isLandScape)
                {
                    _btnCounterRect.anchoredPosition = data._STRbtnLandScapeCounterPos;
                    _btnPickupRect.anchoredPosition = data._STRbtnLandScapePickupPos;
                    _btnMachineRect.anchoredPosition = data._STRbtnLandScapeMachinePos;                  
                }
                else 
                {     
                    _btnCounterRect.anchoredPosition = data._STRbtnPortraitCounterPos;
                    _btnPickupRect.anchoredPosition = data._STRbtnPortraitPickupPos;
                    _btnMachineRect.anchoredPosition = data._STRbtnPortraitMachinePos; 
                }
            }
            
        }

        private void OnDestroy() 
        {
            OnUpdateUICash = null;
            OnEndcardPopup = null;
        }   
    }
}