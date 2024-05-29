using VContainer;
using VContainer.Unity;

namespace Company.Runtime.Game {
  public class GameFlow : IStartable {
    private readonly GameController _gameController;
    private readonly GamePresenter _gamePresenter;

    [Inject]
    public GameFlow(GameController gameController, GamePresenter gamePresenter) {
      _gameController = gameController;
      _gamePresenter = gamePresenter;
    }

    public void Start() {
      _gameController.OnStart();
      _gamePresenter.OnStart();
    }
  }
}