using UnityEngine;
using TMPro;
// using Luna.Unity;

namespace luna_sportshop.Playable002
{
    public class UIDrageGuide : MonoBehaviour
    {
        [Header("=====UIDragGuide Fields=====")]
        [Space] 
        [SerializeField] private TextMeshProUGUI _txtTouchToStart = null;

        //===============================================================
        //Properties
        //===============================================================
        public IPlayerMoveHandler PlayerMoveHandler { set {_playerMoveHandler = value;} }

        //===============================================================
        //Fields
        //===============================================================
        private IPlayerMoveHandler _playerMoveHandler = null;
        private const string TEXTSTART = "Touch To Start!";

        //===============================================================
        //Functions
        //===============================================================
        private void Awake() =>  _txtTouchToStart.gameObject.SetActive(false);
        
        public void SetOffDragGuide()
        {
            if (!this.gameObject.activeSelf)
                return;

            // LifeCycle.GameStarted();
            
            _playerMoveHandler.IsOKToMove = true;
            this.gameObject.SetActive(false);
        }
    } 
}