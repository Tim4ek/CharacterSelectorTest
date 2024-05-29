# Test Assignment: Character Selection Game on Unity

- Overview
This test assignment involves creating a basic Unity game with character selection functionality. The game should allow users to randomly select a character from a set of prefabs and then display that character in a game scene. The project follows best practices by separating game logic from the Unity UI, and it utilizes several Unity tools and libraries for enhanced functionality.

- Tools and Libraries Used
VContainer: Dependency Injection framework for Unity.
DOTween: Animation and timing library.
UniTask: Library for asynchronous programming with Unity.
Addressables: Asset management system for dynamically loading assets.

- Development Notes
Separation of Concerns: All game logic is encapsulated in classes that do not inherit from MonoBehaviour to ensure a clear separation between the logic and the Unity UI.
Dependency Injection: VContainer is used to manage dependencies and ensure decoupled code.
Async Operations: UniTask is utilized for asynchronous operations, such as scene loading, to keep the game responsive.
Animations: DOTween is used to handle any animations within the UI and game
scenes.
Dynamic Asset Management: Addressables are used for efficient and flexible asset management, allowing dynamic loading and unloading of assets as needed.
