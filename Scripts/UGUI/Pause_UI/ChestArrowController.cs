using DG.Tweening;
using UnityEngine;

public class ChestArrowController : MonoBehaviour
{
    private SpriteRenderer arrow;
    private SpriteRenderer arrow2;
    private BoxCollider2D triggerCollider;
    private ParticleSystem particle;

    public void Init()
    {
        this.arrow = this.GetComponent<SpriteRenderer>();
        this.arrow2 = this.transform.GetChild(0).GetComponent<SpriteRenderer>();
        Debug.Log(this.arrow.gameObject.name);
        this.triggerCollider = this.GetComponent<BoxCollider2D>();
        this.particle = this.GetComponentInChildren<ParticleSystem>();
        this.triggerCollider.enabled = false;
        this.gameObject.SetActive(false);
    }
    public void StartArrowAnimation()
    {
        this.gameObject.SetActive(true);
        //this.particle.Play();
        this.arrow.DOFade(1, 0.5f);
        this.arrow2.DOFade(1, 0.5f);
        this.triggerCollider.enabled = true;

        this.transform.DOLocalMoveY(2f, 1f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutQuad);
        this.transform.DOLocalRotate(new Vector3(360f, 0f, 0f), 2f, RotateMode.LocalAxisAdd).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            this.arrow2.DOFade(0, 0.5f);
            this.arrow.DOFade(0, 0.5f).OnComplete(() =>
            {
                this.transform.DOKill();
                this.gameObject.SetActive(false);
            });
        }
    }

    private void OnDestroy()
    {
        if (this.arrow != null)
        {
            this.arrow.DOKill();
            this.transform.DOKill();
        }
    }
}
