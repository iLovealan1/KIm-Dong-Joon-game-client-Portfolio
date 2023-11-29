using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public partial class DataManager
{
    public static readonly DataManager Instance = new DataManager();

    private DataManager() { }

    public void LoadAllDatas()
    {
        this.LoadMonsterData();
        this.LoadMonsterGroupData();
        this.LoadDifficultyData();
        this.LoadDialogData();
        this.LoadActiveSkillData();
        this.LoadRoomData();
        this.LoadEquipmentData();
        this.LoadRelicData();
        this.LoadChestData();
        this.LoadGambleData();
        this.LoadEtcItemData();
        this.LoadWeaponData();
    }
}
