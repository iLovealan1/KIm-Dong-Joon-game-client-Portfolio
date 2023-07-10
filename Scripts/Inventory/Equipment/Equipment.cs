using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Stat
{
    public int powerStat;
    public int fireRateStat;
    public int criticalHitAmount;
    public int criticalHitChance;
}

public abstract class Equipment : MonoBehaviour
{
    public Stat stat;

    public abstract void SetEquipmentStat(int amount);
    public abstract void UnSetEquipmentStat();

}
