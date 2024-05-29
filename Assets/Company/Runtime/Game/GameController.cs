using Company.Runtime.CharacterSelector;
using Company.Runtime.Utilities;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using VContainer;

namespace Company.Runtime.Game {
  public class GameController : IDisposable {
    private readonly SceneLoader _sceneLoader;
    private readonly CharactersSelectorController _characterSelectorController;
    private readonly CharacterSelectorPresenter _characterSelectorPresenter;
    private readonly CharactersSettings _charactersSettings;
    private readonly Transform _characterContainer;
    private BaseCharacter _character;
    private AsyncOperationHandle<GameObject> _handle;

    [Inject]
    public GameController(SceneLoader sceneLoader, CharactersSelectorController characterSelectorController, CharacterSelectorPresenter characterSelectorPresenter, CharactersSettings charactersSettings, Transform characterContainer) {
      _sceneLoader = sceneLoader;
      _charactersSettings = charactersSettings;
      _characterContainer = characterContainer;
      _characterSelectorController = characterSelectorController;
      _characterSelectorPresenter = characterSelectorPresenter;
    }

    public void OnStart() {
      CreateCharacter();
    }

    private async UniTask CreateCharacter() {
      int selectCharacter = _characterSelectorController.CurrentIndex;
      BaseCharacter characterPrefab = await LoadCharacterPrefab(_charactersSettings.CharactersAssets[selectCharacter]);
      if (characterPrefab == null) {
        Debug.LogError("CharactersPool | Init | _characterPrefab == null");
        return;
      }
      _character = UnityEngine.Object.Instantiate(characterPrefab, _characterContainer.transform, false);
      _character.SetVisible(true);
    }



    private async UniTask<BaseCharacter> LoadCharacterPrefab(AssetReference assetReference) {
      _handle = Addressables.LoadAssetAsync<GameObject>(assetReference);
      GameObject characterGO = await _handle;
      if (characterGO.TryGetComponent(out BaseCharacter character) == false) {
        Debug.LogError("Object BaseCharacter is null");
      }
      return character;
    }

    public void OnBackButton() {
      OnClear();      
      _characterSelectorController.OnStart();
      _characterSelectorPresenter.OnStart();
      _sceneLoader.UnloadScene();
    }

    private void OnClear() {
      if (_character != null) {
        _character.DestroyView();
        _character = null;
      }
      if (_handle.IsValid()) {
        Addressables.Release(_handle);
      }
    }

    public void Dispose() {
      OnClear();
    }
  }
}
