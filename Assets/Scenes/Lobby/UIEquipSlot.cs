using Retronia.Contents.Items;
using Retronia.Contents.Properties;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Retronia.Scenes.Lobby
{
  public class UIEquipSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerMoveHandler
  {
    private static LobbySystem Lobby => LobbySystem.Instance;

    [FormerlySerializedAs("back1")] [SerializeField] private Image border;
    [FormerlySerializedAs("back2")] [SerializeField] private Image background;
    [SerializeField] private Image image;
    public EquipmentPart part;
    [SerializeField] private ItemStack stack;

    public void Equip(ItemStack stack)
    {
      if (this.stack != stack && stack is { type: IEquipable equipable })
      {
        image.sprite = stack.type.sprite;
        image.color = stack.type.color;
      }

      if (stack == null)
      {
        image.sprite = null;
        image.color = Color.clear;
      }
      
      this.stack = stack;
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
      border.color = new Color(1f, 0.5f, 0f);
      
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