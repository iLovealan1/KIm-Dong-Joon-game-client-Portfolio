using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LunaBurger.Playable010
{
    public enum eLogoState
    {
        ORIGINAL = 1,
        REVERSED = 2,
    }
    public class LogoManager : MonoBehaviour
    {
        [SerializeField] private eLogoState _state = eLogoState.ORIGINAL;
        [SerializeField] private List<Transform> _logoList;
        public void Init()
        {
            if (_state == eLogoState.ORIGINAL)
            {
                foreach (Transform logoTrans in _logoList)
                {
                    logoTrans.localRotation = Quaternion.Euler(new Vector3 (0, 90, 0));
                }
            }
        }
    }
}