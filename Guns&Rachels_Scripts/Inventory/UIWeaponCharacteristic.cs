using System;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class UIWeaponCharacteristic : MonoBehaviour
{
    [SerializeField] private Text txtWeaponProficiency;
    [SerializeField] private Slider WeaponProficiencySlider;
    [SerializeField] private Text txtWeaponProficiencySlider;

    [SerializeField] private Image imgWeapon_Icon;
    [SerializeField] private Image WeaponSkill1_Icon;
    [SerializeField] private Image WeaponSkill2_Icon;
    [SerializeField] private Image WeaponSkill3_Icon;

    [SerializeField] private Text txtBulletAmount;
    [SerializeField] private Text txtPenetrate;
    [SerializeField] private Text txtkNockBackCharacteristic;
    [SerializeField] private Text txtkMovespeedCharacteristic;
    [SerializeField] private Text txtDashRecover;

    [SerializeField] private UIWeaponSkillPopUp uiWeaponSkillPopUp;

    public Action<Vector3, string> onSkillPopup;
    public Action onSkillPopupClose;

    private SpriteAtlas weaponIconAtlas;
    private SpriteAtlas skillIconAtlas;

    private int bulletAmountCharacteristic;
    private int penetrateCharacteristic;
    private int kncokBackCharacteristic;
    private int moveSpeedCharacteristic;
    private int dashRecoverCharacteristic;


    public void Init()
    {
        //Initializing
        this.onSkillPopup = (pos, skillName) =>
        {
            this.uiWeaponSkillPopUp.gameObject.SetActive(true);
            this.uiWeaponSkillPopUp.RefreshPopup(skillName);
            this.uiWeaponSkillPopUp.transform.localPosition = new Vector3(pos.x, pos.y + 5, pos.z);
        };
        this.onSkillPopupClose = () =>
        {
            this.uiWeaponSkillPopUp.gameObject.SetActive(false);
        };

        this.weaponIconAtlas = AtlasManager.instance.GetAtlasByName("UIWeaponIcon");
        this.skillIconAtlas = AtlasManager.instance.GetAtlasByName("UISkillIcon");

        this.bulletAmountCharacteristic = InfoManager.instance.charactoristicInfo.bulletAmountCharacteristic;
        this.penetrateCharacteristic = InfoManager.instance.charactoristicInfo.penetrateCharacteristic;
        this.kncokBackCharacteristic = InfoManager.instance.charactoristicInfo.kncokBackCharacteristic;
        this.moveSpeedCharacteristic = InfoManager.instance.charactoristicInfo.moveSpeedCharacteristic;
        this.dashRecoverCharacteristic = InfoManager.instance.charactoristicInfo.dashRecoverCharacteristic;

        this.RefreshWeaponStat();
        this.uiWeaponSkillPopUp.Init();
    }

    //Updates when available
    private void OnEnable()
    {
        if (weaponIconAtlas != null)
            this.RefreshWeaponStat();
    }

    public void RefreshWeaponStat()
    {
        this.SetWeaponIcon();
        this.SetWeaponProficiency();
        this.SetWeaponCharactoristic();
    }

    public void SetWeaponIcon()
    {
        this.imgWeapon_Icon.sprite = this.weaponIconAtlas.GetSprite(InfoManager.instance.charactoristicInfo.currentGunName);
        this.SetSkillIcon();
    }

    public void SetWeaponProficiency()
    {
        this.txtWeaponProficiency.text = string.Format("¼÷·Ãµµ : {0}", InfoManager.instance.charactoristicInfo.gunProficiencyLevel);
        this.WeaponProficiencySlider.value = InfoManager.instance.charactoristicInfo.gunProficiencyEXP * 0.001f;
        this.txtWeaponProficiencySlider.text = string.Format("{0}/{1}", InfoManager.instance.charactoristicInfo.gunProficiencyEXP, 1000);
    }

    public void SetWeaponCharactoristic()
    {
        if (1 < InfoManager.instance.charactoristicInfo.bulletAmountCharacteristic)
        {
            this.txtBulletAmount.text = string.Format("ÃÑ¾Ë °¹¼ö : {0}", InfoManager.instance.charactoristicInfo.bulletAmountCharacteristic);
            this.txtBulletAmount.color = Color.green;
        }
        else
        {
            this.txtBulletAmount.text = string.Format("ÃÑ¾Ë °¹¼ö : {0}", InfoManager.instance.charactoristicInfo.bulletAmountCharacteristic);
            this.txtBulletAmount.color = Color.white;
        }

        if (1 < InfoManager.instance.charactoristicInfo.penetrateCharacteristic)
        {
            this.txtPenetrate.text = string.Format("°üÅë·Â : {0}", InfoManager.instance.charactoristicInfo.penetrateCharacteristic);
            this.txtPenetrate.color = Color.green;
        }
        else
        {
            this.txtPenetrate.text = string.Format("°üÅë·Â : {0}", InfoManager.instance.charactoristicInfo.penetrateCharacteristic);
            this.txtPenetrate.color = Color.white;
        }

        if (1 < InfoManager.instance.charactoristicInfo.kncokBackCharacteristic)
        {
            this.txtkNockBackCharacteristic.text = string.Format("³Ë¹é : {0}", InfoManager.instance.charactoristicInfo.kncokBackCharacteristic);
            this.txtkNockBackCharacteristic.color = Color.green;
        }
        else
        {
            this.txtkNockBackCharacteristic.text = string.Format("³Ë¹é : {0}", InfoManager.instance.charactoristicInfo.kncokBackCharacteristic);
            this.txtkNockBackCharacteristic.color = Color.white;
        }

        if (1 < InfoManager.instance.charactoristicInfo.moveSpeedCharacteristic)
        {
            this.txtkMovespeedCharacteristic.text = string.Format("ÀÌµ¿¼Óµµ : {0}", InfoManager.instance.charactoristicInfo.moveSpeedCharacteristic);
            this.txtkMovespeedCharacteristic.color = Color.green;
        }
        else
        {
            this.txtkMovespeedCharacteristic.text = string.Format("ÀÌµ¿¼Óµµ : {0}", InfoManager.instance.charactoristicInfo.moveSpeedCharacteristic);
            this.txtkMovespeedCharacteristic.color = Color.white;
        }

        if (1 < InfoManager.instance.charactoristicInfo.dashRecoverCharacteristic)
        {
            this.txtDashRecover.text = string.Format("´ë½Ã È¸º¹ : {0}", InfoManager.instance.charactoristicInfo.dashRecoverCharacteristic);
            this.txtDashRecover.color = Color.green;
        }
        else
        {
            this.txtDashRecover.text = string.Format("´ë½Ã È¸º¹ : {0}", InfoManager.instance.charactoristicInfo.dashRecoverCharacteristic);
            this.txtDashRecover.color = Color.white;
        }
    }

    public void SetSkillIcon()
    {
        switch (InfoManager.instance.charactoristicInfo.currentGunName)
        {
            case "AssultRifle":
                this.WeaponSkill1_Icon.sprite = this.skillIconAtlas.GetSprite("AssultRifleSkill_01");
                this.WeaponSkill2_Icon.sprite = this.skillIconAtlas.GetSprite("AssultRifleSkill_02");
                this.WeaponSkill3_Icon.sprite = this.skillIconAtlas.GetSprite("AssultRifleSkill_03");
                break;
            case "ShotGun":
                this.WeaponSkill1_Icon.sprite = this.skillIconAtlas.GetSprite("ShotGunSkill_01");
                this.WeaponSkill2_Icon.sprite = this.skillIconAtlas.GetSprite("ShotGunSkill_02");
                this.WeaponSkill3_Icon.sprite = this.skillIconAtlas.GetSprite("ShotGunSkill_03");
                break;
            case "SniperRifle":
                this.WeaponSkill1_Icon.sprite = this.skillIconAtlas.GetSprite("SniperRifleSkill_01");
                this.WeaponSkill2_Icon.sprite = this.skillIconAtlas.GetSprite("SniperRifleSkill_02");
                this.WeaponSkill3_Icon.sprite = this.skillIconAtlas.GetSprite("SniperRifleSkill_03");
                break;
            case "SubmachineGun":
                this.WeaponSkill1_Icon.sprite = this.skillIconAtlas.GetSprite("SubmachineGunSkill_01");
                this.WeaponSkill2_Icon.sprite = this.skillIconAtlas.GetSprite("SubmachineGunSkill_02");
                this.WeaponSkill3_Icon.sprite = this.skillIconAtlas.GetSprite("SubmachineGunSkill_03");
                break;
        }
    }

    //Disable When UI Off
    private void OnDisable()
    {
        this.uiWeaponSkillPopUp.gameObject.SetActive(false);
    }
}
