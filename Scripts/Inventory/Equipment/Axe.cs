using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : Equipment
{
    public override void SetEquipmentStat(int amount)
    {
        this.stat.criticalHitAmount = amount;   
        InfoManager.instance.IncreaseCriticalHitAmountStat(amount);
    }

    public override void UnSetEquipmentStat()
    {
        InfoManager.instance.IncreaseCriticalHitAmountStat(-this.stat.criticalHitAmount);
    }
}
