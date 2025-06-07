using Retronia.IO.Formats;
using UnityEngine;
using CharacterInfo = Retronia.IO.Formats.CharacterInfo;

namespace Retronia.Scenes.Lobby
{
  public class UIInventory : MonoBehaviour
  {
    private static Inventory Inventory => Inventory.Current;
    
    [SerializeField] private int maxSlots = 9;
  }
}