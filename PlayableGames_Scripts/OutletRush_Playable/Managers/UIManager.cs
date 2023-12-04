using System;
using UnityEngine;

namespace luna_sportshop.Playable002
{
    public class UIManager : MonoBehaviour
    {
        [Header("=====UIManager Init Field=====")]
        [Space]
        [SerializeField] private UIObjectField _uiObjects = null;

        //===============================================================
        //Functions
        //===============================================================
        private void Awake()
        {
            _uiObjects.JoyStick.OnTouched += _uiObjects.DrageGuide.SetOffDragGuide;
            MoneyManager.OnMoneyUpdated += _uiObjects.UIMoney.UpdateTextMoney;
        }

        public void Init(IPlayerMoveHandler playerHandler, DisplayShelf clothesShelf ,Action<EGuideArrowState> onGameStart)
        {         
            _uiObjects.JoyStick.PlayerMoveHandler = playerHandler;
            _uiObjects.DrageGuide.PlayerMoveHandler = playerHandler;
            _uiObjects.JoyStick.OnGameStart += onGameStart;
            clothesShelf.OnClothesDisplaying += _uiObjects.BtnCTA.ActivateCTAButton;
            clothesShelf.OnClothesDisplaying += () => playerHandler.IsOKToMove = false;
        }
    }
}