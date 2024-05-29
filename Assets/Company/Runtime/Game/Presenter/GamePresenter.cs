using System;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Company.Runtime.Game {
  public class GamePresenter : MonoBehaviour, IDisposable {
    [SerializeField] private Button _backButton;
    private GameController _gameController;

    [Inject]
    public void Construct(GameController gameController) {
      _gameController = gameController;
    }

    public void OnStart() {
      OnSetAllButtonListener();
    }

    private void OnSetAllButtonListener() {
      _backButton.onClick.AddListener(OnBackButton);
    }

    private void OnBackButton() {
      _gameController.OnBackButton();
    }

    public void Dispose() {
      _backButton.onClick.RemoveListener(OnBackButton);
    }
  }
}
