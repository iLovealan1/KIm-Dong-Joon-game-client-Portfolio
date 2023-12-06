using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace luna_sportshop.Playable002
{
    using Action = System.Action;
    using Random = UnityEngine.Random;

    public delegate ECustomerState RequestChangeStateToCustomer (ECustomerState _currstate, Customer RequestedCustomer);
    public enum ECustomerShelfTarget
    {
        None = -1,
        Shelf1 = 0,
        Shelf2 = 1,
        Clotehs = 2,
    } 

    public class Customer : Unit, IItemListReturner
    {  
        
        [Header("=====Customer SerializeField=====")]
        [Space]
        [SerializeField] private List<Transform>  _nodelList        = null;
        [SerializeField] private Transform        _beforeWaitNode   = null;
        [SerializeField] private Transform        _homeNode         = null;
        [SerializeField] private List<GameObject> _modelList        = null;
        [SerializeField] private Collider         _colider          = null;
        [SerializeField] private UIcustomerCloud  _customerCloud    = null;

        //===============================================================
        //Properties
        //===============================================================
        public event Action<Customer> OnRelease{ add {_onRelease += value;} remove {_onRelease -= value;} }
        public event RequestChangeStateToCustomer OnChangeState{ add {_onChangeState += value;} remove {_onChangeState -= value;} }
        public event Action<EGuideArrowState> OnArrivedAtPurchase{ add {_onArrivedAtPurchase += value;} remove {_onArrivedAtPurchase -= value;} }

        //===============================================================
        //Fields
        //===============================================================
        private RequestChangeStateToCustomer _onChangeState   = null;
        private Action<Customer> _onRelease                   = null;
        private Action<EGuideArrowState> _onArrivedAtPurchase = null;
        private int _targetCarryAmount                        = 0;
        private bool _isCounter                               = false;
        public ECustomerShelfTarget _targetShelf              = ECustomerShelfTarget.None;
        public ECustomerState       _currState                = ECustomerState.None;
        public Transform            _currTargetWaitNode       = null;

        //===============================================================
        //Functions
        //===============================================================
        protected override void Awake()
        {
            base.Awake();
            Init();
        }
        
        private void Init()
        {
            ChooseRanItemAmount();
            ChooseRanModel();
            _currState = ECustomerState.Shopping;
            this.gameObject.SetActive(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_targetShelf == ECustomerShelfTarget.Clotehs)
                return;
            
            var type = (ELayerName)other.gameObject.layer;

            switch(type)
            {
                case ELayerName.DisplayShelf : 
                {
                    if (_takeItemCorutine != null)
                        return;

                    if (_currItemList.Count == _targetCarryAmount)
                        return;

                    var returner = other.GetComponent<IDIsplayItemReturner>();

                    if (returner != null)
                    {
                        var takenItemList = returner.GetItemsFromDisplayShelf(_colider,out Action doneCallBack);

                        if (takenItemList == null)
                        {
                            _takeItemCorutine = this.StartCoroutine(CoWaitForTakeItems(takenItemList,doneCallBack,returner));
                            return;
                        }

                        _takeItemCorutine = this.StartCoroutine(TakeItems(takenItemList,doneCallBack));
                    }
                    
                    return;
                }

                case ELayerName.Counter : 
                {
                    if (_takeItemCorutine != null)
                        return;

                    if (_isCounter)
                        return;

                    var returner = other.GetComponent<IBoxReturner>();
                    this.StartCoroutine(CoWaitForTakeBox(returner));

                    _isCounter = true;
                    return;
                }
            }
        }

        public void MoveCustomerToDisplayShelf()
        {
            if (_currItemList.Count != 0)
                _currItemList[0].Disable();

            var nodeQ = new Queue<Transform>(_nodelList);
            this.StartCoroutine(CoMoveToShelfNode(nodeQ));
        }

        private IEnumerator CoMoveToShelfNode(Queue<Transform> nodeQ)
        {
            var timeLimit = 3f;

            if (_targetShelf == ECustomerShelfTarget.Clotehs)
                timeLimit = 0.8f;

            if (nodeQ.Count == 1)
            {
                if (_targetShelf == ECustomerShelfTarget.Shelf1)
                    timeLimit = 0.5f;
                else if (_targetShelf == ECustomerShelfTarget.Clotehs)
                    timeLimit = 0.4f;
                else 
                    timeLimit = 1f;
            }

            if (nodeQ.Count != 0)
            {
                var targetNode = nodeQ.Dequeue();
                ChangeAnimation(false);

                this.transform.parent = targetNode;
                yield return null;

                this.transform.LookAt(targetNode.position);;
                TweenUtil.TweenLocalPosition(
                this.transform,
                Vector3.zero,
                false,
                timeLimit,
                (done) =>{
                    this.StartCoroutine(CoMoveToShelfNode(nodeQ));
                });
            }
            else
            {
                if (_targetShelf == ECustomerShelfTarget.Shelf2)
                    this.transform.localEulerAngles = new Vector3 (0f,180f,0f);

                else
                    this.transform.localEulerAngles = new Vector3 (0f,-90f,0f);

                _customerCloud.gameObject.SetActive(true);
                ChangeAnimation(true);
            }
        }

        private IEnumerator CoMoveForWaitNode(Transform targetNode)
        {
            if (_customerCloud.gameObject.activeSelf)
                _customerCloud.gameObject.SetActive(false);
                
            var timeLimit = 0.8f;

            if (targetNode == null)
            {
                if (_currState != ECustomerState.BeforeWaitNode)
                    this.transform.localEulerAngles = new Vector3(0f,180f,0f);

                yield return CoroutineUtil.WaitUntil(() => {
                    _currState = _onChangeState.Invoke(_currState,this);
                    targetNode = _currTargetWaitNode;
                    return targetNode != null;
                });
            }
            else
            {
                if (_targetShelf == ECustomerShelfTarget.Shelf2)
                    timeLimit = 1.5f;
            }
          
            ChangeAnimation(false);

            this.transform.parent = targetNode;
            yield return null;

            TweenUtil.TweenLocalPosition(
            this.transform,
            Vector3.zero,
            false,
            timeLimit,
            (done) =>{
                ChangeAnimation(true);                
                _currTargetWaitNode = null;

                if (_currState != ECustomerState.PurchaseNode)
                    this.StartCoroutine(CoMoveForWaitNode(null));
                if (_currState == ECustomerState.PurchaseNode)
                {
                    this.transform.localEulerAngles = new Vector3(0f,180f,0f);

                    if (_onArrivedAtPurchase != null)
                    {
                        _onArrivedAtPurchase.Invoke(EGuideArrowState.Counter_CheckOut);
                        _onArrivedAtPurchase = null;
                    }
                }
            });
            TweenUtil.TweenLocalLookAt(this.transform,Vector3.zero,false,0.1f);
        }

        public IEnumerator CoMoveForHome(Box targetBox)
        {
            ChangeAnimation(false);

            this.transform.parent = _homeNode;
            yield return null;

            TweenUtil.TweenLocalPosition(
            this.transform,
            Vector3.zero,
            false,
            2f,
            (done) =>{
                if (targetBox != null)
                    targetBox.Release();

                Release();
            });
            TweenUtil.TweenLocalLookAt(this.transform,Vector3.zero,false,0.1f);
        }

        protected override void ChangeAnimation(bool isStopped)
        {
            var animName = string.Empty;

            if (isStopped)
            {
                if (_isCarrying)              
                    animName = EUnitAnimationName.CPI_Stack_Idle.ToString();             
                else    
                    animName = EUnitAnimationName.CPI_Player_Idle.ToString();                      
            }
            else
            {
                if (_isCarrying)              
                    animName = EUnitAnimationName.CPI_Stack_Run.ToString();             
                else    
                    animName = EUnitAnimationName.CPI_Player_run.ToString();  
            }

            if (_anim.GetCurrentAnimatorStateInfo(0).IsName(animName))
                return;

            _anim.Play(animName);
        }

        protected override IEnumerator TakeItems(List<Item> takenItemList, Action doneCallback = null)
        {
            _isCarrying = true;
            var needAmount = _targetCarryAmount;
            ChangeAnimation(true);

            yield return CoroutineUtil.WaitForSeconds(0.1f);

            while(takenItemList.Count != 0)
            {
                var lasIdx = takenItemList.Count - 1;
                var item = takenItemList[lasIdx];
                takenItemList.Remove(item);
                _currItemList.Add(item);
                needAmount--;

                if (needAmount == 0)
                {
                    this.StartCoroutine(CoJumpItem(item,doneCallback));
                    yield break;
                }

                if (needAmount != 0 && takenItemList.Count == 0)
                {
                    this.StartCoroutine(CoJumpItem(item,null));
                    yield return CoroutineUtil.WaitUntil( () =>{
                        return takenItemList.Count != 0;
                    });
                }
                else
                {
                    this.StartCoroutine(CoJumpItem(item,null));
                }
            }
        }
        private IEnumerator CoJumpItem(Item item ,Action doneCallBack = null)
        {
            var itemTrans = item.transform;
            itemTrans.parent = FindEmptyPoint();
            yield return null;

            var startSec = Time.time;
            var endSec = startSec + _itemMoveTimeLimit;
            Vector3 startPos = itemTrans.localPosition;
            
            while (Time.time < endSec)
            {
                var ratio = (Time.time - startSec) / _itemMoveTimeLimit;
                itemTrans.localPosition = Vector3.Lerp(startPos, Vector3.zero , _itemMoveCurve.Evaluate(ratio));
                itemTrans.localPosition = itemTrans.localPosition + Vector3.up * _itemJumpCurve.Evaluate(ratio);
                yield return CoroutineUtil.WaitForFixedUpdate;
            }

            var shoe = item as Shoe;
            if (shoe != null)
                shoe.ChangeForm(EItemFormState.None);

            itemTrans.localRotation = Quaternion.identity;
            itemTrans.localPosition = Vector3.zero;
            itemTrans.localScale = item.DefaultSize;
            
            if (doneCallBack != null)
            {
                _takeItemCorutine = null;
                _currState = ECustomerState.BeforeWaitNode;
                yield return CoroutineUtil.WaitForFixedUpdate;

                ChangeAnimation(false);             
                this.StartCoroutine(CoMoveForWaitNode(_beforeWaitNode));
                yield return CoroutineUtil.WaitForSeconds(0.5f);
                doneCallBack.Invoke();
            }

            Transform FindEmptyPoint() // 빈곳 할당
            {
                Transform pointTrans = null;
                for (int i = 0; i < _stackPointList.Count; i++)
                {
                    pointTrans = _stackPointList[i];
                    
                    if (pointTrans.childCount == 0)
                        break;
                }

                return pointTrans;
            }
        }
        
        private IEnumerator CoWaitForTakeItems(List<Item> takenItemStack, Action doneCallback = null, IDIsplayItemReturner returner = null) // for Customer Only
        {
            yield return CoroutineUtil.WaitUntil(() => {
                takenItemStack = returner.GetItemsFromDisplayShelf(_colider,out doneCallback);
                return takenItemStack != null;
            });


            _takeItemCorutine = this.StartCoroutine(TakeItems(takenItemStack, doneCallback)); 
        }


        private IEnumerator CoWaitForTakeBox(IBoxReturner returner)
        {
            Box box = null;
            Action doneCallback = null;

            yield return CoroutineUtil.WaitUntil(() => {
                box = returner.GetBoxObject(out doneCallback);
                return box != null && doneCallback != null;
            });

            this.StartCoroutine(CoTakeBox(box, doneCallback));
        }

        private IEnumerator CoTakeBox(Box targetBox, Action doneCallback)
        {
            _isCarrying = true;

            var boxTrans = targetBox.transform;
            boxTrans.parent = _stackPointList[0];
            yield return null;

            var startSec = Time.time;
            var endSec = startSec + _itemMoveTimeLimit;
            Vector3 startPos = boxTrans.localPosition;
            Vector3 targetPos = new Vector3(0f, -0.018f,0.17f);
            TweenUtil.TweenLocalRotation(boxTrans,new Quaternion(0f,0.7f,0f,0.7f),false,0.3f);
            
            while (Time.time < endSec)
            {
                var ratio = (Time.time - startSec) / _itemMoveTimeLimit;
                boxTrans.localPosition = Vector3.Lerp(startPos, targetPos , _itemMoveCurve.Evaluate(ratio));
                boxTrans.localPosition = boxTrans.localPosition + Vector3.up * _itemJumpCurve.Evaluate(ratio);
                yield return CoroutineUtil.WaitForFixedUpdate;
            }
            
            doneCallback.Invoke();
            this.StartCoroutine(CoMoveForHome(targetBox));
        }

        public List<Item> GetItemList(out Action doneCallBack)
        {
            doneCallBack = () => {
                _isCarrying = false;
                ChangeAnimation(true);
            };
            return _currItemList;
        }

        private int ChooseRanItemAmount() => _targetCarryAmount = 1;

        private void ChooseRanModel()
        {
            var count = _modelList.Count;

            for (int i = 0; i < count; i++)
                _modelList[i].SetActive(false);
            
            var ran = Random.Range(0,count);

            _modelList[ran].SetActive(true);
        }

        private void Release()
        {
            ChooseRanItemAmount();
            ChooseRanModel();
            this.transform.parent = _homeNode;
            this.transform.localPosition = Vector3.zero;
            _currItemList.Clear();
            _currTargetWaitNode = null;
            _isCarrying = false;
            _isCounter = false;
            _onRelease?.Invoke(this);
        }
    }
}
