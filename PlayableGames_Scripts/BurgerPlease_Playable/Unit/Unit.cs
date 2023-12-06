using System.Collections.Generic;
using UnityEngine;

namespace LunaBurger.Playable010
{
    public enum eUnitAnimState
    {
        NONE = -1,
        IDLE = 0,
        RUN = 1,
        STACKIDLE = 2,
        STACKRUN = 3,
        WORK = 4,
    }

    public enum eUnitLevel
    {
        NONE = 0,
        LEVEL1 = 1,
        LEVEL2 = 2,
        LEVEL3 = 3,
        LEVEL4 = 4,
    }

    public abstract class Unit : MonoBehaviour
    {
        [SerializeField] private protected int  _unitLevel;
        public int UnitLevel => _unitLevel;    
        [SerializeField] private protected bool _isDefaultUnit  = false;
        [SerializeField] private protected List<Transform>    _lineSpotTransList;
        [SerializeField] private protected List<WaitLineSpot> _lineSpotCompList;
        [SerializeField] private protected Animator           _workerAnimComp;

        protected const int DEFAULTLEVEL = 1;
   
        public abstract void Init();
    }
}
