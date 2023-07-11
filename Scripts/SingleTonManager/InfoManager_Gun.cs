public partial class InfoManager
{
    public GunCharacteristicInfo charactoristicInfo = new GunCharacteristicInfo();

    /// <summary>
    /// �ѱ�Ư�� �ʱ�ȭ + �ѱ� ���õ� �ʱ�ȭ ( ������ ����� �ݵ�� ȣ�� )
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
    /// �ѱ�Ư��  : �̵� �ӵ� ���� + 1 
    /// </summary>
    /// <param name="amount">�̵��ӵ� ���� ���� (�⺻�� 1)</param>
    public void IncreaseMovespeedCharacteristic(int amount = 1)
    {
        this.charactoristicInfo.moveSpeedCharacteristic += amount;
    }

    /// <summary>
    /// �ѱ�Ư�� : �˹� ���� + 1 
    /// </summary>
    public void IncreaseKnockBackCharacteristic()
    {
        this.charactoristicInfo.kncokBackCharacteristic += 1;
    }

    /// <summary>
    /// �ѱ�Ư�� : �Ѿ� ���� ���� + 1 
    /// </summary>
    public void IncreaseBulletAmountCharacteristic()
    {
        this.charactoristicInfo.bulletAmountCharacteristic += 1;
    }

    /// <summary>
    /// �ѱ�Ư�� : ��� ȸ�� �ӵ� ���� + 1 
    /// </summary>
    public void IncreaseDashRecoverCharacteristic()
    {
        this.charactoristicInfo.dashRecoverCharacteristic += 1;
    }

    /// <summary>
    /// �ѱ�Ư�� : ����� ���� + 1 
    /// </summary>
    public void IncreasePenetrateCharacteristic()
    {
        this.charactoristicInfo.penetrateCharacteristic += 1;
    }

    /// <summary>
    /// �ѱ� ���õ� ���� +1
    /// </summary>
    public void IncreaseGunPreficiencyLevel()
    {
        this.charactoristicInfo.gunProficiencyLevel += 1;
    }

    /// <summary>
    /// �ѱ� ����ġ ���� +4
    /// </summary>
    public void IncreaseGunProficiencyEXP(int exp)
    {
        this.charactoristicInfo.gunProficiencyEXP += exp;
    }

    /// <summary>
    /// ���� ��� �ִ� �ѱ� �̸� ���� (AK47, AWP, UZI, SPAS-12) (�⺻�� : AK47)
    /// </summary>
    /// <param name="weaponName">���� ��� �ִ� ���� �̸�</param>
    public void ChangeCurrentGunName(string weaponName = "AssultRifle")
    {
        this.charactoristicInfo.currentGunName = weaponName;
    }
}
