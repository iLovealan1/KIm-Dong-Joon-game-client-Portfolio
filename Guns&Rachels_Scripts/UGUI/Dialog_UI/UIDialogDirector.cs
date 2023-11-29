using UnityEngine;

public class UIDialogDirector : MonoBehaviour
{
    public UIDialogPanel dialogPanel;

    public AudioSource audioSource;

    public void Init()
    {
        this.audioSource = this.GetComponent<AudioSource>();
        this.dialogPanel.Init();
    }
}
