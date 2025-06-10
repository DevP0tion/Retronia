using Retronia.Contents.Items;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Retronia.Scenes.Lobby
{
  public class UIItemSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerMoveHandler
  {
    private static LobbySystem lobby => LobbySystem.Instance;
    [SerializeField] private ItemStack stack;
    [SerializeField] private Image background, item, 
      // 이후 아이템 등급 구현시 등급 표현 용도
      border;
    [SerializeField] private TMP_Text amountText;
    
    public ItemStack Stack => stack;

    public void SetItem(ItemStack itemStack)
    {
      if (itemStack != null)
      {
        if (stack != itemStack)
        {
          itemStack.onAmountChange.AddListener(SetAmount);
          stack?.onAmountChange.RemoveListener(SetAmount);
        }
        stack = itemStack;
        item.sprite = itemStack.type.sprite;
        item.color = itemStack.type.color;
        amountText.text = itemStack.type.maxAmount.ToString();
      }
      else if(stack != null)
      {
        stack.onAmountChange.RemoveListener(SetAmount);
        stack = null;
        item.sprite = null;
        item.color = Color.clear;
        amountText.text = 0 + "";
      }
    }

    private void SetAmount(int newAmount) => amountText.text = newAmount + "";
    
    public void OnPointerEnter(PointerEventData eventData)
    {
    }
    
    public void OnPointerMove(PointerEventData eventData)
    {
    }

    public void OnPointerExit(PointerEventData eventData)
    {
    }
  }
}