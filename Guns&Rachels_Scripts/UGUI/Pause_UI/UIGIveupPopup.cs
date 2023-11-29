using UnityEngine;
using UnityEngine.UI;

public class UIGIveupPopup : MonoBehaviour
{
    public Button GIveupPopupDim;
    public Button btnSelect;
    public Button btnCancel;

    public UIPauseDirector director;

    public void Init()
    {
        this.GIveupPopupDim.onClick.AddListener(() =>
        {
            var source = this.GetComponentInParent<AudioSource>();
            AudioManager.instance.PlaySFXOneShot(AudioManager.eSFXMusicPlayList.UI_Close, source);
            this.director.onPushPause(null);
        });

        this.btnSelect.onClick.AddListener(() =>
        {
            this.director.onToSanctuary();
        });

        this.btnCancel.onClick.AddListener(() =>
        {
            var source = this.GetComponentInParent<AudioSource>();
            AudioManager.instance.PlaySFXOneShot(AudioManager.eSFXMusicPlayList.UI_Close, source);
            this.director.onPushPause(null);
        });

        this.gameObject.SetActive(false);
    }
}
