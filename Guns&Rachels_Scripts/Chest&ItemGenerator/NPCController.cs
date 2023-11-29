using SpriteGlow;
using System.Collections;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    private enum eNpcType
    {
        SanctuaryNPC,
        DungeonNPC,
        Chest,
        HiddenChest
    }
    [SerializeField] private eNpcType type;

    [SerializeField] private bool isGlowNpc;
    [SerializeField] private SpriteGlowEffect sge;

    public int stepNum; //difficulty
    public int stageNum;

    private void Start()
    {
        if (this.isGlowNpc)
        {
            this.StartCoroutine(this.CoGlow());
        }
    }

    private bool isOff;
    private IEnumerator CoGlow()
    {
        while (true)
        {
            if (!this.isOff)
            {
                this.sge.GlowBrightness -= Time.deltaTime * 3;
                if (this.sge.GlowBrightness < 0) isOff = true;
            }
            else
            {
                this.sge.GlowBrightness += Time.deltaTime * 3;
                if (this.sge.GlowBrightness > 2.5f) isOff = false;
            }
            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.gameObject.tag == "Player")
        {
            if (this.type == eNpcType.SanctuaryNPC)
            {
                EventDispatcher.Instance.Dispatch<string>
                    (EventDispatcher.EventName.UINPCPopupUpdate,this.name);
                EventDispatcher.Instance.Dispatch<Transform>
                    (EventDispatcher.EventName.UINPCPopupActive,this.transform);
            }
            else if (this.type == eNpcType.DungeonNPC)
            {
                EventDispatcher.Instance.Dispatch<string>
                    (EventDispatcher.EventName.UINPCPopupUpdate, this.name);
                EventDispatcher.Instance.Dispatch<Transform>
                    (EventDispatcher.EventName.UINPCPopupActive,this.transform);
            }
            else if (this.type == eNpcType.HiddenChest)
            {
                EventDispatcher.Instance.Dispatch<string>
                    (EventDispatcher.EventName.UINPCPopupUpdate,this.name);
                EventDispatcher.Instance.Dispatch<Transform>
                    (EventDispatcher.EventName.UINPCPopupActive,this.transform);
            }
            else if (this.type == eNpcType.Chest)
            {
                var anim = this.GetComponent<Animator>();
                anim.SetInteger("state", 1);
                this.GetComponent<BoxCollider2D>().enabled = false;
                EventDispatcher.Instance.Dispatch<Transform, string>
                    (EventDispatcher.EventName.ChestItemGeneratorMakeItemForChest,this.transform, this.name);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.gameObject.tag == "Player")
        {
            if (this.type == eNpcType.SanctuaryNPC)
            {
                EventDispatcher.Instance.Dispatch<Transform>
                    (EventDispatcher.EventName.UINPCPopupActive,this.transform);
            }
            else if (this.type == eNpcType.DungeonNPC)
            {
                EventDispatcher.Instance.Dispatch<Transform>
                    (EventDispatcher.EventName.UINPCPopupActive,this.transform);
            }
            else if (this.type == eNpcType.HiddenChest)
            {
                EventDispatcher.Instance.Dispatch<Transform>
                    (EventDispatcher.EventName.UINPCPopupActive,this.transform);
            }
        }
    }

    private void OnDisable()
    {
        if (this.isGlowNpc)
        {
            this.StopAllCoroutines();
        }
    }
}
