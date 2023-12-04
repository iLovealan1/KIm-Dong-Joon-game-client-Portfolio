using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Supercent.Util;

namespace luna_sportshop.Playable002
{
    using Params = Supercent.Util.TweenUtil.Params;
    using Token = Supercent.Util.TweenUtil.Token;
    using TimeYpe = Supercent.Util.TweenUtil.TimeType;

    public class Shoe : Item
    {
        Token shirinkToken;
        
        //===============================================================
        //Functions
        //===============================================================
        protected override void Awake()
        {
            base.Awake();
        }

        public override void ChangeForm(EItemFormState state = EItemFormState.Storage)
        {
            if (state == EItemFormState.None)
            {
                _disableTargetObjet.SetActive(true);
            }
            else
            {
                _currFormState = state;
                _disableTargetObjet.SetActive(false);
            }

        }

        public override void Shrink()
        {
            if (shirinkToken.IsValid())
                shirinkToken.Stop();

            shirinkToken = TweenUtil.TweenScale(this.transform,_targetShrinkSize,false,0.5f);
        }

    }
}

