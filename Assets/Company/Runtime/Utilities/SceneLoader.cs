using Company.Runtime.CharacterSelector;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;

namespace Company.Runtime.Utilities {
  public class SceneLoader {
    private readonly CharacterSelectorScope _characterSelectorScope;
    private Dictionary<string, SceneInstance> _sceneHolder = new Dictionary<string, SceneInstance>();

    [Inject]
    public SceneLoader(CharacterSelectorScope characterSelectorScope) {
      _characterSelectorScope = characterSelectorScope;
    }

    public async UniTask LoadSceneAsync(string sceneName) {
      UnloadScene();
      using (LifetimeScope.EnqueueParent(_characterSelectorScope)) {
        SceneInstance scene = await Addressables.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        string sceneName2 = scene.Scene.name;
        _sceneHolder.Add(sceneName2, scene);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
      }
    }

    public void UnloadScene() {
      foreach (var scene in GetAllLoadedScene()) {
        if (!scene.name.Equals(RuntimeConstants.CHARACTER_SELECTOR_SCENE)) {
          Addressables.UnloadSceneAsync(_sceneHolder[scene.name]);
          _sceneHolder.Remove(scene.name);
        }
      }
    }

    private Scene[] GetAllLoadedScene() {
      int countLoaded = SceneManager.sceneCount;
      var loadedScenes = new Scene[countLoaded];

      for (var i = 0; i < countLoaded; i++) {
        loadedScenes[i] = SceneManager.GetSceneAt(i);
      }

      return loadedScenes;
    }
  }
}