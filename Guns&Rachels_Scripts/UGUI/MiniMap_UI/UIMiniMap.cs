using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIMiniMap : MonoBehaviour
{
    public Button btnMapReGen;
    public Text txtMapCount;
    public Button btnMaxRowPlus;
    public Button btnMaxRowMinus;
    public Button btnMaxColPlus;
    public Button btnMaxColMinus;
    public Text txtRowCol;

    public Camera miniMapCam;
    public GameObject imgCurrentMap;

    [SerializeField]
    private Minimap minimap;
    private RectTransform minimapRect;
    private CanvasGroup minimapCanvas;
    private Image minimapImage;
    private Vector2 minimapPos;

    private bool isExpend;

    public void Init()
    {
        this.imgCurrentMap = Instantiate(this.imgCurrentMap);
        this.StartCoroutine(this.mapFlickerling());

        this.minimapRect = this.minimap.GetComponent<RectTransform>();
        this.minimapCanvas = this.minimap.GetComponent<CanvasGroup>();
        this.minimapImage = this.minimap.GetComponent<Image>();
        this.minimapPos = this.minimapRect.position;

        this.minimap.onTouched = () =>
        {
            this.DoExpend();
        };

        this.btnMapReGen.gameObject.SetActive(false);
        this.btnMaxRowPlus.gameObject.SetActive(false);
        this.btnMaxRowMinus.gameObject.SetActive(false);
        this.btnMaxColPlus.gameObject.SetActive(false);
        this.btnMaxColMinus.gameObject.SetActive(false);
        this.txtMapCount.gameObject.SetActive(false);
        this.txtRowCol.gameObject.SetActive(false);
    }

    public void MiniMapUpdate(Vector3 roomPos)
    {
        //Change Minimap Cam's pos
        this.miniMapCam.transform.position = new Vector3(roomPos.x, roomPos.y, -800f);
        this.imgCurrentMap.transform.position = new Vector3(roomPos.x, roomPos.y, -22f);
        this.lastRoomPos = roomPos;
    }

    private Vector3 lastRoomPos;

    private void DoExpend()
    {
        if (!this.isExpend)
        {
            this.isExpend = true;
            this.lastRoomPos = this.miniMapCam.transform.position;
            this.minimapImage.raycastTarget = false;
            this.minimapCanvas.DOFade(0, 0.1f)
            .OnComplete(() =>
            {
                this.minimapRect.localScale = Vector3.one * 3.3f;
                this.minimapRect.anchorMin = new Vector2(0.5f, 0.5f);
                this.minimapRect.anchorMax = new Vector2(0.5f, 0.5f);
                this.minimapRect.pivot = new Vector2(0.5f, 0.5f);   
                this.minimapRect.localPosition = Vector2.zero;
                this.miniMapCam.orthographicSize = 188f;
                this.minimapCanvas.DOFade(1, 0.1f)
                .OnComplete(() =>
                {
                    this.minimapImage.raycastTarget = true;
                });
            });
        }
        else
        {
            this.isExpend = false;
            this.minimapImage.raycastTarget = false;
            this.minimapCanvas.DOFade(0, 0.1f)
            .OnComplete(() =>
            {
                this.minimapRect.anchorMin = new Vector2(1f, 1f);
                this.minimapRect.anchorMax = new Vector2(1f, 1f);
                this.minimapRect.pivot = new Vector2(1f, 1f);
                this.minimapRect.position = this.minimapPos;
                this.minimapRect.localScale = Vector3.one;
                this.miniMapCam.orthographicSize = 75f;
                this.minimapCanvas.DOFade(1, 0.1f)
                .OnComplete(() =>
                {
                    this.minimapImage.raycastTarget = true;
                    var pos = GameObject.FindGameObjectWithTag("Player").transform.position;
                    this.MiniMapUpdate(lastRoomPos);
                });
            });
        }
    }

    private IEnumerator mapFlickerling()
    {
        while (true)
        {
            this.imgCurrentMap.SetActive(!this.imgCurrentMap.activeSelf);
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void OnDisable()
    {
        this.minimapCanvas.DOKill();
    }
}
