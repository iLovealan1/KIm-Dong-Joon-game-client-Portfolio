using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wand : Equipment
{
    public override void SetEquipmentStat(int amount)
    {
        this.stat.criticalHitChance = amount;
        InfoManager.instance.IncreaseCriticalHitChanceStat(amount);

    }
    public override void UnSetEquipmentStat()
    {
        InfoManager.instance.IncreaseCriticalHitChanceStat(-this.stat.criticalHitChance);
    }
}
