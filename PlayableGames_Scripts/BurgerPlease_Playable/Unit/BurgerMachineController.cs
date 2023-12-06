using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LunaBurger.Playable010
{
    public class BurgerMachineController : Unit
    {   
        [SerializeField] private float _burgerSpawnTime;
        [SerializeField] private float              _burgerSpeed;
        [SerializeField] private List<WaitLineSpot> _waitLineSpotList = new List<WaitLineSpot>();
        
        public int CurrBurgersCount
        {
            get
            {
                var wholeCnt = 0;
                foreach (var spot in _waitLineSpotList)
                {
                    var burgerCnt = spot.transform.childCount;
                    wholeCnt += burgerCnt;
                }
                return wholeCnt;
            }
        }
        private Stack<Transform> _burgerStack = new Stack<Transform>();
        private Coroutine       _coSpwanBurger;
        private float           _rootAnimTime = 1;
        private const int   STARTIDX = 0;
        private const float MINDIST = 1f;
        private const float MINSTACKDIST = 0.6f;
        private const int   MINSTACKCOUNT = 17;

        public override void Init()
        {
            _unitLevel = DEFAULTLEVEL; 

            if(_coSpwanBurger == null)  
            {
                _coSpwanBurger = StartCoroutine(CoSpawnBurger());
            }
            else 
            {
                _coSpwanBurger = null;
                _coSpwanBurger = StartCoroutine(CoSpawnBurger());   
            }
        }

        private IEnumerator CoSpawnBurger()
        {
            yield return CoroutineUtil.WaitForSeconds(_rootAnimTime);
            var cnt = 0;
            while(true)
            {
                cnt = CurrBurgersCount;
                if(cnt <= MINSTACKCOUNT)
                {
                    var burger = ObjectPoolManager.NullableInstance.GetObjFromPool(ePrefabType.BURGER);
                    var burgerTrans = burger.transform;
                    burgerTrans.rotation = Quaternion.Euler(Vector3.zero); 
                    // var spawnPos = _lineSpotTransList[STARTIDX].position;
                    // burgerTrans.position = spawnPos;
                    // StartCoroutine(CoMakeBurgerMove(burgerTrans));
                    StackBurger(burgerTrans);
                }
                yield return CoroutineUtil.WaitForSeconds(_burgerSpawnTime);
            }
        }

        // private IEnumerator CoMakeBurgerMove(Transform burgerTrans, int startidx = STARTIDX)
        // {
        //     var isMoving = true;
        //     var startVec = _lineSpotTransList[startidx].position;
        //     var targetVec = _lineSpotTransList[startidx + 1].position;     
        //     var dist = Vector3.Distance(startVec,targetVec);
        //     var dir = (targetVec - startVec).normalized;
        //     var timer = 0f;

        //     while(isMoving)
        //     {
        //         dist = Vector3.Distance(burgerTrans.position,targetVec); 
        //         timer += Time.fixedDeltaTime;
        //         if(dist <= MINDIST || timer > 1f)
        //         {
        //             isMoving = false;          
        //             startidx++;
        //             var isArrived = startidx == _lineSpotTransList.Count - 1;
        //             if(isArrived) 
        //             {
        //                 StackBurger(burgerTrans);
        //                 yield break;
        //             }
        //             else StartCoroutine(CoMakeBurgerMove(burgerTrans, startidx));
        //         }
        //         else
        //             burgerTrans.Translate(dir *_burgerSpeed * Time.fixedDeltaTime,Space.World); 
        //         yield return CoroutineUtil.WaitForFixedUpdate; 
        //     }
        // }  


        private void StackBurger(Transform burgerTrans)
        {
            Transform spotTrans = null;
            Transform finalSpotTrans = null;
            var minChildCount = 0;
            var cnt = _waitLineSpotList.Count;
            
            for(int i = 0; i < cnt; i++)
            {
                spotTrans = _waitLineSpotList[i].transform;

                if(spotTrans.childCount == 0) 
                {
                    burgerTrans.SetParent(spotTrans);
                    burgerTrans.localPosition = Vector3.zero;
                    _burgerStack.Push(burgerTrans);
                    return;
                }
                
                if(minChildCount == 0)
                {
                    minChildCount = spotTrans.childCount;
                    finalSpotTrans = _waitLineSpotList[i].transform;
                }
                else if (spotTrans.childCount < minChildCount)
                {
                    minChildCount = spotTrans.childCount;
                    finalSpotTrans = _waitLineSpotList[i].transform;
                }
                else if (spotTrans.childCount == minChildCount ) continue;               
            }

            burgerTrans.SetParent(finalSpotTrans);         
            var lastBurgerTrans = finalSpotTrans.GetChild(minChildCount - 1);
            var lastBurgerPos = lastBurgerTrans.localPosition;
            var newBurgerPos = new Vector3(0,lastBurgerPos.y + MINSTACKDIST,0);
            burgerTrans.localPosition = newBurgerPos;
            _burgerStack.Push(burgerTrans);
        }

        public List<Transform> GiveBurgerForManager(int amount)
        {
            if(amount > CurrBurgersCount) return null;
            List<Transform> burgersTrasList = new List<Transform>();

            var isEnough = false;

            while(!isEnough)
            {
                var burger = _burgerStack.Pop();
                burgersTrasList.Add(burger);    
                isEnough = amount == burgersTrasList.Count;
            }

            return burgersTrasList;
        }     
    }
}


