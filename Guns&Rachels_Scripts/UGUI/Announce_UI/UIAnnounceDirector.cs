using DG.Tweening;
using Febucci.UI;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIAnnounceDirector : MonoBehaviour
{
    public enum eAnnounceType
    {
        NONE = -1,
        ROOM,
        STAGE,
        SANCTUARY,
        DUNGEON,
        HIDDENROOM,
        SAFEHOUSE,
        BOSS,
    }

    [SerializeField] public TextAnimatorPlayer textAnimatorPlayer;
    [SerializeField] private Text txtGuide;

    private string txtAnnounce;
    private Coroutine routine;

    public void Init()
    {
        EventDispatcher.Instance.AddListener<eAnnounceType>(EventDispatcher.EventName.UIAnnounceDirectorStartAnnounce,
            this.StartAnnounce);
        this.gameObject.SetActive(false);
    }

    private void StartAnnounce(eAnnounceType type)
    {
        this.gameObject.SetActive(true);
        switch (type)
        {
            case eAnnounceType.NONE:
                break;
            case eAnnounceType.ROOM:
                this.txtAnnounce = string.Format("Room <rainb>Clear!!</rainb>");
                this.txtAnnounce += "\n<speed=6>{rdir}!!!<color=yellow>상자</color> 출현!!!{rdir}";
                break;
            case eAnnounceType.STAGE:
                this.txtAnnounce = string.Format("Stage: {0} <rainb>Clear</rainb>!!", InfoManager.instance.dungeonInfo.CurrentStageInfo);
                this.txtAnnounce += "\n<speed=6><share>!!!스테이지 포탈 출현!!!</share>";
                break;
            case eAnnounceType.SANCTUARY:
                this.txtAnnounce = string.Format("기억의 성소");
                break;
            case eAnnounceType.DUNGEON:
                this.txtAnnounce = string.Format("Stage: {0} <speed=.5>\"{1}\"", InfoManager.instance.dungeonInfo.CurrentStageInfo,
                    this.MakeStageName());
                break;
            case eAnnounceType.SAFEHOUSE:
                this.txtAnnounce = "안전가옥";
                break;
            case eAnnounceType.HIDDENROOM:
                this.txtAnnounce = "희생의 방";
                break;
            case eAnnounceType.BOSS:
                this.txtAnnounce = "<speed=10><color=red><expl>!!!WARNING!!!</expl></color>";
                break;
        }

        if (this.routine == null)
            this.routine = this.StartCoroutine(this.CoStartAnnounce(type));
        else
        {
            this.textAnimatorPlayer.GetComponent<TMP_Text>().DOKill();
            this.textAnimatorPlayer.GetComponent<TMP_Text>().color = Color.white;
            this.StopAllCoroutines();
            this.routine = this.StartCoroutine(this.CoStartAnnounce(type));
        }
    }

    private IEnumerator CoStartAnnounce(eAnnounceType type)
    {
        this.textAnimatorPlayer.ShowText(this.txtAnnounce);

        yield return new WaitForSeconds(3f);

        this.textAnimatorPlayer.GetComponent<TMP_Text>()
            .DOFade(0f, 0.5f).OnComplete(() =>
            {
                this.textAnimatorPlayer.GetComponent<TMP_Text>().color = Color.white;
                this.gameObject.SetActive(false);
            });
    }

    private string MakeStageName()
    {
        var stageName = default(string);
        var stageNum = InfoManager.instance.dungeonInfo.CurrentStageInfo;
        if (stageNum == 1) stageName = "<color=green>마물의 숲</color>";
        else if (stageNum == 2) stageName = "<color=#6F2D00>더러운자들의 무덤</color>";
        else if (stageNum == 3) stageName = "<color=#7DB5FF>고대의 사원</color>";
        else if (stageNum == 4) stageName = "<color=red>악마의 심장</color>";

        if (InfoManager.instance.dungeonInfo.CurrentStageInfo == 1)
            this.StartGuide();

        return stageName;
    }

    private void StartGuide()
    {
        this.txtGuide.DOFade(1f, 0.5f).SetEase(Ease.InCubic).OnComplete(() =>
        {
            DOTween.Sequence().AppendInterval(2f).OnComplete(() =>
            {
                this.txtGuide.DOFade(0f, 0.5f).SetEase(Ease.OutCubic).OnComplete(() =>
                {
                    this.txtGuide.gameObject.SetActive(false);
                });
            });
        });
    }

    private void OnDisable()
    {
        this.txtGuide.DOKill();
    }
}
