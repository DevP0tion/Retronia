using Retronia.IO.Formats;
using UnityEngine;

namespace Retronia.Scenes.Lobby
{
  public class UIInventory : MonoBehaviour
  {
    private static Inventory Inventory => Inventory.Current;
    private static int InventorySize => Inventory.Size;
    
    #region Binding
    [Header("Binding")]
    
    [SerializeField] private Transform inventoryContainer;
    [SerializeField] private GameObject originBar;
    [SerializeField] private UIItemSlotBar[] activeBars = null;

    #endregion

    public void RegenerateInventoryUI()
    {
      var inventorySize = InventorySize / 3 + 1;
      if(activeBars != null)
      {
        // 이미 인벤토리가 초기화됬을 경우 줄이거나 늘림
        if (activeBars.Length > inventorySize)
        {
          // 기존 길이가 더 길 경우 없애기
          for (var i = inventorySize + 1; i < activeBars.Length; i++)
          {
            Destroy(activeBars[i].gameObject);
          }
          
          activeBars = activeBars[..inventorySize];
        }
        else if (activeBars.Length < inventorySize)
        {
          // 기존 길이가 짧을 경우 늘리기
          var newBars = new UIItemSlotBar[inventorySize];
          for (var i = 0; i < activeBars.Length; i++)
          {
            newBars[i] = activeBars[i];
          }
          
          for (var i = activeBars.Length; i < inventorySize; i++)
          {
            newBars[i] = Instantiate(originBar, inventoryContainer)
              .GetComponent<UIItemSlotBar>();
          }
          
          activeBars = newBars;
        }
      }
      else
      {
        // 초기화되지 않았을 경우 새로 생성
        activeBars = new UIItemSlotBar[inventorySize];
        for (var i = 0; i < activeBars.Length; i++)
        {
          activeBars[i] = Instantiate(originBar, inventoryContainer)
            .GetComponent<UIItemSlotBar>();
        }
      }
      
      // 마지막 줄 개수 설정
      var lastSize = InventorySize % 3;
      if (lastSize != 0)
      {
        var lastBar = activeBars[^1];
          
        for (var i = 0; i < lastSize; i++)
        {
          lastBar[i].gameObject.SetActive(true);
        }
      }
      
      // 슬롯마다 아이템 설정
      for (var i = 0; i < InventorySize; i++)
      {
        var item = Inventory.GetItem(i);
        var slot = activeBars[i / 3][i % 3];
        slot.SetItem(item);
      }
    }
    
  }
}