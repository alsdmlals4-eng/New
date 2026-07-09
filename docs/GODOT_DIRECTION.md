# Godot Direction

이 저장소는 2026-07-09 기준으로 Godot 전용 프로젝트입니다.

## 결정

- 엔진은 Godot 4.7 stable을 사용합니다.
- 언어는 GDScript를 우선 사용합니다.
- 루트의 `project.godot`이 실제 작업 기준입니다.
- 모든 씬은 `scenes/` 아래에 둡니다.
- 모든 스크립트는 `scripts/` 아래에 둡니다.

## 작업 기준

- 모바일 세로 화면을 먼저 고려합니다.
- PC 마우스 입력도 함께 지원합니다.
- 초기 MVP에서는 로컬 저장만 사용합니다.
- 온라인, 결제, 광고, 유료 에셋 의존은 추가하지 않습니다.
- 복잡한 프레임워크보다 작은 기능 단위 구현을 우선합니다.

## GitHub Desktop 사용 흐름

```text
작업 전 Pull
-> Godot에서 수정
-> GitHub Desktop에서 변경사항 확인
-> Commit
-> Push
-> Codex/GPT가 GitHub 기준으로 검토 또는 수정
```
