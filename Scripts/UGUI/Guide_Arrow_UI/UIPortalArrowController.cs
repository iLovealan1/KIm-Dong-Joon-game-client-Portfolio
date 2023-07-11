using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPortalArrowController : MonoBehaviour
{
    [SerializeField] private List<GameObject> arrowArr; // Array of UI arrow images according to portal directions

    private GameObject[] portalArr; // Array of portal objects in the north, south, east, and west directions

    private Camera mainCamera;

    private bool isActive;

    private DungeonSceneMain main;

    public void Init()
    {
        this.mainCamera = Camera.main;
        this.isActive = false;
        this.main = GameObject.FindObjectOfType<DungeonSceneMain>();

        EventDispatcher.Instance.AddListener<Transform>(EventDispatcher.EventName.UIPortalArrowControllerInitializingArrows,
            this.InitializingArrows);
        EventDispatcher.Instance.AddListener(EventDispatcher.EventName.UIPortalArrowControllerStopAllArrowCorutines,
            this.StopAllArrowCorutines);

        this.gameObject.SetActive(false);
    }

    private void InitializingArrows(Transform roomTrans)
    {
        this.gameObject.SetActive(true);
        this.arrowArr.ForEach((x) => x.SetActive(false));
        GameObject[] temp = { null, null, null, null };
        this.isActive = true;
        for (int i = 0; i < temp.Length; i++)
        {
            if (roomTrans.GetChild(i).childCount == 1)
            {
                temp[i] = roomTrans.GetChild(i).gameObject;
            }
            else
            {
                temp[i] = null;
            }
        }

        this.portalArr = temp;
        for (int i = 0; i < this.portalArr.Length; i++)
        {
            if (this.portalArr[i] != null)
            {
                this.arrowArr[i].SetActive(this.RoomCheck(i, roomTrans));
                if (this.arrowArr[i].activeSelf)
                {
                    this.ChageArrowColor(this.portalArr[i], this.arrowArr[i]);
                    this.StartCoroutine(this.CoFollowPortals(this.portalArr[i], this.arrowArr[i]));
                }
            }
            else
            {
                this.arrowArr[i].SetActive(false);
            }
        }
    }
    private bool RoomCheck(int portalNum, Transform currentRoom)
    {
        var list = this.main.generator.wholeRoomList;
        var dist = this.main.generator.mapDist;
        var currentRoomPos = currentRoom.transform.position;
        var upperRoom = default(GameObject);
        var isNewRoom = true;

        if (portalNum == 0)
        {
            var upperRoomPos = new Vector3(currentRoomPos.x, currentRoomPos.y + dist, currentRoomPos.z);
            upperRoom = list.Find((x) => x.transform.position == upperRoomPos);
        }
        else if (portalNum == 1)
        {
            var rightRoomPos = new Vector3(currentRoomPos.x + dist, currentRoomPos.y, currentRoomPos.z);
            upperRoom = list.Find((x) => x.transform.position == rightRoomPos);
        }
        else if (portalNum == 2)
        {
            var downRoomPos = new Vector3(currentRoomPos.x, currentRoomPos.y - dist, currentRoomPos.z);
            upperRoom = list.Find((x) => x.transform.position == downRoomPos);
        }
        else if (portalNum == 3)
        {
            var leftRoomPos = new Vector3(currentRoomPos.x - dist, currentRoomPos.y, currentRoomPos.z);
            upperRoom = list.Find((x) => x.transform.position == leftRoomPos);
        }
        if (upperRoom == null) return isNewRoom;
        if (upperRoom.gameObject.activeSelf) isNewRoom = false;
        return isNewRoom;
    }

    private void ChageArrowColor(GameObject portal, GameObject arrow)
    {
        if (portal.transform.GetChild(0).gameObject.name.Contains("Safe"))
        {
            arrow.GetComponent<Image>().color = Color.green;
        }
        else if (portal.transform.GetChild(0).gameObject.name.Contains("Boss"))
        {
            arrow.GetComponent<Image>().color = Color.red;
        }
        else if (portal.transform.GetChild(0).gameObject.name.Contains("Hidden"))
        {
            arrow.GetComponent<Image>().color = Color.yellow;
        }
        else
        {
            arrow.GetComponent<Image>().color = Color.white;
        }
    }

    private IEnumerator CoFollowPortals(GameObject portal, GameObject arrow)
    {
        arrow.transform.DOScale(new Vector3(1f, 1.3f, 1.7f) * 2.5f, 1f)
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
        {
            if (arrow != null)
                arrow.transform.DOScale(new Vector3(1f, 1.3f, 1.7f), 1f).SetEase(Ease.OutQuad);
        });


        while (this.isActive)
        {
            var portalPos = this.mainCamera.WorldToScreenPoint(portal.transform.position);
            if (!this.IsInCameraView(portal.transform.position))
            {
                arrow.gameObject.SetActive(true);
                if (portal.name.Contains("0") || portal.name.Contains("2"))
                {
                    this.ChangeArrowPosition(portalPos, arrow, false);
                    this.ChangeArrowRotation(portalPos, arrow);
                }
                else
                {
                    this.ChangeArrowPosition(portalPos, arrow, true);
                    this.ChangeArrowRotation(portalPos, arrow);
                }
            }
            else
            {
                arrow.gameObject.SetActive(false);
            }
            yield return null;
        }
    }

    private bool IsInCameraView(Vector3 position)
    {
        Vector3 screenPoint = this.mainCamera.WorldToViewportPoint(position);
        return screenPoint.x >= 0f && screenPoint.x <= 1f && screenPoint.y >= 0f && screenPoint.y <= 1f;
    }

    private void ChangeArrowPosition(Vector3 portalPos, GameObject arrow, bool vertical)
    {
        RectTransform arrowRectTransform = arrow.GetComponent<RectTransform>();
        if (!vertical)
        {
            if (portalPos.x < 80)
            {
                portalPos.x = 80;
            }
            else if (portalPos.x > Screen.width - 80)
            {
                portalPos.x = Screen.width - 80;
            }
            arrow.transform.position = new Vector2(portalPos.x, arrow.transform.position.y);
        }
        else
        {
            if (portalPos.y < 80)
            {
                portalPos.y = 80;
            }
            else if (portalPos.y > Screen.height - 80)
            {
                portalPos.y = Screen.height - 80;
            }
            arrow.transform.position = new Vector2(arrow.transform.position.x, portalPos.y);
        }
    }

    private void ChangeArrowRotation(Vector3 portalPos, GameObject arrow)
    {
        Vector3 direction = portalPos - arrow.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        arrow.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // Set the arrow's pointed direction based on the portal direction
        Vector3 localScale = arrow.transform.localScale;
        if (direction.x >= 0)
        {
            localScale.x = Mathf.Abs(localScale.x);
            arrow.transform.localScale = localScale;
        }
        else
        {
            localScale.x = -Mathf.Abs(localScale.x);
            arrow.transform.localScale = localScale;
            arrow.transform.rotation = Quaternion.AngleAxis(angle - 180f, Vector3.forward);
        }
    }

    private void StopAllArrowCorutines()
    {
        this.StopAllCoroutines();
        this.portalArr = null;
        this.isActive = false;
        this.arrowArr.ForEach((x) => x.transform.DOKill());
        this.arrowArr.ForEach((x) => x.transform.localScale = new Vector3(0.4f, 0.51f, 0.7f));
        this.arrowArr.ForEach((x) => x.SetActive(false));
        this.gameObject.SetActive(false);
    }
}