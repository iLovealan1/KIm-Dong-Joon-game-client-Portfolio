using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using static EventDispatcher;
using System.Linq;

public class UIInventory : MonoBehaviour
{
    [SerializeField] private ScrollRect contentGridRect;
    [SerializeField] public Transform content;
    [SerializeField] private GameObject equipmentCell;

    public List<GameObject> inventoryList;
       
    private EquipmentFactory equipmentFactory;

    [SerializeField] private UIPlayerStats uiPlayerStats;
    [SerializeField] private UIInventoryDetailPopup uIInventoryDetailPopup;

    private GameObject dungeonMain;
    private UIInventoryDirector director;

    public System.Action<string,int> onSelected;
    public System.Action<int> onDiscardEquipment;
    public System.Action<int> onSetOffSelect;
    public System.Action onSetOffpopup;    

    public void Init()
    {
        this.dungeonMain = GameObject.Find("DungeonSceneMain");
        this.director = this.transform.GetComponentInParent<UIInventoryDirector>();

        this.onSelected = (EuipmentName,hesh) =>
        {
            this.onSetOffSelect(hesh);
            this.uIInventoryDetailPopup.gameObject.SetActive(true);
            this.uIInventoryDetailPopup.RefreshPopup(EuipmentName, hesh);          
        };

        this.onDiscardEquipment = (hesh) =>
        {
            this.DiscardEquipment(hesh);
        };

        this.onSetOffSelect = (hesh) =>
        {
            for (int i = 0; i < this.content.childCount; i++)
            {
                if (this.content.GetChild(i).GetHashCode() != hesh && this.content.GetChild(i).transform.childCount == 2)
                {
                    this.content.GetChild(i).transform.GetChild(1).GetChild(0).gameObject.SetActive(false);
                }
            }
        };

        this.onSetOffpopup = () =>
        {
            this.uIInventoryDetailPopup.gameObject.SetActive(false);
        };

        this.inventoryList = new List<GameObject>();
        this.equipmentFactory = this.GetComponent<EquipmentFactory>();
        var count = InfoManager.instance.inventoryInfo.InventoryCount;
        this.InitInventoryContents(count);
        this.uIInventoryDetailPopup.Init();

        EventDispatcher.Instance.AddListener(EventDispatcher.EventName.UIInventoryAddCell,this.AddInventoryCells);
        EventDispatcher.Instance.AddListener<string,bool>(EventDispatcher.EventName.UIInventoryAddEquipment, this.AddEquipment) ;
        EventDispatcher.Instance.AddListener<List<string>>(EventDispatcher.EventName.UICurrentInventoryList, this.MakeCurrentInventoryList) ;
    }

    public void InitInventoryContents(int count)
    {
        for (int i = 0; i < count; i++)
        {
            var equipmentCell = GameObject.Instantiate(this.equipmentCell, this.content);
            this.inventoryList.Add(equipmentCell);
        }
        if(InfoManager.instance.inventoryInfo.isEquipment == true)
        {
            for (int i =0; i < InfoManager.instance.inventoryInfo.currentEquipmentsArr.Length; i++)
            {
                Debug.Log(InfoManager.instance.inventoryInfo.currentEquipmentsArr[i]);
                this.AddEquipment(InfoManager.instance.inventoryInfo.currentEquipmentsArr[i]); 
            }
            if(this.dungeonMain != null)
            InfoManager.instance.InitInventoryInfo();
        }
        this.CheckContentGrid(count);
        this.director.RefreshTextInvenAmount();
    }

    private void CheckContentGrid(int count)
    {
        if (count < 16)
        {
            this.contentGridRect.enabled = false;
        }
        else
        {
            this.contentGridRect.enabled = true;
        }
    }

    public void AddInventoryCells() 
    { 
        var count = InfoManager.instance.inventoryInfo.InventoryCount;
        var equipmentCell = GameObject.Instantiate(this.equipmentCell, this.content);
        this.inventoryList.Add(equipmentCell);
        this.CheckContentGrid(count);
        this.director.RefreshTextInvenAmount();
    }


    /// <summary>
    /// Discard Current Equipment
    /// </summary>
    /// <param name="cellHash">cell heshcode</param>
    /// <returns>Discarded Equipments name.</returns>
    public string DiscardEquipment(int cellHash)
    {
        var count = InfoManager.instance.inventoryInfo.InventoryCount;
        string discardEuipmentName = null;
        for (int i = 0; i < count; i++)
        {
            if (this.content.GetChild(i).GetHashCode() == cellHash)
            {
                var equipment = this.content.GetChild(i).transform.GetChild(1).GetChild(1);
                var euipmentComp = equipment.GetComponent<Equipment>();
                euipmentComp.UnSetEquipmentStat();
                this.uiPlayerStats.UpdatePlayerStatUI();
                discardEuipmentName = equipment.gameObject.name;
                Destroy(this.content.GetChild(i).GetChild(1).gameObject);
                break;
            }
        }
        /*EventDispatcher.Instance.Dispatch<string>(EventDispatcher.EventName.ChestItemGeneratorMakeItemForInventory,
          discardEuipmentName);*/
        // when Scene is Sacturary Scene
        if(this.dungeonMain == null)
        {
            this.director.startCoCoInvenInfoSave();
        }
        this.StartCoroutine(this.SorthInventoryCells(count)) ;
        return discardEuipmentName;
    }

    private IEnumerator SorthInventoryCells(int count)
    {
        yield return null;
        for (int i = 0; i < count; i++)
        {
            if (this.content.GetChild(i).childCount == 1)
            {
                this.content.GetChild(i).SetAsLastSibling();
            }
        }
        if (this.dungeonMain == null)
        {
            InfoManager.instance.UpdateEquipmentInfo(this.MakeCurrentInventoryList());
        }
        this.director.RefreshTextInvenAmount();
    }

    /// <summary>
    /// Add Item to Inventory
    /// </summary>
    /// <param name="EquipmentName"> Item name (example : Wood_Sword)</param>
    public bool AddEquipment(string EquipmentName)
    {
        //Debug.Log("AddEquipment");
        var count = InfoManager.instance.inventoryInfo.InventoryCount;
        GameObject cell = null;
        for (int i = 0; i < count; i++)
        {
            if (this.content.GetChild(i).childCount == 1)
            {
                cell = this.content.GetChild(i).gameObject;
                break;
            }
        }
        if (cell != null)
        {
            this.equipmentFactory.MakeEquipment(EquipmentName, cell);
            this.uiPlayerStats.UpdatePlayerStatUI();
            if (this.dungeonMain == null)
            {
                var director = this.transform.GetComponentInParent<UIInventoryDirector>();
                director.startCoCoInvenInfoSave();
            }
            this.director.RefreshTextInvenAmount();
            return true;           
        }
        else
        {
            //Debug.Log("No item cells are Available.");
            return false;
        }
    }

    /// <summary>
    /// Make current inventory list
    /// </summary>
    /// <returns>returns current user item list in Inventory</returns>
    public List<string> MakeCurrentInventoryList() 
    { 
        List<string> equipmentList = new List<string>();    

        for (int i = 0;i < this.content.transform.childCount; i++) 
        {
            if (this.content.GetChild(i).transform.childCount == 2)
            {
                var EuquipmentName = this.content.GetChild(i).GetChild(1).GetChild(1).gameObject.name;
                equipmentList.Add(EuquipmentName);
            }           
        }
        return equipmentList;   
    }

    private void OnDisable()
    {
        this.uIInventoryDetailPopup.gameObject.SetActive(false);
        for (int i = 0; i < this.content.childCount; i++)
        {
            if (this.content.GetChild(i).transform.childCount == 2)
            {
                this.content.GetChild(i).transform.GetChild(1).GetChild(0).gameObject.SetActive(false);
            }
        }
    }
}
