using System.Collections;
using UnityEngine;
using Supercent.Util;

namespace LunaBurger.Playable010
{
    public class UIBounce : MonoBehaviour
    {
        [SerializeField] float sec = 0.5f;
        [SerializeField] Vector3 minScale;
        [SerializeField] Vector3 maxScale;

        IEnumerator coBetween = null;

        void OnEnable()  => (coBetween = CoBetween()).MoveNext();
        void OnDisable() => coBetween = null;
        void Update()    => EnumeratorUtil.Update(coBetween);

        IEnumerator CoBetween()
        {
            var lenPingPong = sec * 2f;
            while (true)
            {
                var tL = Time.unscaledTime % lenPingPong;
                var tPP = Mathf.PingPong(tL, sec);
                var ratio = EaseUtil.Interpolate(EaseType.SineIn, EaseType.SineOut, tPP);

                transform.localScale = Vector3.LerpUnclamped(minScale, maxScale, ratio);
                yield return null;
            }
        }
    }
}

