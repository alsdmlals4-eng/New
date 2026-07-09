# my little boat - Godot MVP

`my little boat`는 작은 보트에 앉아 바다를 감상하는 1인칭 힐링 표류 게임입니다.

이 저장소는 앞으로 **Godot 4.7 stable + GDScript** 기준으로만 작업합니다. 전투, 실패, 경쟁, 결제, 광고, 온라인 편지 공유는 MVP 범위에서 제외합니다.

## 프로젝트 열기

1. Godot 4.7 stable을 실행합니다.
2. `Import`를 누릅니다.
3. 이 폴더의 `project.godot`을 선택합니다.
4. 프로젝트를 연 뒤 `scenes/main_menu.tscn` 또는 실행 버튼을 눌러 시작합니다.

권장 작업 폴더:

```text
C:\Users\user\Documents\GitHub\MyLittleBoat
```

## 현재 구조

```text
MyLittleBoat/
  project.godot
  scenes/
    main_menu.tscn
    game.tscn
    album.tscn
  scripts/
    core/
      game_state.gd
    ui/
      main_menu.gd
      album_view.gd
    voyage/
      game_scene.gd
  assets/
    images/
    audio/
    fonts/
  docs/
    CONCEPT.md
    MVP_SCOPE.md
    CODEX_GOALS.md
    GODOT_DIRECTION.md
    GODOT_MVP_ROADMAP.md
```

## MVP 방향

플레이어는 작은 보트에 앉은 시점으로 바다를 감상합니다. 플레이어의 몸은 보이지 않습니다. 게임의 목표는 조용히 표류하면서 마음 선택, 사진찍기, 편지 발견, 풍경 수집, 동반자 반응을 경험하는 것입니다.

초기 MVP 기능:

- 마음 선택 4종: 평온, 지침, 외로움, 설렘
- 모바일 세로 화면 우선 UI
- PC 마우스 입력 대응
- 사진찍기
- 감상모드
- 속도조절: 느림, 보통, 빠름
- 5분 항해 루프
- 오늘의 항해 기록
- 병 속 편지
- 풍경 수집
- 동반자 호감도 Lv 1~3
- 사진 앨범, 풍경 앨범, 편지 보관함

## 현재 Godot 골격

현재 커밋은 Godot에서 바로 열 수 있는 MVP 골격입니다.

- `scenes/main_menu.tscn`: 제목과 마음 선택
- `scenes/game.tscn`: 1인칭 보트 감상 화면, 타이머, 핵심 조작 버튼
- `scenes/album.tscn`: 사진, 풍경, 편지 기록 확인
- `scripts/core/game_state.gd`: 선택한 마음과 수집 기록을 보관하는 AutoLoad
- `scripts/voyage/boat_camera_controller.gd`: PC 마우스 드래그 카메라 회전

아직 완성 게임이 아니라, 앞으로 기능을 붙여갈 기준 구조입니다.

## 협업 방식

이 저장소는 GitHub Desktop, GPT, Codex가 함께 쓰는 작업 공간입니다.

기본 흐름:

```text
Godot에서 작업
-> GitHub Desktop에서 변경사항 확인
-> Commit
-> Push
-> Codex/GPT가 GitHub 기준으로 분석 또는 수정
```

관련 문서:

- `AGENTS.md`: Codex가 지켜야 할 Godot 작업 규칙
- `AI_COLLABORATION.md`: GPT/Codex 협업 방식
- `CONTRIBUTING.md`: 기여와 테스트 기준
- `docs/CONCEPT.md`: 게임 콘셉트
- `docs/MVP_SCOPE.md`: MVP 범위
- `docs/CODEX_GOALS.md`: Codex에게 줄 작업 목표 예시

## 테스트 체크리스트

- [ ] Godot 4.7 stable에서 프로젝트가 열린다.
- [ ] 실행 시 `main_menu.tscn`이 시작된다.
- [ ] 마음 선택 버튼 4개가 보인다.
- [ ] 마음 선택 후 `game.tscn`으로 이동한다.
- [ ] `game.tscn`에서 바다 Plane, 하늘 배경, 보트 앞부분 placeholder가 보인다.
- [ ] 플레이어 몸이 보이지 않는다.
- [ ] PC 마우스 드래그로 좌우와 상하를 둘러볼 수 있다.
- [ ] 카메라가 위아래로 과하게 꺾이지 않는다.
- [ ] 카메라 수평선이 심하게 기울지 않는다.
- [ ] `사진찍기` 버튼을 누르면 사진 기록 수가 증가한다.
- [ ] `감상모드` 버튼을 누르면 상태 문구가 바뀐다.
- [ ] 속도 버튼이 느림 / 보통 / 빠름으로 바뀐다.
- [ ] `편지 발견` 버튼을 누르면 편지 기록이 추가된다.
- [ ] `풍경 기록` 버튼을 누르면 풍경 기록이 추가된다.
- [ ] `앨범` 버튼으로 앨범 화면에 들어간다.
- [ ] 앨범에서 사진 / 풍경 / 편지 기록 수가 보인다.

## 제외한 것

- 전투
- 실패 조건
- 경쟁 점수
- 결제
- 광고
- 온라인 편지 공유
- 유료 에셋 의존
- 복잡한 상점
