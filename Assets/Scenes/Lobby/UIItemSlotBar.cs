using UnityEngine;

namespace Retronia.Scenes.Lobby
{
  public class UIItemSlotBar : MonoBehaviour
  {
    public UIItemSlot slot1, slot2, slot3;

    public UIItemSlot this[int index] => index switch
    {
      0 => slot1,
      1 => slot2,
      2 => slot3,
      _ => slot1
    };
  }
}