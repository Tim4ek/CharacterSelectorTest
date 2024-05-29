
using Company.Runtime.Utilities;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace Company.Runtime.CharacterSelector {
  public class CharactersSelectorController {
    private readonly SceneLoader _sceneLoader;
    private readonly CharactersPool _charactersPool;

    private int _currentIndex = -1;

    public int CurrentIndex => _currentIndex;

    [Inject]
    public CharactersSelectorController(SceneLoader sceneLoader, CharactersSettings charactersSettings, Transform charactersContainer) {
      _sceneLoader = sceneLoader;
      _charactersPool = new CharactersPool(charactersSettings, charactersContainer);
    }

    public void OnStart(bool isFirstStart = false) {
      if (isFirstStart) {
        InitPool();
      } else {
        CreateCharacter();
      }
    }

    private void InitPool() {
      _charactersPool.Init();
    }

    private void CreateCharacter() {
      BaseCharacter character = _charactersPool.GetFromPoolByIndex(_currentIndex);
      character.SetVisible(true);
    }


    public void Generate() {
      int maxCount = _charactersPool.Count;
      int index = UnityEngine.Random.Range(0, maxCount);
      while (index == _currentIndex) {
        index = UnityEngine.Random.Range(0, maxCount);
      }
      ReturnToPoolLastCharacter();
      _currentIndex = index;
      BaseCharacter character = _charactersPool.GetFromPoolByIndex(_currentIndex);
      character.SetVisible(true);
    }

    private void ReturnToPoolLastCharacter() {
      if (_currentIndex >= 0) {
        BaseCharacter character = _charactersPool.GetFromPoolByIndex(_currentIndex);
        character.SetVisible(false);
      }
    }

    public bool TryPlay() {
      if (_currentIndex >= 0) {
        ReturnToPoolLastCharacter();
        _sceneLoader.LoadSceneAsync(RuntimeConstants.GAME_SCENE).Forget();
        return true;
      } else {
        return false;
      }
    }
  }
}
