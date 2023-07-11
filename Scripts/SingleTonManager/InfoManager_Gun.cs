public partial class InfoManager
{
    public GunCharacteristicInfo charactoristicInfo = new GunCharacteristicInfo();

    /// <summary>
    /// 총기특성 초기화 + 총기 숙련도 초기화 ( 던전씬 엔드시 반드시 호출 )
    /// </summary>
    public void InitCharactorlisticsAndGunProficiency()
    {
        this.charactoristicInfo.moveSpeedCharacteristic = 1;
        this.charactoristicInfo.kncokBackCharacteristic = 1;
        this.charactoristicInfo.bulletAmountCharacteristic = 1;
        this.charactoristicInfo.dashRecoverCharacteristic = 1;
        this.charactoristicInfo.penetrateCharacteristic = 1;
        this.charactoristicInfo.gunProficiencyLevel = 0;
        this.charactoristicInfo.gunProficiencyEXP = 0;

        this.charactoristicInfo.originMoveSpeedCharacteristic = 1;
        this.charactoristicInfo.originKncokBackCharacteristic = 1;
        this.charactoristicInfo.originBulletAmountCharacteristic = 1;
        this.charactoristicInfo.originDashRecoverCharacteristic = 1;
        this.charactoristicInfo.originPenetrateCharacteristic = 1;
    }


    /// <summary>
    /// 총기특성  : 이동 속도 증가 + 1 
    /// </summary>
    /// <param name="amount">이동속도 증가 정도 (기본값 1)</param>
    public void IncreaseMovespeedCharacteristic(int amount = 1)
    {
        this.charactoristicInfo.moveSpeedCharacteristic += amount;
    }

    /// <summary>
    /// 총기특성 : 넉백 증가 + 1 
    /// </summary>
    public void IncreaseKnockBackCharacteristic()
    {
        this.charactoristicInfo.kncokBackCharacteristic += 1;
    }

    /// <summary>
    /// 총기특성 : 총알 숫자 증가 + 1 
    /// </summary>
    public void IncreaseBulletAmountCharacteristic()
    {
        this.charactoristicInfo.bulletAmountCharacteristic += 1;
    }

    /// <summary>
    /// 총기특성 : 대시 회복 속도 증가 + 1 
    /// </summary>
    public void IncreaseDashRecoverCharacteristic()
    {
        this.charactoristicInfo.dashRecoverCharacteristic += 1;
    }

    /// <summary>
    /// 총기특성 : 관통력 증가 + 1 
    /// </summary>
    public void IncreasePenetrateCharacteristic()
    {
        this.charactoristicInfo.penetrateCharacteristic += 1;
    }

    /// <summary>
    /// 총기 숙련도 증가 +1
    /// </summary>
    public void IncreaseGunPreficiencyLevel()
    {
        this.charactoristicInfo.gunProficiencyLevel += 1;
    }

    /// <summary>
    /// 총기 경험치 증가 +4
    /// </summary>
    public void IncreaseGunProficiencyEXP(int exp)
    {
        this.charactoristicInfo.gunProficiencyEXP += exp;
    }

    /// <summary>
    /// 현재 들고 있는 총기 이름 변경 (AK47, AWP, UZI, SPAS-12) (기본값 : AK47)
    /// </summary>
    /// <param name="weaponName">현재 들고 있는 무기 이름</param>
    public void ChangeCurrentGunName(string weaponName = "AssultRifle")
    {
        this.charactoristicInfo.currentGunName = weaponName;
    }
}
