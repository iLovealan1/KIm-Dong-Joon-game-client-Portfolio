using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class UIFieldItemPopupDirector : MonoBehaviour
{
    [SerializeField] private Image imgItem;
    [SerializeField] private Text txtName;
    [SerializeField] private Text txtDetail;
    [SerializeField] private Transform popupTrans;

    [SerializeField] private GameObject imgPlus;
    [SerializeField] private GameObject imgminus;

    private SpriteAtlas atlas;
    private SpriteAtlas RelicAtlas;
    private SpriteAtlas WeaponAtlas;
    private Vector3 originalScale;
    public void Init()
    {
        this.atlas = AtlasManager.instance.GetAtlasByName("UIEquipmentIcon");
        this.RelicAtlas = AtlasManager.instance.GetAtlasByName("Relic");
        this.WeaponAtlas = AtlasManager.instance.GetAtlasByName("UIWeaponIcon");
        this.originalScale = this.popupTrans.localScale;
        this.popupTrans.localScale = Vector3.zero;
        EventDispatcher.Instance.AddListener<string, Vector3>(EventDispatcher.EventName.UIFieldItemPopupDirectorUpdatePopup,
            this.UpdatePopup);
        EventDispatcher.Instance.AddListener(EventDispatcher.EventName.UIFieldItemPopupDirectorClosePopup,
            this.ClosePopup);
        this.imgPlus.SetActive(false);
        this.imgminus.SetActive(false);
        this.popupTrans.gameObject.SetActive(false);
    }

    private void UpdatePopup(string name, Vector3 pos)
    {
        this.popupTrans.gameObject.SetActive(true);
        this.imgPlus.SetActive(false);
        this.imgminus.SetActive(false);
        var itemName = default(string);
        var detail = default(string);
        if (name.Contains("Sword") || name.Contains("Arrow") || name.Contains("Axe") || name.Contains("Wand"))
        {
            this.CheckGrade(name);
            this.imgItem.GetComponent<RectTransform>().sizeDelta = new Vector2(35.53f, 35.53f);
            DataManager.Instance.EquipmentDetailByID(name, out itemName, out detail);
            this.imgItem.sprite = this.atlas.GetSprite(name);
        }
        else if (name.Contains("Food") || name.Contains("Ether") || name.Contains("Bag"))
        {
            this.imgItem.GetComponent<RectTransform>().sizeDelta = new Vector2(35.53f, 35.53f);
            DataManager.Instance.GetEtcNameAndDescbyID(name, out itemName, out detail);
            this.imgItem.sprite = this.atlas.GetSprite(name);
        }
        else if (name.Contains("LaserLine")
            || name.Contains("DashAttack")
            || name.Contains("PoisonBullet")
            || name.Contains("GrenadeLauncher")
            || name.Contains("DefensiveBullet"))
        {
            this.imgItem.GetComponent<RectTransform>().sizeDelta = new Vector2(35.53f, 20f);
            this.imgItem.sprite = this.RelicAtlas.GetSprite(name);
            var data = DataManager.Instance.GetRelicDataFromPrefabName(name);
            itemName = data.name;
            detail = data.disc;
        }
        else if (name.Contains("AssultRifle")
            || name.Contains("ShotGun")
            || name.Contains("SniperRifle")
            || name.Contains("SubmachineGun"))
        {
            DataManager.Instance.GetWeaponNameAndDescbyID(name, out itemName, out detail);
            this.imgItem.GetComponent<RectTransform>().sizeDelta = new Vector2(35.53f, 20f);
            this.imgItem.sprite = this.WeaponAtlas.GetSprite(name);
        }
        this.popupTrans.position = pos;
        if (name.Contains("Ether"))
        {
            this.imgItem.transform.rotation = Quaternion.Euler(0, 0, -90);
        }
        else this.imgItem.transform.rotation = Quaternion.Euler(0, 0, 0);
        this.txtName.text = itemName;
        this.txtDetail.text = detail;
        this.popupTrans.localScale = Vector3.zero;
        this.popupTrans.DOScale(this.originalScale, 0.1f)
            .From(Vector3.zero)
            .SetEase(Ease.InOutQuad);
    }

    private void CheckGrade(string name)
    {
        var list = default(List<string>);
        EventDispatcher.Instance.Dispatch<List<string>>(EventDispatcher.EventName.UICurrentInventoryList, out list);

        // When the inventory is not full or empty
        if (list.Count == 0 || list.Count != InfoManager.instance.inventoryInfo.InventoryCount)
        {
            this.imgPlus.SetActive(true);
            this.imgminus.SetActive(false);
            return;
        }

        bool hasWood = false;
        bool hasIron = false;
        bool hasGold = false;
        bool hasDiamond = false;

        foreach (var item in list)
        {
            if (item.Contains("Wood"))
            {
                hasWood = true;
            }
            else if (item.Contains("Iron"))
            {
                hasIron = true;
            }
            else if (item.Contains("Gold"))
            {
                hasGold = true;
            }
            else if (item.Contains("Diamond"))
            {
                hasDiamond = true;
            }
        }

        // Enable or disable based on the item type
        if (name.Contains("Wood"))
        {
            if (!hasWood) // When there is no Wood
            {
                this.imgPlus.SetActive(false);
                this.imgminus.SetActive(true);
            }
            else
            {
                this.imgPlus.SetActive(false);
                this.imgminus.SetActive(false);
            }
        }
        else if (name.Contains("Iron"))
        {
            if (!hasWood && !hasIron) // When there is no Wood and Iron
            {
                this.imgPlus.SetActive(false);
                this.imgminus.SetActive(true);
            }
            else if (!hasGold && !hasDiamond && !hasIron) // When all are Wood
            {
                this.imgPlus.SetActive(true);
                this.imgminus.SetActive(false);
            }
            else if (hasWood) // When there is at least one Wood
            {
                this.imgPlus.SetActive(true);
                this.imgminus.SetActive(false);
            }
            else // Other cases
            {
                this.imgPlus.SetActive(false);
                this.imgminus.SetActive(false);
            }
        }
        else if (name.Contains("Gold"))
        {
            if (!hasWood && !hasIron && !hasGold) // When there is only Diamond
            {
                this.imgPlus.SetActive(false);
                this.imgminus.SetActive(true);
            }
            else if (!hasGold && !hasDiamond) // When there is no Gold and Diamond
            {
                this.imgPlus.SetActive(true);
                this.imgminus.SetActive(false);
            }
            else if (hasWood || hasIron)  // When there are only Wood and Iron
            {
                this.imgPlus.SetActive(true);
                this.imgminus.SetActive(false);
            }
            else
            {
                this.imgPlus.SetActive(false);
                this.imgminus.SetActive(false);
            }
        }
        else if (name.Contains("Diamond"))
        {
            if (!hasDiamond) // When there is no Diamond
            {
                this.imgPlus.SetActive(true);
                this.imgminus.SetActive(false);
            }
            else if (hasDiamond && (hasGold || hasWood || hasIron)) // When there is at least one Diamond
            {
                this.imgPlus.SetActive(true);
                this.imgminus.SetActive(false);
            }
            else if (!hasGold && !hasWood && !hasIron) // When there is only Diamond
            {
                this.imgPlus.SetActive(false);
                this.imgminus.SetActive(false);
            }
        }
    }

    private void ClosePopup()
    {
        this.popupTrans.DOKill();
        this.popupTrans.DOScale(Vector3.zero, 0.1f)
            .From(this.popupTrans.localScale)
            .SetEase(Ease.InOutQuad)
            .OnComplete(() =>
            {
                this.popupTrans.gameObject.SetActive(false);
            });
    }

    private void OnDisable()
    {
        this.popupTrans.DOKill();
    }
}