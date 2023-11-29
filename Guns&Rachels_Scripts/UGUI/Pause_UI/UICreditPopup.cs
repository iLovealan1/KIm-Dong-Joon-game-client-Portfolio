using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UICreditPopup : MonoBehaviour
{
    [SerializeField] GameObject logoAndName;
    [SerializeField] private Text txtDeveloperName;
    [SerializeField] private Image imgTeamLogo;
    [SerializeField] private Text[] txtNames;

    public void Init()
    {
        this.logoAndName.transform.GetComponent<RectTransform>().localPosition = new Vector3(0, 40, 0);
        this.txtDeveloperName.color = new Color(1, 1, 1, 0);
        this.imgTeamLogo.color = new Color(1, 1, 1, 0);

        for (int i = 0; i < this.txtNames.Length; i++)
        {
            this.txtNames[i].color = new Color(1, 1, 1, 0);
        }
        this.gameObject.SetActive(false);
    }

    //Start Anim
    public void StartAnim()
    {
        //Debug.Log("StartAnim");
        this.StartCoroutine(this.CoCreditAnimStart());
    }

    private IEnumerator CoCreditAnimStart()
    {

        yield return this.txtDeveloperName.DOFade(1f, 1f).SetEase(Ease.InOutBack).SetUpdate(true).WaitForCompletion();

        yield return this.imgTeamLogo.DOFade(1f, 1f).SetEase(Ease.InOutBack).SetUpdate(true).WaitForCompletion();

        yield return this.logoAndName.transform.GetComponent<RectTransform>().DOMoveY(this.logoAndName.transform.GetComponent<RectTransform>().position.y + 120f, 2f)
            .SetEase(Ease.InOutQuad).SetUpdate(true).WaitForCompletion();

        foreach (Text txtName in this.txtNames)
        {
            yield return txtName.DOFade(1f, 1f).SetUpdate(true).WaitForCompletion();
        }
    }

    // StopAnim when user Quit
    public void StopAnim()
    {
        this.logoAndName.transform.GetComponent<RectTransform>().DOKill(); 
        this.txtDeveloperName.DOKill(); 
        this.imgTeamLogo.DOKill(); 
        foreach (Text txtName in this.txtNames)
        {
            txtName.DOKill(); 
        }
       
        this.StopAllCoroutines();
        this.Init();
    }

    private void OnDisable()
    {
        this.StopAnim();
    }
}
