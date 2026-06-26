# FoodMaestro

> Unity 기반 1인 개발 레스토랑 경영 캐주얼 시뮬레이션 출시 프로젝트

Unity와 C#으로 개발한 1인 제작 모바일 레스토랑 경영 시뮬레이션 게임입니다. 손님 응대, 요리, 직원 관리, 주방 업그레이드 등 식당 운영의 핵심 루프를 구현하였으며, Easy Save 3 기반의 로컬 저장 시스템과 DOTween·PolyNav 등 외부 플러그인을 통합하여 완성도 높은 게임플레이를 구성했습니다. 
Singleton, FSM, ScriptableObject, Object Pooling 등 실전적인 아키텍처 패턴을 적용하여 유지보수성과 확장성을 갖춘 구조를 설계하는 것을 목표로 했습니다.

<br/>

## Store / Demo

| 구분 | 링크 |
|------|------|
| itch.io | [fel2.itch.io/idle-test](https://fel2.itch.io/idle-test) |

<br/>

## Project Overview

| 항목 | 내용 |
|------|------|
| 프로젝트명 | FoodMaestro |
| 장르 | 레스토랑 경영 캐주얼 시뮬레이션 |
| 개발 형태 | 1인 개발 |
| 플랫폼 | Android (Mobile) |
| 엔진 | Unity 2D |
| 개발 언어 | C# |
| 주요 구현 | Worker FSM, Guest Queue, Kitchen Upgrade System, Room Customization, Easy Save 3 저장, Object Pooling, DOTween UI 애니메이션, PolyNav AI 경로탐색 |

<br/>

## Tech Stack

### Engine / Language
![Unity](https://img.shields.io/badge/Unity-000000?style=flat-square\&logo=unity\&logoColor=white)
![C#](https://img.shields.io/badge/C%23-239120?style=flat-square\&logo=csharp\&logoColor=white)

- Unity 2D 기반 모바일 캐주얼 게임 개발
- C#으로 전체 게임 로직 및 시스템 구현

### Data / Save
![EasySave3](https://img.shields.io/badge/Easy%20Save%203-FF6B35?style=flat-square\&logoColor=white)

- Easy Save 3를 활용한 로컬 저장/불러오기 구현
- ScriptableObject로 음식·가구 데이터 정의 및 관리

### Animation / UI
![DOTween](https://img.shields.io/badge/DOTween-E91E63?style=flat-square\&logoColor=white)
![TextMeshPro](https://img.shields.io/badge/TextMeshPro-0078D4?style=flat-square\&logoColor=white)

- DOTween으로 UI 스케일·이동·페이드 애니메이션 처리
- TextMeshPro로 게임 내 텍스트 렌더링
- EnhancedScroller v2로 가구 목록 스크롤 최적화

### AI / Navigation
![PolyNav](https://img.shields.io/badge/PolyNav-607D8B?style=flat-square\&logoColor=white)

- PolyNavAgent로 직원·손님 AI 경로탐색 구현
- Waypoint 기반 이동 목표 설정

### Architecture / Pattern
- Singleton 패턴으로 전역 매니저 클래스 관리
- Generic FSM(`StateMachine<T>`)으로 Worker 행동 상태 분리
- Object Pooling으로 골드 코인 파티클 재사용 최적화

<br/>

## Main Features

| 기능명 | 설명 |
|--------|------|
| Guest Queue System | 손님 자동 생성 및 빈 좌석 배정 큐 관리 |
| Worker FSM | Idle → Order → Cook → Serve 상태 전환 기반 직원 AI |
| Kitchen Upgrade System | 주방 레벨업(1~4단계)으로 동시 요리 슬롯 및 요리 속도 향상 |
| Chair Management | 좌석 수 확장 및 레벨업으로 손님 수용 인원 조절 |
| Gold & Currency | 요리 완료 시 골드 획득 및 코인 파티클 연출 |
| Room Customization | 격자 기반 가구 배치 및 인테리어 커스터마이징 |
| System Level Up | 직원 수 · 골드 보너스 · 요리 시간 감소 등 전역 업그레이드 |
| Easy Save 3 Save System | 스테이지별 진행도, 골드, 업그레이드 상태 로컬 저장/불러오기 |
| Multi-Stage Progression | 스테이지 1→2 단계 진행 구조 및 씬 전환 관리 |
| Audio System | 타이틀·인게임·룸 씬별 BGM 및 UI·게임플레이 효과음 관리 |

<br/>

## Project Structure

```text
Root3Game/
└── Assets/
    ├── 01.Scenes/          # LogoScene, TitleScene, GameScene1, GameScene2, RoomScene
    ├── 02.Scripts/
    │   ├── FSM/            # Generic StateMachine + Worker 상태 클래스
    │   ├── Guest/          # Guest, GuestManager, GuestQueue
    │   ├── Kitchen/        # Kitchens, KitchenManager, KitchenLevelPanel
    │   ├── Room/           # FurnitureManager, GridManager, Furniture
    │   ├── Chair/          # Chair, ChairManager
    │   ├── Gold/           # GoldManager, 코인 파티클 Object Pool
    │   ├── UI/             # 인게임 UI 패널 및 팝업
    │   ├── SystemLevel/    # SystemLevelUpManager, SystemLevelUpData
    │   ├── Camera/         # CameraController
    │   └── Util/           # Singleton<T> 베이스 클래스
    ├── 03.Asset/           # 외부 에셋 (EnhancedScroller, UI 테마)
    ├── 04.Sprite/          # 스프라이트 리소스
    ├── 05.Data/            # ScriptableObject 데이터 파일 (FoodData, FurnitureData)
    ├── 06.Animation/       # 캐릭터·UI 애니메이션
    ├── 07.Prefab/          # 재사용 프리팹
    ├── 08.Fonts/           # 커스텀 폰트
    ├── 09.Sound/           # BGM, SFX 오디오 클립
    ├── Easy Save 3/        # 저장 시스템 플러그인
    └── Plugins/            # DOTween, PolyNav 등 외부 플러그인
```

<br/>

## Core Implementation

### 1. Worker FSM (직원 행동 상태 머신)

손님 응대부터 요리 완료까지 직원이 수행하는 복잡한 행동 흐름을 코드로 관리하기 위해 범용 상태 머신을 설계했습니다. 단일 스크립트에 모든 로직이 뭉치는 문제를 방지하고 각 상태를 독립적으로 확장할 수 있는 구조가 필요했습니다.

**구현 내용**
- `StateMachine<T>` 제네릭 클래스로 상태 전환 로직 추상화
- `IdleState` → `OrderState` → `WalkState` → `CookState` → `ServingState` → `OrderWaitingState` 6개 상태 구현
- 주방이 가득 찼을 때 `OrderWaitingState`에서 큐 대기 후 자동 재시도
- `WorkerController`에서 상태 전환 트리거 및 PolyNav 이동 목표 지정

**배운 점**
- 상태 분리로 개별 상태 디버깅이 명확해지고, 새로운 행동 추가 시 기존 코드를 건드리지 않아도 됨을 체감
- FSM 구조가 AI 행동 설계에서 가독성과 유지보수성을 동시에 확보하는 핵심 패턴임을 이해

<br/>

### 2. Guest Queue System (손님 큐 및 좌석 배정)

손님이 동시에 여러 명 입장하더라도 빈 좌석에 순서대로 배정되어야 하고, 좌석이 없으면 대기하다가 자리가 생기면 자동으로 이동하는 흐름이 필요했습니다.

**구현 내용**
- `GuestQueue`로 입장 대기 중인 손님을 FIFO 큐로 관리
- `GuestManager`에서 일정 간격으로 손님 생성 및 빈 의자 확인 후 배정
- `Chair` 상태(비어있음 / 착석 중)를 `ChairManager`에서 중앙 집중 관리
- 손님 착석 → 주문 → 음식 수령 → 퇴장 시 좌석 상태 해제 후 큐 재처리

**배운 점**
- 큐 기반 스케줄링이 스폰 타이밍과 좌석 배정을 분리하여 타이밍 버그를 줄이는 데 효과적임을 확인
- 상태 중앙 관리 매니저 패턴이 여러 오브젝트 간 동기화 문제 해결에 유용함을 학습

<br/>

### 3. Kitchen Upgrade System (주방 레벨업 시스템)

플레이어가 골드를 소비하여 주방을 업그레이드할수록 동시 조리 슬롯이 늘어나고 요리 속도가 빨라지는 핵심 성장 루프를 구현했습니다. 레벨별 데이터 관리와 UI 상태 동기화가 요구되었습니다.

**구현 내용**
- `Kitchens` 컴포넌트에 레벨 1~4 정의, 레벨별 활성 슬롯 수와 업그레이드 비용 배열로 관리
- `KitchenManager`에서 전체 주방 목록 관리 및 사용 가능한 주방 배정
- `KitchenLevelPanel`로 레벨업 버튼·비용·잠금 UI를 런타임에 동기화
- 주방이 가득 찼을 때 직원을 `OrderWaitingState` 큐에 등록하여 슬롯 해제 후 자동 배정

**배운 점**
- 데이터 배열(비용, 슬롯 수)과 현재 레벨 인덱스를 조합하는 방식이 레벨 시스템 설계의 단순하고 명확한 패턴임을 확인
- 업그레이드 UI와 실제 게임 상태를 별도 컴포넌트로 분리하면 UI 수정이 로직에 영향을 주지 않음을 체득

<br/>

### 4. Easy Save 3 Save System (로컬 저장 시스템)

스테이지 진행도, 골드, 업그레이드 상태 등 다양한 게임 데이터를 앱 재실행 후에도 유지해야 했습니다. 직접 파일 IO를 구현하는 대신 Easy Save 3를 선택하여 안정성과 개발 속도를 확보했습니다.

**구현 내용**
- 스테이지별 데이터(골드, 직원 수, 좌석 수, 주방 레벨, 가구 배치)를 키-값 방식으로 저장
- 전역 데이터(스테이지 진행 인덱스, 시스템 레벨업 상태)를 별도 파일로 분리 저장
- 씬 로드 시점에 저장 데이터 존재 여부 확인 후 기본값 또는 저장값으로 초기화
- `KitchenLevelPanel` 상태까지 직렬화하여 UI 복원 처리

**배운 점**
- 저장 키 네이밍 규칙을 초기에 설계하지 않으면 스테이지가 늘어날수록 키 충돌 위험이 생김을 경험
- 저장 시점(레벨업 직후 vs 씬 이탈 시)을 명확히 정하는 것이 데이터 무결성의 핵심임을 학습

<br/>

### 5. Object Pooling (골드 코인 파티클 풀)

요리 완료마다 골드 코인 파티클이 화면에 다수 생성되는데, 매번 `Instantiate` / `Destroy`를 반복하면 GC 압박과 프레임 드롭이 발생했습니다.

**구현 내용**
- `ObjectPool<GameObject>` 제네릭 풀로 코인 파티클 오브젝트 재사용
- `GoldManager`에서 풀에서 꺼내 위치 설정 후 사용, 애니메이션 완료 후 반환
- 풀 초기 사이즈와 최대 사이즈를 설정하여 메모리 사용량 제어

**배운 점**
- 반복 생성·파괴 패턴을 풀링으로 교체하면 GC 발생 빈도가 체감될 만큼 줄어듦을 프로파일러로 확인
- 오브젝트 반환 타이밍(애니메이션 종료 콜백)을 명확히 처리하지 않으면 풀이 고갈되는 버그가 생김을 학습

<br/>

### 6. Multi-Stage Progression & Scene Management (스테이지 진행 및 씬 전환)

스테이지 1 클리어 후 스테이지 2로 이어지는 진행 구조와, 인게임 씬과 룸 커스터마이징 씬 사이를 오가는 전환 흐름을 안정적으로 관리해야 했습니다.

**구현 내용**
- `GameManager` 싱글턴으로 현재 스테이지 인덱스 및 클리어 조건 관리
- `InGameSceneManager`로 GameScene ↔ RoomScene 전환 및 전환 시 저장 처리
- LogoScene → TitleScene → GameScene 순서의 초기 진입 플로우 구현
- 스테이지 클리어 조건(전체 시스템 업그레이드 + 주방 최대 레벨) 충족 시 다음 스테이지 버튼 활성화

**배운 점**
- 씬 전환 시점에 반드시 저장을 선행하는 규칙을 설정하면 데이터 유실 버그를 사전에 방지할 수 있음을 경험
- 씬 간 공유 데이터는 싱글턴 매니저를 통해 전달하는 것이 `DontDestroyOnLoad` 남용보다 추적이 쉬움을 확인

<br/>

## Repository Purpose

이 저장소는 포트폴리오용 코드 공개를 목적으로 합니다. 상용 에셋 라이선스에 해당하는 플러그인 원본 파일과 빌드 산출물은 제거하였으며, 게임 로직·시스템 설계·아키텍처 구조를 확인할 수 있는 C# 스크립트와 씬·프리팹 구조를 중심으로 공개합니다.

<br/>

## Security Notice

- Easy Save 3, DOTween, EnhancedScroller, PolyNav 등 유료 에셋 원본 파일 제외
- 빌드 산출물 (`.apk`, `Build/`) 제외
- 개인 식별 정보 및 서비스 인증키 없음 (로컬 전용 저장, 백엔드 미사용)

<br/>

## What I Learned

- Generic FSM(`StateMachine<T>`) 설계로 AI 행동 로직을 상태 단위로 분리하면 디버깅·확장이 명확해짐
- ScriptableObject 데이터 분리가 기획 변경 시 코드 수정 없이 수치 조정을 가능하게 함
- Object Pooling으로 반복 생성·파괴 패턴을 제거하면 GC 압박이 체감될 만큼 줄어드는 것을 프로파일러로 확인
- 저장 키 네이밍 규칙과 저장 시점 정의를 초기에 설계하는 것이 데이터 무결성 유지의 핵심임을 경험
- 씬 전환 시 저장 선행 규칙처럼 명시적인 개발 컨벤션이 런타임 버그를 사전에 차단함을 학습
- 1인 개발에서 Singleton·Manager 패턴의 적절한 분리가 프로젝트 규모가 커질수록 유지보수성에 직접적인 영향을 줌을 체득

<br/>

## Notes

이 저장소는 게임 로직과 시스템 아키텍처 검토를 위한 포트폴리오 공개용입니다. 상용 에셋 및 빌드 파일은 포함되지 않으며, 실제 실행을 위해서는 해당 플러그인의 별도 구매 및 설치가 필요합니다.
