using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIGameOverDirector : MonoBehaviour
{
    [SerializeField] private Text txtGameOver;
    [SerializeField] private Button btnBackToSactuary;
    [SerializeField] private Text txtBackToSactuary;

    public void Init()
    {
        this.btnBackToSactuary.onClick.AddListener(() =>
        {
            EventDispatcher.Instance.Dispatch(EventDispatcher.EventName.DungeonMainPlayerToSanctuary);
        });

        this.btnBackToSactuary.gameObject.SetActive(false);
        this.gameObject.SetActive(false);

        EventDispatcher.Instance.AddListener(EventDispatcher.EventName.UIGameOverPopUp, this.GameOverAnimStart);
    }

    public void GameOverAnimStart()
    {
        EventDispatcher.Instance.Dispatch(EventDispatcher.EventName.UIDungeonDirectorUISetOff);
        this.gameObject.SetActive(true);
        this.StartCoroutine(this.CoGameOverTxtAnimON());
    }

    private IEnumerator CoGameOverTxtAnimON()
    {
        float alpha = txtGameOver.color.a;
        float duration = 1.5f;

        while (alpha < 1f)
        {
            alpha += Time.deltaTime / duration;
            txtGameOver.color = new Color(txtGameOver.color.r, txtGameOver.color.g, txtGameOver.color.b, alpha);
            yield return null;
        }

        this.StartCoroutine(this.CoGameOverBtnON());
    }

    private IEnumerator CoGameOverBtnON()
    {
        this.btnBackToSactuary.gameObject.SetActive(true);
        var btnImageComp = this.btnBackToSactuary.GetComponent<Image>();
        float alpha = btnImageComp.color.a;
        float alpha2 = this.txtBackToSactuary.color.a;
        float duration = 1f;

        while (alpha < 1f || alpha2 < 1f)
        {
            alpha += Time.deltaTime / duration;
            alpha2 += Time.deltaTime / duration;
            this.txtBackToSactuary.color = new Color(this.txtBackToSactuary.color.r,
                this.txtBackToSactuary.color.g, this.txtBackToSactuary.color.b, alpha);
            btnImageComp.color = new Color(this.txtGameOver.color.r, this.txtGameOver.color.g,
                this.txtGameOver.color.b, alpha);
            yield return null;
        }
    }
}
