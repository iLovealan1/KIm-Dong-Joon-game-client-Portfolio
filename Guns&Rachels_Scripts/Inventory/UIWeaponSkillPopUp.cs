using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class UIWeaponSkillPopUp : MonoBehaviour
{
    [SerializeField] private Image imgStatPopUpSkill;
    [SerializeField] private Text txtStatPopUpSkillName;
    [SerializeField] private Text txtStatPopUpSkillDetail;

    private SpriteAtlas uiSkillAtlas;

    public void Init()
    {
        this.uiSkillAtlas = AtlasManager.instance.GetAtlasByName("UISkillIcon");
        this.gameObject.SetActive(false);
    }

    public void RefreshPopup(string skillImageName)
    {
        var skillName = default(string);
        var skillDetail = default(string);
        DataManager.Instance.GetActiveSkillNameAndDetails(skillImageName, out skillName, out skillDetail);

        this.imgStatPopUpSkill.sprite = this.uiSkillAtlas.GetSprite(skillImageName);
        this.txtStatPopUpSkillName.text = skillName;
        this.txtStatPopUpSkillDetail.text = skillDetail;
    }
}
