# Retronia

**Retronia**는 로그라이트 요소가 결합된 멀티플레이 서바이벌 게임으로, 다양한 장비 조합과 무작위 스테이지에서의 생존을 중심으로 한 하이브리드 플레이 경험을 제공합니다.  
Nova Drift에서 영감을 받은 아케이드 스타일 전투에, 커스터마이징 가능한 장비와 언어 기반 시스템이 더해져 반복 플레이에 깊이를 더합니다.

현재는 코어 시스템과 주요 기능을 중심으로 개발 중이며, PC 플랫폼을 우선으로 하며 안드로이드도 지원 예정입니다.

---

## 🧩 핵심 특징

- 🎮 **장르**: 로그라이트 + 멀티플레이 서바이벌
- 🛠 **장비 커스터마이징**: 전투 중 실시간으로 무기와 장비 조합 변경 가능
- 🌐 **Localization 기반 언어 시스템**: 게임 내 언어 학습 및 해독 요소 내장
- 🔁 **자동 생성형 스테이지**: 반복될수록 달라지는 전장 구성
- 🕹 **게임 스타일**: Steam 게임 [Nova Drift](https://store.steampowered.com/app/858210/Nova_Drift/)와 유사한 미니멀 아케이드 슈팅
- 🌍 **플랫폼**: PC (Unity 기반) / Android (지원 예정)

---

## ⚠️ 현재 상태

이 저장소는 **Retronia**의 코어 시스템 중심으로 구성되어 있으며, 다음과 같은 시스템이 포함되어 있습니다:

- 씬 기반 구조 (`Intro`, `Lobby`, `Multiplayer`, `World`)
- `GameManager`, `AudioManager`, `Localizer` 등 핵심 시스템
- Unity Addressables + Localization 시스템 도입
- 에디터 디버깅을 위한 NaughtyAttributes 도입

게임의 전체 구조는 유연한 확장을 고려하여 설계되고 있으며, 향후 세계관, 캐릭터 설정, 멀티플레이 로직 등이 추가될 예정입니다.

---

## 📁 프로젝트 구조

---

## 🧩 주요 시스템

### GameManager
- 싱글톤 구조로 구현되어 전역 접근 가능
- `DontDestroyOnLoad` 사용으로 씬 전환 시에도 유지
- Localization 및 Addressables 연동 준비됨

### AudioManager / AudioHelper
- 효과음 및 배경음 관리 기능 포함
- 구조적 오디오 재생 지원 예상

### Localizer
- Unity Localization 패키지 기반 문자열 로딩 기능

---

## ⚙️ 기술 스택

- **Unity** 2021 이상
- **Addressables** for asset management
- **Unity Localization** for multilingual support
- **NaughtyAttributes** for enhanced editor UI
- **SerializableDictionary** for editor/debug storage

---

## 🧪 개발 및 디버깅 도구

- `[Button]` 어트리뷰트를 통한 인스펙터 내 디버깅 함수 실행
- `#if UNITY_EDITOR` 조건부 코드로 에디터 전용 기능 지원

---

## 📝 기여 및 확장

본 저장소는 Retronia 프로젝트의 기반 코드로, 다양한 모듈을 독립적으로 테스트하고 확장할 수 있도록 설계되었습니다. 기여 시에는 Pull Request 전에 다음 사항을 확인해주세요:

- 코드 스타일 가이드 준수
- 새로운 기능 추가 시 유닛 테스트 또는 디버깅 방법 포함
