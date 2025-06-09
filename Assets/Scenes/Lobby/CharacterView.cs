using UnityEngine;

namespace Retronia.Scenes.Lobby
{
  public class CharacterView : MonoBehaviour
  {
    [SerializeField] private UIEquipSlot[] slots = null;

    private void Awake()
    {
      if (slots == null)
      {
        slots = GetComponentsInChildren<UIEquipSlot>();
      }
    }
  }
}