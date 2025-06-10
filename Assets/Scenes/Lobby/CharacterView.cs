using System.Linq;
using Retronia.Contents.Items;
using Retronia.Contents.Properties;
using Retronia.Utils;
using UnityEngine;

namespace Retronia.Scenes.Lobby
{
  public class CharacterView : MonoBehaviour
  {
    [SerializeField] private UIEquipSlot[] slots = null;

    [SerializeField, GetSet(nameof(OpenSlots))] private bool openSlots = false;
    public bool OpenSlots
    {
      get => openSlots;
      set
      {
        if (slots != null)
        {
          foreach (var slot in slots)
          {
            slot.gameObject.SetActive(value);
          }
        }
        openSlots = value;
      }
    }

    private void Awake()
    {
      if (slots == null)
      {
        slots = GetComponentsInChildren<UIEquipSlot>();
      }
    }

    public void Equip(ItemStack item)
    {
      if (item.type is IEquipable equipable)
      {
        var equipSlot = (from slot in slots where slot.part == equipable.Part select slot).First();

        equipSlot?.Equip(item);
      }
    }
  }
}