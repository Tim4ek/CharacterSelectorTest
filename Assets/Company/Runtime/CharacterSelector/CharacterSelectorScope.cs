using Company.Runtime.Utilities;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Company.Runtime.CharacterSelector {
  public class CharacterSelectorScope : LifetimeScope {
    [SerializeField] private CharacterSelectorPresenter characterSelectorPresenter;
    [SerializeField] private CharactersSettings charactersSettings;
    [SerializeField] private Transform charactersContainer;
    protected override void Configure(IContainerBuilder builder) {
      builder.Register<SceneLoader>(Lifetime.Singleton);
      builder.Register<CharactersSelectorController>(Lifetime.Singleton).WithParameter("charactersContainer", charactersContainer);

      builder.RegisterComponent(characterSelectorPresenter);
      builder.RegisterComponent(charactersSettings);

      builder.RegisterEntryPoint<CharacterSelectorFlow>();
    }
  }
}