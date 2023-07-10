using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInventoryDirector : MonoBehaviour
{

    [SerializeField] private Button btnStatInventory;
    private Transform btnTrans;
    private Vector3 btnOrgScale;

    [SerializeField] public UIStatInventory statInventory;

    public System.Action onInventoryClicked;
    public System.Action<GameObject> onPushInventory;

    [SerializeField] private TMP_Text txtFullField;
    [SerializeField] private TMP_Text txtFullPopup;
    [SerializeField] private TMP_Text txthealthField;
    [SerializeField] private TMP_Text txtHeathPopup;
    [SerializeField] private Text txtInvenAmount;

    private bool isFullField;
    private bool isHealthField;

    private Color defaultFieldColor;
    private Color defaultFieldColor2;
    private Color defaultPopupColor;
    private Color defaultPopupColor2;

    private AudioSource audioSource;

    public void Init()
    {
        // Initializing
        this.InitFullUI();
        this.btnTrans = this.btnStatInventory.transform;
        this.btnOrgScale = this.btnTrans.localScale;
        this.audioSource = this.GetComponent<AudioSource>();

        this.btnStatInventory.onClick.AddListener(() =>
        {
            this.statInventory.gameObject.SetActive(true);
            AudioManager.instance.PlaySFXOneShot(AudioManager.eSFXMusicPlayList.UI_Open, this.audioSource);
            this.onPushInventory(this.statInventory.gameObject);
            this.onInventoryClicked();
        });

        this.statInventory.onInventoryclosed = () =>
        {
            AudioManager.instance.PlaySFXOneShot(AudioManager.eSFXMusicPlayList.UI_Close, this.audioSource);
            this.onInventoryClicked();
        };

        //StatInventory Initializing
        this.statInventory.Init();

        //Add Listnener To EventDispatcher
        EventDispatcher.Instance.AddListener<Transform>
            (EventDispatcher.EventName.UIInventoryDirectorMakeFieldFullText, this.MakeFieldFullText);
        EventDispatcher.Instance.AddListener
            (EventDispatcher.EventName.UIInventoryDirectorMakeFullPopupText, this.MakeFullPopupText);
        EventDispatcher.Instance.AddListener<Transform>
            (EventDispatcher.EventName.UIInventoryDirectorMakeFieldFullHealthText, this.MakeFieldHealthText);
        EventDispatcher.Instance.AddListener
            (EventDispatcher.EventName.UIInventoryDirectorMakeHealthPopupText, this.MakeHealthPopupText);
        EventDispatcher.Instance.AddListener
            (EventDispatcher.EventName.UIInventoryDirectorButtonScaleAnim, this.ButtonScaleAnim);
    }

    //Initalizing FullPopups
    private void InitFullUI()
    {
        this.defaultFieldColor = this.txtFullField.color;
        this.txtFullField.gameObject.SetActive(false);
        this.defaultPopupColor = this.txtFullPopup.color;
        this.txtFullPopup.gameObject.SetActive(false);

        this.defaultFieldColor2 = this.txthealthField.color;
        this.txthealthField.gameObject.SetActive(false);
        this.defaultPopupColor2 = this.txtHeathPopup.color;
        this.txtHeathPopup.gameObject.SetActive(false);
    }

    private void MakeFieldFullText(Transform playerTrans)
    {
        float yOffset = 50f;
        if (!this.isFullField)
        {
            this.txtFullField.gameObject.SetActive(true);
            this.isFullField = true;
            var playerPos = playerTrans.position;
            var fieldPos = Camera.main.WorldToScreenPoint(playerPos);
            var finalPos = new Vector3(fieldPos.x, fieldPos.y + yOffset, fieldPos.z);

            this.txtFullField.rectTransform.position = finalPos;

            this.txtFullField.DOFade(1, 0.1f).SetEase(Ease.OutCubic);
            this.txtFullField.rectTransform.DOMoveY(this.txtFullField.rectTransform.position.y + 50f, 0.2f).SetEase(Ease.OutCubic).OnComplete(() =>
                {
                    this.txtFullField.rectTransform.DOShakePosition(0.2f, new Vector3(30f, -30f, 0f), 20, 90f, false, true).SetEase(Ease.OutCubic).OnComplete(() =>
                    {
                        DOTween.Sequence()
                            .AppendInterval(0.5f)
                            .Append(this.txtFullField.DOFade(0, 0.2f).SetEase(Ease.InCubic))
                            .Join(this.txtFullField.rectTransform.DOMoveY(this.txtFullField.rectTransform.position.y + 50f, 0.3f).SetEase(Ease.InCubic))
                            .AppendInterval(0.5f)
                            .OnComplete(() =>
                            {
                                this.isFullField = false;
                                this.txtFullField.color = this.defaultFieldColor;
                                this.txtFullField.gameObject.SetActive(false);
                            });
                    });
                });
        }
    }

    private void MakeFullPopupText()
    {
        this.txtFullPopup.gameObject.SetActive(true);
        DOTween.Kill(this.txtFullPopup);
        this.txtFullPopup.color = this.defaultPopupColor;

        this.txtFullPopup.transform.DOShakePosition(0.3f, 10).OnComplete(() =>
        {
            DOTween.Sequence()
                .AppendInterval(0.5f).OnComplete(() =>
                {
                    this.txtFullPopup.DOFade(0, 0.5f).SetEase(Ease.InElastic).OnComplete(() =>
                    {
                        this.txtFullPopup.gameObject.SetActive(false);
                    });
                });
        });
    }


    private void MakeFieldHealthText(Transform playerTrans)
    {
        float yOffset = 80f;
        if (!this.isHealthField)
        {
            this.txthealthField.gameObject.SetActive(true);
            this.isHealthField = true;
            var playerPos = playerTrans.position;
            var fieldPos = Camera.main.WorldToScreenPoint(playerPos);
            var finalPos = new Vector3(fieldPos.x, fieldPos.y + yOffset, fieldPos.z);

            this.txthealthField.rectTransform.position = finalPos;
            this.txthealthField.DOFade(1, 0.1f).SetEase(Ease.OutCubic);
            this.txthealthField.rectTransform.DOMoveY(this.txthealthField.rectTransform.position.y + 50f, 0.2f).SetEase(Ease.OutCubic).OnComplete(() =>
            {
                this.txthealthField.rectTransform.DOShakePosition(0.2f, new Vector3(30f, -30f, 0f), 20, 90f, false, true).SetEase(Ease.OutCubic).OnComplete(() =>
                {
                    DOTween.Sequence()
                        .AppendInterval(0.5f)
                        .Append(this.txthealthField.DOFade(0, 0.2f).SetEase(Ease.InCubic))
                        .Join(this.txthealthField.rectTransform.DOMoveY(this.txthealthField.rectTransform.position.y + 50f, 0.3f).SetEase(Ease.InCubic))
                        .AppendInterval(0.5f)
                        .OnComplete(() =>
                        {
                            this.isHealthField = false;
                            this.txthealthField.color = this.defaultFieldColor2;
                            this.txthealthField.gameObject.SetActive(false);
                        });
                });
            });
        }
    }

    private void MakeHealthPopupText()
    {
        this.txtHeathPopup.gameObject.SetActive(true);
        DOTween.Kill(this.txtHeathPopup);
        this.txtHeathPopup.color = this.defaultPopupColor2;

        this.txtHeathPopup.transform.DOShakePosition(0.3f, 10).OnComplete(() =>
        {
            DOTween.Sequence()
                .AppendInterval(0.5f).OnComplete(() =>
                {
                    this.txtHeathPopup.DOFade(0, 0.5f).SetEase(Ease.InElastic).OnComplete(() =>
                    {
                        this.txtHeathPopup.gameObject.SetActive(false);
                    });
                });
        });
    }

    private float scaleDuration = 0.15f;
    private float defaultScale = 1.2f;
    private float maxScale = 1.2f;
    private float midleScale = 0.7f;
    private float finalScale = 1f;
    
    private void ButtonScaleAnim()
    {
        this.btnTrans.DOKill();
        this.maxScale += 0.1f;
        this.btnTrans.DOScale(this.maxScale, this.scaleDuration)
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                this.btnTrans.DOScale(this.midleScale, this.scaleDuration)
                .SetEase(Ease.InQuad)
                .OnComplete(() =>
                {
                    this.btnTrans.DOScale(this.finalScale, this.scaleDuration)
                    .SetEase(Ease.OutQuad)
                    .OnComplete(() =>
                    {
                        this.maxScale = this.defaultScale;
                    });
                });
            });
    }

    private void OnDisable()
    {
        this.txtFullField.DOKill();
        this.txtFullPopup.DOKill();
        this.txthealthField.DOKill();
        this.txtHeathPopup.DOKill();
        this.btnTrans.DOKill();
    }

    //RefreshTextAMount for InventoryUIIcon 
    public void RefreshTextInvenAmount()
    {
        var content = this.statInventory.inventory.content;
        var currentAmount = default(int);
        var maxAmount = InfoManager.instance.inventoryInfo.InventoryCount;
        var txtAmount = default(string);
        var childCount = 2;
        for (int i = 0; i < content.childCount; i++)
        {
            if (content.GetChild(i).transform.childCount == childCount)
            {
                currentAmount++;
            }
        }

        if (currentAmount == maxAmount)
        {
            txtAmount = string.Format("<color=red>{0} / {1}</color>", currentAmount, maxAmount);
        }
        else if (currentAmount == 0)
        {
            txtAmount = string.Format("{0} / {1}", currentAmount, maxAmount);
        }
        else
        {
            txtAmount = string.Format("<color=green>{0}</color> / {1}", currentAmount, maxAmount);
        }

        this.txtInvenAmount.text = txtAmount;
    }

    //Saving InventoryInfo
    public void startCoCoInvenInfoSave()
    {
        this.StartCoroutine(this.CoInvenInfoSave());
    }

    private IEnumerator CoInvenInfoSave()
    {
        yield return null;
        var list = default(List<string>);
        EventDispatcher.Instance.Dispatch<List<string>>
            (EventDispatcher.EventName.UICurrentInventoryList, out list);
        InfoManager.instance.UpdateEquipmentInfo(list);
    }

   
}