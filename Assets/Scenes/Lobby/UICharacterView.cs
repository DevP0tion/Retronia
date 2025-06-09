using System;
using UnityEngine;

namespace Retronia.Scenes.Lobby
{
  public class UICharacterView : MonoBehaviour
  {
    [SerializeField] private UIEquipSlot[] slots = null;

    private void Awake()
    {
      if (slots == null)
      {
        slots = GetComponentsInChildren<UIEquipSlot>();
      }
    }
  }
}