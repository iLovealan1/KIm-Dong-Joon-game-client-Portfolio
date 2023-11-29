using UnityEngine;
using UnityEngine.UI;

public class UIPlayerStats : MonoBehaviour
{
    [SerializeField] private Text txtBattleRate;
    [SerializeField] private Text txtPowerStat;
    [SerializeField] private Text txtFireRateStat;
    [SerializeField] private Text txtCriticalHitChance;
    [SerializeField] private Text txtCriticalHitAmount;

    private int BattleRate;
    private int powerStat;
    private int fireRateStat;
    private int criticalHitChance;
    private int criticalHitAmount;

    public void Init()
    {
        // Initializing
        this.BattleRate = InfoManager.instance.statInfo.BattleRate;
        this.powerStat = InfoManager.instance.statInfo.powerStat;
        this.fireRateStat = InfoManager.instance.statInfo.fireRateStat;
        this.criticalHitChance = InfoManager.instance.statInfo.criticalHitChance;
        this.criticalHitAmount = InfoManager.instance.statInfo.criticalHitAmount;
        this.UpdatePlayerStatUI();
    }

    //Update Player Stats when Inventory Enable
    private void OnEnable()
    {
        this.UpdatePlayerStatUI();
    }

    public void UpdatePlayerStatUI()
    {
        if (this.BattleRate < InfoManager.instance.statInfo.BattleRate)
        {
            this.txtBattleRate.text = string.Format("������ : {0}", InfoManager.instance.statInfo.BattleRate);
            this.txtBattleRate.color = Color.green;
        }
        else
        {
            this.txtBattleRate.text = string.Format("������ : {0}", InfoManager.instance.statInfo.BattleRate);
            this.txtBattleRate.color = Color.white;
        }

        if (this.powerStat < InfoManager.instance.statInfo.powerStat)
        {
            this.txtPowerStat.text = string.Format("���ݷ� : {0}", InfoManager.instance.statInfo.powerStat);
            this.txtPowerStat.color = Color.green;
        }
        else
        {
            this.txtPowerStat.text = string.Format("���ݷ� : {0}", InfoManager.instance.statInfo.powerStat);
            this.txtPowerStat.color = Color.white;
        }

        if (this.fireRateStat < InfoManager.instance.statInfo.fireRateStat)
        {
            this.txtFireRateStat.text = string.Format("���ݼӵ� : {0}", InfoManager.instance.statInfo.fireRateStat);
            this.txtFireRateStat.color = Color.green;
        }
        else
        {
            this.txtFireRateStat.text = string.Format("���ݼӵ� : {0}", InfoManager.instance.statInfo.fireRateStat);
            this.txtFireRateStat.color = Color.white;
        }

        if (this.criticalHitChance < InfoManager.instance.statInfo.criticalHitChance)
        {
            this.txtCriticalHitChance.text = string.Format("ġ��Ÿ Ȯ�� : {0}", InfoManager.instance.statInfo.criticalHitChance);
            this.txtCriticalHitChance.color = Color.green;
        }
        else
        {
            this.txtCriticalHitChance.text = string.Format("ġ��Ÿ Ȯ�� : {0}", InfoManager.instance.statInfo.criticalHitChance);
            this.txtCriticalHitChance.color = Color.white;
        }

        if (this.criticalHitAmount < InfoManager.instance.statInfo.criticalHitAmount)
        {
            this.txtCriticalHitAmount.text = string.Format("ġ��Ÿ ���� : {0}", InfoManager.instance.statInfo.criticalHitAmount);
            this.txtCriticalHitAmount.color = Color.green;
        }
        else
        {
            this.txtCriticalHitAmount.text = string.Format("ġ��Ÿ ���� : {0}", InfoManager.instance.statInfo.criticalHitAmount);
            this.txtCriticalHitAmount.color = Color.white;
        }
    }
}
