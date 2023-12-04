using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace luna_sportshop.Playable002
{
    public enum ELayerName
    {
        None = -1,
        Player = 6,
        Customer = 7,
        Counter = 8,
        Shelf = 9,
        DisplayShelf = 10,
        MoneyStacker = 11,
        CounterPlayerSide = 12,
        ShelfCustomer = 13,
    }
    public class GameManger : MonoBehaviour
    {
        [Header("======Init Fieldsx")]
        [Space]
        [SerializeField] private WorldObjectField _worldObjects = null;

        [Header("=====Manager Fields=====")]
        [Space]
        [SerializeField] private UIManager _UIManager = null;
        [SerializeField] private CustomerManager _customerManager = null;
        [SerializeField] private AudioManager _audioManger = null;
        [SerializeField] private StoolManager _stoolManager = null;

        //===============================================================
        //Luna SerializeField
        //===============================================================
        // [LunaPlaygroundField("MoneyStackingMode ON/OFF")]
        [SerializeField] private bool _isMoneyStackingMode = true;

        // [LunaPlaygroundField("Start Money Amount")]
        [SerializeField] private int _startMoney = 90;
        // [LunaPlaygroundField("Money amount per one")] 
        [SerializeField] private int _moneyPrice = 10;
        
        // [LunaPlaygroundField("Item Price")] 
        [SerializeField] private int _itemPrice = 10;
        
        // [LunaPlaygroundField("카운터",1,"UpgradeStool Prices")] 
        [SerializeField] private int _counter_UpgradePrice = 30;

        // [LunaPlaygroundField("신발 매대",2,"UpgradeStool Prices")] 
        [SerializeField] private int _dipslay_Shoe1_UpgradePrice = 30;

        // [LunaPlaygroundField("신발 매대 2",3,"UpgradeStool Prices")] 
        [SerializeField] private int _dipslay_Shoe2_UpgradePrice = 30;

        // [LunaPlaygroundField("옷 매대",4,"UpgradeStool Prices")] 
        [SerializeField] private int _dipslay_Clothes_UpgradPrice = 50;

        // [LunaPlaygroundField("고용",5,"UpgradeStool Prices")] 
        [SerializeField] private int _counter_Employee_UpgradePrice = 40;
        
        // [LunaPlaygroundField("신발 창고",6,"UpgradeStool Prices")] 
        [SerializeField] private int _storage_Shoe_UpgradePrice = 30;

        // [LunaPlaygroundField("옷 창고",7,"UpgradeStool Prices")] 
        [SerializeField] private int _storage_Clothes_UpgradePrice = 60;

        //===============================================================
        //Functions
        //===============================================================
        private void Awake()
        {

#region Cashing
            var player = _worldObjects.Player;
            var playerCam = _worldObjects.PlayerCamera;

            var counter = _worldObjects.Counter;
            var counterStool = _worldObjects.CounterUpdradeStool;
            var employee_Stool = _worldObjects.CounterEmployee_UpgradeStool;

            var displayShoe_1 = _worldObjects.DisplayShoe_1;
            var displayShoe_1_Stool = _worldObjects.DipslayShoe_1_UpgradeStool;
            var displayShoe_2 = _worldObjects.DisplayShoe_2;
            var displayShoe_2_Stool = _worldObjects.DipslayShoe2_UpgradeStool;
            var displayClothes = _worldObjects.DisplayClothes;
            var displayClothes_Stool = _worldObjects.DipslayClothes_UpgradeStool;

            var storageShoe = _worldObjects.StorageShoe;
            var storageShoe_Stool = _worldObjects.StorageShoe_UpgradeStool;
            var storageClothes = _worldObjects.StorageClothes;
            var storageClothes_Stool = _worldObjects.StorageClothes_UpgradeStool;

            var moneyStacker = _worldObjects.MoneyStacker;

            var guideArrow = _worldObjects.GuideArrow;
#endregion

#region LunaFields
            player.IsMoneyStackingMode = _isMoneyStackingMode;
            UpgradeStool.IsMoneyStackingMode = _isMoneyStackingMode; 
            MoneyManager.CurrentMoney += _startMoney;
            Item.Price = _itemPrice;
            Money.Price = _moneyPrice;

            counterStool.UpgradePrice = _counter_UpgradePrice;
            displayShoe_1_Stool.UpgradePrice = _dipslay_Shoe1_UpgradePrice;
            displayShoe_2_Stool.UpgradePrice = _dipslay_Shoe2_UpgradePrice;
            displayClothes_Stool.UpgradePrice = _dipslay_Clothes_UpgradPrice;
            employee_Stool.UpgradePrice = _counter_Employee_UpgradePrice;
            storageShoe_Stool.UpgradePrice = _storage_Shoe_UpgradePrice;
            storageClothes_Stool.UpgradePrice = _storage_Clothes_UpgradePrice;
#endregion

            player.PlayerCamAngles = playerCam.transform.eulerAngles;
            playerCam.PlayerPosistionReturner = player;

#region GuideArrow Events
            // 플레이어 첫 디스플레이 매대 업그레이드 이벤트 (손님 스폰)
            playerCam.OnFirstMoveEventDone += _customerManager.StartSapwnShelf_1_Customer;;

            // 플레이어 창고매대 아이템 수령 이벤트
            storageShoe.OnPlayerTakeItems += guideArrow.StartGuide;

            // 플레이어 신발 디스플레이 매대 1 아이템 전달 이벤트
            player.OnItemTakened += guideArrow.StopGuide;

            // 첫번째 손님 계산 완료 이벤트
            counter.OnFinishCheckOut += guideArrow.StopGuide;

            // 플레이어 돈 수령 이벤트
            moneyStacker.OnPlayerTakeMoney += guideArrow.StartGuide;

            // 첫번쨰 매대 손님 추가 이벤트
            displayShoe_1.OnFirstDisplaying += _customerManager.AddExtraCustomerToList;

            // 카운터 업그레이드 이벤트
            counterStool.OnGaugeFull += counter.Upgrade;
            counterStool.OnGaugeFull += displayShoe_1_Stool.ActivateStool;
            counterStool.OnGaugeFulGuideCall += guideArrow.StartGuide;
            counter.OnFinnishGetBox += moneyStacker.GenerateMoney;
  
            // 디스플레이 신발 매대 1 업그레이드 이벤트
            displayShoe_1_Stool.OnGaugeFull += displayShoe_1.Upgrade;
            displayShoe_1_Stool.OnGaugeFull += storageShoe_Stool.ActivateStool;
            displayShoe_1_Stool.OnGaugeFull += _worldObjects.Door.OpenStorage;
            displayShoe_1_Stool.OnGaugeFulGuideCall += guideArrow.StartGuide;
            displayShoe_1_Stool.OnGaugeFulCamCall += playerCam.StartEventCamYoyo;

            // 창고 신발 매대 업그레이드 이벤트
            storageShoe_Stool.OnGaugeFull += storageShoe.Upgrade;
            storageShoe_Stool.OnGaugeFull += displayShoe_2_Stool.ActivateStool;
            storageShoe_Stool.OnGaugeFulGuideCall += guideArrow.StartGuide;

            // 디스플레이 신발 매대 2 업그레이드 이벤트
            displayShoe_2_Stool.OnGaugeFull += displayShoe_2.Upgrade;
            displayShoe_2_Stool.OnGaugeFull += employee_Stool.ActivateStool;
            displayShoe_2_Stool.OnGaugeFull += guideArrow.StopGuide;
            displayShoe_2_Stool.OnGaugeFull += _customerManager.StartSapwnShelf_2_Customer;
            displayShoe_2_Stool.OnGaugeFulCamCall += playerCam.StartEventCamYoyo;

            // 카운터 직원 업그레이드 이벤트
            employee_Stool.OnGaugeFull += counter.ActiveEmployee;
            employee_Stool.OnGaugeFull += displayClothes_Stool.ActivateStool;
            employee_Stool.OnGaugeFulCamCall += playerCam.StartEventCamYoyo;

            // 디스플레이 옷 매대 업그레이드 이벤트
            displayClothes_Stool.OnGaugeFull += displayClothes.Upgrade;
            displayClothes_Stool.OnGaugeFull += storageClothes_Stool.ActivateStool;
            displayClothes_Stool.OnGaugeFull += _customerManager.StartSpawnClotehs_Customer;
            displayClothes_Stool.OnGaugeFulCamCall += playerCam.StartEventCamYoyo;

            // 창고 옷 매대 업그레이드 이벤트
            storageClothes_Stool.OnGaugeFull += storageClothes.Upgrade;
#endregion

#region Manager Initializing
            _UIManager.Init(player,displayClothes,guideArrow.StartGuide); // 게임 시작 가이드 애로우 이벤트
            _customerManager.Init<Action<EGuideArrowState>>(guideArrow.StartGuide); //첫번쨰 손님 계산대 도착 이벤트
            _audioManger.Init();
#endregion

        }

        private void OnDestroy() 
        {

#region Cashing for Disable
            var player = _worldObjects.Player;
            var playerCam = _worldObjects.PlayerCamera;

            var counter = _worldObjects.Counter;
            var counterStool = _worldObjects.CounterUpdradeStool;
            var employee_Stool = _worldObjects.CounterEmployee_UpgradeStool;

            var displayShoe_1 = _worldObjects.DisplayShoe_1;
            var displayShoe_1_Stool = _worldObjects.DipslayShoe_1_UpgradeStool;
            var displayShoe_2 = _worldObjects.DisplayShoe_2;
            var displayShoe_2_Stool = _worldObjects.DipslayShoe2_UpgradeStool;
            var displayClothes = _worldObjects.DisplayClothes;
            var displayClothes_Stool = _worldObjects.DipslayClothes_UpgradeStool;

            var storageShoe = _worldObjects.StorageShoe;
            var storageShoe_Stool = _worldObjects.StorageShoe_UpgradeStool;
            var storageClothes = _worldObjects.StorageClothes;
            var storageClothes_Stool = _worldObjects.StorageClothes_UpgradeStool;

            var moneyStacker = _worldObjects.MoneyStacker;

            var guideArrow = _worldObjects.GuideArrow;
#endregion

#region Events DisAble
            // 플레이어 창고매대 아이템 수령 이벤트
            storageShoe.OnPlayerTakeItems -= guideArrow.StartGuide;

            // 플레이어 신발 디스플레이 매대 1 아이템 전달 이벤트
            player.OnItemTakened -= guideArrow.StopGuide;

            // 첫번째 손님 계산 완료 이벤트
            counter.OnFinishCheckOut -= guideArrow.StopGuide;

            // 플레이어 돈 수령 이벤트
            moneyStacker.OnPlayerTakeMoney -= guideArrow.StartGuide;

            // 첫번쨰 매대 손님 추가 이벤트
            displayShoe_1.OnFirstDisplaying -= _customerManager.AddExtraCustomerToList;

            // 카운터 업그레이드 이벤트
            counterStool.OnGaugeFull -= counter.Upgrade;
            counterStool.OnGaugeFull -= displayShoe_1_Stool.ActivateStool;
            counterStool.OnGaugeFulGuideCall -= guideArrow.StartGuide;
            counter.OnFinnishGetBox -= moneyStacker.GenerateMoney;
  
            // 디스플레이 신발 매대 1 업그레이드 이벤트
            displayShoe_1_Stool.OnGaugeFull -= displayShoe_1.Upgrade;
            displayShoe_1_Stool.OnGaugeFull -= storageShoe_Stool.ActivateStool;
            displayShoe_1_Stool.OnGaugeFull -= _worldObjects.Door.OpenStorage;
            displayShoe_1_Stool.OnGaugeFulGuideCall -= guideArrow.StartGuide;
            displayShoe_1_Stool.OnGaugeFulCamCall -= playerCam.StartEventCamYoyo;

            // 창고 신발 매대 업그레이드 이벤트
            storageShoe_Stool.OnGaugeFull -= storageShoe.Upgrade;
            storageShoe_Stool.OnGaugeFull -= displayShoe_2_Stool.ActivateStool;
            storageShoe_Stool.OnGaugeFulGuideCall -= guideArrow.StartGuide;

            // 디스플레이 신발 매대 2 업그레이드 이벤트
            displayShoe_2_Stool.OnGaugeFull -= displayShoe_2.Upgrade;
            displayShoe_2_Stool.OnGaugeFull -= employee_Stool.ActivateStool;
            displayShoe_2_Stool.OnGaugeFull -= guideArrow.StopGuide;
            displayShoe_2_Stool.OnGaugeFull -= _customerManager.StartSapwnShelf_2_Customer;
            displayShoe_2_Stool.OnGaugeFulCamCall -= playerCam.StartEventCamYoyo;

            // 카운터 직원 업그레이드 이벤트
            employee_Stool.OnGaugeFull -= counter.ActiveEmployee;
            employee_Stool.OnGaugeFull -= displayClothes_Stool.ActivateStool;
            employee_Stool.OnGaugeFulCamCall -= playerCam.StartEventCamYoyo;

            // 디스플레이 옷 매대 업그레이드 이벤트
            displayClothes_Stool.OnGaugeFull -= displayClothes.Upgrade;
            displayClothes_Stool.OnGaugeFull -= storageClothes_Stool.ActivateStool;
            displayClothes_Stool.OnGaugeFull -= _customerManager.StartSpawnClotehs_Customer;
            displayClothes_Stool.OnGaugeFulCamCall -= playerCam.StartEventCamYoyo;

            // 창고 옷 매대 업그레이드 이벤트
            storageClothes_Stool.OnGaugeFull -= storageClothes.Upgrade;
#endregion

        }
    }
}