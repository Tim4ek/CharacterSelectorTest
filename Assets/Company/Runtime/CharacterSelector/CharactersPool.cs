using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Company.Runtime.CharacterSelector {
  public class CharactersPool : IDisposable {
    private readonly CharactersSettings _charactersSettings;
    private readonly List<AsyncOperationHandle<GameObject>> _handles = new();
    private readonly List<BaseCharacter> _charactersPool = new();

    private readonly Transform _charactersContainer;

    public int Count => _charactersPool.Count;


    public CharactersPool(CharactersSettings charactersSettings, Transform charactersContainer) {
      _charactersSettings = charactersSettings;
      _charactersContainer = charactersContainer;
    }

    public async void Init() {
      if (_charactersSettings == null || _charactersSettings.CharactersAssets == null || _charactersSettings.CharactersAssets.Count <= 0) {
        Debug.LogError("CharactersPool | Init | _charactersSettings is invalid");
        return;
      }
      int count = _charactersSettings.CharactersAssets.Count;
      for (int i = 0; i < count; i++) {
        BaseCharacter characterPrefab = await LoadCharacterPrefab(_charactersSettings.CharactersAssets[i]);
        if (characterPrefab == null) {
          Debug.LogError("CharactersPool | Init | _characterPrefab == null");
          return;
        }
        BaseCharacter character = UnityEngine.Object.Instantiate(characterPrefab, _charactersContainer.transform, false);
        character.SetVisible(false);
        _charactersPool.Add(character);
      }
    }

    public BaseCharacter GetFromPoolByIndex(int index) {
      if (index < 0) {
        return null;
      }
      return _charactersPool[index];
    }

    private async UniTask<BaseCharacter> LoadCharacterPrefab(AssetReference assetReference) {
      AsyncOperationHandle<GameObject> handle = Addressables.LoadAssetAsync<GameObject>(assetReference);
      _handles.Add(handle);
      GameObject characterGO = await handle;
      if (characterGO.TryGetComponent(out BaseCharacter character) == false) {
        Debug.LogError("Object BaseCharacter is null");
      }
      return character;
    }

    public void OnClear() {
      int charactersPoolCount = _charactersPool.Count;
      for (int i = 0; i < charactersPoolCount; i++) {
        _charactersPool[i].DestroyView();
      }
      _charactersPool.Clear();

      int operationHandleCount = _handles.Count;
      for (int i = 0; i < operationHandleCount; i++) {
        if (_handles[i].IsValid()) {
          Addressables.Release(_handles[i]);
        }
      }
      _handles.Clear();
    }

    public void Dispose() {
      OnClear();
    }
  }
}
