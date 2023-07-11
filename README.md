![header](https://capsule-render.vercel.app/api?type=waving&color=gradient&height=250&section=header&text=UnityEngine3D%20클라이언트%20개발자%20김동준%20포트폴리오&fontSize=35)
<br>
소개
=============

유니티엔진 클라이언트 개발자직 지원자 김동준(Alan Kim)의 포트폴리오에 오신걸 환영합니다.<br>
-------------
저는 개발자 지망생으로 2022.12 부터 2023.06 까지 반년간의 직업훈련을 받았습니다.<br>
개발 참여 작품으로는 유니티엔진을 활용하여 제작,발매한 "건즈앤레이첼스"가 있습니다.<br>

:page_facing_up: : 개발자 블로그 : [Blog Link](https://bueong-e.tistory.com)


⬇️: "건즈앤레이첼스" 프로젝트에서 담당했던 개발 목록입니다.<br>

# 목차
- - -
:red_circle: A*알고리즘을 활용한 절차적 랜덤 맵 생성 스크립트 제작.[바로가기](#Astar_Random_Map_Generator)<br>
:red_circle: Main to Director 스크립트를 이용한 씬 전환 및 스테이지 전환.[바로가기](#Main_Directors)<br>
:red_circle: 스테이지 루프 로직 및 씬전환에 필요한 데이터 연동 구조 기획 및 제작.[바로가기](#Main_Directors)<br>
:red_circle: 유니티 타일맵 컴포넌트를 사용한 마을 & 던전 레벨 디자인.<br>
* * *
:green_circle: 스탯인벤토리 제작.[바로가기](#StatInventory)<br>
:green_circle: 상자 출현 아이템 생성 구조 설계 및 스크립트 제작.[바로가기](#Chest_ItemGenerator)<br>
:green_circle: 오브젝트 풀링을 이용한 필드 출현 Coin 스크립트 및 필드 획득 아이템 제작[바로가기](#Field_Items).<br>
:green_circle: UniRx 플러그인을 활용한 필드 아이템 터치 조작 기획 및 스크립트 제작(+ DOTween 획득 애니메이션 코드제어).<br>
* * *
:large_blue_circle: **제작한 GUI 리스트 (구성,기획,스크립트 제작)**<br>
 &nbsp;&nbsp;&nbsp;&nbsp; :heavy_check_mark: 일시정지화면 UI <br>
 &nbsp;&nbsp;&nbsp;&nbsp; :heavy_check_mark: 게임 오버 UI <br>
 &nbsp;&nbsp;&nbsp;&nbsp; :heavy_check_mark: 재화 UI(디자인)<br>
 &nbsp;&nbsp;&nbsp;&nbsp; :heavy_check_mark: 다이얼로그 UI<br>
 &nbsp;&nbsp;&nbsp;&nbsp; :heavy_check_mark: 아이템 필드 팝업 UI<br>
 &nbsp;&nbsp;&nbsp;&nbsp; :heavy_check_mark: NPC 월드 팝업 UI <br>
 &nbsp;&nbsp;&nbsp;&nbsp; :heavy_check_mark: 가이드 애로우 UI <br>
 &nbsp;&nbsp;&nbsp;&nbsp; :heavy_check_mark: 안내멘트 팝업 UI<br>
 &nbsp;&nbsp;&nbsp;&nbsp; :heavy_check_mark: 미니맵 UI<br>
:large_blue_circle: **싱글톤 패턴 스크립트 제작 리스트**.<br>
 &nbsp;&nbsp;&nbsp;&nbsp; :heavy_check_mark: InfoManager Script <br>
 &nbsp;&nbsp;&nbsp;&nbsp; :heavy_check_mark: DataManager Script <br>
 &nbsp;&nbsp;&nbsp;&nbsp; :heavy_check_mark: AudioManager Script (외부 플러그인기반으로 프로젝트에 맞게 새로 제작한 Mono 싱글톤 스크립트)<br>
 &nbsp;&nbsp;&nbsp;&nbsp; :heavy_check_mark: AtlasManager Script (Mono 싱글톤 스크립트)<br>
 &nbsp;&nbsp;&nbsp;&nbsp; :heavy_check_mark: EventDispatcher Script (객체간의 원활한 통신을 위해 제작)<br>
 * * *
:yellow_circle: 메인 카메라 로직 제작 (User Following Cam 및 Hit Effect 연출).<br>
:yellow_circle: 디파짓 시스템 핵심 기능 제작.<br>
:yellow_circle: 포스트 프로세싱 Bloom + 도트 스타일의 게임 에셋을 이용한 게임의 전체적인 맵 비주얼및 톤앤 매너 정리.<br>
:yellow_circle: 그외 관련 스크립트들의 메모리 최적화 작업.<br>
* * *

# 발매작 : 건즈앤레이첼스 (GunsN'Rachels) 개요

![GraphicImage](https://github.com/iLovealan1/KIm-Dong-Joon-game-client-Portfolio/assets/124248265/fe788bde-68c5-4185-a71d-fa9520d29ff5)


## 🎥: **홍보영상**<br>
[![유튜브 동영상](https://img.youtube.com/vi/uf8yAuG5YM0/0.jpg)](https://www.youtube.com/watch?v=uf8yAuG5YM0)<br>
*클릭시 유튜브 링크로 연결됩니다.

##

- **제목** : 건즈앤레이첼스(Guns N` Rahcels)<br>
- **장르** : 로그라이크 핵앤슬래시 액션 슈터<br>
- **엔진** : UnityEngine3D<br>
- **플랫폼** : iOS , Android<br>
- **출시일** : 2023. 6. 7 (Android)  2023. 6. 16 (iOS)<br>
- **제작** : Team Vizeon<br>

*"건즈앤레이첼스" 는 현재 구글플레이스토어 와 앱스토어 양대마켓에서 "건즈앤레이첼스" 를 검색하여 지금 플레이 하실수 있습니다.

:iphone: iOS : [AppStore Link][iOS Link]

[iOS Link]: https://apps.apple.com/us/app/%EA%B1%B4%EC%A6%88%EC%95%A4%EB%A0%88%EC%9D%B4%EC%B2%BC%EC%8A%A4/id6450149470

:iphone: Android : [Google PlayStore Link][GooglePlayStore Link]

[GooglePlayStore Link]: https://play.google.com/store/apps/details?id=com.teamvizeon.gunsandrachels&hl=ko
## 🎮:게임의 특징

- 플레이 할때마다 맵이 바뀌는 바뀌는 로그라이크 스타일의 액션 슈터
- 게임을 클리어한 뒤에도 이어지는 윤회시스템
- RPG요소를 차용한 인벤토리 시스템과 스탯 강화 시스템
- 4종류의 특색있는 총기를 이용한 핵앤 슬래시 액션
- 디파짓 시스템을 통한 재화 수집 및 캐릭터 강화를 위한 에테르 수집
- 12종류의 특색있는 스킬들

## 📜: 제작 스크립트 설명

* * *

:red_circle: A*알고리즘을 응용한 랜덤맵 생성기 [코드보기](https://github.com/iLovealan1/KIm-Dong-Joon-game-client-Portfolio/tree/main/Scripts/Astar_MapGenerator)
===
### Astar_Random_Map_Generator
![AstarDungeonMap_Generator](https://github.com/iLovealan1/KIm-Dong-Joon-game-client-Portfolio/assets/124248265/8a6c67fa-e122-4def-9e5a-fb8a2a20fad7)
### **이미지 설명**
- 1스테이지 에서 랜덤으로 생성되는 맵 종류의 일부을 볼수 있습니다. (보라색은 시작맵, 빨간색은 보스맵, 초록색은 상점맵, 노란색은 히든맵)

### **요약**
- A*알고리즘을 반대로 적용한 절차적 랜덤맵 생성기.
- 랜덤알고리즘의 경우 매번 새로운 시드생성을 위해 (다채로운 랜덤을 위해) Unity.Random이 아닌 System.Random 클래스를 사용.
- 2차원 배열,리스트 자료구조 사용, LINQ를 사용하여 관리.
- 타일맵 컴포넌트를 하나의 스프라이트로 변경해주는 TilemapToSprite 스크립트
  
### **상세 내용**
- A* 알고리즘을 이용하여 절차적인 맵 생성기를 제작하였습니다.

- 맵 생성기는 Init() 메서드를 호출하여 맵을 생성합니다. 맵생단계는 아래와 같습니다.
1. InfoManager를 참조하여 현재 유저의 던전 정보를 가져와 2차원 배열맵의 Maxcol 과 MaxRow를 랜덤하게 정합니다.
2. 생성된 2차원 인덱스 값 X,Y에 Vector값을 곱해 (맵 사이간의 거리가 됩니다.) 2차원 벡터 배열을 생성합니다.
3. 이렇게 생성된 2차원 배열중 절반의 인덱스를 래덤하게 선정하고 각각의 거리를 비교하여 직선거리가 가장 먼 인덱스 두개와 거리가 가장 가까운 인덱스 2개를 선정합니다.
4. 시작맵, 보스맵, 상점맵 세개의 고정맵을 해당 위치에 스폰 시킵니다.
5. A* 알고리즘을 반대로 적용하여 시작맵부터 보스맵까지 가중치가 가장 높은 포지션을 선정해 일반맵을 생성합니다. 이후 같은 방법으로 시작맵부터 상점맵까지 일반맵을 생성합니다.
6. 일반맵중 랜덤한 맵을 선택해 히든맵(보상맵) 으로 변경합니다.
7. 생성된 맵 사이사이를 이어주는 포탈을 생성합니다.

- 타일맵 컴포넌트를 가진 오브젝트를 Instanciate 하는 것이 불가능하여 타일맵을 하나의 스프라이트 .png 확장자로 변경해주는 TilemapToSprite 스크립트를 제작하였습니다.
- TilemapToSprite 스크립트는 메인 카메라로 이미지를 촬영후 RenderTexture 클래스를 사용하여 렌더 텍스쳐에서 스프라이트로 변환후 파일로 저장하는작업을 수행합니다.
- PortalController 스크립트는 ePortaltype 필드로 포탈 타입을 지정해 OnTriggerEnter2D 메서드로 플레이어의 진입여부를 판단합니다.
- EventDispatcher 싱글톤 스크립트를 이용해 DungeonMain는 이벤트를 수신하여 유저를 이동시킵니다.
- InfoManager 스크립트와 GPGSManager 싱글톤 스크립트와 통신해 유저의 스테이지 정보를 변경하거나 도전과제를 달성시킵니다.
- IDungeonBossHandler 인터페이스를 제작해 보스 제작 팀원과 원활한 협업

[목차로](#목차)

* * *

:red_circle: Main to Director 스크립트를 이용한 씬 전환 및 스테이지 전환 [코드보기](https://github.com/iLovealan1/KIm-Dong-Joon-game-client-Portfolio/tree/main/Scripts/Main%26Director)
===
### Main_Directors
![씬전환](https://blog.kakaocdn.net/dn/pFvfH/btsezDXGikn/hQxYW7efHIi3mff72kzGk0/img.gif)
![포탈이펙트](https://blog.kakaocdn.net/dn/bnDJvn/btserNfbalg/kfAk5YvwLhBA02UyKZS231/img.gif)
![StagePortal](https://github.com/iLovealan1/KIm-Dong-Joon-game-client-Portfolio/assets/124248265/9d9333a2-1209-4313-b7e7-84810ddd698b)
### **이미지 설명(왼쪽 상단부터)**
-  씬전환 모습, 포탈이동 모습, 스테이전 전환 연출을 볼수 있습니다.

### **요약**
- 앱의 시작부터 종료까지 게임의 씬전환, 기초 데이터 로드를 책임지는 최상위 단의 App class 스크립트(Mono)
- 각 씬의 Initialize를 담당하는 Main 스크립트, 씬의 스크립트 실행 주기를 Init()메서드를 통해 관리합니다.
- 각 씬의 UI들을 관리하는 Director 스크립트 씬의 UI 실행 주기를 Init()메서드를 통해 관리합니다.

### **상세 내용**
**App**<br>
 &nbsp;&nbsp;&nbsp;&nbsp;●App 스크립트는 씬전환을 담당하며 앱의 시작과 끝까지 살아 있는 스크립트입니다.<br>
 &nbsp;&nbsp;&nbsp;&nbsp;●App 스크립트는 게임의 시작시 저장데이터의 유무여부에 따라 유저가 신규유저인지 기존유저인지 판단합니다.<br>
 &nbsp;&nbsp;&nbsp;&nbsp;●App 스크립트는 GPGS 플러그인과 Firebase 플러그인의 Authenticate 를 담당합니다.<br>
 &nbsp;&nbsp;&nbsp;&nbsp;●App 스크립트는 유저가 앱을 벗어나 홈화면으로 나갈시 UIPauseDirector의 씬 존재유무에 따라 앱을 정지합니다.<br>
 &nbsp;&nbsp;&nbsp;&nbsp;●App 스크립트는 SceneArgs클래스를 사용, 유저가 어떤 씬에서 넘어왔는지에 대한 정보를 다음  LoadingSceneMain 스크립트에 전달합니다.<br>
 **SanctuarySceneMain**<br>
 &nbsp;&nbsp;&nbsp;&nbsp;●SanctuarySceneMain 스크립트는 마을맵의 오브젝트들을 Initialize 하며 관리합니다.<br>
 &nbsp;&nbsp;&nbsp;&nbsp;●SanctuarySceneMain 스크립트는 유저의 스폰 및 파티클시스템을 이용한 연출을 담당합니다.<br>
 **DungeonSceneMain**<br>
 &nbsp;&nbsp;&nbsp;&nbsp;●DungeonSceneMain 스크립트는 유저의 현재 스테이지 정보에 따라 맵의 생성수를 조절합니다. (while문 사용)<br>
 &nbsp;&nbsp;&nbsp;&nbsp;●DungeonSceneMain 스크립트는 유저의 스테이지 이동시 UIDungeonLoadingDirector 스크립트의 화면 전환 연출 사이 맵과 데이터를 불러옵니다.<br>
 &nbsp;&nbsp;&nbsp;&nbsp;●DungeonSceneMain 스크립트는 유저가 포탈에 접근시 해당 포탈의 목적지인 다음 포탈까지 유저를 이동시킵니다.<br>
 &nbsp;&nbsp;&nbsp;&nbsp;●DungeonSceneMain 스크립트는 카메라 연출과 파티클시스템 사용을 통한 연출 또한 담당합니다.<br>
 **UIDungeonDirector**<br>
 &nbsp;&nbsp;&nbsp;&nbsp;●UIDungeonDirector 스크립트는 Stack 자료구조를 사용하여 휴대전화의 뒤로가기 버튼에 대응합니다. UI팝업창이 뜬 순서대로  Stack에 쌓아 순서대로 UI를 종료 비활성화 시킵니다.<br>
 &nbsp;&nbsp;&nbsp;&nbsp;●UIDungeonLoadingDirector 스크립트는 DOTween을 이용한 포탈 이동 연출을 사용하였습니다.<br>

[목차로](#목차)

* * *

:green_circle:스탯인벤토리 [코드보기](https://github.com/iLovealan1/KIm-Dong-Joon-game-client-Portfolio/tree/main/Scripts/Main%26Director)
===
### StatInventory
![InventoryOverall](https://github.com/iLovealan1/KIm-Dong-Joon-game-client-Portfolio/assets/124248265/a52d87ba-f218-4244-a532-4b5cbb139158)
![InventoryOverall](https://blog.kakaocdn.net/dn/dKQf9B/btsgGkg6zYn/BhQiJuaHqKgrB1ktOgdtqk/img.gif)
![InventoryOverall](https://blog.kakaocdn.net/dn/crhbgQ/btsgDOw8JaM/SDogCa8f4zpLf6KGJjGaik/img.gif)<br>
![InventoryOverall](https://img1.daumcdn.net/thumb/R1280x0/?scode=mtistory2&fname=https%3A%2F%2Fblog.kakaocdn.net%2Fdn%2FLnXhH%2FbtsitZwiQ9B%2F076offkN08cQfXz9fTMGO1%2Fimg.png)
![InventoryOverall](https://img1.daumcdn.net/thumb/R1280x0/?scode=mtistory2&fname=https%3A%2F%2Fblog.kakaocdn.net%2Fdn%2Fb51xQF%2Fbtsivf6tlpI%2FjPDVopBeczpTC7D99K4ibK%2Fimg.png)

### **이미지 설명(최상단부터)**
- 스탯인벤토리의 전체적인 기능 사용 모습입니다.
- 단일 아이템 획득 연출 & 다중 아이템 획득 연출
- 실시간 아이템 갯수 표시 UI (여유&꽉참)

### **요약**
- 스탯인벤토리는 유저의 스탯과 인벤토리 아이템을 한번에 표시해주는 역할.
- UGUI를 활용하여 제작, Scroll Rect, Mask, Content Size Filter, Grid Rayout Group 내장 컴포넌트를 활용.
- 추상팩토리 패턴, 구조체 클래스와 상속을 활용한 아이템 스탯 적용.
- JsonConvert.DeserializeObject 메서드를 활용한 아이템 데이터 역직렬화.
- DOTween을 활용한 애니메이션 제어.
- 포스트 프로세싱 Bloom 효과와 All in 1 Shader 외부 플러그인을 활용한 비주얼 향상.
- IDragHandler, IPointerDownHandler 인터페이스를 상속받은 터치 인풋 제어
- DataManager, InfoManager 싱글톤 클래스와 변하는 값의 실시간 데이터 공유

### **상세 내용**
- ContentGrid클래스는 IDragHandler, EquipmentBG클래스는 IPointerDownHandler 인터페이스를 상속받아 터치 인풋을 처리하며 터치시 System.Action을 통해 자식으로 들어온 아이템의 이름과 HashCode 정보를 인벤토리 클래스에 전달합니다.
- 유저가 아이템을 획득시 추상 팩토리 패턴을 사용해 획득한 아이템을 인벤토리에 생성합니다. [코드](https://github.com/iLovealan1/KIm-Dong-Joon-game-client-Portfolio/tree/main/Scripts/Inventory/ABSFactory)
- 유저가 아이템을 획득시 Stat 구조체 클래스(스탯 필드 값을 가진 구조체) 변수를 가진 Equipment 클래스를 상속받은 4종류의 장비 아이템 클래스의 능력치를 InfoManager에 전달하여 유저의 능력치를 상승시킵니다.[코드](https://github.com/iLovealan1/KIm-Dong-Joon-game-client-Portfolio/tree/main/Scripts/Inventory/Equipment)
- 유저가 총기를 변경시 변경된 총기의 스탯을 반영하여 InfoManager에 전달, 유저의 스탯를 실시간으로 표시합니다.
- 인벤토리 아이콘은 아이템의 갯수를 실시간으로 체크하며 인벤토리 아이템 갯수에 따라 여유공간이 있는지 여부를 아이콘 하단에 표시합니다.
- 유저가 아이템을 획득시 아이콘으로 빨려들어가는 연출과 연속해서 아이템을 획득시 점점 커지는 애니메이션을 DOTween을 이용해 제어합니다
- DataManager 싱글톤 스크립트를 이용해 JsonConvert.DeserializeObject 외부 플러그인을 사용하여 json 파일을 역직렬화 한 Data를 이용해 스탯과 아이템의 설명, 이름을 가져옵니다.


[목차로](#목차)

* * *
:green_circle:상자 생성 및 아이템 생성[코드보기](https://github.com/iLovealan1/KIm-Dong-Joon-game-client-Portfolio/tree/main/Scripts/Chest%26ItemGenerator)
===
### Chest_ItemGenerator
![image](https://blog.kakaocdn.net/dn/by0xsP/btsibv2StgN/rmt9xJfzpsOw256gV9oUSk/img.gif)
![ChestAppear](https://github.com/iLovealan1/KIm-Dong-Joon-game-client-Portfolio/assets/124248265/d19ac2aa-c489-4cfc-a558-a7f09fe3e6dd)


### **이미지 설명(최상단부터)**
- UINPCPopupDirector 스크립트를 이용한 월드 UI, 상자 아이템 생성 및 연출.
- 상자 생성 연출(코인 일괄 획득 연출과 안내 UI 팝업, 체스트 가이드 애로우 확인이 가능합니다.).

### **요약**
- NPCController 와 UINPCPopupDirector스크립트는 NPC 와 상자에 사용되는 스크립트로 유저의 물리적인 감지와 터치 인터렉션을 수행.
- NPCController 와 UINPCPopupDirector스크립트는 EventDispatcher 싱글톤 스크립트로 ChestItemGenerator 스크립트와 통신하여 아이템 생성
- ChestItemGenerator 스크립트는 EventDispatcher로 전달받은 String , Vector3, vector2 타입의 값을 활용해 아이템을 제작.
- 포스트 프로세싱 Bloom 효과를 사용해 상자객체의 외곽선을 랜더링하고 빛나는 연출 구현.
- HashSet, List, Dictionary, array 등의 자료구조를 사용.

### **상세 내용**
**NPCController**<br>
&nbsp;&nbsp;&nbsp;&nbsp;● enum 타입의 NPC 타입을 정의하여 [SerializeField] eNpcType 필드에 값을 할당해 자신의 타입별로 if 문을 사용해 어떤 이벤트를 호출할지 결정합니다.
&nbsp;&nbsp;&nbsp;&nbsp;● NPC와 상자는 Collider2D 컴포넌트를 사용하며 OnTriggerEnter2D 메서드와 OnTriggerExit2D 메서드를 사용해 유저의 위치에 따라 이벤트를 호출합니다.
**UINPCPopupDirector**<br>
&nbsp;&nbsp;&nbsp;&nbsp;● enum 타입의 팝업 상태를 정의하며 [SerializeField] ePopupType popupType 필드에 값을 할당해 자신의 타입별로 switch 문과 if문을 사용해 어떤 이벤트를 호출할지 결정합니다.
&nbsp;&nbsp;&nbsp;&nbsp;● 데미지를 주는 히든 상자에 의해 호출될 경우 TakeChestDamage 메서드를 통해 EventDispatcher를 사용하여 유저에게 데미지를 가합니다.(체력과 아이템 교환)
&nbsp;&nbsp;&nbsp;&nbsp;● 골드를 소비하는 히든 상자의 경우 Infomanger 싱글톤 스크립트와 통신하여 유저의 잔액량을 확인한뒤 GUI의 텍스트를 변경하거나 아이템을 생성합니다.
&nbsp;&nbsp;&nbsp;&nbsp;● DOTween을 사용하여 팝업이 펼쳐지고 다시 들어가는 연출을 만들었습니다.
**ChestItemGenerator**<br>
&nbsp;&nbsp;&nbsp;&nbsp;● eDropItemGrade 와 eDropItemType enum 타입으로 아이템을 구분합니다.
&nbsp;&nbsp;&nbsp;&nbsp;● 상자와 아이템은 GameObject 타입의 프리팹을  [SerializeField] 로 할당하여 Instanciate 합니다.
&nbsp;&nbsp;&nbsp;&nbsp;● 아이템은 factory 패턴을 사용하여 생성합니다.
&nbsp;&nbsp;&nbsp;&nbsp;● InfoManager 싱글톤 스크립트와 통신하여 현재 유저의 던전 상태(난이도,스테이지)에 따라 아이템과 재화를 생성합니다.
&nbsp;&nbsp;&nbsp;&nbsp;● HashSet 자료구조를 이용해 아이템이 상자에서 나올때 중복된 Vector2값에 겹치지 않게 제작하였습니다.
&nbsp;&nbsp;&nbsp;&nbsp;● DOTween을 사용하여 아이템의 생성연출 애니메이션을 만들었습니다.
&nbsp;&nbsp;&nbsp;&nbsp;● UniRx 플러그인과 Physics2D 클래스를 사용해 터치 인풋을 제어합니다. (UniRx 플러그인을 활용한 필드 아이템 터치 조작 기획 및 로직 제작 참조) 
&nbsp;&nbsp;&nbsp;&nbsp;● SpriteGlowEffect 스크립트를 포스트 프로세싱 Bloom 효과에 적용해 아이템의 외곽선과 빛나는 연출을 제작하였습니다.
**ChestArrowController**<br>
&nbsp;&nbsp;&nbsp;&nbsp;● BoxCollider2D 컴포넌트를 이용해 유저의 상자 진입여부를 판단합니다.
&nbsp;&nbsp;&nbsp;&nbsp;● 유저 접근시 DOTWeen의 DOFade 메서드를 사용해 화살표가 사라지는 연출을 조절합니다.

[목차로](#목차)

* * *

:green_circle:오브젝트 풀링을 이용한 필드 출현 Coin & 아이템 스크립트[코드보기](https://github.com/iLovealan1/KIm-Dong-Joon-game-client-Portfolio/tree/main/Scripts/Chest%26ItemGenerator)
===
### Field_Items
![GetCoin](https://github.com/iLovealan1/KIm-Dong-Joon-game-client-Portfolio/assets/124248265/b1a7ce3c-100e-4d93-a74c-1269e90e98cd)
![GetCoins](https://github.com/iLovealan1/KIm-Dong-Joon-game-client-Portfolio/assets/124248265/c648bc0f-bf08-49fc-bfb0-35be7a8fa1f3)
![coinspin](https://github.com/iLovealan1/KIm-Dong-Joon-game-client-Portfolio/assets/124248265/e3286a16-e4bb-413b-9316-c1e3cb0973db)
![itemFloating](https://github.com/iLovealan1/KIm-Dong-Joon-game-client-Portfolio/assets/124248265/b2cb028f-7512-4199-aa7f-3c933119a3bd)

### **이미지 설명(최상단부터)**
- 필드 코인 개별 획득
- 라운드 종료시 코인 일괄 획득
- 코인 스핀 애니메이션
- 아이템 플로팅 애니메이

### **요약**
-
- 필드코인은 몬스터를 사냥시 30%확률로 획득 가능한 전리품.
- BoxCollider2D 컴포넌트를 사용해 유저 접근시 획득연출 (DOTween 을 이용한 애니메이션 코드 제어)
- DOTween 을 이용한 회전 연출 및 아이템 Floating 연출
- SpriteGlowEffect 외부 스크립트를 활용한 포스트 프로세싱 Bloom 효과 연출
- 

### **상세 내용**
**NPCController**<br>
&nbsp;&nbsp;&nbsp;&nbsp;● enum 타입의 NPC 타입을 정의하여 [SerializeField] eNpcType 필드에 값을 할당해 자신의 타입별로 if 문을 사용해 어떤 이벤트를 호출할지 결정합니다.
&nbsp;&nbsp;&nbsp;&nbsp;● NPC와 상자는 Collider2D 컴포넌트를 사용하며 OnTriggerEnter2D 메서드와 OnTriggerExit2D 메서드를 사용해 유저의 위치에 따라 이벤트를 호출합니다.
**UINPCPopupDirector**<br>
&nbsp;&nbsp;&nbsp;&nbsp;● enum 타입의 팝업 상태를 정의하며 [SerializeField] ePopupType popupType 필드에 값을 할당해 자신의 타입별로 switch 문과 if문을 사용해 어떤 이벤트를 호출할지 결정합니다.
&nbsp;&nbsp;&nbsp;&nbsp;● 데미지를 주는 히든 상자에 의해 호출될 경우 TakeChestDamage 메서드를 통해 EventDispatcher를 사용하여 유저에게 데미지를 가합니다.(체력과 아이템 교환)
&nbsp;&nbsp;&nbsp;&nbsp;● 골드를 소비하는 히든 상자의 경우 Infomanger 싱글톤 스크립트와 통신하여 유저의 잔액량을 확인한뒤 GUI의 텍스트를 변경하거나 아이템을 생성합니다.
&nbsp;&nbsp;&nbsp;&nbsp;● DOTween을 사용하여 팝업이 펼쳐지고 다시 들어가는 연출을 만들었습니다.
**ChestItemGenerator**<br>
&nbsp;&nbsp;&nbsp;&nbsp;● eDropItemGrade 와 eDropItemType enum 타입으로 아이템을 구분합니다.
&nbsp;&nbsp;&nbsp;&nbsp;● 상자와 아이템은 GameObject 타입의 프리팹을  [SerializeField] 로 할당하여 Instanciate 합니다.
&nbsp;&nbsp;&nbsp;&nbsp;● 아이템은 factory 패턴을 사용하여 생성합니다.
&nbsp;&nbsp;&nbsp;&nbsp;● InfoManager 싱글톤 스크립트와 통신하여 현재 유저의 던전 상태(난이도,스테이지)에 따라 아이템과 재화를 생성합니다.
&nbsp;&nbsp;&nbsp;&nbsp;● HashSet 자료구조를 이용해 아이템이 상자에서 나올때 중복된 Vector2값에 겹치지 않게 제작하였습니다.
&nbsp;&nbsp;&nbsp;&nbsp;● DOTween을 사용하여 아이템의 생성연출 애니메이션을 만들었습니다.
&nbsp;&nbsp;&nbsp;&nbsp;● UniRx 플러그인과 Physics2D 클래스를 사용해 터치 인풋을 제어합니다. (UniRx 플러그인을 활용한 필드 아이템 터치 조작 기획 및 로직 제작 참조) 
&nbsp;&nbsp;&nbsp;&nbsp;● SpriteGlowEffect 스크립트를 포스트 프로세싱 Bloom 효과에 적용해 아이템의 외곽선과 빛나는 연출을 제작하였습니다.
**ChestArrowController**<br>
&nbsp;&nbsp;&nbsp;&nbsp;● BoxCollider2D 컴포넌트를 이용해 유저의 상자 진입여부를 판단합니다.
&nbsp;&nbsp;&nbsp;&nbsp;● 유저 접근시 DOTWeen의 DOFade 메서드를 사용해 화살표가 사라지는 연출을 조절합니다.
