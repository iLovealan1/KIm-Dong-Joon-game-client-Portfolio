using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public partial class InfoManager
{
    public PossessionAmountInfo possessionAmountInfo = new PossessionAmountInfo(); //���� ��ȭinfo
    /// <summary>
    /// ������� �ʱ�ȭ ( �� : 0 )
    /// </summary>
    public void InitDungeonGoldAmount()
    {
        this.possessionAmountInfo.dungeonGoldAmount = 0;
        this.possessionAmountInfo.LastDipositList = new List<int[]>() {
            new int[2], new int[2], new int[2], new int[2] };
    }

    /// <summary>
    /// ���� ��� ���� 
    /// </summary>
    public void IncreaseDungeonGold(int coin, bool isFieldCoin = false)
    {
        Debug.LogFormat("coin ���� :{0}", coin);
        this.possessionAmountInfo.dungeonGoldAmount += coin;
        EventDispatcher.Instance.Dispatch(EventDispatcher.EventName.UICurrencyDirectorUpdateGoldUI);
        if(!isFieldCoin)
        this.SavepossessionDungeonGoldInfo();
    }

    /// <summary>
    /// ���׸� ����
    /// </summary>
    public void IncreaseEther(int ether)
    {
        Debug.LogFormat("ether ���� :{0}", ether);
        this.possessionAmountInfo.etherAmount += ether;
        EventDispatcher.Instance.Dispatch(EventDispatcher.EventName.UICurrencyDirectorUpdateEtherUI);
        InfoManager.instance.possessionAmountInfo.totalDungeonEther += ether;
        this.SavepossessionGoodsInfo();
    }

    /// <summary>
    /// ���,���׸� ����(������ ����) + �ڵ� ����
    /// </summary>
    public int DecreasePossessionGoods(int price)
    {
        this.possessionAmountInfo.totalConsumptionGold+=price;
        Debug.LogFormat("<color=red>item price:{0},totalConsumptionGold:{1}</color>", price, this.possessionAmountInfo.totalConsumptionGold);
        //if (this.possessionAmountInfo.totalConsumptionGold >= 1000000)
        //{
        //    Debug.Log("<color=yellow>totalConsumptionGold ���� �޼�</color>");
        //    GPGSManager.instance.ReportAchievement("CgkI1MrQrKgMEAIQCA", 100);
        //}

        this.possessionAmountInfo.goldAmount -= price;
        this.SavepossessionGoodsInfo();
        EventDispatcher.Instance.Dispatch(EventDispatcher.EventName.UICurrencyDirectorUpdateGoldUI);
        EventDispatcher.Instance.Dispatch(EventDispatcher.EventName.UICurrencyDirectorUpdateEtherUI);
        return this.possessionAmountInfo.goldAmount;
    }

    /// <summary>
    /// ������ ��� ����
    /// </summary>
    /// <param name="price"></param>
    /// <returns></returns>
    public bool DecreaseDungeonGold(int price)
    {
        if (this.possessionAmountInfo.dungeonGoldAmount >= price)
        {
            this.possessionAmountInfo.totalConsumptionGold += price;
            Debug.LogFormat("<color=red>item price:{0},totalConsumptionGold:{1}</color>", price, this.possessionAmountInfo.totalConsumptionGold);
            //if (this.possessionAmountInfo.totalConsumptionGold >= 1000000)
            //{
            //    Debug.Log("<color=yellow>totalConsumptionGold ���� �޼�</color>");
            //    GPGSManager.instance.ReportAchievement("CgkI1MrQrKgMEAIQCA", 100);
            //}
            this.possessionAmountInfo.dungeonGoldAmount -= price;
            EventDispatcher.Instance.Dispatch(EventDispatcher.EventName.UICurrencyDirectorUpdateGoldUI);
            return true;
        }
        else
            return false;
    }
    

    /// <summary>
    /// ���׸� �Ҹ�
    /// </summary>
    public bool DecreaseEther(int price)
    {
        if (this.possessionAmountInfo.etherAmount >= price)
        {
            this.possessionAmountInfo.etherAmount -= price;
            EventDispatcher.Instance.Dispatch(EventDispatcher.EventName.UICurrencyDirectorUpdateEtherUI);
            var usedEther = InfoManager.instance.gameInfo.allUsedEther += price;
            //if(usedEther >= 1000)
            //{
            //    GPGSManager.instance.ReportAchievement("CgkI1MrQrKgMEAIQCQ", 100);
            //    InfoManager.instance.SaveGameInfo();
            //}


            this.SavepossessionGoodsInfo();
            return true;
        }
        else
            return false;
    }

    /// <summary>
    /// ������ �޼��� ������ ���� �ݾ� ���� ������ ���� + �ڵ� ���� (�����ܿ� �ϰ�� ���ڰ� true)
    /// </summary>
    /// <param name="isRouge">�����ܿ��ϰ�� true</param>
    public bool Deposit(bool isRouge = false)
    {
        var price = this.possessionAmountInfo.dungeonGoldAmount;
        if (price <= 50) { Debug.Log("������"); return false; }

        if (this.possessionAmountInfo.LastDipositList == null)
            this.possessionAmountInfo.LastDipositList = new List<int[]>() { new int[2], new int[2], new int[2], new int[2] }; ;

        var tempArr = this.possessionAmountInfo.LastDipositList[this.dungeonInfo.CurrentStageInfo - 1];

        if (isRouge)
        {
            var ran = UnityEngine.Random.Range(0, 2);
            var finalGold = price * ran;
            this.possessionAmountInfo.LastDipositList[this.dungeonInfo.CurrentStageInfo - 1]
                = new int[] { tempArr[0] + finalGold, tempArr[1] + price };
            this.possessionAmountInfo.goldAmount += finalGold;
        }
        else
        {
            var ran = UnityEngine.Random.Range(1, 11) * 0.1f;
            var depositGold = price * ran;
            var finalGold = (int)Math.Round(depositGold / 10.0f) * 10;
            this.possessionAmountInfo.LastDipositList[this.dungeonInfo.CurrentStageInfo - 1]
                = new int[] { tempArr[0] + finalGold, tempArr[1] + price };
            this.possessionAmountInfo.goldAmount += finalGold;
            Debug.Log(finalGold);
        }
        this.possessionAmountInfo.dungeonGoldAmount -= price;
        Debug.LogFormat("������ ��� : {0} , ���� ��� {1}",
            this.possessionAmountInfo.LastDipositList[this.dungeonInfo.CurrentStageInfo - 1][0],
            this.possessionAmountInfo.LastDipositList[this.dungeonInfo.CurrentStageInfo - 1][1]);
        this.SavepossessionGoodsInfo();
        EventDispatcher.Instance.Dispatch(EventDispatcher.EventName.UICurrencyDirectorUpdateGoldUI);

        return true;
    }

    /// <summary>
    /// ������ ����� Ʃ�� ����Ʈ�� ��ȯ�մϴ� itme1 : ���������� ������ ������ �ݾ� itme2 : ���������� �������� �ݾ�
    /// </summary>
    /// <returns></returns>
    public List<Tuple<int, int>> MakeDipositTupleList()
    {
        var tupleList = new List<Tuple<int, int>>();
        var templist = this.possessionAmountInfo.LastDipositList;

        if (templist != null)
        {
            for (int i = 0; i < templist.Count; i++)
            {
                tupleList.Add(new Tuple<int, int>(templist[i][0], templist[i][1]));
            }
        }

        //tupleList.ForEach((x) => Debug.LogFormat("Ʃ�� ������ 1 : {0} ������ 2 {1}", x.Item1, x.Item2));

        return tupleList;
    }


    /// <summary>
    /// �÷��̾� ������ȭ �ҷ�����
    /// </summary>
    public void LoadpossessionGoodsInfo()
    {
        //string path = string.Format("{0}/possessionAmount_info.json",
        //    Application.persistentDataPath);
        //string json = File.ReadAllText(path);
        //this.possessionAmountInfo = JsonConvert.DeserializeObject<PossessionAmountInfo>(json);
        //Debug.Log("�÷��̾� ��ȭ ���� �ε� �Ϸ�");

        //string path = string.Format("{0}/possessionAmount_info.json", Application.persistentDataPath);
        //string encryptedJson = File.ReadAllText(path);
        //string decryptedJson = encryption.GetGeneric<string>(path, encryptedJson);
        //this.possessionAmountInfo = JsonConvert.DeserializeObject<PossessionAmountInfo>(decryptedJson);
        //Debug.Log("<color=red>possessionGoodsInfo loaded successfully.</color>");

        try
        {
            string path = string.Format("{0}/possessionAmount_info.json", Application.persistentDataPath);
            string encryptedJson = File.ReadAllText(path);
            string decryptedJson = encryption.GetGeneric<string>(path, encryptedJson);
            //string json = File.ReadAllText(decryptedJson);
            this.possessionAmountInfo = JsonConvert.DeserializeObject<PossessionAmountInfo>(decryptedJson);
            Debug.Log("<color=red>possessionGoodsInfo loaded successfully.</color>");
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to save inventoryInfo: " + e.Message);
        }
    }

    /// <summary>
    /// �÷��̾� ������ȭ ����
    /// </summary>
    public void SavepossessionGoodsInfo()
    {
        //string path = string.Format("{0}/possessionAmount_info.json",
        //    Application.persistentDataPath);
        //string json = JsonConvert.SerializeObject(this.possessionAmountInfo);
        //File.WriteAllText(path, json);
        //Debug.Log("�÷��̾� ��ȭ ���� �Ϸ�");
        //Debug.LogFormat("Save plssessionAmountGold : {0}", this.possessionAmountInfo.goldAmount);

        //string path = string.Format("{0}/possessionAmount_info.json", Application.persistentDataPath);
        //string json = JsonConvert.SerializeObject(this.possessionAmountInfo);
        //encryption.SetGeneric(path, json);
        //Debug.Log("<color=red>possessionGoodsInfo saved successfully.</color>");

        try
        {
            string path = string.Format("{0}/possessionAmount_info.json", Application.persistentDataPath);
            string json = JsonConvert.SerializeObject(this.possessionAmountInfo);
            encryption.SetGeneric(path, json);
            File.WriteAllText(path, json);
            Debug.Log("<color=red>possessionGoodsInfo saved successfully.</color>");
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to save inventoryInfo: " + e.Message);
        }
    }

    /// <summary>
    /// ���� ��� ����
    /// </summary>
    public void SavepossessionDungeonGoldInfo()
    {
        //string path = string.Format("{0}/possessionAmount_info.json",
        //    Application.persistentDataPath);
        //string json = JsonConvert.SerializeObject(this.possessionAmountInfo);
        //File.WriteAllText(path, json);
        //Debug.Log("�÷��̾� ��ȭ ���� �Ϸ�");
        //Debug.LogFormat("Save plssessionAmountGold : {0}", this.possessionAmountInfo.dungeonGoldAmount);

        string path = string.Format("{0}/possessionAmount_info.json", Application.persistentDataPath);
        string json = JsonConvert.SerializeObject(this.possessionAmountInfo);
        encryption.SetGeneric(path, json);
        File.WriteAllText(path, json);
        Debug.Log("<color=red>SavepossessionDungeonGoldInfo saved successfully.</color>");
    }


}
