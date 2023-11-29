using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDialogPanel : MonoBehaviour
{
    public enum eDialogType
    {
        NONE = -1,
        TUTORIALBASIC,
        TUTORIALSTAT,
        TUTORIALSHOP,
        TUTORIALRESULT,
        TUTORIALKNIGHTDIPOSIT,
        TUTORIALROGUEDIPOSIT,
        TUTORIALDICE,
    }

    [SerializeField]
    private Text txtNPCName;

    public Text txtDialog;
    public Button btnDim;
    public Animator anim;
    public GameObject endArrowGo;

    [SerializeField]
    private Image portrait;
    private int idx;

    [SerializeField]
    private float charPerSecond;
    private string targetDialog;

    private int diaologIdx;
    private bool isTalking;

    // Temporary values
    private string dialogA = "�̺� ����ÿ,�� ���⸦ �޾ư�������";
    private string dialogB = "�ǽ��� ��� ����ÿ";

    private string gun;

    private List<DialogData> dialogDataList;
    private List<string> dialogList;
    private List<string> npcNameList;
    private List<string> SpriteNameList;

    public event Action OnDialogEnd;

    private AudioSource audioSource;

    private string[] gunArr = { "AssultRifle", "ShotGun", "SubmachineGun", "SniperRifle" };

    public void Init()
    {

        this.dialogDataList = new List<DialogData>();
        this.dialogList = new List<string>();
        this.npcNameList = new List<string>();
        this.SpriteNameList = new List<string>();
        this.audioSource = this.GetComponentInParent<AudioSource>();

        EventDispatcher.Instance.AddListener<Action>(EventDispatcher.EventName.UIDialogPanelRandomWeaponDialog,
            this.RandomWeaponDialog);
        EventDispatcher.Instance.AddListener<eDialogType>(EventDispatcher.EventName.UIDialogPanelStartDialog,
            this.StartDialog);

        this.gameObject.SetActive(false);
    }

    /// <summary>
    /// Random Weapon Dialog (Conversation with Knight Commander)
    /// </summary>
    private void RandomWeaponDialog(Action onPortalAnim)
    {
        this.gameObject.SetActive(true);

        // Reset dialog-related fields
        this.btnDim.onClick.RemoveAllListeners();
        this.idx = 0;
        this.isTalking = false;

        // Prepare dialog data
        this.dialogDataList.Clear();
        this.dialogList.Clear();
        this.npcNameList.Clear();
        this.SpriteNameList.Clear();

        this.dialogList.Add(this.dialogA);
        this.txtNPCName.text = "������";

        // Randomly select a weapon
        var random = new System.Random();
        var ran = random.Next(0, 4);
        this.gun = this.gunArr[ran];
        if (this.gun == "AssultRifle")
        {
            this.dialogList.Add("���� \"AK47\" �� ������.");
            this.dialogList.Add("���ݼ��Ѱ迭�� ����� ���� �������� ģ����!");
        }
        else if (this.gun == "SniperRifle")
        {
            this.dialogList.Add("���� \"AWP\" �� ������.");
            this.dialogList.Add("�������� �ѹ� �ѹ��� ������ ���� �ż��� ������!");
        }
        else if (this.gun == "ShotGun")
        {
            this.dialogList.Add("���� \"SPAS-12\" �� ������.");
            this.dialogList.Add("��! ����! ���� ���� ��ȭ�� ��������!!");
        }
        else if (this.gun == "SubmachineGun")
        {
            this.dialogList.Add("���� \"UZI\" �� ������.");
            this.dialogList.Add("��������� ����� �ϳ������� ���� ������ ��������!");
        }

        this.dialogList.Add(this.dialogB);

        // Change NPC image using Atlas Manager
        this.portrait.sprite = AtlasManager.instance.GetAtlasByName("UINPCPortraitIcon").GetSprite("EliteKnight_Idle_1");
        this.portrait.transform.localScale = new Vector3(50, 50, 50);
        var pos = this.portrait.transform.localPosition;
        this.portrait.transform.localPosition = new Vector3(pos.x, pos.y + 350, pos.z);

        this.SetDialog(this.dialogList[this.idx]);
        this.anim.Play("UIDialogPanel_Portrait", -1, 0);
        this.btnDim.onClick.AddListener(() =>
        {
            if (!this.isTalking)
            {
                Debug.Log(this.audioSource);
                AudioManager.instance.PlaySFXOneShot(AudioManager.eSFXMusicPlayList.UI_Click, this.audioSource);
                this.anim.Play("UIDialogPanel_Portrait", -1, 0);
                this.idx++;
                if (this.idx >= this.dialogList.Count)
                {
                    this.idx = 0;
                    EventDispatcher.Instance.Dispatch<string>(EventDispatcher.EventName.DungeonSceneMainTakeGun, this.gun);
                    onPortalAnim();
                    this.gameObject.SetActive(false);
                }
            }
            this.SetDialog(this.dialogList[this.idx]);
        });
    }

    /// <summary>
    /// Start NPC dialog based on the dialog type
    /// </summary>
    /// <param name="type">Dialog type</param>
    private void StartDialog(eDialogType type)
    {
        this.gameObject.SetActive(true);

        this.SetDialogList(type);
        this.SetDialog(this.dialogList[this.idx]);
        this.SetPortraitAndName(this.npcNameList[this.idx], this.SpriteNameList[this.idx]);

        this.anim.Play("UIDialogPanel_Portrait", -1, 0);

        this.btnDim.onClick.AddListener(() =>
        {
            if (!this.isTalking)
            {
                this.anim.Play("UIDialogPanel_Portrait", -1, 0);

                this.idx++;
                if (this.idx >= this.dialogList.Count)
                {
                    var pos = this.portrait.transform.localPosition;
                    if (type == eDialogType.TUTORIALDICE) this.portrait.transform.localPosition = new Vector3(pos.x, pos.y - 200, pos.z);
                    else this.portrait.transform.localPosition = new Vector3(pos.x, pos.y - 350, pos.z);
                    this.idx = 0;
                    this.OnDialogEnd?.Invoke();
                    this.OnDialogEnd = null;
                    this.gameObject.SetActive(false);
                }

            }
            this.SetPortraitAndName(this.npcNameList[this.idx], this.SpriteNameList[this.idx]);
            this.SetDialog(this.dialogList[this.idx]);
        });
    }

    private void SetDialogList(eDialogType type)
    {
        // Reset dialog-related fields
        this.btnDim.onClick.RemoveAllListeners();
        this.idx = 0;
        this.isTalking = false;
        var pos = this.portrait.transform.localPosition;
        if (type == eDialogType.TUTORIALDICE) this.portrait.transform.localPosition = new Vector3(pos.x, pos.y + 200, pos.z);
        else this.portrait.transform.localPosition = new Vector3(pos.x, pos.y + 350, pos.z);

        // Prepare dialog data
        this.dialogDataList.Clear();
        this.dialogList.Clear();
        this.npcNameList.Clear();
        this.SpriteNameList.Clear();
        this.dialogDataList = DataManager.Instance.GetDialog(type);
        this.dialogDataList.ForEach((x) => { this.dialogList.Add(x.dialogKOR); });
        this.dialogDataList.ForEach((x) => { this.SpriteNameList.Add(x.npcSpriteName); });
        this.dialogDataList.ForEach((x) => { this.npcNameList.Add(x.npcName); });
    }

    public void SetDialog(string dialog)
    {
        if (this.isTalking)
        {
            this.txtDialog.text = this.targetDialog;
            CancelInvoke();
            this.EndEffect();
        }
        else
        {
            this.targetDialog = dialog;
            this.StartEffect();
        }
    }

    private void SetPortraitAndName(string NPCName, string NPCPortraitName)
    {
        this.txtNPCName.text = NPCName;
        if (NPCPortraitName == "")
        {
            this.portrait.sprite = AtlasManager.instance.GetAtlasByName("UINPCPortraitIcon").GetSprite("nullImage");
        }
        else
        {
            this.portrait.sprite = AtlasManager.instance.GetAtlasByName("UINPCPortraitIcon").GetSprite(NPCPortraitName);
            this.portrait.transform.localScale = new Vector3(50, 50, 50);
        }
    }

    // Animation Control: 3 Stages
    private void StartEffect()
    {
        this.txtDialog.text = "";
        this.diaologIdx = 0;
        this.endArrowGo.SetActive(false);
        this.isTalking = true;

        Invoke("OnEffect", 1 / this.charPerSecond);
    }

    private void OnEffect()
    {
        if (this.txtDialog.text == this.targetDialog)
        {
            this.EndEffect();
            return;
        }

        this.txtDialog.text += this.targetDialog[this.diaologIdx];
        this.diaologIdx++;

        Invoke("OnEffect", 1 / this.charPerSecond);
    }

    private void EndEffect()
    {
        this.endArrowGo.SetActive(true);
        this.isTalking = false;
    }
}
