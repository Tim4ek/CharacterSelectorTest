using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Company.Runtime.Game {
  public class GameScope : LifetimeScope {
    [SerializeField] private GamePresenter gamePresenter;
    [SerializeField] private Transform characterContainer;
    protected override void Configure(IContainerBuilder builder) {
      builder.Register<GameController>(Lifetime.Scoped).WithParameter("characterContainer", characterContainer);

      builder.RegisterComponent(gamePresenter);

      builder.RegisterEntryPoint<GameFlow>();
    }
  }
}