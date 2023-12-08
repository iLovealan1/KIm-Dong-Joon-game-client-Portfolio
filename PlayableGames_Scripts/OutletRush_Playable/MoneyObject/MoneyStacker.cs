using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace luna_sportshop.Playable002
{
    public class MoneyStacker : MonoBehaviour, IMoneyStackReturner
    {
        [Header("MoneyStacker SerializeField")]
        [Space]
        [SerializeField] private MoneyPool _moneyPool = null;
        [SerializeField] private Transform[] _defaultPosArr = null;
        [Space]

        [Header("돈 쌓고 시작하기")]
        [Space]
        [SerializeField] private bool _isStackStart = false;
        [Header("얼마나 쌓을까요? (돈의 값어치에 따라 달라집니다.)")]
        [Space]
        [SerializeField] private int _startStackMoney = 100;

        //===============================================================
        //Properties
        //===============================================================
        public event Action<EGuideArrowState> OnPlayerTakeMoney { add {_onPlayerTakeMoney += value;} remove {_onPlayerTakeMoney -= value;} }

        //===============================================================
        //Fields
        //===============================================================
        private Action<EGuideArrowState> _onPlayerTakeMoney = null;
        private const float SPACINGY = 0.2f;
        private Stack<Money> _currMoneyStack  = null;
        private bool _isOkToGen = true;
        private int _minMoneyGenPrice = 0;

        //===============================================================
        //Functions
        //===============================================================
        private void Awake() => _currMoneyStack = new Stack<Money>();

        private void Start()
        {
            _minMoneyGenPrice = Money.Price;

            if (_isStackStart)
                GenerateMoney(_startStackMoney); 
        }

        public void GenerateMoney(int amount) => this.StartCoroutine(CoGenerateMoney(amount));

        private IEnumerator CoGenerateMoney(int amount)
        {
            yield return CoroutineUtil.WaitUntil(() => {return _isOkToGen;});

            var count = Math.Round((float)amount / (float)Money.Price , 1) ;

            for (int i =0; i < count; i++)
            {
                var money = _moneyPool.GetMoney();
                _currMoneyStack.Push(money);
                SetMoneyPos(money);
            }
        }

        private void SetMoneyPos(Money money)
        {
            Transform moneyTrans =  money.transform;
            Transform targetTrans = null;

            var minCount = 0f;
            for (int i = 0; i < _defaultPosArr.Length; i++)
            {
                var childCount = _defaultPosArr[i].childCount;

                if (childCount == 0)
                {
                    targetTrans = _defaultPosArr[i];
                    moneyTrans.parent = targetTrans;
                    moneyTrans.localPosition = Vector3.zero;
                    return;
                }

                if (minCount == 0)
                {
                    minCount = childCount;
                    targetTrans = _defaultPosArr[i];
                }

                if (minCount > childCount)
                {
                    minCount = childCount;
                    targetTrans = _defaultPosArr[i];
                }
            }

            moneyTrans.parent = targetTrans;
            moneyTrans.localRotation = Quaternion.identity;
            moneyTrans.localPosition = new Vector3(0, SPACINGY * minCount, 0);
            
            
        }

        public Stack<Money> GetMoneyStack(out System.Action doneCallback)
        {
            doneCallback = () => {
                _isOkToGen = true;
                if (_onPlayerTakeMoney != null)
                {
                    _onPlayerTakeMoney.Invoke(EGuideArrowState.DisplayShelf_Shoe2_Upgrade);
                    _onPlayerTakeMoney = null;
                }
             };

            if (_currMoneyStack.Count == 0)
            {
                return null;         
            }
            else
            {
                _isOkToGen = false;
                return _currMoneyStack;
            }
        }
    }
}
