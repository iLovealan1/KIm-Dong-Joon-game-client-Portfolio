namespace LunaBurger.Playable010
{
    public class CashManager
    {
        private static CashManager _instance;
        public static CashManager Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new CashManager();
                }   
                return _instance;   
            }            
        }
        private CashManager() {}

        private const int DEAFUALTCASH = 50;
        private int _currentCash = DEAFUALTCASH;  public int CurrentCash => _currentCash;

        public void UpdateCurrentCash(int cash) =>  _currentCash += cash;            
    }
}

