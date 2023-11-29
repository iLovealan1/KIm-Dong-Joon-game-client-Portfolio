using System.Collections;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    public enum ePortaltype
    {
        SANCTUARY,
        DUNGEON_STAGE1,
        DUNGEON_STAGE2,
        DUNGEON_STAGE3,
        DUNGEON_STAGE4,
        ROOM
    }
    [SerializeField] private ePortaltype portaltype;
    public System.Action onPlayerGoToThePortal;
    private void Start()
    {
        if (!(this.portaltype == ePortaltype.ROOM || this.portaltype == ePortaltype.SANCTUARY))
            this.StartCoroutine(EnableColliderAfterDelay());
    }

    private IEnumerator EnableColliderAfterDelay()
    {
        yield return new WaitForSeconds(0.5f);
        if (this.gameObject != null)
        {
            BoxCollider2D collider = GetComponent<BoxCollider2D>();
            if (collider != null)
            {
                collider.enabled = true;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (this.portaltype == ePortaltype.SANCTUARY)
            {
                this.GetComponent<BoxCollider2D>().enabled = false;
                this.onPlayerGoToThePortal();
            }
            else if (this.portaltype == ePortaltype.DUNGEON_STAGE1)
            {
                Debug.LogWarning("<color=red>1 Stage End</color>");
                //GPGSManager.instance.ReportAchievement("CgkI1MrQrKgMEAIQAg", 100);
                EventDispatcher.Instance.Dispatch(EventDispatcher.EventName.UIPortalArrowControllerStopAllArrowCorutines);
                this.GetComponent<BoxCollider2D>().enabled = false;
                InfoManager.instance.ChangeDungeonStepInfo(InfoManager.eNextStageType.STAGE2);
                EventDispatcher.Instance.Dispatch(EventDispatcher.EventName.UIDungeonLoadingDirectorStageLoading);
            }
            else if (this.portaltype == ePortaltype.DUNGEON_STAGE2)
            {
                Debug.LogWarning("<color=blue>2 Stage End</color>");
                //GPGSManager.instance.ReportAchievement("CgkI1MrQrKgMEAIQAw", 100);
                EventDispatcher.Instance.Dispatch(EventDispatcher.EventName.UIPortalArrowControllerStopAllArrowCorutines);
                this.GetComponent<BoxCollider2D>().enabled = false;
                InfoManager.instance.ChangeDungeonStepInfo(InfoManager.eNextStageType.STAGE3);
                EventDispatcher.Instance.Dispatch(EventDispatcher.EventName.UIDungeonLoadingDirectorStageLoading);
            }
            else if (this.portaltype == ePortaltype.DUNGEON_STAGE3)
            {
                Debug.LogWarning("<color=white>3 Stage End</color>");
                //GPGSManager.instance.ReportAchievement("CgkI1MrQrKgMEAIQBA", 100);
                EventDispatcher.Instance.Dispatch(EventDispatcher.EventName.UIPortalArrowControllerStopAllArrowCorutines);
                this.GetComponent<BoxCollider2D>().enabled = false;
                InfoManager.instance.ChangeDungeonStepInfo(InfoManager.eNextStageType.STAGE4);
                EventDispatcher.Instance.Dispatch(EventDispatcher.EventName.UIDungeonLoadingDirectorStageLoading);
            }
            else if (this.portaltype == ePortaltype.DUNGEON_STAGE4)
            {
                Debug.LogWarning("<color=cyan>4 Stage End</color>");
                //GPGSManager.instance.ReportAchievement("CgkI1MrQrKgMEAIQBQ", 100);
                EventDispatcher.Instance.Dispatch(EventDispatcher.EventName.UIPortalArrowControllerStopAllArrowCorutines);
                this.GetComponent<BoxCollider2D>().enabled = false;
                InfoManager.instance.dungeonInfo.isClearStage4 = true;
                InfoManager.instance.gameInfo.roundCnt++;
                InfoManager.instance.SaveGameInfo();
                EventDispatcher.Instance.Dispatch(EventDispatcher.EventName.UIDungeonLoadingDirectorSanctuarytLoading);
            }
            else if (this.portaltype == ePortaltype.ROOM)
            {
                EventDispatcher.Instance.Dispatch<Vector2, string, Vector3>(EventDispatcher.EventName.DungeonMainPlayerToNextRoom,
                    this.transform.parent.parent.position, this.transform.parent.name, this.transform.position);
            }
        }
    }
}
