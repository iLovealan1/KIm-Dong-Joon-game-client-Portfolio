using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainCameraController : MonoBehaviour
{
    [SerializeField] private float shakeDuration = 0.2f;
    [SerializeField] private float shakeMagnitude = 0.7f;
    [SerializeField] private float fadeDuration = 1f;

    [SerializeField] private List<GameObject> redEffects;
    private List<Vector3> camBounds;
    private List<SpriteRenderer> effectSprites;
    private Color defaultColor = Color.white;

    private bool isShaking;
    public bool isPlayerTeleporting;

    public GameObject player;
    private Vector3 cameraOffset = new Vector3(0, 0, -11);

    private DungeonSceneMain dungeonMain;

    [SerializeField] private RectTransform UIBtnInvenRect;
    [SerializeField] private Transform invenPoint;

    [SerializeField] private RectTransform UIRelicPoint1;
    [SerializeField] private Transform RelicPoint1;

    [SerializeField] private RectTransform UIRelicPoint2;
    [SerializeField] private Transform RelicPoint2;

    [SerializeField] private RectTransform UIRelicPoint3;
    [SerializeField] private Transform RelicPoint3;

    public void Init()
    {
        this.player = GameObject.FindGameObjectWithTag("Player");
        this.camBounds = new List<Vector3>();
        this.effectSprites = new List<SpriteRenderer>();
        this.dungeonMain = GameObject.FindObjectOfType<DungeonSceneMain>();

        var SceneName = SceneManager.GetActiveScene().name;
        this.InitUIPoints(SceneName);
        Debug.Log(SceneName);

        for (int i = 0; i < 4; i++)
        {
            var comp = this.redEffects[i].GetComponent<SpriteRenderer>();
            this.effectSprites.Add(comp);
        }
        EventDispatcher.Instance.AddListener(EventDispatcher.EventName.MainCameraControllerHitEffects, this.HitEffects);
    }

    private void InitUIPoints(string SceneName)
    {
        if (SceneName == "SanctuaryScene")
        {
            this.invenPoint.position = Camera.main.ScreenToWorldPoint(this.UIBtnInvenRect.position);
        }
        else if (SceneName == "DungeonScene")
        {
            this.invenPoint.position = Camera.main.ScreenToWorldPoint(this.UIBtnInvenRect.position);
            this.RelicPoint1.position = Camera.main.ScreenToWorldPoint(this.UIRelicPoint1.position);
            this.RelicPoint2.position = Camera.main.ScreenToWorldPoint(this.UIRelicPoint2.position);
            this.RelicPoint3.position = Camera.main.ScreenToWorldPoint(this.UIRelicPoint3.position);
        }
    }

    private Vector3 GetCameraBoundsCenter(Vector3 viewportPoint)
    {
        Vector3 screenPoint = new Vector3(viewportPoint.x * Screen.width, viewportPoint.y
            * Screen.height, Camera.main.nearClipPlane);
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(screenPoint);
        worldPoint.z = -10f;
        return worldPoint;
    }

    private void CamBoundsInit()
    {
        this.camBounds.Clear();
        this.camBounds.Add(GetCameraBoundsCenter(new Vector3(0.5f, 1f)));
        this.camBounds.Add(GetCameraBoundsCenter(new Vector3(1f, 0.5f)));
        this.camBounds.Add(GetCameraBoundsCenter(new Vector3(0.5f, 0f)));
        this.camBounds.Add(GetCameraBoundsCenter(new Vector3(0f, 0.5f)));
    }

    private void HitEffects()
    {
        this.isShaking = true; 
        this.CamBoundsInit();

        for (int i = 0; i < 4; i++)
        {
            this.redEffects[i].SetActive(true);
            this.redEffects[i].transform.DOKill();
            this.effectSprites[i].DOKill();
            this.transform.DOKill();
            this.redEffects[i].transform.position = this.camBounds[i];
            this.effectSprites[i].color = this.defaultColor;
        }
        this.StartCoroutine(this.CoHitEffects());
    }

    private IEnumerator CoHitEffects()
    {
        this.transform.DOShakePosition(0.2f, new Vector3(1f, -1f, 0f), 20, 90f, false, true)
                 .SetEase(Ease.InFlash).OnComplete(() =>
                 {
                     this.isShaking = false; 
                 });
        for (int i = 0; i < 4; i++)
        {
            int index = i;
            this.redEffects[i].transform
                .DOShakePosition(this.shakeDuration, new Vector3(2f, -2f, 0f), 20, 90f, false, true)
                .SetEase(Ease.OutCubic).OnComplete(() =>
                {
                    this.effectSprites[index].DOFade(0, this.fadeDuration).OnComplete(() =>
                    {
                        this.redEffects[index].SetActive(false);
                    });
                });
        }
        yield return null;
    }

    private void LateUpdate()
    {
        if (!this.isShaking && !this.isPlayerTeleporting && this.player != null)
        {
            Vector3 playerPos = this.player.transform.position;
            this.transform.position = playerPos + this.cameraOffset;
        }

        if (this.dungeonMain != null && this.dungeonMain.playerHP < 2)
        {
            this.redEffects.ForEach(i => { i.SetActive(true); });
            this.effectSprites.ForEach(i => { i.color = Color.white; });
        }
        else if (this.dungeonMain != null && !this.isShaking)
        {
            this.redEffects.ForEach(i => { i.SetActive(false); });
            this.effectSprites.ForEach(i => { i.color = this.defaultColor; });
        }
    }
}