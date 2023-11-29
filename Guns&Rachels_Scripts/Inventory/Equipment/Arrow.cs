using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Arrow : Equipment
{
    public override void SetEquipmentStat(int amount)
    {
        this.stat.fireRateStat = amount;
        InfoManager.instance.IncreaseFireRateStat(amount);
    }
    public override void UnSetEquipmentStat()
    {
        InfoManager.instance.IncreaseFireRateStat(-this.stat.fireRateStat);
    }
}
