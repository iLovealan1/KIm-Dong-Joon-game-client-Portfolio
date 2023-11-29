using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISanctuaryDirector : MonoBehaviour
{
    [SerializeField]
    private UIShopDirector shopDirector;
    [SerializeField]
    private UINPCPopupDirector NPCPopupDirector;
    [SerializeField]
    private UIInventoryDirector InventoryDirector;
    [SerializeField]
    private UIPopupResult popupResult;
    [SerializeField]
    private UIStatsUpgrade statsUpgrade;
    [SerializeField]
    private UIPauseDirector pauseDirector;
    [SerializeField]
    private UICurrencyDirector currencyDirector;
    [SerializeField]
    private UIDialogDirector dialogDirector;
    [SerializeField]
    private UIAnnounceDirector announceDirector;
    [SerializeField]
    private UIJoysticDirector joystickDirector;
    [SerializeField]
    private UIFieldItemPopupDirector fieldItemPopupDirector;
    [SerializeField]
    private UIFieldGuideDirector fieldGuideDirector;

    public UITutorialDirector tutorialDirector;

    private GameObject targetUI;

    //기사단장과의 대화가 끝나면 넘어가도록 수정 필요
    //던전에서 플레이어가 죽었을 때 호출되야 함
    public System.Action intotheDungeon;

    private Stack<GameObject> uiStackList;

    public void Init()
    {
        this.uiStackList = new Stack<GameObject>(); 

        this.InventoryDirector.onInventoryClicked = () =>
        {
            this.pauseDirector.gameObject.SetActive(!this.pauseDirector.gameObject.activeSelf);
        };
        this.joystickDirector.Init();
        this.announceDirector.Init();
        this.shopDirector.SanctuaryShopInit();  
        this.NPCPopupDirector.Init();
        this.InventoryDirector.Init();
        this.pauseDirector.Init();
        this.popupResult.Init();
        this.currencyDirector.Init();
        this.dialogDirector.Init();
        this.fieldItemPopupDirector.Init();
        this.fieldGuideDirector.Init();
        this.tutorialDirector.Init();

        this.NPCPopupDirector.onShopPopup = () =>
        {
            this.targetUI = this.shopDirector.gameObject;
            if (!InfoManager.instance.gameInfo.isShopTuto)
            {
                this.dialogDirector.dialogPanel.OnDialogEnd += ActiveUI;
                EventDispatcher.Instance.Dispatch(EventDispatcher.EventName.UIDialogPanelStartDialog,
                    UIDialogPanel.eDialogType.TUTORIALSHOP);
                InfoManager.instance.TutorialDone(InfoManager.eTutorialType.SHOP);
            }
            else
            {
                this.ActiveUI();
            }
        };
        this.NPCPopupDirector.onStatPopup = () =>
        {
            this.statsUpgrade.Init();
            this.targetUI = this.statsUpgrade.gameObject;
            if (!InfoManager.instance.gameInfo.isStatTuto)
            {
                this.dialogDirector.dialogPanel.OnDialogEnd += ActiveUI;
                EventDispatcher.Instance.Dispatch(EventDispatcher.EventName.UIDialogPanelStartDialog,
                    UIDialogPanel.eDialogType.TUTORIALSTAT);
                InfoManager.instance.TutorialDone(InfoManager.eTutorialType.STAT);
            }
            else
            {
                this.ActiveUI();
            }          
        };

        this.NPCPopupDirector.onResultPopup = () =>
        {
            this.targetUI = this.popupResult.gameObject;
            if (!InfoManager.instance.gameInfo.isResultTuto)
            {
                this.dialogDirector.dialogPanel.OnDialogEnd += ActiveUI;
                EventDispatcher.Instance.Dispatch(EventDispatcher.EventName.UIDialogPanelStartDialog,
                    UIDialogPanel.eDialogType.TUTORIALRESULT);
                InfoManager.instance.TutorialDone(InfoManager.eTutorialType.RESULT);
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

        this.InventoryDirector.onPushInventory = (x) =>
        {
            this.PushAndPopUI(x);
        };

        this.StartCoroutine(this.CoStartAnnounce());
    }

    public void ActiveUI()
    {
        this.targetUI.SetActive(true);
        this.targetUI = null;
    }

    private IEnumerator CoStartAnnounce()
    {
        yield return new WaitForSeconds(0.5f);
        EventDispatcher.Instance.Dispatch<UIAnnounceDirector.eAnnounceType>(EventDispatcher.EventName.UIAnnounceDirectorStartAnnounce,
            UIAnnounceDirector.eAnnounceType.SANCTUARY);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(this.uiStackList.Count != 0)
            {
                this.PushAndPopUI();
            }
        }
    }

    private void PushAndPopUI(GameObject targetUI = null)
    {
        if(targetUI != null)
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
            else if (offTarget == this.InventoryDirector.statInventory.gameObject)
            {
                this.uiStackList.Pop();
                this.InventoryDirector.statInventory.UIStatInventoryClosing();
            }           
            else
                this.uiStackList.Pop().SetActive(false);
        }
    }
}
