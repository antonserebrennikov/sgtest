# Game Project (Unity, URP, C#)

A modular Unity project built with a MVP (Model-View-Presenter) architecture, lightweight dependency injection, and asynchronous loading patterns. The project includes multiple feature scenes (e.g., Cards, Words, Fire) and common infrastructure for scenes, presenters, and asset loading.

Project was developed in just 16 working hours prioritizing architecture, extensibility, and maintainability over visual polish and logic feature details (which can be easily fixed and polished later).

Latest WebGL build is here: https://play.unity.com/en/games/9637d1c6-761b-4b69-9207-f73ce4bde3a5/sg-test-game

---

## Overview
This project demonstrates a clean separation between UI logic (Presenters), UI visuals (Views), and scene lifecycle management (Scene Starters/Controllers). It uses a small DI container for wiring dependencies and emphasizes async operations for loading assets, presenters (with caching), and scenes. Universal Render Pipeline (URP) is used for rendering.

## Tech Stack
- Unity Editor 6000.1.4f1 (URP)
- C# 9.0
- .NET Framework 4.7.1 (target)
- Addressables for prefab loading
- DOTween for tween for scripted animations
- Lightweight DI via TinyContainer
- Multiple free assets from the Unity Asset Store

## Architecture

### Modules and Responsibilities
- Common (Infrastructure)
  - Scene utilities, prefab loaders, presenter loader, loading presenter access, and shared controllers (e.g., back button behavior).
- Feature Modules
  - Cards: Card views and presenters, stack/animation flow, and a scene starter/controller to orchestrate the feature.
  - Words: Model-driven content with a words presenter and a dedicated scene starter.
  - Fire: Simple Animator-driven flow with a presenter/view and a scene starter.
- Scene Starters / Controllers
  - Each scene has a dedicated starter that resolves dependencies, initializes presenters/controllers, and coordinates loading visibility.

### Dependency Injection
- TinyContainer is used for:
  - Global services (scene loader, prefab loader, presenter loader, models).
  - Scene-level presenters and services.
- Global registration occurs in the initial scene (InitialSceneStarter).
- Scene-level resolution is done from MonoBehaviours to retrieve services for that context.

### Asynchronous Flow
- Asset and presenter loading uses async/await to keep the UI responsive.
- Loading UI is controlled centrally (show/hide) during initialization and deinitialization of scenes and presenters.
- Scene starters typically:
  - Resolve services
  - Load necessary presenters/assets
  - Hide the loading overlay on success
  - Re-show it (and clean up) on deinit

### Asset Loading
- Prefabs are loaded through an abstraction (prefab loader), backed by Addressables.
- UI presenters are instantiated from prefabs placed under a defined folder (convention-based, Assets/AddressableResources/Prefabs), making it easy to add new presenter prefabs.

## Project Layout
- Assets/
  - Anim/ - Animator controllers and animation clips
  - Configs/ - Feature-specific ScriptableObjects and configurations
  - Scenes/ - Scene definitions
    - Initial.unity - Initial scene for global service registration
    - Cards.unity - Cards feature scene
    - Words.unity - Words feature scene
    - Fire.unity - Fire feature scene
  - AddressableResources/
    - Prefabs/ - UI prefabs for presenters, core game prefabs, and UI prefabs for Addressables
  - Scripts/
    - Game/
      - Common/ - Infrastructure (controllers, loaders, common logic and utilities)
      - Cards/ - Cards feature (presenter/view/controller)
      - Words/ - Words feature module (presenter/view/controller/model)
      - Fire/ - Fire feature module (presenter/view/controller)
    - MVP/ - Base MVP framework (Presenter, View, interfaces)

## Getting Started

### Opening the Project
- Use Unity Hub to add the project folder and open it with Unity 6000.1.4f1.

### Running in Editor
- Open the initial/boot scene Assets/Scenes/Initial.unity.
- Press Play in the Unity Editor.
- The initial scene registers global services, shows a loading UI, and transitions to the main/feature scenes using the scene loader.

## Scenes
- Initial Scene
  - Sets target FPS and registers global services (scene loader, prefab loader, presenter loader, data/model services).
  - Loads the initial gameplay/menu scene after preparing the loading presenter.
- Feature Scenes
  - Cards: Demonstrates stacked card animations and shuffling. Uses async initialization and shows a completion presenter when done.
  - Words: Loads dialogs content and visuals asynchronously, showing and hiding loading appropriately.
  - Fire: Demonstrates a simple animator-driven feature of changing ParticleSystem start color by using states and trigger initiated transition.

Navigation and Back Button
- Feature scene starters inherit from a common back-button-aware controller (e.g., showing loading on exit or navigation events).

## Development Guide

### Adding a New Presenter/View
1. Create a View MonoBehaviour for UI elements.
2. Create a Presenter MonoBehaviour to coordinate the View.
3. Create a prefab for the presenter (place it under the UI prefabs root AddressableResources/Prefabs/UI).
4. Load the presenter via the presenter loader in the relevant scene starter or controller.
5. Show or hide the presenter according to the scene flow.
6. Create a corresponding Model if needed and register it in the DI container (typically at the app start: InitialSceneStarter).

### Working with Addressables
- Group UI prefabs under a consistent path for lookups.
- Maintain address keys consistent with loader conventions.
- Rebuild Addressables when adding or changing assets.

## Coding Standards
- Use clear, self-documenting class and method names.
- Favor composition of presenters and views; keep logic in presenters where possible.
- Follow Conventional Commits for commit messages:
  - Example: feat(cards): add stacked card animations
  - Scopes: use module or feature names (e.g., cards, words, fire, common)
- Prefer async patterns for any blocking or loading operations.

## FAQ
- Which scene should I play first?
  - The Scenes/Initial scene. It registers services and coordinates the first transition.

## TODOs
- Make better looking layouts for the UI.
- Make cards animations interruptable (force complete tweens).
- Add emoji support for words (replacing {vars} in text with unicode codes, select font with emojis as backup font).
- Add object pooling to avoid creating/destroying objects repeatedly (cards, card stacks).
- Remove unused graphic assets, pack remaining Sprites into atlases.
- Enhance testing coverage for edge cases and error handling scenarios.
- Enhance error handling and logging (network loaded deserialization, async operations).