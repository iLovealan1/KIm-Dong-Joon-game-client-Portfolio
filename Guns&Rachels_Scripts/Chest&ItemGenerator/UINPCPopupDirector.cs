using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using static EventDispatcher;

public class UINPCPopupDirector : MonoBehaviour
{
    private enum ePopupType
    {
        SANCTUARY,
        DUNGEON,
    }

    [SerializeField] ePopupType popupType;
    [SerializeField] private Text txtNPCName;
    [SerializeField] private Button btnPopup;
    [SerializeField] private Text txtSelect;
    [SerializeField] private Transform target;

    private float duration = 0.5f;
    private float moveDistance = 3.0f;

    public System.Action onShopPopup;
    public System.Action onResultPopup;
    public System.Action onStatPopup;

    public System.Action onDungeonShopPopup;
    public System.Action onSmugglerShopPopup;
    public System.Action onDipositPopup;
    public System.Action onRogueDepositPopup;

    private Transform NPCPos;

    private string popupName;

    private AudioSource audioSource;

    public void Init()
    {
        this.audioSource = this.GetComponent<AudioSource>();
        this.btnPopup.onClick.AddListener(() =>
        {
            AudioManager.instance.PlaySFXOneShot(AudioManager.eSFXMusicPlayList.UI_Click, this.audioSource);
            if (this.popupType == ePopupType.SANCTUARY)
            {
                switch (this.popupName)
                {
                    case "Merchant":
                        this.onShopPopup();
                        break;
                    case "Nun":
                        this.onStatPopup();
                        break;
                    case "DepoistResult":
                        this.onResultPopup();
                        break;
                }
            }
            else if (this.popupType == ePopupType.DUNGEON)
            {
                switch (this.popupName)
                {
                    case "Merchant":
                        this.onDungeonShopPopup();
                        break;
                    case "Smuggler(Clone)":
                        this.onSmugglerShopPopup();
                        break;
                    case "Knight":
                        this.onDipositPopup();
                        break;
                    case "Rogue":
                        this.onRogueDepositPopup();
                        break;
                    case "HiddenGoldChest":
                        this.TakeChestDamage(2, "HiddenGoldChest");
                        break;
                    case "HiddenIronChest":
                        this.TakeChestDamage(1, "HiddenIronChest");
                        break;
                    case "HiddenWoodChest":
                        this.DecreaseDungeonGoldAndOpenChest();
                        break;
                }
            }
        });

        EventDispatcher.Instance.AddListener<string>
            (EventName.UINPCPopupUpdate, UpdateNPCPopup);
        EventDispatcher.Instance.AddListener<Transform>
            (EventName.UINPCPopupActive, ActiveNPCPopup);
        this.gameObject.SetActive(false);
    }

    private void TakeChestDamage(int damage, string chestType)
    {
        var isOK = false;
        EventDispatcher.Instance.Dispatch<int, bool>
            (EventName.DungeonSceneMainTakeChestDamage, damage, out isOK);
        if (isOK)
        {
            EventDispatcher.Instance.Dispatch<Transform, string>
                (EventDispatcher.EventName.ChestItemGeneratorMakeItemForChest, this.NPCPos, chestType);
            this.NPCPos.gameObject.GetComponent<Animator>().SetInteger("state", 1);
            this.NPCPos.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            this.gameObject.SetActive(false);
        }
        else
        {
            this.txtSelect.text = "체력 부족";
        }
    }


    // 1. Calculate based on the current user's dungeon gold as a percentage (e.g., Wood = 20%)
    // 2. Calculate based on the maximum gold of the current stage as a percentage (e.g., 1st Stage MaxGold = 4950, Wood = 20%)
    private void DecreaseDungeonGoldAndOpenChest()
    {
        var isEnough = InfoManager.instance.DecreaseDungeonGold(1500);
        if (isEnough)
        {
            EventDispatcher.Instance.Dispatch<Transform, string>
                (EventDispatcher.EventName.ChestItemGeneratorMakeItemForChest, this.NPCPos, "HiddenWoodChest");
            this.NPCPos.gameObject.GetComponent<Animator>().SetInteger("state", 1);
            this.NPCPos.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            this.gameObject.SetActive(false);
        }
        else
        {
            this.txtSelect.text = "잔액 부족";
        }
    }


    // Update the NPC popup
    private void UpdateNPCPopup(string NPCName)
    {
        this.popupName = NPCName;
        switch (NPCName)
        {
            case "Nun":
                NPCName = "수녀";
                this.txtSelect.text = "대화하기";
                break;
            case "Merchant":
                NPCName = "상인";
                this.txtSelect.text = "대화하기";
                break;
            case "DepoistResult":
                NPCName = "이전 전투 결과";
                this.txtSelect.text = "확인하기";
                break;
            case "Smuggler(Clone)":
                NPCName = "밀수업자";
                this.txtSelect.text = "대화하기";
                break;
            case "Rogue":
                NPCName = "도적단원";
                this.txtSelect.text = "대화하기";
                break;
            case "Knight":
                NPCName = "기사단원";
                this.txtSelect.text = "대화하기";
                break;
            case "HiddenGoldChest":
                NPCName = "숨겨진 골드 상자\n체력 2개 소모";
                this.txtSelect.text = "열기";
                break;
            case "HiddenIronChest":
                NPCName = "숨겨진 아이언 상자\n체력 1개 소모";
                this.txtSelect.text = "열기";
                break;
            case "HiddenWoodChest":
                NPCName = "숨겨진 나무 상자\n필요 골드 1500골드";
                this.txtSelect.text = "열기";
                break;
        }
        this.txtNPCName.text = NPCName;
    }

    // Activate the NPC popup
    private void ActiveNPCPopup(Transform NPCPos)
    {
        this.gameObject.SetActive(!this.gameObject.activeSelf);
        if (this.gameObject.activeSelf == true)
            this.StartCoroutine(this.PopupSetOn(NPCPos));
    }


    // Animation part
    private IEnumerator PopupSetOn(Transform NPCPos)
    {
        // Save initial position and rotation
        this.NPCPos = NPCPos;
        Vector3 initialPosition = NPCPos.position;
        Quaternion initialRotation = this.target.rotation;

        this.target.localScale = new Vector3(0.0001f, 0.013f, 0.009f);
        this.target.position = NPCPos.position;
        float distanceToMove = Mathf.Abs(NPCPos.position.y - initialPosition.y) + this.moveDistance;
        if (this.popupType == ePopupType.DUNGEON && NPCPos.gameObject.name.Contains("Merchant"))
            distanceToMove = 0;
        else if (this.popupType == ePopupType.SANCTUARY && NPCPos.gameObject.name.Contains("DepoistResult"))
            distanceToMove -= 1f;
        else if (this.popupType == ePopupType.SANCTUARY && NPCPos.gameObject.name.Contains("Nun"))
            distanceToMove -= 1f;
        this.target.DOMoveY(NPCPos.position.y + distanceToMove / 2f, this.duration / 10)
             .SetEase(Ease.OutExpo)
             .OnComplete(() =>
             {
                 this.target.DOMoveY(NPCPos.position.y + distanceToMove, this.duration / 10)
                      .SetEase(Ease.InOutExpo)
                      .OnComplete(() =>
                      {
                          this.target.rotation = initialRotation;
                          this.target.DOScaleX(0.0001f, this.duration / 10)
                                .SetEase(Ease.OutExpo)
                                .OnComplete(() =>
                                {
                                    this.target.DOScaleX(0.013f, this.duration / 10)
                                          .SetEase(Ease.OutExpo);
                                });
                      });
             });
        yield return null;
    }

    private void OnDestroy()
    {
        this.StopAllCoroutines();
    }
}
