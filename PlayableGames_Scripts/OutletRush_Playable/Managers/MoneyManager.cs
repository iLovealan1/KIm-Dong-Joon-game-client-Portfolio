using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace luna_sportshop.Playable002
{
    public static class MoneyManager
    {
        //===============================================================
        //Static Fields
        //===============================================================
        private static int _currentMoney = 0;
        public static int CurrentMoney { get{ return _currentMoney; } set { _currentMoney += value; } }
        private static Action _onMoneyUpdated = null;

        //===============================================================
        //Static Properties
        //===============================================================
        public static event Action OnMoneyUpdated{ add { _onMoneyUpdated += value; } remove { _onMoneyUpdated -= value; } }

        //===============================================================
        //Functions
        //===============================================================
        public static int UpdateCurrentMoney(int money)
        {
            _currentMoney += money;
            _onMoneyUpdated?.Invoke();
            return _currentMoney;
        }

        public static void SetCurrentMoney (int money)
        {
            _currentMoney = money;
            _onMoneyUpdated?.Invoke();
        }
    }
}
