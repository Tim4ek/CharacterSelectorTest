using VContainer;
using VContainer.Unity;

namespace Company.Runtime.CharacterSelector {
  public class CharacterSelectorFlow : IStartable {
    private readonly CharactersSelectorController _characterSelectorController;
    private readonly CharacterSelectorPresenter _characterSelectorPresenter;

    [Inject]
    public CharacterSelectorFlow(CharactersSelectorController characterSelectorController, CharacterSelectorPresenter characterSelectorPresenter) {
      _characterSelectorController = characterSelectorController;
      _characterSelectorPresenter = characterSelectorPresenter;
    }

    public void Start() {
      _characterSelectorController.OnStart(true);
      _characterSelectorPresenter.OnStart();
    }
  }
}