using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace luna_sportshop.Playable002
{
    public class InteractibleArea : MonoBehaviour
    {

        [Header("=====InteractibleArea SerializeField=====")]
        [Space]
        [SerializeField] private float scaleTime = .5f;
        [SerializeField] private Transform targetSprite;

        //===============================================================
        //Fields
        //===============================================================
        private TweenUtil.Token _scaleUpToken;
        private TweenUtil.Token _scaleDownToken;
        private Vector3 _baseScale;
        private float scaleMultiplier = 1.2f;

        //===============================================================
        //Functions
        //===============================================================
        private void Awake()
        {
            _baseScale = targetSprite.localScale;
        }

        private void ScaleUpAnimation()
        {
            _scaleDownToken.Stop();
            _scaleUpToken = TweenUtil.TweenScale(targetSprite,_baseScale*scaleMultiplier,false,scaleTime);
        }

        private void ScaleDownAnimation()
        {
            _scaleUpToken.Stop();
            _scaleUpToken = TweenUtil.TweenScale(targetSprite,_baseScale,false,scaleTime);
        }

        protected virtual void OnTriggerEnter(Collider other) 
        {
            if (other.gameObject.layer != (int)ELayerName.Player)
                return;
        
            ScaleUpAnimation();
        } 

        protected virtual void OnTriggerExit(Collider other) 
        {
            if (other.gameObject.layer != (int)ELayerName.Player)
                return;

            ScaleDownAnimation();
        }

    }
}

