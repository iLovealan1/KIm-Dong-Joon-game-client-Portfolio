using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Supercent.Util;

namespace luna_sportshop.Playable002
{
    public class Clothes : Item
    {
        [Header("Clothes SerializeField")]
        [Space]
        [SerializeField] private GameObject _foldedFormClothes = null;

        //===============================================================
        //Functions
        //===============================================================
        protected override void Awake()
        {
            base.Awake();
            _disableTargetObjet.SetActive(false);
        }

        public override void ChangeForm(EItemFormState state = EItemFormState.Storage)
        {
            _currFormState = state;

            if (_currFormState == EItemFormState.Storage)
            {
                _foldedFormClothes.SetActive(true);
                _disableTargetObjet.SetActive(false);
            }
            else
            {
                _foldedFormClothes.SetActive(false);
                _disableTargetObjet.SetActive(true);
            }
        }

        public override void Shrink(){}
    }
}


