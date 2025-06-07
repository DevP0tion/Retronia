using Retronia.IO.Formats;
using UnityEngine;

namespace Retronia.Scenes.Lobby
{
  public class UIInventory : MonoBehaviour
  {
    private static Inventory Inventory => Inventory.Current;
    
    [SerializeField] private int size => Inventory.Size;
    
    #region Binding
    [Header("Binding")]
    
    [SerializeField] private Transform inventoryContainer;
    [SerializeField] private UIItemSlotBar originBar;
    [SerializeField] private UIItemSlotBar[] activeBars = null;

    #endregion

    private void RegenerateInventoryUI()
    {
      if(activeBars != null)
      {
        // 이미 인벤토리가 초기화됬을 경우 줄이거나 늘림
      }
      else
      {
        // 초기화되지 않았을 경우 새로 생성
      }
    }
  }
}