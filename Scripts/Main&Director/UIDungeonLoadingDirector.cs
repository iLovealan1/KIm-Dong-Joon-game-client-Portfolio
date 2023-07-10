using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDungeonLoadingDirector : MonoBehaviour
{
    [SerializeField] private Image imgDim;

    private CanvasGroup CanvasGroup;
    private Transform portalEffectTrans;

    public void Init()
    {
        this.imgDim.color = Color.black;
        this.CanvasGroup = this.imgDim.GetComponent<CanvasGroup>();
        this.CanvasGroup.alpha = 0f;
        this.portalEffectTrans = this.imgDim.transform.GetChild(1);

        EventDispatcher.Instance.AddListener(EventDispatcher.EventName.UIDungeonLoadingDirectorStageLoading,
            this.StageLoading);

        EventDispatcher.Instance.AddListener(EventDispatcher.EventName.UIDungeonLoadingDirectorSanctuarytLoading,
            this.SanctuaryLoading);

        this.gameObject.SetActive(false);
    }


    private void StageLoading()
    {
        this.gameObject.SetActive(true);
        this.portalEffectTrans.DOScale(new Vector3 (5,5,5),2f).SetEase(Ease.InCubic).OnComplete(() =>
        {
            this.portalEffectTrans.localScale = new Vector3(1, 1, 1);
        });
        this.CanvasGroup.DOFade(1f, 1f).SetEase(Ease.Linear).OnComplete(() =>
        {
            EventDispatcher.Instance.Dispatch<System.Action>(EventDispatcher.EventName.DungeonMainToNextStage, () =>
            {
                DOTween.Sequence()
                .AppendInterval(0.5f)
                .OnComplete(() =>
                {
                   
                    this.CanvasGroup.DOFade(0f, 0.2f).SetEase(Ease.Linear).OnComplete(() =>
                    {
                        this.gameObject.SetActive(false);
                    });
                });

            });

        });
    }

    private void SanctuaryLoading()
    {
        this.imgDim.color = Color.white;
        this.gameObject.SetActive(true);
        this.imgDim.transform.GetChild(1).gameObject.SetActive(false);
        this.CanvasGroup.DOFade(1f, 1f).SetEase(Ease.Linear).OnComplete(() => 
        {
            EventDispatcher.Instance.Dispatch(EventDispatcher.EventName.DungeonMainPlayerToSanctuary);
        });
    }
}