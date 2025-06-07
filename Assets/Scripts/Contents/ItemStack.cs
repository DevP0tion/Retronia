using System;
using Retronia.Contents.Properties;
using Retronia.Utils;
using UnityEngine;

namespace Retronia.Contents
{
  [Serializable]
  public class ItemStack
  {
    /// <summary>
    /// 임시로 Max값으로 선언.
    /// 나중에 정상적?인 숫자로 변경예정
    /// </summary>
    public const int MaxCount = int.MaxValue;

    public ItemProperties item;
    [SerializeField, GetSet(nameof(Count))] protected int count;
    public int Count
    {
      get => count;
      set => count = Math.Max(Math.Min(MaxCount, value), 0);
    }

    public ItemStack(ItemProperties item, int count = 0)
    {
      this.item = item;
      Count = count;
    }
    
    public static implicit operator ItemStack(ItemProperties item) => new (item);
    public static implicit operator ItemProperties(ItemStack item) => item.item;
  }
}