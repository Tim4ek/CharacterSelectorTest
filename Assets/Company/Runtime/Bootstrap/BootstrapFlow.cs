using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using VContainer.Unity;

namespace Company.Runtime.Bootstrap {
  public class BootstrapFlow : IStartable {
    public async void Start() {
      SceneInstance scene = await Addressables.LoadSceneAsync(RuntimeConstants.CHARACTER_SELECTOR_SCENE, UnityEngine.SceneManagement.LoadSceneMode.Additive);
      SceneManager.SetActiveScene(SceneManager.GetSceneByName(RuntimeConstants.CHARACTER_SELECTOR_SCENE));
      SceneManager.UnloadSceneAsync(RuntimeConstants.BOOTSTRAP);
    }
  }
}