using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LunaBurger;

namespace luna_sportshop.Playable002
{
    using Action = System.Action;

    public class UIObjectField : MonoBehaviour
    {
       [SerializeField] private VJHandler _joystick = null;
       public VJHandler JoyStick => _joystick;

       [SerializeField] private UIDrageGuide _drageGuide = null;
       public UIDrageGuide DrageGuide => _drageGuide;

       [SerializeField] private UIMoney _uiMoney = null;
       public UIMoney UIMoney => _uiMoney;

       [SerializeField] private CTAButton _btnCTA = null;
       public CTAButton BtnCTA => _btnCTA;
    }
}

