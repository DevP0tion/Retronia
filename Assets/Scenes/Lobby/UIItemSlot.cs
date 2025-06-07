using Retronia.Contents;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Retronia.Scenes.Lobby
{
  public class UIItemSlot : MonoBehaviour
  {
    [SerializeField] private ItemStack stack;
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text amountText;
    
    public ItemStack Stack => stack;

    public void SetItem(ItemStack itemStack)
    {
      stack = itemStack;
      image.sprite = itemStack.type.sprite;
      image.color = itemStack.type.color;
      amountText.text = itemStack.type.maxAmount.ToString();
    }
  }
}