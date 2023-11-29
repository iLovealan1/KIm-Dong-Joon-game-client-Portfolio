using DigitalRuby.SoundManagerNamespace;
using System;
using UnityEngine;
using UnityEngine.UI;

public class UISettingPopup : MonoBehaviour
{
    public Button SettingPopupDim; 
    public Slider MusicVolumeSlider; 
    public Slider SFXVolumeSlider; 
    public Toggle vibrationToggle; 
    public Button BtnAnnouncement; 
    public Button BtnHelp; 
    public Text txtVersion;
    public Button btnSettingPopupClose;

    [SerializeField] Button BtnCredit; 
    [SerializeField] Button btnCreditPopupDim; 

    public Action onCreditPopupOn;
    public Action onCreditPopupOff;

    private UIPauseDirector uiPauseDirector;

    public void Init()
    {
        this.uiPauseDirector = this.GetComponentInParent<UIPauseDirector>();    

        this.MusicVolumeSlider.onValueChanged.AddListener((x) =>
        {
            this.OnBackgroundMusicSliderValueChanged(x);
        });

        this.SFXVolumeSlider.onValueChanged.AddListener((x) =>
        {
            this.OnSoundEffectSliderValueChanged(x);
        });

        this.vibrationToggle.onValueChanged.AddListener((x) =>
        {
            this.OnVibrationToggleValueChanged(x);
        });

        this.BtnAnnouncement.onClick.AddListener(() =>
        {
            var source = this.GetComponentInParent<AudioSource>();
            AudioManager.instance.PlaySFXOneShot(AudioManager.eSFXMusicPlayList.UI_Click, source);
            Application.OpenURL("https://gunsnrachels.blogspot.com/2023/04/blog-post_22.html");
        });

        this.BtnHelp.onClick.AddListener(() =>
        {
            var source = this.GetComponentInParent<AudioSource>();
            AudioManager.instance.PlaySFXOneShot(AudioManager.eSFXMusicPlayList.UI_Click, source);
            GameObject.FindObjectOfType<UITutorialDirector>().Show();
            //Application.OpenURL("https://gunsnrachels.blogspot.com/2023/04/blog-post.html");
        });

        this.BtnCredit.onClick.AddListener(() =>
        {
            var source = this.GetComponentInParent<AudioSource>();
            AudioManager.instance.PlaySFXOneShot(AudioManager.eSFXMusicPlayList.UI_Open, source);
            this.onCreditPopupOn();
        });

        this.btnCreditPopupDim.onClick.AddListener(() =>
        {
            var source = this.GetComponentInParent<AudioSource>();
            AudioManager.instance.PlaySFXOneShot(AudioManager.eSFXMusicPlayList.UI_Close, source);
            this.onCreditPopupOff();
        });

        this.btnSettingPopupClose.onClick.AddListener(() =>
        {
            var source = this.GetComponentInParent<AudioSource>();
            AudioManager.instance.PlaySFXOneShot(AudioManager.eSFXMusicPlayList.UI_Click, source);
            this.uiPauseDirector.onPushPause(null);
        });

        this.SettingPopupDim.onClick.AddListener(() =>
        {
            var source = this.GetComponentInParent<AudioSource>();
            AudioManager.instance.PlaySFXOneShot(AudioManager.eSFXMusicPlayList.UI_Close, source);
            this.uiPauseDirector.onPushPause(null);
        });


        this.MusicVolumeSlider.value = InfoManager.instance.settingInfo.musicVolume;
        this.SFXVolumeSlider.value = InfoManager.instance.settingInfo.sfxVolume;
        this.vibrationToggle.isOn = InfoManager.instance.settingInfo.isVivb;

        //ver info : Major.Minor.Patch 
        this.txtVersion.text = string.Format("Ver.{0}", Application.version);

        this.gameObject.SetActive(false);
    }

    // Callback method called when the background music volume slider value is changed
    public void OnBackgroundMusicSliderValueChanged(float value)
    {
        SoundManager.MusicVolume = value;
        InfoManager.instance.settingInfo.musicVolume = value;
    }

    // Callback method called when the sound effect volume slider value is changed
    public void OnSoundEffectSliderValueChanged(float value)
    {
        SoundManager.SoundVolume = value;
        InfoManager.instance.settingInfo.sfxVolume = value;
    }

    // Callback method called when the vibration toggle value is changed
    public void OnVibrationToggleValueChanged(bool value)
    {
        InfoManager.instance.settingInfo.isVivb = value;
        AudioManager.instance.Vibrate();
    }

    private void OnDisable()
    {
        InfoManager.instance.SaveSettingInfo();
    }
}
