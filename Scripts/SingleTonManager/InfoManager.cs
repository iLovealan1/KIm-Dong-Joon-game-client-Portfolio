using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
[Serializable]
public partial class InfoManager
{
    public enum ePermanent
    {
        NO, YES
    }

    public enum eNextStageType
    {
        NONE = 1,
        STAGE2,
        STAGE3,
        STAGE4,
    }

    public enum eTutorialType
    {
        NONE = -1,
        SHOP,
        STAT,
        RESULT,
        KNIGHTDIPOSIT,
        ROGUEDIPOSIT,
        DICE,
    }

    public static readonly InfoManager instance = new InfoManager();
    private InfoManager() { }

    public DungeonInfo dungeonInfo = new DungeonInfo();
    public InventoryInfo inventoryInfo = new InventoryInfo();
    public GameInfo gameInfo = new GameInfo();

    /// <summary>
    /// �κ��丮 ���� �ʱ�ȭ + �ڵ� ����( ������ ����� �ݵ�� ȣ�� )
    /// </summary>
    public void InitInventoryInfo()
    {
        Debug.Log("<color=red>�κ��丮 ���� �̴ϼȶ���¡</color>");
        this.inventoryInfo.isEquipment = false;
        this.inventoryInfo.currentEquipmentsArr = null;
        this.SaveInventoryInfo();
    }

    /// <summary>
    /// ���� ���� ���̵� �� �������� ���� �ʱ�ȭ( ������ ����� �ݵ�� ȣ�� )
    /// </summary>
    public void InitDungeonInfo()
    {
        this.dungeonInfo.currentStepInfo = 1;
    }

    /// <summary>
    /// �κ��丮 ���� ��������(1��) +  �ڵ� ����
    /// </summary>
    public int IncreaseInventoryCount()
    {
        this.inventoryInfo.InventoryCount += 1;
        this.SaveInventoryInfo();
        return this.inventoryInfo.InventoryCount;
    }

    /// <summary>
    /// ������ ���Ž� Inventory Info �� ���ž������� ������ ����Ʈ ���� + �ڵ� ����
    /// </summary>
    public void UpdateEquipmentInfo(List<string> EquipmentList)
    {
        Debug.Log("<color=red>�κ��丮 ���� ������Ʈ</color>");
        if(EquipmentList.Count != 0)
            this.inventoryInfo.isEquipment = true;
        else 
            this.inventoryInfo.isEquipment = false;
        this.inventoryInfo.currentEquipmentsArr = EquipmentList.ToArray();
        this.SaveInventoryInfo();
    }

    /// <summary>
    /// �÷��̾� InventoryInfo �ҷ�����
    /// </summary>
    public void LoadInventoryInfo()
    {
        //string path = string.Format("{0}/Inventory_Info.json",
        //    Application.persistentDataPath);
        //string json = File.ReadAllText(path);
        //this.inventoryInfo = JsonConvert.DeserializeObject<InventoryInfo>(json);
        //Debug.Log("�κ��丮 ���� �ε� �Ϸ�");

        //string path = string.Format("{0}/Inventory_Info.json", Application.persistentDataPath);
        //string encryptedJson = File.ReadAllText(path);
        //string decryptedJson = encryption.GetGeneric<string>(path, encryptedJson);
        //this.inventoryInfo = JsonConvert.DeserializeObject<InventoryInfo>(decryptedJson);
        //Debug.Log("<color=red>inventoryInfo loaded successfully.</color>");

        try
        {
            string path = string.Format("{0}/Inventory_Info.json", Application.persistentDataPath);
            string encryptedJson = File.ReadAllText(path);
            string decryptedJson = encryption.GetGeneric<string>(path, encryptedJson);
            //string json = File.ReadAllText(decryptedJson);
            this.inventoryInfo = JsonConvert.DeserializeObject<InventoryInfo>(decryptedJson);
            Debug.Log("<color=red>inventoryInfo loaded successfully.</color>");
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to load inventoryInfo: " + e.Message);
        }
    }

    /// <summary>
    /// �÷��̾� Inventory ����
    /// </summary>
    public void SaveInventoryInfo()
    {
        //string path = string.Format("{0}/Inventory_Info.json",
        //    Application.persistentDataPath);
        //string json = JsonConvert.SerializeObject(this.inventoryInfo);
        //File.WriteAllText(path, json);
        //Debug.Log("�κ��丮 ���� ���� �Ϸ�");

        //string path = string.Format("{0}/Inventory_Info.json", Application.persistentDataPath);
        //string json = JsonConvert.SerializeObject(this.inventoryInfo);
        //encryption.SetGeneric(path, json);
        //Debug.Log("<color=red>inventoryInfo saved successfully.</color>");

        try
        {
            string path = string.Format("{0}/Inventory_Info.json", Application.persistentDataPath);
            string json = JsonConvert.SerializeObject(this.inventoryInfo);
            encryption.SetGeneric(path, json);
            File.WriteAllText(path, json);
            Debug.Log("<color=red>inventoryInfo saved successfully.</color>");
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to save inventoryInfo: " + e.Message);
        }
    }

    /// <summary>
    /// ���� ���̵� ���� (�������� ���� �Է½� �ش� �������� ���� ���̵��� �ڵ� ����), �Ű����� ������ = ���̵� + 1
    /// <para>��������2 : ���� ���̵� = 3</para>
    /// <para>��������3 : ���� ���̵� = 6</para>
    /// <para>��������4 : ���� ���̵� = 10</para>
    /// </summary>
    /// <param name="stage">���� ���̵� ������ �������� enum �Է�</param>
    public void ChangeDungeonStepInfo(eNextStageType stage = default(eNextStageType))
    {
        if (stage == default(eNextStageType))
        {
            if (this.dungeonInfo.currentStepInfo == 2)
                this.dungeonInfo.currentStepInfo = 2;
            else if (this.dungeonInfo.currentStepInfo == 5)
                this.dungeonInfo.currentStepInfo = 5;
            else if (this.dungeonInfo.currentStepInfo == 9)
                this.dungeonInfo.currentStepInfo = 9;
            else
                this.dungeonInfo.currentStepInfo += 1;
        }
        else if (stage == eNextStageType.STAGE2)
        {
            this.dungeonInfo.currentStepInfo = 3;
        }
        else if (stage == eNextStageType.STAGE3)
        {
            this.dungeonInfo.currentStepInfo = 6;
        }
        else if (stage == eNextStageType.STAGE4)
        {
            this.dungeonInfo.currentStepInfo = 10;
        }
    }

    public void LoadGameInfo()
    {
        //string path = string.Format("{0}/game_info.json",
        //    Application.persistentDataPath);
        //string json = File.ReadAllText(path);
        //this.gameInfo = JsonConvert.DeserializeObject<GameInfo>(json);
        //Debug.Log("���� ���� �ε� �Ϸ�");

        //string path = string.Format("{0}/game_info.json", Application.persistentDataPath);
        //string encryptedJson = File.ReadAllText(path);
        //string decryptedJson = encryption.GetGeneric<string>(path, encryptedJson);
        //this.gameInfo = JsonConvert.DeserializeObject<GameInfo>(decryptedJson);
        //Debug.Log("<color=red>gameInfo loaded successfully.</color>");

        try
        {
            string path = string.Format("{0}/game_info.json", Application.persistentDataPath);
            string encryptedJson = File.ReadAllText(path);
            string decryptedJson = encryption.GetGeneric<string>(path, encryptedJson);
            //string json = File.ReadAllText(decryptedJson);
            this.gameInfo = JsonConvert.DeserializeObject<GameInfo>(decryptedJson);
            Debug.Log("<color=red>gameInfo loaded successfully.</color>");
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to save inventoryInfo: " + e.Message);

        }
    }

    /// <summary>
    /// �÷��̾� Inventory ����
    /// </summary>
    public void SaveGameInfo()
    {
        //string path = string.Format("{0}/game_info.json",
        //    Application.persistentDataPath);
        //string json = JsonConvert.SerializeObject(this.gameInfo);
        //File.WriteAllText(path, json);
        //Debug.Log("���� ���� ���� �Ϸ�");

        //string path = string.Format("{0}/game_info.json", Application.persistentDataPath);
        //string json = JsonConvert.SerializeObject(this.gameInfo);
        //encryption.SetGeneric(path, json);
        //Debug.Log("<color=red>gameInfo saved successfully.</color>");

        try
        {
            string path = string.Format("{0}/game_info.json", Application.persistentDataPath);
            string json = JsonConvert.SerializeObject(this.gameInfo);
            encryption.SetGeneric(path, json);
            File.WriteAllText(path, json);
            Debug.Log("<color=red>gameInfo saved successfully.</color>");
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to save gameInfo: " + e.Message);

        }

    }

    /// <summary>
    /// Ʃ�丮�� �ϷῩ�� ���� + �ڵ�����
    /// </summary>
    /// <param name="type"></param>
    public void TutorialDone(eTutorialType type)
    {
        switch (type)
        {
            case eTutorialType.SHOP:
                this.gameInfo.isShopTuto = true;
                break;
            case eTutorialType.STAT:
                this.gameInfo.isStatTuto = true;
                break;
            case eTutorialType.RESULT:
                this.gameInfo.isResultTuto = true;
                break;
            case eTutorialType.KNIGHTDIPOSIT:
                this.gameInfo.isKnightDipositTuto = true;
                break;
            case eTutorialType.ROGUEDIPOSIT:
                this.gameInfo.isRogueDipositTuto = true;
                break;
            case eTutorialType.DICE:
                this.gameInfo.isDiceTuto = true;
                break;
        }
        this.SaveGameInfo();
    }
}
