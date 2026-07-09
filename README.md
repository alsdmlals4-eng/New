# my little boat - Unity 1차 MVP

작은 보트에 앉은 1인칭 시점으로 바다를 감상하는 모바일 + PC 대응 힐링 표류 게임 MVP입니다. 전투, 실패, 경쟁, 결제, 광고, 온라인 편지 공유는 구현하지 않았습니다.

## 프로젝트 열기

1. Unity Hub에서 `Add project from disk`를 선택합니다.
2. 이 폴더를 선택합니다.
3. 검증한 Unity 버전은 `6000.0.77f1`입니다. 기본 uGUI와 Primitive 오브젝트만 사용했습니다.
4. 프로젝트를 처음 열면 `Assets/Editor/MyLittleBoatEditorSetup.cs`가 `Assets/Scenes/MainMenuScene.unity`, `Assets/Scenes/GameScene.unity`를 자동 생성하고 Build Settings에 등록합니다.
5. 자동 생성이 되지 않으면 상단 메뉴에서 `my little boat > Setup MVP Scenes`를 실행하세요.

## 씬 구성

### MainMenuScene

런타임에 `MainMenuController`가 자동 생성됩니다.

역할:
- 게임 제목 `my little boat` 표시
- 오늘의 마음 4종 선택
  - 평온
  - 지침
  - 외로움
  - 설렘
- 마음 선택 후 `GameScene`으로 이동

### GameScene

런타임에 `GameSceneController`가 자동 생성됩니다.

역할:
- 1인칭 보트 카메라 생성
- 플레이어 몸은 생성하지 않음
- 보트 앞부분, 바다, 하늘, 동반자 펫 생성
- 모바일 터치 / PC 마우스 드래그로 주변 둘러보기
- 카메라 상하 회전 제한과 수평선 안정 유지
- 사진찍기 / 감상모드 / 속도조절 버튼 생성
- 5분 항해 타이머와 오늘의 항해 기록 UI 표시
- 병 속 편지와 풍경 이벤트 자동 발생
- 앨범 UI 표시

## 구현된 MVP 기능

- 마음 선택 4종: `GameMood.cs`, `MainMenuController.cs`
- 실제 시간대 시스템: `TimeOfDayService.cs`
  - 06:00~17:59 = 오전 바다
  - 18:00~05:59 = 밤바다
- 1인칭 카메라 드래그: `FirstPersonLook.cs`
- 속도조절 3종: `OceanDriftController.cs`
  - 느림
  - 보통
  - 빠름
- 사진찍기: `GameSceneController.TakePhoto()`
- 감상모드: `GameSceneController.ToggleAppreciationMode()`
- 5분 항해 루프: `VoyageTimerController.cs`
- 병 속 편지 MVP: `BottleLetterController.cs`
- 풍경 수집 MVP: `SceneryEventController.cs`
  - 일출
  - 일몰
  - 비
  - 고래
- 동반자 펫 MVP: `CompanionPetController.cs`
  - 이름은 따로 정하지 않고 `동반자`로 표시
  - 호감도에 따라 Lv 1~3 변화
  - 사진, 편지, 풍경, 항해 완료 시 반응 문구 변경
- 앨범 MVP: `AlbumUiController.cs`
  - 사진 앨범
  - 풍경 앨범
  - 편지 보관함

## 오브젝트 연결 방법

이번 MVP는 초보자가 Inspector 연결을 하지 않아도 실행되도록 만들었습니다.

- `MainMenuScene`을 열고 Play를 누르면 `MyLittleBoatBootstrap`이 `MainMenuController`를 자동 생성합니다.
- `GameScene`으로 이동하면 `GameSceneController`가 카메라, 보트, 바다, UI, 동반자, 이벤트 컨트롤러를 자동 생성합니다.
- 수동으로 연결해야 하는 SerializeField는 없습니다.
- 5분 타이머 시간을 테스트용으로 줄이고 싶다면 `Assets/Scripts/Gameplay/GameSceneController.cs`의 `voyageDurationSeconds` 기본값을 임시로 낮추면 됩니다.

## 테스트 체크리스트

### MainMenuScene

- [ ] Play 시 `my little boat` 제목이 보인다.
- [ ] 마음 선택 버튼 4개가 보인다.
- [ ] `평온` 선택 시 GameScene으로 이동한다.
- [ ] `지침`, `외로움`, `설렘`도 각각 GameScene으로 이동한다.

### GameScene 기본

- [ ] 플레이어 몸이 보이지 않는다.
- [ ] 보트 앞부분과 바다, 하늘, 동반자가 보인다.
- [ ] PC에서 마우스 드래그로 카메라가 좌우/상하로 움직인다.
- [ ] 모바일에서 터치 스와이프로 카메라가 움직인다.
- [ ] 카메라가 과하게 위아래로 꺾이지 않는다.
- [ ] 수평선이 기울어지지 않는다.

### 핵심 조작

- [ ] `사진찍기` 버튼을 누르면 사진 앨범에 기록이 추가된다.
- [ ] `감상모드` 버튼을 누르면 HUD가 옅어지고 다시 누르면 돌아온다.
- [ ] `느림`, `보통`, `빠름` 버튼을 누르면 표류 속도 표시가 바뀐다.
- [ ] `앨범` 버튼을 누르면 사진 / 풍경 / 편지 탭을 볼 수 있다.

### 시간과 루프

- [ ] 실제 시간이 06:00~17:59이면 오전 바다로 표시된다.
- [ ] 실제 시간이 18:00~05:59이면 밤바다로 표시된다.
- [ ] 5분 후 오늘의 항해 기록 UI가 표시된다.
- [ ] 기록 표시 후에도 바다 감상은 계속 유지된다.

### 편지 / 풍경 / 동반자

- [ ] 시작 후 일정 시간이 지나면 병 속 편지가 표시된다.
- [ ] 편지 보관함에 편지가 저장된다.
- [ ] 시작 후 일정 시간이 지나면 일출 / 일몰 / 비 / 고래 중 하나가 발생한다.
- [ ] 풍경 앨범에 풍경이 저장된다.
- [ ] 사진, 편지, 풍경을 수집하면 동반자 반응 문구가 바뀐다.
- [ ] 호감도가 오르면 동반자 Lv가 1에서 2, 3으로 상승한다.

## 제외한 것

- 온라인 편지 공유
- 결제 시스템
- 광고 시스템
- 복잡한 상점
- 고급 물 셰이더 직접 제작
- 외부 유료 에셋 의존

## 주요 파일

- `Assets/Scripts/Core/MyLittleBoatBootstrap.cs`
- `Assets/Scripts/UI/MainMenuController.cs`
- `Assets/Scripts/Gameplay/GameSceneController.cs`
- `Assets/Scripts/Gameplay/FirstPersonLook.cs`
- `Assets/Scripts/Gameplay/OceanDriftController.cs`
- `Assets/Scripts/Gameplay/BottleLetterController.cs`
- `Assets/Scripts/Gameplay/SceneryEventController.cs`
- `Assets/Scripts/Gameplay/CompanionPetController.cs`
- `Assets/Scripts/UI/AlbumUiController.cs`

## 검증 결과

- Unity `6000.0.77f1` 배치 모드 검증용 복사본에서 스크립트 import와 컴파일을 확인했습니다.
- 로그 기준 `LogAssemblyErrors (0ms)`, `Exiting batchmode successfully`를 확인했습니다.
- `MainMenuScene.unity`, `GameScene.unity` 생성 후 원본 프로젝트에 반영했습니다.

