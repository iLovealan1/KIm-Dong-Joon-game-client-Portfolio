using UnityEngine;

public class UIStat : MonoBehaviour
{
    [SerializeField] private UIPlayerStats playerStats;
    [SerializeField] private UIWeaponCharacteristic weaponCharacteristic;

    public void Init()
    {
        //Initializing
        this.playerStats.Init();
        this.weaponCharacteristic.Init();
    }
}
