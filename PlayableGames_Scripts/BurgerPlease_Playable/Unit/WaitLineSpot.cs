using UnityEngine;

namespace LunaBurger.Playable010
{
    public class WaitLineSpot : MonoBehaviour
    {
        [SerializeField] private int        _weight;
        public int        Weight      => _weight;   
        [SerializeField] private eUnitLevel _parentLevel;
        public eUnitLevel ParentLevel => _parentLevel;    

        public int ParentObjTransHash { get; set; }
    }   
}