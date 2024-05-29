using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Company.Runtime.CharacterSelector {
  [CreateAssetMenu(fileName = "CharactersSettings", menuName = "Company/CharactersSettings")]
  public class CharactersSettings : ScriptableObject {
    [SerializeField] private List<AssetReference> _charactersAssets;
    public IReadOnlyList<AssetReference> CharactersAssets => _charactersAssets;
  }
}
