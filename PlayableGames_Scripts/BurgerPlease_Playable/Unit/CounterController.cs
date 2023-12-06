using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LunaBurger.Playable010
{   
    public class CounterController : Unit
    {                 
        [SerializeField] public ParticleSystem        _coinParticle;
        [SerializeField] private CustomerController[] _defaultCustomerArr;
        [SerializeField] private ParticleSystem[]     _particleSystemArr;

        private int                                   _lastWeight;
        private int                                   _objectHash;
        private bool                                  _isActived;

        public override void Init()
        {           
            if(_unitLevel == DEFAULTLEVEL)
            {
                StartCoroutine(CoStartOrder());
            }

            var defaultIdx = 0;
            var waitLineComp = _lineSpotCompList[defaultIdx];

            _objectHash = this.transform.GetHashCode();   
            waitLineComp.ParentObjTransHash = _objectHash;          
            
            if (!_isDefaultUnit)
                this.gameObject.SetActive(false); 
        }
        public void PlayUpgradeParticles()
        {
            foreach (var particle in _particleSystemArr)
            {
                particle.Play();
            }
        }     

        public void ChangeAnimState(eUnitAnimState state) 
        {
            switch (state)
            {
                case eUnitAnimState.IDLE:
                _workerAnimComp.Play("Idle");
                break;
                case eUnitAnimState.WORK:
                _workerAnimComp.Play("Package_work");
                break;
                default:                
                _workerAnimComp.Play("Idle");
                Debug.LogError($"카운터 상태와 관련된 enum이 아닙니다. : {state}");
                break;
            }
        }

        private IEnumerator CoStartOrder()
        {
            yield return CoroutineUtil.WaitForSeconds(0.5f);

            foreach (var comp in _defaultCustomerArr)
                comp.OnDoNextMoveForDefault();
        }

        public void CollectMoney(int cash)
        {
            var income = cash;
            CashManager.Instance.UpdateCurrentCash(income);       
            _coinParticle.Play();
            UIManager.OnUpdateUICash();
            ChangeAnimState(eUnitAnimState.IDLE);      
        } 

        public Transform FindEmptySpot()
        {
            Transform emptySpot = null;
            foreach (var spot in _lineSpotCompList)
            {
                var weight = spot.Weight;
                if (spot.transform.childCount == 0 && weight == 5) 
                {
                    emptySpot = spot.transform;
                    break;
                }
            } 
            return emptySpot;             
        }      

        public Transform FindMidEmptySpot(WaitLineSpot spotComp) // 2, 3, 4, 5
        {
            Transform emptySpot = null;
            var currWeight = spotComp.Weight;   
            var targetWeight = --currWeight; 

            foreach (var comp in _lineSpotCompList)
            {
                if(comp.Weight == targetWeight)
                {
                    if(comp.transform.childCount == 0) // 손님이 점거 중인지 확인
                    {
                        emptySpot = comp.transform; // 빈곳을 찾았을때
                        break;
                    }
                    else 
                        break;
                }
            }
                        
            return emptySpot;                  
        }
    }   
}
