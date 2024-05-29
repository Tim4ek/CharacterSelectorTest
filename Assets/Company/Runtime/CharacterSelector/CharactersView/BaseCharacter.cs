using System;
using UnityEngine;

namespace Company.Runtime.CharacterSelector {
  public abstract class BaseCharacter : MonoBehaviour {
    public void SetVisible(bool isVisible) {
      this.gameObject.SetActive(isVisible);
    }

    public virtual void DestroyView() {
      Destroy(this.gameObject);
    }
  }
}
