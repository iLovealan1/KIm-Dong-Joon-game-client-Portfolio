using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Equipment
{
    public override void SetEquipmentStat(int amount)
    {
        this.stat.powerStat = amount;
        InfoManager.instance.IncreasePowerStat(amount);
    }
    public override void UnSetEquipmentStat()
    {
        InfoManager.instance.IncreasePowerStat(-this.stat.powerStat);
    }
}

