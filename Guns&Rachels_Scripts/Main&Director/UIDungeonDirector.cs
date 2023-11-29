using System.Collections.Generic;
using UnityEngine;

public class UIDungeonDirector : MonoBehaviour
{
    [SerializeField] private UIMiniMap miniMapDirector;
    [SerializeField] private UINPCPopupDirector NPCPopupDirector;
    [SerializeField] private UIShopDirector shopDirector;
    [SerializeField] private UIDiceDirector diceDirector;
    [SerializeField] private UIDipositDirector dipositDirector;
    [SerializeField] private UIInventoryDirector inventoryDirector;
    [SerializeField] private UIPauseDirector pauseDirector;
    [SerializeField] public UIGameOverDirector gameOverDirector;
    [SerializeField] public UIPortalArrowController portalArrowController;
    [SerializeField] private UIRelicDirector relicDirector;
    [SerializeField] private UICurrencyDirector currencyDirector;
    [SerializeField] private UIDialogDirector dialogDirector;
    [SerializeField] private UIAnnounceDirector announceDirector;
    [SerializeField] private UIDungeonLoadingDirector dungeonLoadingDirector;
    [SerializeField] private UIFieldItemPopupDirector fieldItemPopupDirector;

    public UIHealthDirector healthDirector;
    public UIJoysticDirector joystickDirector;
    public UIGunCharacteristicChoice gunCharacteristicChoice;
    public UIExpSlideDirector expSliderDirector;
    public UITutorialDirector tutorialDirector;
    private GameObject targetUI;

    public System.Action<Vector3> onMiniMapUpdatePopup;

    private Stack<GameObject> uiStackList = new Stack<GameObject>();

    public void Init()
    {
        this.InitAllUI();
        this.inventoryDirector.onInventoryClicked = () =>
        {
            this.pauseDirector.gameObject.SetActive(!this.pauseDirector.gameObject.activeSelf);
        };

        this.onMiniMapUpdatePopup = (pos) =>
        {
            this.miniMapDirector.MiniMapUpdate(pos);
        };

        this.NPCPopupDirector.onDipositPopup = () =>
        {
            this.targetUI = this.dipositDirector.gameObject;
            if (!InfoManager.instance.gameInfo.isKnightDipositTuto)
            {
                this.dipositDirector.uiDiposit.uIDipositPopup.NPCtype =
                InfoManager.eTutorialType.KNIGHTDIPOSIT;
                this.dipositDirector.uiDiposit.RefreshPopup();
                this.dialogDirector.dialogPanel.OnDialogEnd += ActiveUI;
                EventDispatcher.Instance.Dispatch
                    (EventDispatcher.EventName.UIDialogPanelStartDialog,UIDialogPanel.eDialogType.TUTORIALKNIGHTDIPOSIT);
                InfoManager.instance.TutorialDone(InfoManager.eTutorialType.KNIGHTDIPOSIT);
            }
            else
            {
                this.ActiveUI();
                this.dipositDirector.uiDiposit.uIDipositPopup.NPCtype =
                InfoManager.eTutorialType.KNIGHTDIPOSIT;
                this.dipositDirector.uiDiposit.RefreshPopup();
            }
        };

        this.NPCPopupDirector.onRogueDepositPopup = () =>
        {
            this.targetUI = this.dipositDirector.gameObject;
            if (!InfoManager.instance.gameInfo.isRogueDipositTuto)
            {
                this.dipositDirector.uiDiposit.uIDipositPopup.NPCtype =
                InfoManager.eTutorialType.ROGUEDIPOSIT;
                this.dipositDirector.uiDiposit.RefreshPopup();
                this.dialogDirector.dialogPanel.OnDialogEnd += ActiveUI;
                EventDispatcher.Instance.Dispatch
                    (EventDispatcher.EventName.UIDialogPanelStartDialog,UIDialogPanel.eDialogType.TUTORIALROGUEDIPOSIT);
                InfoManager.instance.TutorialDone(InfoManager.eTutorialType.ROGUEDIPOSIT);
            }
            else
            {
                this.ActiveUI();
                this.dipositDirector.uiDiposit.uIDipositPopup.NPCtype =
                InfoManager.eTutorialType.ROGUEDIPOSIT;
                this.dipositDirector.uiDiposit.RefreshPopup();
            }
        };

        this.NPCPopupDirector.onSmugglerShopPopup = () =>
        {
            this.targetUI = this.diceDirector.gameObject;
            if (!InfoManager.instance.gameInfo.isDiceTuto)
            {
                this.dialogDirector.dialogPanel.OnDialogEnd += ActiveUI;
                EventDispatcher.Instance.Dispatch
                    (EventDispatcher.EventName.UIDialogPanelStartDialog,UIDialogPanel.eDialogType.TUTORIALDICE);
                InfoManager.instance.TutorialDone(InfoManager.eTutorialType.DICE);
            }
            else
            {
                this.ActiveUI();
            }
        };

        this.NPCPopupDirector.onDungeonShopPopup = () =>
        {
            this.targetUI = this.shopDirector.gameObject;
            if (!InfoManager.instance.gameInfo.isShopTuto)
            {
                this.dialogDirector.dialogPanel.OnDialogEnd += ActiveUI;
                EventDispatcher.Instance.Dispatch
                    (EventDispatcher.EventName.UIDialogPanelStartDialog,UIDialogPanel.eDialogType.TUTORIALSHOP);
                InfoManager.instance.TutorialDone(InfoManager.eTutorialType.SHOP);
            }
            else
            {
                this.ActiveUI();
            }
        };


        this.pauseDirector.onPushPause = (x) =>
        {
            this.PushAndPopUI(x);
        };

        this.inventoryDirector.onPushInventory = (x) =>
        {
            this.PushAndPopUI(x);
        };

        EventDispatcher.Instance.AddListener
            (EventDispatcher.EventName.UIDungeonDirectorUISetOff, this.UISetOff);
    }

    private void InitAllUI()
    {
        this.announceDirector.Init();
        this.miniMapDirector.Init();
        this.NPCPopupDirector.Init();
        this.shopDirector.DungeonShopInit();
        this.diceDirector.Init();
        this.dipositDirector.Init();
        this.pauseDirector.Init();
        this.inventoryDirector.Init();
        this.gameOverDirector.Init();
        this.portalArrowController.Init();
        this.relicDirector.Init();
        this.currencyDirector.Init();
        this.dialogDirector.Init();
        this.dungeonLoadingDirector.Init();
        this.joystickDirector.Init();
        this.expSliderDirector.Init();
        this.healthDirector.Init();
        this.fieldItemPopupDirector.Init();
        this.tutorialDirector.Init();
    }

    private void UISetOff()
    {
        this.miniMapDirector.gameObject.SetActive(false);
        this.inventoryDirector.gameObject.SetActive(false);
        this.pauseDirector.gameObject.SetActive(false);
        this.healthDirector.gameObject.SetActive(false);
        this.joystickDirector.gameObject.SetActive(false);
        this.expSliderDirector.gameObject.SetActive(false);
        this.relicDirector.gameObject.SetActive(false);
        this.currencyDirector.gameObject.SetActive(false);
    }

    public void ActiveUI()
    {
        this.targetUI.SetActive(true);
        this.targetUI = null;
    }

    //When push back button of SmartPhone
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (this.uiStackList.Count != 0)
            {
                this.PushAndPopUI();
            }
        }
    }

    private void PushAndPopUI(GameObject targetUI = null)
    {
        if (targetUI != null)
        {
            this.uiStackList.Push(targetUI);
        }
        else
        {
            var offTarget = this.uiStackList.Peek();
            if (offTarget == this.pauseDirector.uiPauseMenu.gameObject)
            {
                this.uiStackList.Pop();
                this.pauseDirector.ActivePauseUI();
            }
            else if (offTarget == this.inventoryDirector.statInventory.gameObject)
            {
                this.uiStackList.Pop();
                this.inventoryDirector.statInventory.UIStatInventoryClosing();
            }
            else
                this.uiStackList.Pop().SetActive(false);
        }
    }
}
