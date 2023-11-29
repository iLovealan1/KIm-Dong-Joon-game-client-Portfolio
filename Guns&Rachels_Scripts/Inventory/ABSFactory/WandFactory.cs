using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandFactory : AbsEquipmentFactory
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
        Sprite WandImage = null;
        switch (grade)
        {
            case "Wood":
                WandImage = atlas.GetSprite("Wood_Wand");
                break;
            case "Iron":
                WandImage = atlas.GetSprite("Iron_Wand");
                break;
            case "Gold":
                WandImage = atlas.GetSprite("Gold_Wand");
                break;
            case "Diamond":
                WandImage = atlas.GetSprite("Diamond_Wand");
                break;
        }
        return WandImage;
    }

    public override int MakeEquipmentStat(string grade)
    {
        var stat = DataManager.Instance.GetStatByTypeAndGrade("Wand", grade);
        return stat;
    }
}
