using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using luna_sportshop.Playable002;

public class VJHandler : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    [Header("=====Joystick SerializeField=====")]
    [Space]
    [SerializeField] private Image _jsContainer;
    [SerializeField] private Image _joystick;

    //===============================================================
    //Properties
    //===============================================================
    public Vector3 InputDirection {get; private set;}
    public IPlayerMoveHandler PlayerMoveHandler { set { if (_playerMoveHandler == null) _playerMoveHandler = value;} }
    public event Action OnTouched{ add { _onTouched += value; } remove { _onTouched -= value; } }
    public event Action<EGuideArrowState> OnGameStart { add { _onGameStart += value; } remove { _onGameStart -= value; } }

    //===============================================================
    //Fields
    //===============================================================
    private IPlayerMoveHandler _playerMoveHandler = null;
    private Action _onTouched = null;
    private Action<EGuideArrowState> _onGameStart = null;
    private RectTransform _baseRect = null;
    private Canvas _canvas;
    private Camera _cam = null;
    float moveThreshold = 10f;

    //===============================================================
    //Functions
    //===============================================================
    private void Start()
    {
        InputDirection = Vector3.zero;
        _baseRect = GetComponent<RectTransform>();
        _canvas = GetComponentInParent<Canvas>();
        _jsContainer.gameObject.SetActive(false);
    }

    public void OnDrag(PointerEventData ped)
    {
        if (_cam = null)
        {
            if (_canvas.renderMode == RenderMode.ScreenSpaceCamera)
                _cam = _canvas.worldCamera;
        }
        
        Vector2 position = Vector2.zero;

        ScreenPointToLocalPointInRectangle
                (_jsContainer.rectTransform,
                ped.position,
                ped.pressEventCamera,
                out position);

        position.x = (position.x / _jsContainer.rectTransform.sizeDelta.x);
        position.y = (position.y / _jsContainer.rectTransform.sizeDelta.y);

        float x = (_jsContainer.rectTransform.pivot.x == 1f) ? position.x : position.x;
        float y = (_jsContainer.rectTransform.pivot.y == 1f) ? position.y : position.y;

        InputDirection = new Vector3(x, y, 0);
        InputDirection = (InputDirection.magnitude > 1) ? InputDirection.normalized : InputDirection;

        _joystick.rectTransform.anchoredPosition = new Vector3(InputDirection.x * (_jsContainer.rectTransform.sizeDelta.x / 3)
                                                               , InputDirection.y * (_jsContainer.rectTransform.sizeDelta.y) / 3);
        if (Vector2.Distance(ped.position, ped.pressPosition) > moveThreshold)
        {
            _playerMoveHandler.MovePlayer(InputDirection.y, InputDirection.x);                                           
        }
    }

    public void OnPointerDown(PointerEventData ped)
    {
        _onTouched?.Invoke();      

        if (_onGameStart != null)
        {
            _onGameStart.Invoke(EGuideArrowState.Counter_Upgrade);
            _onGameStart = null;
        }

        _jsContainer.rectTransform.anchoredPosition = ScreenPointToAnchoredPosition(ped.position);
        _jsContainer.gameObject.SetActive(true);
        OnDrag(ped);
    }

    public void OnPointerUp(PointerEventData ped)
    {
        InputDirection = Vector3.zero;
        _joystick.rectTransform.anchoredPosition = Vector3.zero;
        _jsContainer.gameObject.SetActive(false);
        _playerMoveHandler.MovePlayer(InputDirection.y, InputDirection.x);
    }

    protected Vector2 ScreenPointToAnchoredPosition(Vector2 screenPosition)
    {
        Vector2 localPoint = Vector2.zero;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_baseRect, screenPosition, _cam, out localPoint))
        {
            Vector2 pivotOffset = _baseRect.pivot * _baseRect.sizeDelta;
            return localPoint - (_jsContainer.rectTransform.anchorMax * _baseRect.sizeDelta) + pivotOffset;
        }
        return Vector2.zero;
    }

    private static bool ScreenPointToLocalPointInRectangle(RectTransform rect, Vector2 screenPoint, Camera cam, out Vector2 localPoint)
    {
        localPoint = Vector2.zero;
        if (ScreenPointToWorldPointInRectanglee(rect, screenPoint, cam, out var worldPoint))
        {
            localPoint = rect.InverseTransformPoint(worldPoint);
            return true;
        }

        return false;
    }

    private static bool ScreenPointToWorldPointInRectanglee(RectTransform rect, Vector2 screenPoint, Camera cam, out Vector3 worldPoint)
    {
        worldPoint = Vector2.zero;
        Ray ray = ScreenPointToRay(cam, screenPoint);
        Plane plane = new Plane(rect.rotation * Vector3.back, rect.position);
        float enter = 0f;
        float num = Vector3.Dot(Vector3.Normalize(rect.position - ray.origin), plane.normal);
        if (num != 0f && !plane.Raycast(ray, out enter))
        {
            return false;
        }

        worldPoint = ray.GetPoint(enter);
        return true;
    }

    private static Ray ScreenPointToRay(Camera cam, Vector2 screenPos)
    {
        if (cam != null)
        {
            return cam.ScreenPointToRay(screenPos);
        }

        Vector3 origin = screenPos;
        origin.z -= 100f;
        return new Ray(origin, Vector3.forward);
    }
}