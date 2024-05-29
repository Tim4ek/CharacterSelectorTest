using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using UnityEngine;
using VContainer;

namespace Company.Runtime.CharacterSelector {
  public class CharacterSelectorPresenter : MonoBehaviour, IDisposable {
    [SerializeField] private GenerateAndPlayButtons generateAndPlayButtons;
    private const string _alertTweenID = "AlertTween";
    [SerializeField] private CanvasGroup _alertPanel;

    private CharactersSelectorController _�haractersSelectorController;
    private bool _isShowingAlert;

    [Inject]
    public void Construct(CharactersSelectorController �haractersSelectorController) {
      _�haractersSelectorController = �haractersSelectorController;
    }

    public void OnStart() {
      OnSetAllButtonListener();
      OnSetView();
    }

    private void OnSetView() {
      _alertPanel.gameObject.SetActive(false);
    }

    private void OnSetAllButtonListener() {
      generateAndPlayButtons.SetListenerGenerateButton(() => {
        _�haractersSelectorController.Generate();
      });
      generateAndPlayButtons.SetListenerPlayButton(() => {
        if (!_�haractersSelectorController.TryPlay()) {
          ShowAlertPanel();
        } else {
          Dispose();
        }
      });
    }

    private async UniTask ShowAlertPanel() {
      if (_isShowingAlert) {
        return;
      }
      _alertPanel.gameObject.SetActive(true);
      _alertPanel.alpha = 0;
      DOTween.Kill(_alertTweenID);
      Sequence anim = DOTween.Sequence(_alertTweenID).OnComplete(() => {
        _alertPanel.alpha = 0;
        _alertPanel.gameObject.SetActive(false);
        _isShowingAlert = false;
      })
         .Insert(0f, _alertPanel.DOFade(1, 1.0f))
         .Insert(1.0f, _alertPanel.DOFade(0, 0.5f));
      _isShowingAlert = true;
      await anim.Play().ToUniTask();
    }

    public void Dispose() {
      generateAndPlayButtons.Dispose();
    }
  }
}
