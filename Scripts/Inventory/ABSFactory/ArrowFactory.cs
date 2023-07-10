using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowFactory : AbsEquipmentFactory
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
        Sprite ArrowImage = null;
        switch (grade)
        {
            case "Wood":
                ArrowImage = atlas.GetSprite("Wood_Arrow");
                break;
            case "Iron":
                ArrowImage = atlas.GetSprite("Iron_Arrow");
                break;
            case "Gold":
                ArrowImage = atlas.GetSprite("Gold_Arrow");
                break;
            case "Diamond":
                ArrowImage = atlas.GetSprite("Diamond_Arrow");
                break;
        }
        return ArrowImage;
    }

    public override int MakeEquipmentStat(string grade)
    {
        var stat = DataManager.Instance.GetStatByTypeAndGrade("Arrow" , grade);
        return stat;
    }
}
