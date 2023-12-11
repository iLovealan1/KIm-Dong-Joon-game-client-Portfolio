![header](https://capsule-render.vercel.app/api?type=waving&color=gradient&height=250&section=header&text=Luna%20Playable%20Games&fontSize=35)<br>

# 소개
안녕하세요. 플레이어블게임 포트폴리오에 오신걸 환영합니다.

- 저는 슈퍼센트 주식회사에서 3개월간 인턴으로 근무하며 루나플레이어블 플랫폼 게임을 개발하는 업무를 담당하였습니다.<br>
- 전체 개발을 담당한 게임 2개와 리펙토링에 참여한 게임 1개가 있습니다.<br>
- 전체 개발을 담당한 플레이어블게임들은 슈퍼센트 주식회사의 모바일 플랫폼 아케이드 아이들 장르의 게임인 Burger Please! 와 Outlet Rush의 미니게임들입니다.<br>
- 두개의 게임 모두 현재 전면광고 게임으로 현재 ironSource, Applovin, Lunaplayable 등의 플랫폼에서 배포되어 서비스 중입니다.<br>

아래는 개발한 게임 2개의 기술 목록입니다.

# Contacts
📧: 이메일 :  korindj@kakao.com<br>
🏠: 개발자 블로그 : [Blog Link](https://bueong-e.tistory.com)

# 목차

개요 : [📚:설명보기](#playableGame)<br>
게임소개 및 플레이링크 : [📚:설명보기](#IntroDuction)<br>

### Outlet Rush Playable [📂 : 폴더로 이동 ](https://github.com/iLovealan1/KIm-Dong-Joon-game-client-Portfolio/tree/main/PlayableGames_Scripts/OutletRush_Playable)

:red_circle: 플레이어 이동로직 및 카메라 이벤트 로직 및 연출.[📚:설명보기](#player_control_camera_event)<br>
:red_circle: 아이템 스택킹 로직 및 연출.[📚:설명보기](#item_stacking)<br>
:red_circle: 버전에 따른 돈 스택킹 로직 및 연출.[📚:설명보기](#money_stacking)<br>
:red_circle: 손님 이동로직 및 연출.[📚:설명보기](#customer_move)<br>

### Burger Please! Playable [📂 : 폴더로 이동 ](https://github.com/iLovealan1/KIm-Dong-Joon-game-client-Portfolio/tree/main/PlayableGames_Scripts/BurgerPlease_Playable)

:red_circle: App, Manager, Controller Init 구조.[📚:설명보기](#manager_init)<br>
:red_circle: 군중 컨트롤 및 줄서기 로직.[📚:설명보기](#crowd_control)<br>

* * *


# 개요
### playableGame

![GraphicImage](https://docs.lunalabs.io/assets/lpp-overview.png)
🌕 Luna playable : [Docs Link][Luna playable Link]

[Luna playable Link]: https://docs.lunalabs.io/docs/playable/overview

▶️ 플레이어블 게임?
- 루나 플레이어블 플랫폼을 기반으로 제작된 전면광고용 미니 게임입니다.
- 현 라이브 서비스중인 게임의 홍보를 위해 제작되는 게임으로 모태가 되는 게임의 미니 게임입니다.

# **게임소개 및 플레이링크**<br>

### IntroDuction

*하단에 소개된 게임들은 전면 광고 게임으로 웹상에서 자바스크립트로 변환되어 플레이되기 때문에 게임소개 및 플레이링크 항목의 링크에서 플레이가 가능합니다.<br>
*사용중인 브라우저에서 플레이 불가능시 다른 인터넷 브라우저로 플레이 가능합니다.<br>
*게임 시작을 위해 클릭시 인게임 사운드가 재생됩니다.<br>
*웹상에서 플레이시 모바일로 플레이할때와 다르게 그래픽 오류등이 있을수 있습니다.

---

## 🎮: 게임소개

![image](https://github.com/iLovealan1/KIm-Dong-Joon-game-client-Portfolio/assets/124248265/366eae4c-4d68-4633-ac23-84ae3c59d4db)

- **제목** : Outlet Rush Playable <br> 
- **장르** : Arcade Idle <br>
- **엔진** : UnityEngine3D<br>
- **플랫폼** : Luna Playable<br>
- **Impressions (실제 게임 플레이에 기반하는 게임 노출도)** : 2천 3백만회 이상<br>

## ⭐: 게임의 특징

*해당 게임은 기획단계부터 참여하여 전체를 제작한 프로젝트입니다.*
- 신발 과 옷등 아울렛을 운영하며 확장하는 아케이드 아이들 장르의 게임
- 유명 브랜드 신발과 옷을 판매하며 매대를 확장하고 직원을 고용하며 돈을 모아 가게를 확장하는 플로우
- 돈을 모아 최종 언락인 옷 매대 언락을하게 되면 게임이 끝나는 방식

👉 신발 혹은 옷 등을 손 위에 쌓고 매대로 옮겨지는 중독성 있는 애니메이션 <br>
👉 등뒤에 쌓이는 돈을 통해 돈을 모으는 재미를 시각적으로 표현<br>
👉 신발 -> 옷 순으로 언락되며 판매 품목이 많아지고 가게가 확장되는 것을 보는 재미<br>

## 🎲: 플레이 링크

👟: Outlet Rrush : [Link_Stack_ver][OutletRush 002]

[OutletRush 002]: https://playground.lunalabs.io/preview/117526/165584/1703aff40e6d4548f15efe206918c6945f053e4a8bd126710bf82d53d1925cc4


👟: Outlet Rrush : [Link_none_Stack_ver][OutletRush 002_none Stack]

[OutletRush 002_none Stack]: https://playground.lunalabs.io/preview/117526/165586/1703aff40e6d4548f15efe206918c6945f053e4a8bd126710bf82d53d1925cc4

---

## 🎮: 게임소개

![image](https://github.com/iLovealan1/KIm-Dong-Joon-game-client-Portfolio/assets/124248265/17e5c8df-276f-4f89-ad77-836ce2df4742)

- **제목** : Burger Please! Playable <br> 
- **장르** : Upgrade Idle <br>
- **엔진** : UnityEngine3D<br>
- **플랫폼** : Luna Playable<br>
- **Impressions (실제 게임 플레이에 기반하는 게임 노출도)** : 70만회 이상<br>

## ⭐: 게임의 특징

*해당 게임은 기획단계부터 참여하여 전체를 제작한 프로젝트입니다.*
- 주문을 위한 카운터, 버거 픽업을 위한 픽업대 구성의 업그레이드 아이들 게임
- 버거를 수령한 손님에게서 돈을 수금하여 카운터와 픽업대를 늘려나가는것이 게임의 주요 목표
- 게임의 마지막 언락 요소인 새로운 머신 추가시 게임이 끝나는 방식<br>

👉 버거를 주문하고 받아가기위해 분주하게 움직이는 손님들을 보는 재미요소<br>
👉 돈을 모아 가게를 확장해 가는 재미요소


## 🎲: 플레이 링크
🍔: Burger Please! Playable : [Link][Burger Please! 010]

[Burger Please! 010]: https://playground.lunalabs.io/preview/110188/155622/1703aff40e6d4548f15efe206918c6945f053e4a8bd126710bf82d53d1925cc4

---

## 🎮: 게임소개

![image](https://github.com/iLovealan1/KIm-Dong-Joon-game-client-Portfolio/assets/124248265/e6e42a15-3cf5-442a-ae4d-fded7a76eff8)

- **제목** : Burger Please! Playable2 <br> 
- **장르** : Arcade Idle <br>
- **엔진** : UnityEngine3D<br>
- **플랫폼** : Luna Playable<br>
- **Impressions (실제 게임 플레이에 기반하는 게임 노출도)** : 3천만회 이상<br>

## ⭐: 게임의 특징

*해당게임은 기존 라이브 서비스중이던 프로젝트에 버거머신 단계 추가, 맵 오브젝트 재배치, 버그수정등 리펙토링에 참여한 프로젝트입니다.*
- 버거머신에서 버거를 수령해 손님에게 팔아 가게를 확장하는 아케이드 아이들 장르의 게임
- 총 3단계의 버거 머신 업그레이드와 드라이브스루 파트로의 확장, 카운터 직원 고용이 게임의 핵심요소
- 돈을 모아 마지막 확장요소인 가게 크기 확장을 하면 게임이 끝나는 방식


👉 버거를 손 위에 쌓아 카운터로 전달하는 중독성 있는 애니메이션<br>
👉 업그레이드된 버거머신으로 부터 나오는 버거의 진화버전을 보는 재미<br>
👉 드라이브 스루 언락으로 인해 혼자 담당하기 힘들어진 가게를 직원 고용을 통해 해소하여 몰입감을 선사<br>

## 🎲: 플레이 링크
🍔: Burger Please! Playable 2 : [Link][Burger Please! 011]

[Burger Please! 011]: 
https://playground.lunalabs.io/preview/113799/160490/1703aff40e6d4548f15efe206918c6945f053e4a8bd126710bf82d53d1925cc4

---

# 제작 스크립트 설명

## 🟢: Outlet Rrush Playable<br>[📂:폴더이동](https://github.com/iLovealan1/KIm-Dong-Joon-game-client-Portfolio/tree/main/PlayableGames_Scripts/OutletRush_Playable)

### player_control_camera_event

![image](https://github.com/iLovealan1/KIm-Dong-Joon-game-client-Portfolio/blob/main/PlayableGames_Image/OutletRush_PlayerMove.gif)![image](https://github.com/iLovealan1/KIm-Dong-Joon-game-client-Portfolio/blob/main/PlayableGames_Image/OutletRush_EventCam.gif)


### **이미지 설명(좌측부터)**
- 가상 조이스틱을 이용한 플레이어의 움직임을 볼 수 있습니다.
- 플레이어를 따라다니는 카메라의 움직임을 볼 수 있습니다.
- 이벤트 발생시 카메라의 움직임을 볼 수 있습니다.

### **요약**
- Joystick UI 객체와 MainCamer 객체는 플레이어와 Interface를 통해 소통하여 객체 은닉화.
- Event System을 통한 IPlayerMoveHandler 메서드 호출로 인터페이스에 플레이어의 움직임을 위임
- IPositionReturner 인터페이스의 GetPosition() 메서드를 통한 플레이어의 현재 포지션 값을 카메라에 전달
- IPlayerMoveHandler, IPositionReturner 간의 인터페이스 상속으로 카메라 이벤트 호출시 다운캐스팅을 통해 플레이어의 움직임 제어
- 내부 서브모듈 TWeen 유틸과 Ease 유틸을 활용한 카메라 이벤트 움직임

### **관련 스크립트**

**IPlayerMoveHandler**[📜 : 스크립트 전문보기](https://github.com/iLovealan1/KIm-Dong-Joon-game-client-Portfolio/blob/main/PlayableGames_Scripts/OutletRush_Playable/InterFaces/IPlayerMoveHandler.cs)<br>
**IPositionReturner**[📜 : 스크립트 전문보기](https://github.com/iLovealan1/KIm-Dong-Joon-game-client-Portfolio/blob/main/PlayableGames_Scripts/OutletRush_Playable/InterFaces/IPositionReturner.cs)<br>
**Player**[📜 : 스크립트 전문보기](https://github.com/iLovealan1/KIm-Dong-Joon-game-client-Portfolio/blob/main/PlayableGames_Scripts/OutletRush_Playable/Unit/Player.cs)<br>
###코드

&nbsp;&nbsp;&nbsp;&nbsp;● MovePlayer() : 플레이어의 이동 로직 메서드입니다.<br>

````
public void MovePlayer(float vertical ,float horizontal) // 잔딜받은 float 값을 통한 플레이어 제어
{
    if (IsOKToMove == false) // 카메라 이벤트 시작시 플레이어 움직임 제한
    {
        _rigedBody.velocity = Vector3.zero;
        ChangeAnimation(true);
        return;
    }
    
    if (vertical == 0 && horizontal == 0)
    {
        _rigedBody.velocity = Vector3.zero;
        ChangeAnimation(true);
        return;
    }
    
    Vector3 targetDirection = _cameraForward * vertical + _cameraRight * horizontal;
    targetDirection.Normalize();
    
    if (targetDirection != Vector3.zero)
    {
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, targetRotation, _rotationSpeed);
    }
    
    ChangeAnimation(false);
    _rigedBody.velocity = targetDirection * _speed;
}
````

**VJHandler**[📜 : 스크립트 전문보기](https://github.com/iLovealan1/KIm-Dong-Joon-game-client-Portfolio/blob/main/PlayableGames_Scripts/OutletRush_Playable/UI/VJHandler.cs)<br>
###코드

&nbsp;&nbsp;&nbsp;&nbsp;● OnDrag() : 이벤트 시스템을  MovePlayer() 메서드를 호출합니다. .<br>

````
 public void OnDrag(PointerEventData ped)
{
    Vector2 position = Vector2.zero;

    ScreenPointToLocalPointInRectangle
            (_jsContainer.rectTransform,
            ped.position,
            ped.pressEventCamera,
            out position);

    position.x = (position.x / _jsContainer.rectTransform.sizeDelta.x);
    position.y = (position.y / _jsContainer.rectTransform.sizeDelta.y);

    float x = (_jsContainer.rectTransform.pivot.x == 1f) ? position.x : position.x;
    float y = (_jsContainer.rectTransform.pivot.y == 1f) ? position.y : position.y;

    InputDirection = new Vector3(x, y, 0);
    InputDirection = (InputDirection.magnitude > 1) ? InputDirection.normalized : InputDirection;

    _joystick.rectTransform.anchoredPosition = new Vector3(InputDirection.x * (_jsContainer.rectTransform.sizeDelta.x / 3)
                                                           , InputDirection.y * (_jsContainer.rectTransform.sizeDelta.y) / 3);
    if (Vector2.Distance(ped.position, ped.pressPosition) > moveThreshold)
    {
        _playerMoveHandler.MovePlayer(InputDirection.y, InputDirection.x);   // 플레이어 이동 인터페이스 위임                                        
    }
}

````

**PlayerCamera**[📜 : 스크립트 전문보기](https://github.com/iLovealan1/KIm-Dong-Joon-game-client-Portfolio/blob/main/PlayableGames_Scripts/OutletRush_Playable/ETC/PlayerCamera.cs)<br>
###코드

&nbsp;&nbsp;&nbsp;&nbsp;● GetPosition() : 플레이어의 위치를 받아오는 인터페이스 메서드입니다. LateUpdate에서 호출됩니다. <br>
&nbsp;&nbsp;&nbsp;&nbsp;● StartEventCamYoyo() : 카메라 이벤트 이동 메서드입니다. 이동은 Tween 유틸과 Ease 유틸을 활용하였습니다. <br>

````

private void LateUpdate()
{
    if (!_isEvnet)
    {
        _playerPos = _playerPosReturner.GetPosition();
        this.transform.position = _playerPos - _targetPos;
    }
}

public void StartEventCamYoyo(EEventCamType type)
{
    var moveHandler =_playerPosReturner as IPlayerMoveHandler; // 다운캐스팅을 통한 bool값 제어
    
    if ( moveHandler == null )
        return;

    if ( _eventCamToken.IsValid() )
        return;

    _isEvnet = true;
    moveHandler.IsOKToMove = false;

    var defaultPos = this.transform.position;
    var idx = (int)type;
    var targetTrans = _eventCamTransList[idx];

    var camMoveTimer = _camMoveTimer;
    var camComebackTimer = _camComeBackTimer;

    if (type == EEventCamType.DisplayCloathes)
    {
        camMoveTimer -= 0.2f;
        camComebackTimer -= 0.2f;
    }

    TweenUtil.TweenPosition(
        this.transform,
        targetTrans,
        new Params(TimeType.Scale)
        {    
            secDuration = camMoveTimer, 
            timeModular = (t) => EaseUtil.SineIn(t) 
        }, 
        (done_MoveIn) =>{

            var waitPos = new Vector3(
                this.transform.position.x, 
                this.transform.position.y + 0.0001f, 
                this.transform.position.z);

            TweenUtil.TweenPosition(
                this.transform,
                waitPos,
                false,
                _camWaitTimer,
                (done_Wait) =>{
                    TweenUtil.TweenPosition(
                    this.transform,
                    defaultPos,
                    new Params(TimeType.Scale)
                    {    
                        secDuration = camComebackTimer, 
                        timeModular = (t) => EaseUtil.SineIn(t) 
                    }, 
                    (done_MoveOut) =>{

                        if (_onFirstMoveEventDone != null)
                        {
                            _onFirstMoveEventDone.Invoke();
                            _onFirstMoveEventDone = null;
                        }

                        _isEvnet = false;
                        moveHandler.IsOKToMove = true;
                    });
            });
    });
}
````
[📑: 목차로](#목차)

### item_stacking

![image](https://github.com/iLovealan1/KIm-Dong-Joon-game-client-Portfolio/blob/main/PlayableGames_Image/OutletRush_PlayerStack.gif)![image](https://github.com/iLovealan1/KIm-Dong-Joon-game-client-Portfolio/blob/main/PlayableGames_Image/OutletRush_CounterStack.gif)
![image](https://github.com/iLovealan1/KIm-Dong-Joon-game-client-Portfolio/blob/main/PlayableGames_Image/OutletRsuh_ClothesStack.gif)![image](https://github.com/iLovealan1/KIm-Dong-Joon-game-client-Portfolio/blob/main/PlayableGames_Image/OutletRush_Clothes_Shelf_Stack.gif)![image](https://github.com/iLovealan1/KIm-Dong-Joon-game-client-Portfolio/blob/main/PlayableGames_Image/OutletRush_PlayerStack%20Point.png)


### **이미지 설명(좌측 상단부터)**
- 플레이어가 창고매대에서 아이템을 스택 후 매장 신발매대에 디스플레잉 하는 모습입니다.
- 카운터에서 손님의 아이템을 픽업하여 포장후 다시 손님에게로 스택되는 모습입니다.
- 플레이어가 창고매대에서 옷과 신발을 섞어서 스택하는 모습입니다.
- 매장 옷매대에서 옷만을 골라 디스플레잉 하는 모습입니다.
- 플레이어의 손위 스택 포인트 기즈모를 확인할수 있습니다.
- 모든 객체와 상호작용시 객체 하단의 그림자 인터렉티브 에리어의 사이즈가 변경되는걸 확인할수 있습니다.


### **요약**
- 각 객체는 자신의 colider의 TriggerEnter() 메서드 호출로 레이어로 객체를 구분하여 그에 맞는 기능을 수행.
- 상호작용시 해당 클래스가 상속받은 인터페이스를 TryGetComponent로 null 체크를 수행한뒤 인터페이스의 기능을 호출.
- 아이템을 가지고 있을수 있는 객체들은 상황에 맞는 IItemListReturner,IDIsplayItemReturner,IBoxReturner 인터페이스를 상속하여 플레이어 혹은 손님 객체와 소통.
- 아이템 스택은 IItemListReturner로 Item 제너릭 리스트를 넘겨받아 자신의 리스트에 넣으며 애니메이션을 실행.
- 스택 완료시 함께 Out인자로 전달받은 done callback 을 스택이 마무리된 객체가 호출.
- 아이템 스택은 Lerp() 함수와 Animation Curve 클래스를 활용하여 연출.
- 자체 제작 오브젝트 Pool 클래스를 활용하여 메모리 부담을 덜 수 있게 활용.

### **관련 스크립트**
**IItemListReturner**[📜 : 스크립트 전문보기](https://github.com/iLovealan1/KIm-Dong-Joon-game-client-Portfolio/blob/main/PlayableGames_Scripts/OutletRush_Playable/InterFaces/IItemListReturner.cs)<br>
**IDIsplayItemReturner**[📜 : 스크립트 전문보기](https://github.com/iLovealan1/KIm-Dong-Joon-game-client-Portfolio/blob/main/PlayableGames_Scripts/OutletRush_Playable/InterFaces/IDIsplayItemReturner.cs)<br>
**IBoxReturner**[📜 : 스크립트 전문보기](https://github.com/iLovealan1/KIm-Dong-Joon-game-client-Portfolio/blob/main/PlayableGames_Scripts/OutletRush_Playable/InterFaces/IBoxReturner.cs)<br>
**ItemPool**[📜 : 스크립트 전문보기](https://github.com/iLovealan1/KIm-Dong-Joon-game-client-Portfolio/blob/main/PlayableGames_Scripts/OutletRush_Playable/Items/ItemPool.cs)<br>
**Player**[📜 : 스크립트 전문보기](https://github.com/iLovealan1/KIm-Dong-Joon-game-client-Portfolio/blob/main/PlayableGames_Scripts/OutletRush_Playable/Unit/Player.cs)<br>
### 코드

&nbsp;&nbsp;&nbsp;&nbsp;● TakeItems() : 플레이어가 성공적으로 매대의 인터페이스를 가져왔을떄 호출되는 메서드입니다. <br>
&nbsp;&nbsp;&nbsp;&nbsp;● CoJumpItem() : 플레이어의 아이템 획득 애니메이션 로직이 실행되는 유니티 코루틴입니다. <br>
&nbsp;&nbsp;&nbsp;&nbsp;● FindEmptyPoint() : 아이템을 스택할수 있는 빈 공간을 찾는 내부함수 입니다. <br>
````
protected override IEnumerator TakeItems(List<Item> takenItemList, Action doneCallback = null)
{
    _isCarrying = true;
    _isDuringStacking = true;
    var needAmount = _maxItemCarryAmount - _currItemList.Count;

    for (int i = 0; i < needAmount; i++)
    {
        var lasIdx = takenItemList.Count - 1; 
        var item = takenItemList[lasIdx]; //넘겨받은 아이템 리스트의 마지막 인덱스 요소
        takenItemList.Remove(item);
        _currItemList.Add(item);        //스택할 객체의 아이템 리스트에 넣기

        var isClothes = item is Clothes;   //is 연산자를 통해 item을 상속받은 Clothes 클래스인지 다운캐스팅 가능여부 확인

        if (i == needAmount - 1) //필요한 마지막 아이템인지 확인
        {
            this.StartCoroutine(CoJumpItem(item,isClothes,doneCallback)); 
        }
        else 
        {
            this.StartCoroutine(CoJumpItem(item,isClothes));
        }

        yield return CoroutineUtil.WaitForSeconds(_itemTakeTimeInterval); 
    }
}

private IEnumerator CoJumpItem(Item item, bool isClothes = false ,Action doneCallBack = null)
{
    var itemTrans = item.transform;
    itemTrans.parent = null;
    yield return null;

    var targetTrans = FindEmptyPoint(); //지역함수를 활용해 스택 가능한 위치 확인
    var startSec = Time.time;
    var endSec = startSec + _itemMoveTimeLimit;
    var startPos = itemTrans.position;

    AudioManager.NullableInstance.PlaySFX(EAudioName.StackSound, false, false,0.25f);

    while (Time.time < endSec) //Lerp() 함수를 활용하여 아이템 이동연출
    {
        var ratio = (Time.time - startSec) / _itemMoveTimeLimit;

        if (isClothes)
        {
            itemTrans.position = Vector3.Lerp(itemTrans.position, targetTrans.position, _itemMoveCurve.Evaluate(ratio));
        }
        else
        {
            itemTrans.position = Vector3.Lerp(itemTrans.position, targetTrans.position, _itemMoveCurve.Evaluate(ratio));
        }

        itemTrans.position = itemTrans.position + Vector3.up * _itemJumpCurve.Evaluate(ratio);
        yield return CoroutineUtil.WaitForFixedUpdate;
    }

    //...

    if (doneCallBack != null) // 마지막 아이템 이었다면 done Callback 호출
    {
        AudioManager.NullableInstance.ResetPitch(EAudioName.StackSound);
        doneCallBack.Invoke();
        _takeItemCorutine = null;
        _isDuringStacking = false;
    }

    Transform FindEmptyPoint() // 빈곳 할당을 위한 지역함수
    {
        Transform pointTrans = null;

        var idx = _currItemList.Count - 1;
        pointTrans = _stackPointList[idx];

        return pointTrans;
    }
}
````
**StorageShelf**[📜 : 스크립트 전문보기](https://github.com/iLovealan1/KIm-Dong-Joon-game-client-Portfolio/blob/main/PlayableGames_Scripts/OutletRush_Playable/Shelfs/StorageShelf.cs)<br>
### 코드

&nbsp;&nbsp;&nbsp;&nbsp;● GetItemList() : IItemListReturner를 상속받은 객체가 구현한 메서드로 자신의 상황에 따라 아이템리스트와 doneCallback을 out으로 반환합니다<br>

````
public List<Item> GetItemList(out Action doneCallBack)
{
    if (_isItemSpawning)
    {
        doneCallBack = null;
        return null;
    }
    else
    {
        if (_onPlayerTakeItems != null)
        {
            doneCallBack = () => {
                _onPlayerTakeItems.Invoke(EGuideArrowState.DisplayShelf_Shoe1_Take);
                _onPlayerTakeItems = null;
                GenerateItems();
            };
        }
        else
        {
            doneCallBack = GenerateItems;
        }
    
        return _currItemList;
    }
}                
````
**DisplayShelf**[📜 : 스크립트 전문보기](https://github.com/iLovealan1/KIm-Dong-Joon-game-client-Portfolio/blob/main/PlayableGames_Scripts/OutletRush_Playable/Shelfs/DisplayShelf.cs)<br>
### 코드

&nbsp;&nbsp;&nbsp;&nbsp;● OnTriggerEnter() : 콜라이더에 접근한 객체를 레이어로 확인하여 IItemListReturner 인터페이스의 GetItemList() 메서드를 호출합니다.<br>

````
private void OnTriggerEnter(Collider other) 
{
    var type = (ELayerName)other.gameObject.layer; 

    switch(type) // 레이어 구분
    {
        case ELayerName.Player :  
        {
            if (_type == EDisplayShelfType.Shoe_2 && _isPrepared == false)
                return;

            if (_takeItemCoroutine != null)
                return;
                
            if (_currItemList.Count == _pointList.Count)
                return;

            if (other.TryGetComponent<IItemListReturner>(out IItemListReturner returner)) //TryGetComponent를 통해 GetItemList() 메서드 호출 시도
            {
                var takenItemList = returner.GetItemList(out Action doneCallBack);

                 if (takenItemList == null)
                    return;

                if (!CheckItemList(_type, takenItemList, out int availAmount))
                    return;

                _takeItemCoroutine = this.StartCoroutine(TakeItems(takenItemList,availAmount,doneCallBack));
            }

            return;
        }

        case ELayerName.Customer :
        {
            if (_type == EDisplayShelfType.Clothes)
                return;

            _waitCustomerQueue.Enqueue(other);
            return;
        }
    }       
}        
````

[📑: 목차로](#목차)

### money_stacking

![image](https://github.com/iLovealan1/KIm-Dong-Joon-game-client-Portfolio/blob/main/PlayableGames_Image/OutletRush_MoneyStack_2.gif)![image](https://github.com/iLovealan1/KIm-Dong-Joon-game-client-Portfolio/blob/main/PlayableGames_Image/OutletRush_MoneyNoneStack.gif)


### **이미지 설명(좌측 상단부터)**
- 이번 프로젝트에서는 두가지 모드가 제작되었습니다. 하나는 돈이 등 뒤로 실물로 쌓이는 모드와 그렇지 않은 모드가 있습니다.
- 첫번째는 돈이 등 뒤로 쌓이는 모드의 모습을 볼수 있습니다.
- 두번째는 돈이 유저에게 흡수되는 모드의 모습을 볼수 있습니다.

### **요약**
- 아이템 스택과 동일하게 인터페이스로 소통하지만 IMoneyStackReturner 인터페이스로 제너릭 Stack을 이용해 Money 클래스 객체들을 저장.
- 플레이어 돈 스택킹의 경우 Lerp() 함수와 사내 Tween 유틸을 활용하여 돈 스택킹 애니메이션을 구현.
- 돈이 쌓이는 위치에는 Transform 배열을 활용하여 돈이 쌓이는 위치별 childCount 를 확인하여 구분하는 방식으로 돈 스택킹 구현. 
- 자체 제작 오브젝트 Pool 클래스를 활용하여 메모리 부담을 덜 수 있게 활용.
- 돈획득시 MoneyManager static 클래스를 통해 실제 유저의 금액을 증가시키고 static 클래스 내부에선 게임 Awake 단에서 할당된 이벤트를 호출합니다.
  

### **관련 스크립트**
**IMoneyStackReturner**[📜 : 스크립트 전문보기](https://github.com/iLovealan1/KIm-Dong-Joon-game-client-Portfolio/blob/main/PlayableGames_Scripts/OutletRush_Playable/InterFaces/IMoneyStackReturner.cs)<br>
**Money**[📜 : 스크립트 전문보기](https://github.com/iLovealan1/KIm-Dong-Joon-game-client-Portfolio/blob/main/PlayableGames_Scripts/OutletRush_Playable/MoneyObject/Money.cs)<br>
**MoneyManager**[📜 : 스크립트 전문보기](https://github.com/iLovealan1/KIm-Dong-Joon-game-client-Portfolio/blob/main/PlayableGames_Scripts/OutletRush_Playable/Managers/MoneyManager.cs)<br>
**Player**[📜 : 스크립트 전문보기](https://github.com/iLovealan1/KIm-Dong-Joon-game-client-Portfolio/blob/main/PlayableGames_Scripts/OutletRush_Playable/Unit/Player.cs)<br>
### 코드

&nbsp;&nbsp;&nbsp;&nbsp;● CoTakeMoney() : 플레이어가 성공적으로 인터페이스를 가져왔을떄 호출되는 메서드입니다. <br>
&nbsp;&nbsp;&nbsp;&nbsp;● JumpMoney() : Lerp() 함수와 TweenUtil을 활용하여 돈을 스택하는 애니메이션을 실행합니다. <br>

````
private IEnumerator CoTakeMoney(Stack<Money> takenMoneyStack , Action doneCallback)
{ 
    if (!IsMoneyStackingMode) // GameManager의 설정된 모드에 따라 기능 수행
        AudioManager.NullableInstance.PlaySFX(EAudioName.MoneyTakeSound,true);
   
    while(takenMoneyStack.Count > 0)
    {
        var money = takenMoneyStack.Pop(); // 가져온 스택에서 객체 pop
        _currMoneyStack.Push(money); pop으로 꺼낸 객체 저장 pop

        var cnt = _moneyStackPoint.childCount;
        var targetPos = _moneyStackInterval * cnt;

        if (IsMoneyStackingMode)
            money.transform.parent = _moneyStackPoint; 
        else 
            money.transform.parent = null;
        

        if (takenMoneyStack.Count == 0)
        {
            doneCallback.Invoke();

            if (IsMoneyStackingMode)
                this.StartCoroutine(JumpMoney(money,targetPos,doneCallback));
            else    
                this.StartCoroutine(CoJumpMoney_NoStackVer(money,doneCallback));
        }
        else
        {
           if (IsMoneyStackingMode)
                this.StartCoroutine(JumpMoney(money,targetPos));
            else    
                this.StartCoroutine(CoJumpMoney_NoStackVer(money));
        }

        yield return CoroutineUtil.WaitForSeconds(_moneyTakeInterval);
    }
}

private IEnumerator JumpMoney(Money money, Vector3 targetPos, Action doneCallback = null)
{
    var moneyTrans = money.transform;
    var startSec = Time.time;
    var endSec = startSec + _moneyMoveTimeLimit;
    Vector3 startPos = moneyTrans.localPosition;

    // 트윈 유틸을 활용한 머니 회전
    TweenUtil.TweenLocalRotation(moneyTrans,Quaternion.Euler(new Vector3(0f, 90f,0f)),false,_itemMoveTimeLimit);

    AudioManager.NullableInstance.PlaySFX(EAudioName.MoneyStackSound,true,false, 0.05f);
 
    while (Time.time < endSec) // Lerp() 함수와 AnimationCurve클래스를 활용한 머니 스택킹 애니메이션
    {
        var ratio = (Time.time - startSec) / _moneyMoveTimeLimit;
        moneyTrans.localPosition = Vector3.Lerp(moneyTrans.localPosition, targetPos , _moneyMoveCurve.Evaluate(ratio));
        moneyTrans.localPosition = moneyTrans.localPosition + Vector3.up * _moneyJumpCurve.Evaluate(ratio);
        yield return CoroutineUtil.WaitForFixedUpdate;
    }

    moneyTrans.localPosition = targetPos; // 타겟 포지션 고정

    if (doneCallback != null) // 마지막 돈 획득시 호출되는 donecallback
    {
        AudioManager.NullableInstance.ResetPitch(EAudioName.MoneyStackSound);
        _takeMoneyCoroutine = null;

        if (_isPlayerInMoneyStacker)
            _checkMoneyGenCoroutine = this.StartCoroutine(CoCheckMoneyGen());
        
        doneCallback.Invoke();
    }

    MoneyManager.UpdateCurrentMoney(Money.Price); //  MoneyManager 스태틱 클래스의 메서드를 통해 금액 업데이트 (내부에선 콜백을 사용하여 UI와 소통)
}
````

**MoneyStacker**[📜 : 스크립트 전문보기](https://github.com/iLovealan1/KIm-Dong-Joon-game-client-Portfolio/blob/main/PlayableGames_Scripts/OutletRush_Playable/MoneyObject/MoneyStacker.cs)<br>
### 코드

&nbsp;&nbsp;&nbsp;&nbsp;● GenerateMoney() : CoGenerateMoney() 머니 생성 코루틴을 _isOkToGen 부울 값을 이용해 제어합니다 <br>
&nbsp;&nbsp;&nbsp;&nbsp;● SetMoneyPos() : 돈의 생성 위치를 Transform배열과 childCount를 활용해 조정합니다. <br>
&nbsp;&nbsp;&nbsp;&nbsp;● GetMoneyStack() : IMoneyStackReturner의 메서드로 done Callback과 께 머니 Stack을 반환합니다. <br>

````
// Awake 단에서 이벤트로 Counter 클래스의 결재 로직 마지막에 호출
public void GenerateMoney(int amount) => this.StartCoroutine(CoGenerateMoney(amount));

private IEnumerator CoGenerateMoney(int amount) //인자는 전체 머니의 양
{
    yield return CoroutineUtil.WaitUntil(() => {return _isOkToGen;}); // 머니 젠이 가능할때 까지 프레임을 넘기며 대기

    var count = Math.Round((float)amount / (float)Money.Price , 1) ; // 설정된 머니다발의 값어치 만큼 돈을 생성

    for (int i =0; i < count; i++)
    {
        var money = _moneyPool.GetMoney();
        _currMoneyStack.Push(money);
        SetMoneyPos(money);
    }
}

private void SetMoneyPos(Money money) // 머니 위치 설정
{
    Transform moneyTrans =  money.transform;
    Transform targetTrans = null;

    var minCount = 0f;
    for (int i = 0; i < _defaultPosArr.Length; i++)
    {
        var childCount = _defaultPosArr[i].childCount; // 설정된 default Transform의 자식 갯수

        if (childCount == 0) // 자식의 갯수가 0인 Transform 우선
        {
            targetTrans = _defaultPosArr[i];
            moneyTrans.parent = targetTrans;
            moneyTrans.localPosition = Vector3.zero;
            return;
        }

        if (minCount == 0) // 모든 Transform 배열 요소의 자식 갯수가 0이 아니고 최소 갯수가 0일경우
        {
            minCount = childCount;  // 최소 자식 갯수 캐싱
            targetTrans = _defaultPosArr[i]; // 타겟 포즈 설정
        }

        if (minCount > childCount) // 최소 갯수가 다음 요소 Transform의 자식 갯수보다 클경우 새로운 타겟 트랜스폼 설정
        {
            minCount = childCount;
            targetTrans = _defaultPosArr[i];
        }
    }

    moneyTrans.parent = targetTrans;
    moneyTrans.localRotation = Quaternion.identity;
    moneyTrans.localPosition = new Vector3(0, SPACINGY * minCount, 0);
}

public Stack<Money> GetMoneyStack(out System.Action doneCallback) // IMoneyStackReturner 인터페이스 메서드
{
    doneCallback = () => {
        // 돈 회수가 끝난 시점에서 호출될 콜백 정의 
        _isOkToGen = true;
        if (_onPlayerTakeMoney != null)
        {
            _onPlayerTakeMoney.Invoke(EGuideArrowState.DisplayShelf_Shoe2_Upgrade);
            _onPlayerTakeMoney = null;
        }
     };

    if (_currMoneyStack.Count == 0) // 현재 저장된 돈이 없을경우 null 반환
    {
        return null;         
    }
    else  //아니라면 돈이 회수될때까지 돈 생성 제한
    {
        _isOkToGen = false;
        return _currMoneyStack;
    }
}
````

[📑: 목차로](#목차)

### customer_move

![image](https://github.com/iLovealan1/KIm-Dong-Joon-game-client-Portfolio/blob/main/PlayableGames_Image/OutletRush_Customer_Move_2.gif)


### **이미지 설명(좌측 상단부터)**
- 각 노드에 맞게 움직이는 손님 NPC들을 확인할수 있습니다.

### **요약**
- Nav Mesh 컴포넌트 사용이 불가능한 Luna 플랫폼의 특성으로 노드 리스트를 이용한 손님 움직임 구현
- enum을 활용한 간이 상태 패턴을 사용하여 상태에 맞는 이동 로직과 노드를 사용하는 Customer Class.
- 각 손님마다 할당된 node transform 제너릭 리스트를 새로운 Queue로 복사하여 이동에 사용.
- 트윈을 이용한 움직임 구현 및 회전
- 손님 프리팹마다 각각의 메시 모델 리스트를 가지고 랜덤하게 자신의 모습을 바꾸어 입장하는 손님들
- Customer Manager Class와 이벤트 호출로 소통하며 자신의 상태 변경을 요청
  

### **관련 스크립트**
**Customer**[📜 : 스크립트 전문보기](https://github.com/iLovealan1/KIm-Dong-Joon-game-client-Portfolio/blob/main/PlayableGames_Scripts/OutletRush_Playable/Unit/Customer.cs)<br>
### 코드

&nbsp;&nbsp;&nbsp;&nbsp;● MoveCustomerToDisplayShelf() : 손님을 가게로 입장시키는 메서드입니다. <br>
&nbsp;&nbsp;&nbsp;&nbsp;● CoMoveToShelfNode() : 노드로 이동하는 손님의 로직이 담긴 메서드 입니다. <br>

````
public void MoveCustomerToDisplayShelf() // 손님이 가게로 진입하기 전 호출
{
    if (_currItemList.Count != 0)
        _currItemList[0].Disable();

    var nodeQ = new Queue<Transform>(_nodelList); // Queue로 현재 손님이 가지고 있는 리스트를 복사
    this.StartCoroutine(CoMoveToShelfNode(nodeQ));
}

private IEnumerator CoMoveToShelfNode(Queue<Transform> nodeQ)
{
    var timeLimit = 3f;

    if (_targetShelf == ECustomerShelfTarget.Clotehs)
        timeLimit = 0.8f;

    if (nodeQ.Count == 1) // 마지막 노드일 경우 시간 설정
    {
        if (_targetShelf == ECustomerShelfTarget.Shelf1)
            timeLimit = 0.5f;
        else if (_targetShelf == ECustomerShelfTarget.Clotehs)
            timeLimit = 0.4f;
        else 
            timeLimit = 1f;
    }

    if (nodeQ.Count != 0) // 노드 Queue가 Dequeue 가능한 상태일 경우
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
            this.StartCoroutine(CoMoveToShelfNode(nodeQ)); // 목적지에 도착시 재귀적으로 호출
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
````

**CustomerManager**[📜 : 스크립트 전문보기](https://github.com/iLovealan1/KIm-Dong-Joon-game-client-Portfolio/blob/main/PlayableGames_Scripts/OutletRush_Playable/Unit/Customer.cs)<br>
### 코드

&nbsp;&nbsp;&nbsp;&nbsp;● ChangeCustomerState() : 호출된 손님의 상태에 따라 상태를 재정의 합니다. <br>
&nbsp;&nbsp;&nbsp;&nbsp;● FindEmptyWaitNode() : 줄서기 로직을 담은 메서드입니다. 새치기를 방지하기 위해 현재 줄선 위치를 기억하여 다음 줄로 이동시킵니다. <br>

````
private ECustomerState ChangeCustomerState(ECustomerState currState, Customer requestedCutomer)
{
    var newState = ECustomerState.None;

    switch (currState) // 호출된 손님의 상태에 따라 분기
    {
        case ECustomerState.Shopping : 
        {
            newState = ECustomerState.BeforeWaitNode;
            break;
        }
        case ECustomerState.BeforeWaitNode : // 손님이 물건을 매대에서부터 수령하여 줄을 서야하는 상태일경우
        {
            newState = FindEmptyWaitNode(requestedCutomer);
            break;
        }
        case ECustomerState.PurchaseNode : 
        {
            break;
        }
        default : // 손님의 상태에 따라 다음 줄서는 위치를 선정
        {
            var idx = (int)currState - 1;

            if (_waitNodeList[idx].childCount == 0)
            {
                requestedCutomer._currTargetWaitNode = _waitNodeList[idx];
                newState = (ECustomerState)idx;

            }
            else
            {
                requestedCutomer._currTargetWaitNode = null;
                newState = requestedCutomer._currState;
            }
            break;
        }
    }

    return newState;
}

private ECustomerState FindEmptyWaitNode(Customer requestedCutomer) // 손님이 처음 줄서는 위치를 선정
{
    var newState = ECustomerState.None;

    for (int i = 0; i< _waitNodeList.Count; i++ ) // Manager가 보유중인 줄의 자식 수를 파악하여 목적지 선정
    {
        var node = _waitNodeList[i];

        if (node.childCount == 0)
        {
            requestedCutomer._currTargetWaitNode = node;
            newState = (ECustomerState)i;
            break;
        }
    }
    
    if (newState == ECustomerState.None)
    {
        requestedCutomer._currTargetWaitNode = null;
        newState = requestedCutomer._currState;
    }

    return newState;
}
````

[📑: 목차로](#목차)

---

# 🟢: Burger Please! Playable<br>[📂:폴더이동](https://github.com/iLovealan1/KIm-Dong-Joon-game-client-Portfolio/tree/main/PlayableGames_Scripts/BurgerPlease_Playable)


### manager_init
![image](https://github.com/iLovealan1/KIm-Dong-Joon-game-client-Portfolio/blob/main/PlayableGames_Image/Burger_Graph.png)


### **이미지 설명(최상단부터)**
- Manager 와 Controller 간의 Init() 관계도입니다.

### **요약**
- App 클래스의 유니티 라이프사이클 Awake() 메서드 단에서 시작되는 Init 구조를 활용하여 각 객체별 이니셜라이징 순서를 조정.
- 각 Manager 클래스는 관리대상으로 소유중인 Controller 클래스들의 이니셜라이징과 함께 자신을 초기화.
- 각 Manager 클래스는 외부 매니저 혹은 객체와 소통할수 있도록 이벤트 등록.

### **상세 내용**
**App**[📜 : 스크립트 보기](https://github.com/iLovealan1/KIm-Dong-Joon-game-client-Portfolio/blob/main/PlayableGames_Scripts/BurgerPlease_Playable/Manager/App.cs)<br>
**GameManager**[📜 : 스크립트 보기](https://github.com/iLovealan1/KIm-Dong-Joon-game-client-Portfolio/blob/main/PlayableGames_Scripts/BurgerPlease_Playable/Manager/GameManager.cs)<br>
**CounterManager**[📜 : 스크립트 보기](https://github.com/iLovealan1/KIm-Dong-Joon-game-client-Portfolio/blob/main/PlayableGames_Scripts/BurgerPlease_Playable/Manager/CounterManager.cs)<br>
**PickupManager**[📜 : 스크립트 보기](https://github.com/iLovealan1/KIm-Dong-Joon-game-client-Portfolio/blob/main/PlayableGames_Scripts/BurgerPlease_Playable/Manager/PickupManager.cs)<br>
**BurgerMachineManager**[📜 : 스크립트 보기](https://github.com/iLovealan1/KIm-Dong-Joon-game-client-Portfolio/blob/main/PlayableGames_Scripts/BurgerPlease_Playable/Manager/BurgerMachineManager.cs)<br>
**CustomerManager**[📜 : 스크립트 보기](https://github.com/iLovealan1/KIm-Dong-Joon-game-client-Portfolio/blob/main/PlayableGames_Scripts/BurgerPlease_Playable/Manager/CustomerManager.cs)<br>
**UIManager**[📜 : 스크립트 보기](https://github.com/iLovealan1/KIm-Dong-Joon-game-client-Portfolio/blob/main/PlayableGames_Scripts/BurgerPlease_Playable/UI/UIManager.cs)<br>


[📑: 목차로](#목차)

### crowd_control

![image](https://github.com/iLovealan1/KIm-Dong-Joon-game-client-Portfolio/blob/main/PlayableGames_Image/Burger_customer_Point.png)
![image](https://github.com/iLovealan1/KIm-Dong-Joon-game-client-Portfolio/blob/main/PlayableGames_Image/BurgerPlayable_CustomerLine.gif)


### **이미지 설명(최상단부터)**
- 각 카운터와 픽업대 별로 손님들이 줄을 설수 있는 Node 기즈모 들을 볼수 있습니다.
- 입장노드 → 카운터노드 → 픽업대노드 → 출구노드 순으로 상태에 따라 이동하는 손님들을 볼수 있습니다.

### **요약**
- Counter, Pickup 객체마다 소유중인 Node를 관리.
- 각 노드가 Available 하다면 해당 객체의 Manager와 이벤트로 소통하여 손님의 이동 가능여부를 Customer Manager에 전달하여 손님을 이동.
- 각 노드들은 가중치를 주어 현재 손님이 있는 노드의 가중치와 비교하여 새치기를 방지.
- Customer Manager는 각 손님으로부터 전달받은 상태를 기반으로 어떤 매니저에게 소통할지 여부 결정.
- 손님들의 상태는 enum으로 정의하여 손님들은 자신의 상태를 기반으로 유동적인 상태변환.

### **상세 내용**
**CustomerManager**[📜 : 스크립트 보기](https://github.com/iLovealan1/KIm-Dong-Joon-game-client-Portfolio/blob/main/PlayableGames_Scripts/BurgerPlease_Playable/Manager/CustomerManager.cs)<br>
### 코드

&nbsp;&nbsp;&nbsp;&nbsp;● SetupByStateAndMoveCustomer() : Init 단계에서 정의된 이벤트로 Customer Class를 전달받아 상태에 따라 다음 행동을 정의합니다.<br>
&nbsp;&nbsp;&nbsp;&nbsp;● CoMoveCustomerToSpot() : 손님의 상태에따라 재귀적으로 호출되는 손님 이동 코루틴입니다. 이동은 TimeLimit과 목적지 node와의 거리를 기반하여 
Translate() 메서드를 이용해 이동합니다.<br>

````
private IEnumerator SetupByStateAndMoveCustomer(CustomerController customerComp)
{
    var moveTimeLimit = 0f;
    var isFull = false;

    switch (customerComp._state)
    {
        case eCustomerState.SPAWNTOSPOT:               
        isFull = SetupSpawnToSpot(customerComp);
        if (isFull)
            break;
        else
            moveTimeLimit = customerComp.MoveTimeLimit;
        break;

        case eCustomerState.COUNTERMIDLINESPOT:    
        yield return StartCoroutine(CoSetupCounterMidlineSpot(customerComp));
        moveTimeLimit = customerComp.MoveTimeLimit;
        break;

        case eCustomerState.COUNTERORDERSPOT:
        yield return StartCoroutine(CoSetupCounterOrderSpot(customerComp));
        moveTimeLimit = customerComp.MoveTimeLimit;
        break;

        case eCustomerState.PICKUPMIDSPOT:
        yield return StartCoroutine(CoSetupPickupMidSpot(customerComp));
        moveTimeLimit = customerComp.MoveTimeLimit;
        break;

        case eCustomerState.BACKTOHOME:
        StartCoroutine(CoBackToHome(customerComp)); 
        yield break;
    }
    
    if (!isFull)
        StartCoroutine(CoMoveCustomerToSpot(customerComp,moveTimeLimit));
}

private IEnumerator CoMoveCustomerToSpot(CustomerController customerComp, float moveTimeLimit)
{
    var customerTrans = customerComp.transform;
    var targetComp = customerComp.TargetSpotComp;
    var startPos = customerTrans.position;
    var endPos = targetComp.transform.position;
    var dir = (endPos - startPos).normalized; 
    var dist = Vector3.Distance(startPos, endPos); 
    var timer = 0f;     

    customerTrans.LookAt(endPos);
    customerComp.OnChangeAnimState(eUnitAnimState.RUN);   

    while(true)
    {
        dist = Vector3.Distance(customerTrans.position,endPos);         
        timer += Time.fixedDeltaTime;
        if(dist < _minDist || timer > moveTimeLimit) 
        {
            customerTrans.position = endPos;
            customerComp.CurrSpotComp = targetComp; 
            customerTrans.localRotation = Quaternion.Euler(Vector3.zero); 
            customerComp.OnChangeAnimState(eUnitAnimState.IDLE);
            customerComp.OnNextMove();
            yield break;
        }
        customerTrans.Translate(dir * _customerSpeed * Time.fixedDeltaTime,Space.World);
        yield return CoroutineUtil.WaitForFixedUpdate;
    }
}
````
**WaitLineSpot**[📜 : 스크립트 보기](https://github.com/iLovealan1/KIm-Dong-Joon-game-client-Portfolio/blob/main/PlayableGames_Scripts/BurgerPlease_Playable/Unit/WaitLineSpot.cs)<br>
**CounterController**[📜 : 스크립트 보기](https://github.com/iLovealan1/KIm-Dong-Joon-game-client-Portfolio/blob/main/PlayableGames_Scripts/BurgerPlease_Playable/Unit/CounterController.cs)<br>
**PickupController**[📜 : 스크립트 보기](https://github.com/iLovealan1/KIm-Dong-Joon-game-client-Portfolio/blob/main/PlayableGames_Scripts/BurgerPlease_Playable/Unit/PickupController.cs)<br>
### 코드

&nbsp;&nbsp;&nbsp;&nbsp;● FindEmptySpot() : 현재 새치기가능 여부를 판단하여 가중치를 비교하여 빈 Node를 반환합니다.<br>
&nbsp;&nbsp;&nbsp;&nbsp;● CheckEmptySpotsAndMakeList() : 현재 비어있는 Node를 제너릭 List로 반환합니다.<br>

````
public Transform FindEmptySpot()
{
    if (!IsWaitAvailable) 
        return null;     

    Transform emptySpot = null;
    bool isFull;
    List<WaitLineSpot> emptySpotCompList = CheckEmptySpotsAndMakeList(out isFull); // 현재 줄 상태 리스트

    if(isFull) // 빈 곳이 없다면
    {
        return emptySpot;   
    }       

    if(!emptySpotCompList.Contains(null)) // 전부 빈 곳이라면
    {
        emptySpot = emptySpotCompList[0].transform;

        emptySpotCompList.Clear();
        emptySpotCompList = null;

        return emptySpot;   
    } 

    var emptyMinWeight = 0;
    var fullMaxWeight = 0;
    var isEmptyFound = false;
    var isCutLinePossible = false;

    for (int i = 0; i < _spotCount; i++) // 새치기 가능여부 확인 부분
    {
        var targetSpot = emptySpotCompList[i];
        if (targetSpot == null) // 스팟에 손님이 있다면
        {
            if(isEmptyFound) // 스팟에 손님이 없는곳 뒤에 손님이 있다면
            {
                isCutLinePossible = true;
            }
            fullMaxWeight = i+1;
        }   
        else // 스팟에 손님이 없다면
        {           
            isEmptyFound = true;
            var targetWeight = targetSpot.Weight;   

            if (emptyMinWeight == 0)
            {
                emptyMinWeight = targetWeight;
            }
        }
    }    

    if(isCutLinePossible) // 대기라인 스팟 확정
    {
        if (fullMaxWeight != _spotCount)
            emptySpot = _lineSpotTransList[fullMaxWeight];
    }
    else
    {
        var idx = emptyMinWeight - 1;
        emptySpot = _lineSpotTransList[idx];    
    }

    return emptySpot;             
}    

private List<WaitLineSpot> CheckEmptySpotsAndMakeList(out bool isFull) //손님이 없는 대기라인을 찾기
{
    List<WaitLineSpot> tempTransList = new List<WaitLineSpot>();
    isFull = true;

    for (int i = 0; i < _spotCount; i++)
    {
        if(_lineSpotTransList[i].childCount == 0)
        {
            tempTransList.Add(_lineSpotCompList[i]);    // 비어있는 Node가 한곳이라도 있다면
            isFull = false;
        }
        else
        {
            tempTransList.Add(null);  // 만약 비어있지 않다면 Null 할당
        }
    }
    
    if (isFull) // 리스트의 Node들이 전부 null이라면
    {
        tempTransList.Clear();
        tempTransList = null;
        return tempTransList;
    }

    return tempTransList;      
}
````

[📑: 목차로](#목차)

**CounterManager**[📜 : 스크립트 보기](https://github.com/iLovealan1/KIm-Dong-Joon-game-client-Portfolio/blob/main/PlayableGames_Scripts/BurgerPlease_Playable/Manager/CounterManager.cs)<br>
**PickupManager**[📜 : 스크립트 보기](https://github.com/iLovealan1/KIm-Dong-Joon-game-client-Portfolio/blob/main/PlayableGames_Scripts/BurgerPlease_Playable/Manager/PickupManager.cs)<br>
### 코드

&nbsp;&nbsp;&nbsp;&nbsp;● FindWaitLineSpotForCustomer() : Active 되어있는 Pickup 객체들을 모두에게서 사용 가능한 Node를 받아 반환받습니다.<br>
&nbsp;&nbsp;&nbsp;&nbsp;● FindMidLineSpotForCustomer() : 이미 줄을 한가운데 있는 손님의 앞 노드가 비어있는지 확인하고 노드를 반환합니다.<br>
&nbsp;&nbsp;&nbsp;&nbsp;● CompareWeight() :Active 되어있는 Pickup 객체들의 레벨과 노드 가중치를 비교하여 손님을 어디로 보낼지 판단하고 최종노드를 반환합니다.<br>


````
 private Transform FindWaitLineSpotForCustomer() // 손님이 처음 pickup으로 들어올 경우.
{
    Transform nullableWaitLineSpot = null;
    var transList = new List<Transform>();  

    foreach(var pickup in _pickupList) // 활성화된 모든 픽업대 객체들로 부터 사용가능한 node 반환
    {
        if(pickup.gameObject.activeSelf || pickup.UnitLevel == 1)
        {
            var emptySpotTrans = pickup.FindEmptySpot();
            transList.Add(emptySpotTrans); // 새로운 리스트에 담기
        }
    }

    nullableWaitLineSpot = CompareWeight(transList);   // 가중치 비교
    return nullableWaitLineSpot; // 최종 위치 전달
}

private Transform FindMidLineSpotForCustomer(eCustomerLevel customerLevel, WaitLineSpot waitLineSpotComp) // 손님이 줄을 서던 도중 노드 요청을 할 경우.
{
    Transform nullableWaitLineSpot = null;
    var idx = (int)customerLevel;
    nullableWaitLineSpot = _pickupList[idx - 1].FindMidEmptySpot(waitLineSpotComp);        
    return nullableWaitLineSpot;
}

private Transform CompareWeight(List<Transform> transList) // pickup 객체들의 레벨과 각각 전달받은 노드들의 가중치를 비교.
{
    Transform finalSpot = null;
    var minWeight = 0;
    var defaultTransCnt = 1;
    var count = transList.Count;

    foreach (var spotTrans in transList)
    {
        if(count == defaultTransCnt) // 노드가 1개밖에 없을 경우.
        {
            finalSpot = spotTrans;
            return finalSpot;
        }              

        if(spotTrans != null) // 노드가 2개 이상일 경우 비교.
        {
            var comp = spotTrans.GetComponent<WaitLineSpot>(); 
            if(minWeight == 0) 
            {
                minWeight = comp.Weight;
                finalSpot = spotTrans;
            }
            else if(minWeight > comp.Weight)
            {                      
                minWeight = comp.Weight;
                finalSpot = spotTrans;      
            }
            else continue;
        }
    }   
    return finalSpot;
}
````

[📑: 목차로](#목차)

---
