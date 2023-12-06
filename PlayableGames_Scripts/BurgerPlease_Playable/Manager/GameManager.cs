using UnityEngine;

namespace LunaBurger.Playable010
{
    public enum EGameState
    {
        None = 0,
        STATE1 = 1, // 픽업 카운터 레벨 ALL 1
        STATE2 = 2, // 픽업 카운터 레벨 ALL 2
        STATE3 = 3, // 픽업 카운터 레벨 ALL 3
        STATE4 = 4, // 픽업 카운터 레벨 ALL 4
    }
    public class GameManager : MonoBehaviour
    {
        public  static GameManager NullableInstance => _instance;
        private static GameManager _instance;

        [SerializeField] private UIManager            _uiManager;
        [SerializeField] private CounterManager       _counterManager;
        [SerializeField] private BurgerMachineManager _burgerMachineManager;
        [SerializeField] private PickupManager        _pickupManager;
        [SerializeField] private CustomerManager      _customerManager;
        [SerializeField] private OrderBalloonManager  _orderBalloonManager;  
        [SerializeField] private LogoManager          _logoManager;

        [SerializeField] private MainCamController    _mainCamCon;

        public EGameState                            _currstate;
        
        public void Init()
        {
            _instance = this;
            ChangeStateAndCamPos(EGameState.STATE1);
            _logoManager.Init();
            _counterManager.Init();
            _pickupManager.Init();
            _burgerMachineManager.Init();
            _customerManager.Init();
            _uiManager.Init(ChangeStateAndCamPos);
            _orderBalloonManager.Init();
            _mainCamCon.Init();

            SoundManager.NullableInstance.OnPlayBGM();
        }

        private void ChangeStateAndCamPos(EGameState state)
        {
            if(state == _currstate)
            {
                return;
            }
            else
            {
                Debug.Log("현재 상태");
                _currstate = state;
            }
 
            _mainCamCon.ChangeCamState(state);
        }
    }
}