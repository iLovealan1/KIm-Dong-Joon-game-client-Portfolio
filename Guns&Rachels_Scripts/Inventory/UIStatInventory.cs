using UnityEngine;
using UnityEngine.UI;

public class UIStatInventory : MonoBehaviour
{
    private UIInventoryDirector UIInventoryDirector;

    [SerializeField] private UIStat stat;
    [SerializeField] public UIInventory inventory;

    [SerializeField] private Button btnInventoryDim;
    [SerializeField] private Button btnInventoryClose;

    public System.Action onInventoryclosed;

    public void Init()
    {
        // Initializing
        this.UIInventoryDirector = this.GetComponentInParent<UIInventoryDirector>();

        this.btnInventoryDim.onClick.AddListener(() =>
        {
            this.UIInventoryDirector.onPushInventory(null);
        });
        this.btnInventoryClose.onClick.AddListener(() =>
        {
            this.UIInventoryDirector.onPushInventory(null);
        });

        //Inventory & Stat UI Initializing
        this.stat.Init();
        this.inventory.Init();

        this.gameObject.SetActive(false);
    }

    public void UIStatInventoryClosing()
    {
        this.gameObject.SetActive(false);
        this.onInventoryclosed();
    }
}
