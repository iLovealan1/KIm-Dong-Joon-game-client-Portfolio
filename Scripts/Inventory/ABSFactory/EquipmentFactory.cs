using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentFactory : MonoBehaviour
{
    [SerializeField]
    private GameObject swordPrefab;

    [SerializeField]
    private GameObject axePrefab;

    [SerializeField]
    private GameObject wandPrefab;

    [SerializeField]
    private GameObject arrowPrefab;

    [SerializeField]
    private GameObject bgEuipmentPrefab;

    public GameObject MakeEquipment(string EquipmentName, GameObject cell)
    {
        var grade = this.ExtractGrade(EquipmentName);
        GameObject equipment = null;
        if (EquipmentName.Contains("Sword")) this.MakeSword(grade, cell, out equipment);
        else if (EquipmentName.Contains("Axe")) this.MakeAxe(grade, cell, out equipment);
        else if (EquipmentName.Contains("Arrow")) this.MakeArrow(grade, cell, out equipment);
        else if (EquipmentName.Contains("Wand")) this.MakeWand(grade, cell, out equipment);    

        return equipment;
    }

    private void MakeSword(string grade , GameObject cell, out GameObject equipment)
    {
        AbsEquipmentFactory swordFactory = new SwordFactory();

        var EquipmentGO = Instantiate(this.bgEuipmentPrefab,cell.transform);

        var swordGO = Instantiate(this.swordPrefab, EquipmentGO.transform);
        var swordComp = swordGO.GetComponent<Sword>();
        var swordImage = swordGO.GetComponent<Image>();
        var EuipmentImage = swordFactory.MakeEquipmentImage(grade);

        swordComp.SetEquipmentStat(swordFactory.MakeEquipmentStat(grade));
        swordImage.sprite = EuipmentImage;
        EquipmentGO.GetComponent<Image>().color = swordFactory.MakeGradeBGColor(grade);
        swordGO.gameObject.name = EuipmentImage.name.Replace("(Clone)", "");

        equipment = EquipmentGO;   
    }

    private void MakeAxe(string grade, GameObject cell, out GameObject equipment)
    {
        AbsEquipmentFactory AxeFactory = new AxeFactory();

        var EquipmentGO = Instantiate(this.bgEuipmentPrefab, cell.transform);

        var axeGO = Instantiate(this.axePrefab, EquipmentGO.transform);
        var axeComp = axeGO.GetComponent<Axe>();
        var axeImage = axeGO.GetComponent<Image>();
        var EuipmentImage = AxeFactory.MakeEquipmentImage(grade);

        axeComp.SetEquipmentStat(AxeFactory.MakeEquipmentStat(grade));
        axeImage.sprite = EuipmentImage;
        EquipmentGO.GetComponent<Image>().color = AxeFactory.MakeGradeBGColor(grade);
        axeGO.gameObject.name = EuipmentImage.name.Replace("(Clone)", "");

        equipment = EquipmentGO;
    }

    private void MakeArrow(string grade, GameObject cell, out GameObject equipment)
    {
        AbsEquipmentFactory arrowFactory = new ArrowFactory();

        var EquipmentGO = Instantiate(this.bgEuipmentPrefab, cell.transform);

        var arrowGo = Instantiate(this.arrowPrefab, EquipmentGO.transform);
        var arrowComp = arrowGo.GetComponent<Arrow>();
        var arrowImage = arrowGo.GetComponent<Image>();
        var EuipmentImage = arrowFactory.MakeEquipmentImage(grade);

        arrowGo.gameObject.name = arrowImage.sprite.name.Replace("(Clone)", "");
        arrowComp.SetEquipmentStat(arrowFactory.MakeEquipmentStat(grade));
        arrowImage.sprite = EuipmentImage;
        EquipmentGO.GetComponent<Image>().color = arrowFactory.MakeGradeBGColor(grade);
        arrowGo.gameObject.name = EuipmentImage.name.Replace("(Clone)", "");

        equipment = EquipmentGO;
    }

    private void MakeWand(string grade, GameObject cell, out GameObject equipment)
    {
        AbsEquipmentFactory wandFactory = new WandFactory();

        var EquipmentGO = Instantiate(this.bgEuipmentPrefab, cell.transform);

        var wandGO = Instantiate(this.wandPrefab, EquipmentGO.transform);
        var wandComp = wandGO.GetComponent<Wand>();
        var wandImage = wandGO.GetComponent<Image>();
        var EuipmentImage = wandFactory.MakeEquipmentImage(grade);

        wandComp.SetEquipmentStat(wandFactory.MakeEquipmentStat(grade));
        wandImage.sprite = EuipmentImage;
        EquipmentGO.GetComponent<Image>().color = wandFactory.MakeGradeBGColor(grade);
        wandGO.gameObject.name = EuipmentImage.name.Replace("(Clone)", "");

        equipment = EquipmentGO;
    }

    private string ExtractGrade(string EquipmentName)
    {
        var index = EquipmentName.IndexOf('_');
        var grade = EquipmentName.Substring(0, index);       
        return grade;
    }
}
