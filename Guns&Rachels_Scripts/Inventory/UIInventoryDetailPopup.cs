using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class UIInventoryDetailPopup : MonoBehaviour
{
    [SerializeField] private UIInventory inventory;
    [SerializeField] private Button btnInventoryDetailClose;
    [SerializeField] private Button btnDiscardEquipment;
    [SerializeField] private Image imgEquipment;
    [SerializeField] private Text txtEquipmentName;
    [SerializeField] private Text txtEquipmentStat1;
    [SerializeField] private Text txtEquipmentStat2;

    private int currentEquipmentCellHesh;
    private SpriteAtlas atlas;

    public void Init()
    {
        this.atlas = AtlasManager.instance.GetAtlasByName("UIEquipmentIcon");

        this.btnInventoryDetailClose.onClick.AddListener(() =>
        {
            var source = this.transform.GetComponentInParent<AudioSource>();
            AudioManager.instance.PlaySFXOneShot(AudioManager.eSFXMusicPlayList.UI_Close, source);
            this.gameObject.SetActive(false);
            this.inventory.onSetOffSelect(0);
        });

        this.btnDiscardEquipment.onClick.AddListener(() =>
        {
            var source = this.transform.GetComponentInParent<AudioSource>();
            AudioManager.instance.PlaySFXOneShot(AudioManager.eSFXMusicPlayList.UI_Close, source);
            this.inventory.onDiscardEquipment(this.currentEquipmentCellHesh);
            this.gameObject.SetActive(false);
        });

        this.gameObject.SetActive(false);
    }

    public void RefreshPopup(string equipment, int hesh)
    {
        var equipmentName = default(string);
        var statDisc = default(string);

        DataManager.Instance.EquipmentDetailByID(equipment, out equipmentName, out statDisc);
        this.currentEquipmentCellHesh = hesh;
        this.imgEquipment.sprite = this.atlas.GetSprite(equipment);
        this.txtEquipmentName.text = equipmentName;
        this.txtEquipmentStat1.text = statDisc;
    }
}
