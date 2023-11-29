using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public abstract class AbsEquipmentFactory
{
    public abstract Color32 MakeGradeBGColor(string grade);
    public abstract Sprite MakeEquipmentImage(string grade);      
    public abstract int MakeEquipmentStat(string grade);     
}
