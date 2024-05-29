using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Company.Runtime.CharacterSelector {
  public class GenerateAndPlayButtons : MonoBehaviour {
    [SerializeField] private Button generateBtn, playBtn;

    public void SetListenerGenerateButton(UnityAction action) {
      generateBtn.onClick.RemoveAllListeners();
      generateBtn.onClick.AddListener(action);
    }

    public void SetListenerPlayButton(UnityAction action) {
      playBtn.onClick.RemoveAllListeners();
      playBtn.onClick.AddListener(action);
    }

    public void Dispose() {
      playBtn.onClick.RemoveAllListeners();
      generateBtn.onClick.RemoveAllListeners();
    }
  }
}
