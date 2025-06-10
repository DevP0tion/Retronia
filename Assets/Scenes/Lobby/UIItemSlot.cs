using Retronia.Contents.Items;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Retronia.Scenes.Lobby
{
  public class UIItemSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerMoveHandler
  {
    private static LobbySystem Lobby => LobbySystem.Instance;
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
        amountText.text = itemStack.Amount.ToString();
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
      border.color = new Color(1f, 0.5f, 0f);
      LobbySystem.Instance.focusedSlot = this;
      
      if (stack == null || !stack.type) return;
      Lobby.itemDescriptionPanel.Item = stack?.type;
      Lobby.itemDescriptionPanel.gameObject.SetActive(true);
    }
    
    public void OnPointerMove(PointerEventData eventData)
    {
      border.color = new Color(1f, 0.5f, 0f);

      if (stack == null || !stack.type) return;
      Lobby.itemDescriptionPanel.transform.position = eventData.position + new Vector2(5, 5);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
      border.color = Color.white;
      LobbySystem.Instance.focusedSlot = null;

      if (stack == null || !stack.type) return;
      Lobby.itemDescriptionPanel.gameObject.SetActive(false);
    }
  }
}