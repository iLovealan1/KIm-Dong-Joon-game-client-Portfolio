using Newtonsoft.Json;
using System;
using System.IO;
using UnityEngine;

public partial class InfoManager
{

    public StatInfo statInfo = new StatInfo();

    public StatInfo GetStatInfo()
    {
        return this.statInfo;
    }

    /// <summary>
    /// �ɷ�ġ �ʱ�ȭ �޼��� + �ڵ�����
    /// </summary>
    /// <param name="powerStat">���� ���� ���ݷ�</param>
    /// <param name="fireRateStat">���� ���� ���ݼӵ�</param>
    /// <param name="criticalHitAmount">���� ���� ġ��Ÿ ����</param>
    /// <param name="criticalHitChance">���� ���� ġ��Ÿ Ȯ��</param>
    public void InitStats(int powerStat, int fireRateStat,
        int criticalHitChance, int criticalHitAmount)
    {
        this.statInfo.powerStat = powerStat;
        this.statInfo.fireRateStat = fireRateStat;
        this.statInfo.criticalHitAmount = criticalHitAmount;
        this.statInfo.criticalHitChance = criticalHitChance;
        GameObject.FindObjectOfType<GunShell>()?.GetComponent<GunShell>().SetGun();
        this.SaveStatInfo();
    }

    public void UpgradeStats(int powerStat, int fireRateStat,
        int criticalHitChance, int criticalHitAmount)
    {
        this.statInfo.powerStatOrigin += powerStat;
        this.statInfo.fireRateStatOrigin += fireRateStat;

        this.statInfo.criticalHitAmountOrigin += criticalHitAmount;
        this.statInfo.criticalHitChanceOrigin += criticalHitChance;

        this.statInfo.powerStat += powerStat;
        this.statInfo.fireRateStat += fireRateStat;
        this.statInfo.criticalHitAmount += criticalHitAmount;
        this.statInfo.criticalHitChance += criticalHitChance;

        GameObject.FindObjectOfType<GunShell>()?.GetComponent<GunShell>().SetGun();
        this.SaveStatInfo();
    }

    public void ResetStats()
    {
        int powerStatChange = this.statInfo.powerStatOrigin - 10;
        int fireRateStatChange = this.statInfo.fireRateStatOrigin - 10;
        int criticalHitAmountChange = this.statInfo.criticalHitAmountOrigin - 10;
        int criticalHitChanceChange = this.statInfo.criticalHitChanceOrigin - 10;

        this.statInfo.powerStat -= powerStatChange;
        this.statInfo.fireRateStat -= fireRateStatChange;
        this.statInfo.criticalHitAmount -= criticalHitAmountChange;
        this.statInfo.criticalHitChance -= criticalHitChanceChange;

        this.statInfo.powerStatOrigin = 10;
        this.statInfo.fireRateStatOrigin = 10;
        this.statInfo.criticalHitAmountOrigin = 10;
        this.statInfo.criticalHitChanceOrigin = 10;

        GameObject.FindObjectOfType<GunShell>()?.GetComponent<GunShell>().SetGun();
        this.SaveStatInfo();
    }

    /// <summary>
    /// ĳ������ ���ݷ��� ������ŵ�ϴ�. **���� ������ ��� Enum Ÿ�� ���� �ʿ�**
    /// <para>ù ��° ����: <paramref name="amount"/> ������Ű�� ���� ����.</para>
    /// <para>�� ��° ����: <paramref name="type"/> ���� ���� ���� ���� (�⺻�� No).</para>
    /// </summary>
    /// <param name="amount">���ݷ� ���� ����</param>
    /// <param name="type">������ ���� ����</param>
    public void IncreasePowerStat(int amount, ePermanent type = ePermanent.NO)
    {
        if (type == ePermanent.YES)
        {
            this.statInfo.powerStat += amount;
            this.SaveStatInfo();
        }
        else
        {
            this.statInfo.powerStat += amount;
        }

        GameObject.FindObjectOfType<GunShell>()?.GetComponent<GunShell>().SetGun();
    }

    /// <summary>
    /// ĳ������ ���ݼӵ��� ������ŵ�ϴ�. **���� ������ ��� Enum Ÿ�� ���� �ʿ�**
    /// <para>ù ��° ����: <paramref name="amount"/> ������Ű�� ���� ����.</para>
    /// <para>�� ��° ����: <paramref name="type"/> ���� ���� ���� ���� (�⺻�� No).</para>
    /// </summary>
    /// <param name="amount">���ݼӵ� ���� ����</param>
    /// <param name="type">������ ���� ����</param>
    public void IncreaseFireRateStat(int amount = 1, ePermanent type = ePermanent.NO)
    {
        if (type == ePermanent.YES)
        {
            this.statInfo.fireRateStat += amount;
            this.SaveStatInfo();
        }
        else
        {
            this.statInfo.fireRateStat += amount;
        }

        GameObject.FindObjectOfType<GunShell>()?.GetComponent<GunShell>().SetGun();
    }

    /// <summary>
    /// ĳ������ ġ��Ÿ ���ط��� ������ŵ�ϴ�. **���� ������ ��� Enum Ÿ�� ���� �ʿ�**
    /// <para>ù ��° ����: <paramref name="amount"/> ������Ű�� ���� ����.</para>
    /// <para>�� ��° ����: <paramref name="type"/> ���� ���� ���� ���� (�⺻�� No).</para>
    /// </summary>
    /// <param name="amount">ġ��Ÿ ���ط� ���� ����</param>
    /// <param name="type">������ ���� ����</param>
    public void IncreaseCriticalHitAmountStat(int amount, ePermanent type = ePermanent.NO)
    {
        if (type == ePermanent.YES)
        {
            this.statInfo.criticalHitAmount += amount;
            this.SaveStatInfo();
        }
        else
        {
            this.statInfo.criticalHitAmount += amount;
        }

        GameObject.FindObjectOfType<GunShell>()?.GetComponent<GunShell>().SetGun();
    }

    /// <summary>
    /// ĳ������ ġ��Ÿ Ȯ���� ������ŵ�ϴ�. **���� ������ ��� Enum Ÿ�� ���� �ʿ�**
    /// <para>ù ��° ����: <paramref name="amount"/> ������Ű�� ���� ����.</para>
    /// <para>�� ��° ����: <paramref name="type"/> ���� ���� ���� ���� (�⺻�� No).</para>
    /// </summary>
    /// <param name="amount">ġ��Ÿ Ȯ�� ���� ����</param>
    /// <param name="type">������ ���� ����</param>
    public void IncreaseCriticalHitChanceStat(int amount = 1, ePermanent type = ePermanent.NO)
    {
        if (type == ePermanent.YES)
        {
            this.statInfo.criticalHitChance += amount;
            this.SaveStatInfo();
        }
        else
        {
            this.statInfo.criticalHitChance += amount;
        }

        GameObject.FindObjectOfType<GunShell>()?.GetComponent<GunShell>().SetGun();
    }

    /// <summary>
    /// �÷��̾� StatInfo �ҷ�����
    /// </summary>
    public void LoadStatInfo()
    {
        //string path = string.Format("{0}/stat_info.json",
        //    Application.persistentDataPath);
        //string json = File.ReadAllText(path);
        //this.statInfo = JsonConvert.DeserializeObject<StatInfo>(json);
        //Debug.Log("�÷��̾� �ɷ�ġ ���� �ε� �Ϸ�");

        //string path = string.Format("{0}/stat_info.json", Application.persistentDataPath);
        //string encryptedJson = File.ReadAllText(path);
        //string decryptedJson = encryption.GetGeneric<string>(path, encryptedJson);
        //this.statInfo = JsonConvert.DeserializeObject<StatInfo>(decryptedJson);
        //Debug.Log("<color=red>statInfo loaded successfully.</color>");

        try
        {
            string path = string.Format("{0}/stat_info.json", Application.persistentDataPath);
            string encryptedJson = File.ReadAllText(path);
            string decryptedJson = encryption.GetGeneric<string>(path, encryptedJson);
            //string json = File.ReadAllText(decryptedJson);
            this.statInfo = JsonConvert.DeserializeObject<StatInfo>(decryptedJson);
            Debug.Log("<color=red>statInfo loaded successfully.</color>");
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to save statInfo: " + e.Message);
        }
    }

    /// <summary>
    /// �÷��̾� StatInfo ����
    /// </summary>
    public void SaveStatInfo()
    {
        //string path = string.Format("{0}/stat_info.json",
        //    Application.persistentDataPath);
        //string json = JsonConvert.SerializeObject(this.statInfo);
        //File.WriteAllText(path, json);
        //Debug.Log("�÷��̾� �ɷ�ġ���� ���� �Ϸ�");

        //string path = string.Format("{0}/stat_info.json", Application.persistentDataPath);
        //string json = JsonConvert.SerializeObject(this.statInfo);
        //encryption.SetGeneric(path, json);
        //Debug.Log("<color=red>statInfo saved successfully.</color>");

        try
        {
            string path = string.Format("{0}/stat_info.json", Application.persistentDataPath);
            string json = JsonConvert.SerializeObject(this.statInfo);
            encryption.SetGeneric(path, json);
            File.WriteAllText(path, json);
            Debug.Log("<color=red>statInfo saved successfully.</color>");
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to save statInfo: " + e.Message);

        }
    }
}
