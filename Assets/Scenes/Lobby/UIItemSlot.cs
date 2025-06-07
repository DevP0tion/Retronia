using Retronia.Contents;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Retronia.Scenes.Lobby
{
  public class UIItemSlot : MonoBehaviour
  {
    [SerializeField] private ItemStack stack;
    [SerializeField] private Image background, item, 
      // 이후 아이템 등급 구현시 등급 표현 용도
      border;
    [SerializeField] private TMP_Text amountText;
    
    public ItemStack Stack => stack;

    public void SetItem(ItemStack itemStack)
    {
      stack = itemStack;
      item.sprite = itemStack.type.sprite;
      item.color = itemStack.type.color;
      amountText.text = itemStack.type.maxAmount.ToString();
    }
  }
}