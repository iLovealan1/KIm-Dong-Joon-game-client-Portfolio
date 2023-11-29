using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeFactory : AbsEquipmentFactory
{
    public override Color32 MakeGradeBGColor(string grade)
    {
        Color32 gradeColor = new Color32();
        switch (grade)
        {
            case "Wood":
                gradeColor = new Color32(176, 82, 0, 255);
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
        Sprite AxeImage = null;
        switch (grade)
        {
            case "Wood":
                AxeImage = atlas.GetSprite("Wood_Axe");
                break;
            case "Iron":
                AxeImage = atlas.GetSprite("Iron_Axe");
                break;
            case "Gold":
                AxeImage = atlas.GetSprite("Gold_Axe");
                break;
            case "Diamond":
                AxeImage = atlas.GetSprite("Diamond_Axe");
                break;
        }
        return AxeImage;
    }

    public override int MakeEquipmentStat(string grade)
    {
        var stat = DataManager.Instance.GetStatByTypeAndGrade("Axe", grade);
        return stat;
    }
}
