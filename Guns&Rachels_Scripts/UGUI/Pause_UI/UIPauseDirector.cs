using UnityEngine;
using UnityEngine.UI;

public class UIPauseDirector : MonoBehaviour
{
    public UIPauseMenu uiPauseMenu;
    public Button BtnPause;

    public System.Action onToSanctuary;
    public System.Action<GameObject> onPushPause;

    public AudioSource audioSource;

    public void Init()
    {
        this.audioSource = this.GetComponent<AudioSource>();
        this.uiPauseMenu.onResume = () =>
        {
            AudioManager.instance.PlaySFXOneShot(AudioManager.eSFXMusicPlayList.UI_Close, this.audioSource);
            this.onPushPause(null);
            this.ResumeGame();
        };
        this.onToSanctuary = () =>
        {
            this.ResumeGame();
            EventDispatcher.Instance.Dispatch(EventDispatcher.EventName.DungeonMainPlayerToSanctuary);
        };

        this.BtnPause.onClick.AddListener(() =>
        {
            AudioManager.instance.PlaySFXOneShot(AudioManager.eSFXMusicPlayList.UI_Open, this.audioSource);
            this.ActivePauseUI();
            this.onPushPause(this.uiPauseMenu.gameObject);
        });

        this.uiPauseMenu.Init();

    }
    private void PauseGame()
    {
        Time.timeScale = 0f;
    }

    private void ResumeGame()
    {
        Time.timeScale = 1f;
    }

    public void ActivePauseUI()
    {
        bool isMenuActive = this.uiPauseMenu.gameObject.activeSelf;
        this.uiPauseMenu.gameObject.SetActive(!isMenuActive);
        if (isMenuActive)
            this.ResumeGame();
        else
            this.PauseGame();
    }
}
