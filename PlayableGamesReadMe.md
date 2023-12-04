![header](https://capsule-render.vercel.app/api?type=waving&color=gradient&height=250&section=header&text=Luna%20Playable%20Games&fontSize=35)<br>

# 소개
안녕하세요. 플레이어블게임 포트폴리오에 오신걸 환영합니다.

- 저는 슈퍼센트 주식회사에서 3개월간 인턴으로 근무하며 루나플레이어블 플랫폼 게임을 개발하는 업무를 담당하였습니다.<br>
- 당시 전체 개발을 담당한 게임 2개와 리펙토링에 참여한 게임 1개가 있습니다.<br>
- 전체 개발을 담당한 플레이어블게임들은 슈퍼센트 주식회사의 모바일 플랫폼 아케이드 아이들 장르의 게임인 BurgerPlease! 와 OutletRush의 미니게임 형식 게임들입니다.<br>
- 두개의 게임 모두 현재 전면광고 게임으로 현재 ironSource, Applovin, Lunaplayable 등의 플랫폼에서 배포되어 서비스 중입니다.<br>

아래는 개발한 게임 2개의 기술 목록입니다.

# Contacts
📧: 이메일 :  korindj@kakao.com<br>
🏠: 개발자 블로그 : [Blog Link](https://bueong-e.tistory.com)

# 목차

개요 : [📚:설명보기](#playableGame)<br>
게임소개 및 플레이링크 : [📚:설명보기](#IntroDuction)<br>

:red_circle: A*알고리즘을 활용한 절차적 랜덤 맵 생성 스크립트 제작.[📚:설명보기](#astar_random_map_generator) [📜:스크립트 보기](https://github.com/iLovealan1/KIm-Dong-Joon-game-client-Portfolio/tree/main/Scripts/Astar_MapGenerator)<br>
:red_circle: Main to Director 스크립트를 이용한 씬 전환 및 스테이지 전환.[📚:설명보기](#main_directors) [📜:스크립트 보기](https://github.com/iLovealan1/KIm-Dong-Joon-game-client-Portfolio/tree/main/Scripts/Main%26Director)<br>
:red_circle: 스테이지 루프 로직 및 씬전환에 필요한 데이터 연동 구조 기획 및 제작.[📚:설명보기](#main_directors)<br>
:red_circle: 유니티 타일맵 컴포넌트를 사용한 마을 & 던전 레벨 디자인.<br>
* * *


# 개요
### playableGame

![GraphicImage](https://docs.lunalabs.io/assets/lpp-overview.png)
🌕 Luna playable : [Docs Link][Luna playable Link]

[Luna playable Link]: https://docs.lunalabs.io/docs/playable/overview

▶️ 플레이어블 게임?
- 루나 플레이어블 플랫폼을 기반으로 제작된 전면광고용 미니 게임입니다.
- 현 라이브 서비스중인 게임의 홍보를 위해 제작되는 게임으로 맛보기 미니 게임의 성향을 가지고 있습니다.

🍔: Burger Please! Playable : [Game Play Link][Burger Please! 010]

[Burger Please! 010]: https://playground.lunalabs.io/preview/110188/155622/1703aff40e6d4548f15efe206918c6945f053e4a8bd126710bf82d53d1925cc4

🍔: Burger Please! Playable 2 : [Game Play Link][Burger Please! 011]
*리펙토링을 담당한 버전의 플레이어블 게임입니다.

[Burger Please! 011]: 
https://playground.lunalabs.io/preview/113799/160490/1703aff40e6d4548f15efe206918c6945f053e4a8bd126710bf82d53d1925cc4

👟 OutletRrush : [Game Play Link_Stack_ver][OutletRush 002]

[OutletRush 002]: https://playground.lunalabs.io/preview/117526/165584/1703aff40e6d4548f15efe206918c6945f053e4a8bd126710bf82d53d1925cc4


👟 OutletRrush : [Game Play Link_none_Stack_ver][OutletRush 002_none Stack]

[OutletRush 002_none Stack]: https://playground.lunalabs.io/preview/117526/165586/1703aff40e6d4548f15efe206918c6945f053e4a8bd126710bf82d53d1925cc4


# **게임소개 및 플레이링크**<br>
### IntroDuction

*하단에 소개된 게임들은 전면 광고 게임으로 웹상에서 자바스크립트로 변환되어 플레이되기 때문에 게임소개 및 플레이링크 항목의 링크와 QR을 통해 웹상과 모바일에서 플레이가 가능합니다.<br>
*사용중인 브라우저에서 플레이 불가능시 다른 인터넷 브라우저로 플레이 가능합니다.<br>
*게임 시작을 위해 클릭시 인게임 사운드가 재생됩니다.<br>

## 🎮: 게임소개

![GraphicImage](https://private-user-images.githubusercontent.com/124248265/287611325-0df14cc0-d948-40fb-9a3e-2119cb44b2bb.png?jwt=eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJnaXRodWIuY29tIiwiYXVkIjoicmF3LmdpdGh1YnVzZXJjb250ZW50LmNvbSIsImtleSI6ImtleTEiLCJleHAiOjE3MDE2NzI1ODgsIm5iZiI6MTcwMTY3MjI4OCwicGF0aCI6Ii8xMjQyNDgyNjUvMjg3NjExMzI1LTBkZjE0Y2MwLWQ5NDgtNDBmYi05YTNlLTIxMTljYjQ0YjJiYi5wbmc_WC1BbXotQWxnb3JpdGhtPUFXUzQtSE1BQy1TSEEyNTYmWC1BbXotQ3JlZGVudGlhbD1BS0lBSVdOSllBWDRDU1ZFSDUzQSUyRjIwMjMxMjA0JTJGdXMtZWFzdC0xJTJGczMlMkZhd3M0X3JlcXVlc3QmWC1BbXotRGF0ZT0yMDIzMTIwNFQwNjQ0NDhaJlgtQW16LUV4cGlyZXM9MzAwJlgtQW16LVNpZ25hdHVyZT03MTNjNzBlNmNmYmQ3NDc2Yjk2ZWJmZWUzYTFhMTU5NmZjMzhlMTFlMWYxYTQ5M2RmZjRkNTQzYjcyOGRhZWU1JlgtQW16LVNpZ25lZEhlYWRlcnM9aG9zdCZhY3Rvcl9pZD0wJmtleV9pZD0wJnJlcG9faWQ9MCJ9.Wq9GLm8RpAp4ypbClIomX2GwZJD7u-0u12uOk2cl_Lw)
- **제목** : Burger Please! Playable & Burger Please! Playable2 <br> 
- **장르** : 업그레이드 아이들<br>
- **엔진** : UnityEngine3D<br>
- **플랫폼** : Luna Playable<br>

## ⭐: 게임의 특징

- 주문을 위한 카운터, 버거 픽업을 위한 픽업대 구성의 업그레이드 아이들 게임
- 버거를 수령한 손님에게서 돈을 수금하여 카운터와 픽업대를 늘려나가는것이 게임의 주요 목표
- 게임의 마지막 언락 요소인 새로운 머신 추가시 게임이 끝나는 방식<br>

👉 버거를 주문하고 받아가기위해 분주하게 움직이는 손님들을 보는 재미요소<br>
👉 돈을 모아 가게를 확장해 가는 재미요소

## 🎲 플레이 링크 및 QR
🍔: Burger Please! Playable : [Game Play Link][Burger Please! 010]

[Burger Please! 010]: https://playground.lunalabs.io/preview/110188/155622/1703aff40e6d4548f15efe206918c6945f053e4a8bd126710bf82d53d1925cc4


## 제작 스크립트 설명

:green_circle:오브젝트 풀링을 이용한 필드 출현 Coin & 아이템 스크립트<br>[📂:폴더이동](https://github.com/iLovealan1/KIm-Dong-Joon-game-client-Portfolio/tree/main/Scripts/Field_Coin%26Items)
===
### field_items
![GetCoin](https://github.com/iLovealan1/KIm-Dong-Joon-game-client-Portfolio/assets/124248265/b1a7ce3c-100e-4d93-a74c-1269e90e98cd)
![GetCoins](https://github.com/iLovealan1/KIm-Dong-Joon-game-client-Portfolio/assets/124248265/c648bc0f-bf08-49fc-bfb0-35be7a8fa1f3)<br>
![coinspin](https://github.com/iLovealan1/KIm-Dong-Joon-game-client-Portfolio/assets/124248265/e3286a16-e4bb-413b-9316-c1e3cb0973db)<br>
![itemFloating](https://github.com/iLovealan1/KIm-Dong-Joon-game-client-Portfolio/assets/124248265/b2cb028f-7512-4199-aa7f-3c933119a3bd)<br>

### **이미지 설명(최상단부터)**
- 필드 코인 개별 획득
- 라운드 종료시 코인 일괄 획득
- 코인 스핀 애니메이션
- 아이템 플로팅 애니메이

### **요약**
- ChestItemGenerator 클래스와 DropItem 클래스를 이용한 아이템 생성.
- ChestItemGenerator 클래스와 CoinPool 오브젝트 풀링 스크립트를 이용한 코인 생성.
- Field Coin은 ChestItemGenerator 의 MakeFieldCoin 메서드의 UnityEngine.Random.Range() 메서드 사용 30% 의 확률로 생성
- BoxCollider2D 컴포넌트를 사용해 유저 접근시 획득연출. (DOTween 을 이용한 애니메이션 코드 제어)
- DOTween을 이용한 회전 연출 및 아이템 Floating 연출.
- SpriteGlowEffect 외부 스크립트를 활용한 포스트 프로세싱 Bloom 효과 연출.

### **상세 내용**
📜:**DropItem**[스크립트 보기](https://github.com/iLovealan1/KIm-Dong-Joon-game-client-Portfolio/blob/main/Scripts/Field_Coin%26Items/DropItem.cs)<br>
&nbsp;&nbsp;&nbsp;&nbsp;● ChestItemGenerator 클래스의 메서드 팩토리 패턴으로 생성된 객체의 이름에 따라 switch문 과 if문을 통해 각각 다른 메서드를 호출합니다.<br>
&nbsp;&nbsp;&nbsp;&nbsp;● DOTween플러그인을 이용해 각각 다른 UI위치로 아이템을 보내는 연출을 코드로 제어하였습니다.<br>
&nbsp;&nbsp;&nbsp;&nbsp;● DOTween플러그인을 이용해 FloatingEffect() 메서드와 FieldCoinAimStart() 메서드를 제작해 각기 다른 애니메이션을 코드로 제어하였습니다.<br>
&nbsp;&nbsp;&nbsp;&nbsp;● EventDispatcher 싱글톤 클래스로 이벤트를 호출하여 아이템 Full 팝업 UI를 체력 Full 팝업 UI 를 포하 각기 다른 UI의 메서드를 호출하였습니다.<br>
&nbsp;&nbsp;&nbsp;&nbsp;● EventDispatcher 싱글톤 클래스로 이벤트를 등록하여 ChestItemGenerator 의 터치이벤트에 반응하는 메서드를 호출시킵니다.<br>
&nbsp;&nbsp;&nbsp;&nbsp;● DataManager 싱글톤 클래스로 변하지 않는 기획된 양 만큼의 코인 값을 Infomanager 싱글톤 스크립트에 전달하여 직렬화 하여 저장하였습니다.<br>
&nbsp;&nbsp;&nbsp;&nbsp;● SpriteGlowEffect 외부 스크립트를 활용해 포스트 프로세싱 Bloom  효과를 더한 아이템 획득 이펙트 연출을 제작하였습니다.<br><br>
📜:**CoinPool**[스크립트 보기](https://github.com/iLovealan1/KIm-Dong-Joon-game-client-Portfolio/blob/main/Scripts/Field_Coin%26Items/CoinPool.cs)<br>
&nbsp;&nbsp;&nbsp;&nbsp;● List 자료구조를 사용해 FieldCoin 게임 오브젝트 프리팹을 Initializing 단계에서 생성하여 오브젝트 풀을 관리하였습니다.<br>
&nbsp;&nbsp;&nbsp;&nbsp;● 코드를 사용해 tag, sortingLayerName,sortingOrder,localScale,name,BoxCollider2D의 size를 제어하였습니다.<br>
&nbsp;&nbsp;&nbsp;&nbsp;● GetObjectFromPool() 메서드를 호출하여 List의 코인을 가져오며 만약 코인이 부족할시 새롭게 만들어 리스트에 추가하여 관리하였습니다.<br>
&nbsp;&nbsp;&nbsp;&nbsp;● Mono 오브젝트로 만들어 객체의 자식으로 코인을 관리하여 Hierarchy 창에서 관리가 용의 하게 제작하였습니다.<br><br>
📜:**MonsterGenerator**[스크립트 보기](https://github.com/iLovealan1/KIm-Dong-Joon-game-client-Portfolio/blob/main/Scripts/Main%26Director/MonsterGenerator.cs)<br><br>
&nbsp;&nbsp;&nbsp;&nbsp;● 레벨 종료시 GetAllFieldCoins() 메서드를 호출해 LINQ로 DropItem Field Coin 객체를 모두 담아 DropItem클래스의 ClickedItemCheck() 메서드를 호출하여 일괄적으로 수급합니다.<br>


[📑: 목차로](#목차)

* * *
