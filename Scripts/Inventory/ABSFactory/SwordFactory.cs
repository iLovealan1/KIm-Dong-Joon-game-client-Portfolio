using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwordFactory : AbsEquipmentFactory
{

    public override Color32 MakeGradeBGColor(string grade)
    {
        Color32 gradeColor = new Color32();
        switch (grade)
        {
            case "Wood":
                gradeColor = new Color32(176, 82, 0,255);
                break;
            case "Iron":
                gradeColor = new Color32(192, 178, 166, 255);
                break;
            case "Gold":
                gradeColor = new Color32(255, 255, 0, 255);
                break;
            case "Diamond":
                gradeColor = new Color32(0, 255, 188, 255);
                break;
        }
        return gradeColor;
    }

    public override Sprite MakeEquipmentImage(string grade)
    {
        var atlas = AtlasManager.instance.GetAtlasByName("UIEquipmentIcon");
        Sprite SwordImage = null;
        switch (grade)
        {
            case "Wood":
                SwordImage = atlas.GetSprite("Wood_Sword");
                break;
            case "Iron":
                SwordImage = atlas.GetSprite("Iron_Sword");
                break;
            case "Gold":
                SwordImage = atlas.GetSprite("Gold_Sword");
                break;
            case "Diamond":
                SwordImage = atlas.GetSprite("Diamond_Sword");
                break;
        }
        return SwordImage; 
    }

    public override int MakeEquipmentStat(string grade)
    {
        var stat = DataManager.Instance.GetStatByTypeAndGrade("Sword", grade);
        return stat;
    }
}
