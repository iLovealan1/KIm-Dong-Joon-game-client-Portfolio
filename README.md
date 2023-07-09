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

- - -
:red_circle: A*알고리즘을 활용한 절차적 랜덤 맵 생성 스크립트 제작. [바로가기](#:red_circle: A*알고리즘을 응용한 랜덤맵 생성기)<br>
:red_circle: 마을씬 던전씬의 Main to Director 스크립트 구조 기획 및 설계.<br>
:red_circle: 스테이지 루프 로직 및 씬전환에 필요한 데이터 연동 구조 기획 및 제작.<br>
:red_circle: 마을과 던전 레벨 디자인.<br>
* * *
:green_circle: 인벤토리 기획 및 제작.<br>
:green_circle: 상자 출현 아이템 생성 구조 설계 및 스크립트 제작.<br>
:green_circle: 오브젝트 풀링을 이용한 필드 출현 Coin 스크립트 제작.<br>
:green_circle: UniRx 플러그인을 활용한 필드 아이템 터치 조작 기획 및 로직 제작.<br>
:green_circle: DOTween 플러그인을 이용한 아이템 획득 연출.<br>
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


- **제목** : 건즈앤레이첼스(Guns N` Rahcels)<br>
- **장르** : 로그라이크 핵앤슬래시 액션 슈터<br>
- **엔진** : UnityEngine3D<br>
- **플랫폼** : iOS , Android<br>
- **출시일** : 2023. 6. 7 (Android)  2023. 6. 16 (iOS)<br>
- **제작** : Team Vizeon<br>

"건즈앤레이첼스" 는 현재 구글플레이스토어 와 앱스토어 양대마켓에서 "건즈앤레이첼스" 를 검색하여 지금 플레이 하실수 있습니다.

:iphone: iOS : [AppStore Link][iOS Link]

[iOS Link]: https://apps.apple.com/us/app/%EA%B1%B4%EC%A6%88%EC%95%A4%EB%A0%88%EC%9D%B4%EC%B2%BC%EC%8A%A4/id6450149470

:iphone: Android : [Google PlayStore Link][GooglePlayStore Link]

[GooglePlayStore Link]: https://play.google.com/store/apps/details?id=com.teamvizeon.gunsandrachels&hl=ko

### 게임의 특징

- 플레이 할때마다 맵이 바뀌는 바뀌는 로그라이크 스타일의 액션 슈터
- 게임을 클리어한 뒤에도 이어지는 윤회시스템
- RPG요소를 차용한 인벤토리 시스템과 스탯 강화 시스템
- 4종류의 특색있는 총기를 이용한 핵앤 슬래시 액션
- 디파짓 시스템을 통한 재화 수집 및 캐릭터 강화를 위한 에테르 수집
- 12종류의 특색있는 스킬들

## 제작 스크립트 설명

### :red_circle: A*알고리즘을 응용한 랜덤맵 생성기 [코드보기](https://github.com/iLovealan1/KIm-Dong-Joon-game-client-Portfolio/tree/main/Scripts/Astar_MapGenerator)

![AstarDungeonMap_Generator](https://github.com/iLovealan1/KIm-Dong-Joon-game-client-Portfolio/assets/124248265/8a6c67fa-e122-4def-9e5a-fb8a2a20fad7)
