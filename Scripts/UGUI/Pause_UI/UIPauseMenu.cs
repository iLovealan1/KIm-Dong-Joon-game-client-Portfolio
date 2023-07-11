using UnityEngine;
using UnityEngine.UI;
public class UIPauseMenu : MonoBehaviour
{
    public Button BtnPauseMenuResume;
    public Button BtnPauseMenuSetting;
    public Button BtnPauseMenuGiveup;

    public UISettingPopup uiSettingPopup;
    public UIGIveupPopup uiGIveupPopup;
    [SerializeField] UICreditPopup creditPopup; 

    private UIPauseDirector uiPauseDirector; 

    public System.Action onResume;

    public void Init()
    {
        this.uiPauseDirector = this.GetComponentInParent<UIPauseDirector>();
        this.creditPopup.Init();
        this.uiSettingPopup.Init();
        this.uiGIveupPopup.Init();

        this.BtnPauseMenuResume.onClick.AddListener(() =>
        {
            this.onResume();
        });

        this.BtnPauseMenuSetting.onClick.AddListener(() =>
        {
            var source = this.GetComponentInParent<AudioSource>();
            AudioManager.instance.PlaySFXOneShot(AudioManager.eSFXMusicPlayList.UI_Open, source);
            this.uiSettingPopup.gameObject.SetActive(true);
            this.uiPauseDirector.onPushPause(this.uiSettingPopup.gameObject);
        });

        this.uiSettingPopup.onCreditPopupOn = () =>
        {
            this.creditPopup.gameObject.SetActive(true);
            this.uiPauseDirector.onPushPause(this.creditPopup.gameObject);
            this.creditPopup.StartAnim();
        };

        this.uiSettingPopup.onCreditPopupOff = () =>
        {
            this.uiPauseDirector.onPushPause(null);
        };

        if (this.BtnPauseMenuGiveup != null)
        {
            this.BtnPauseMenuGiveup.onClick.AddListener(() =>
            {
                var source = this.GetComponentInParent<AudioSource>();
                AudioManager.instance.PlaySFXOneShot(AudioManager.eSFXMusicPlayList.UI_Close, source);
                this.uiGIveupPopup.gameObject.SetActive(true);
                this.uiPauseDirector.onPushPause(this.uiGIveupPopup.gameObject);
            });
        }
        this.gameObject.SetActive(false);
    }
}
