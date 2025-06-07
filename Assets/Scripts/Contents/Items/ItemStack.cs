using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Retronia.Contents.Properties;
using Retronia.IO;
using Retronia.Utils;
using UnityEngine;
using UnityEngine.Events;

namespace Retronia.Contents.Items
{
  [Serializable]
  public class ItemStack : IJsonSerializable
  {
    private static Dictionary<string, ItemProperties> items = new();

    public ItemProperties type = ItemProperties.None;
    // 바뀔 새로운 값을 넘겨줍니다.
    public UnityEvent<int> onAmountChange;
    [SerializeField, GetSet(nameof(Amount))] protected int amount;
    public int Amount
    {
      get => amount;
      set
      {
        var newValue = Math.Max(Math.Min(type.maxAmount, value), 0);
        onAmountChange?.Invoke(newValue);
        amount = newValue;
      }
    }

    public ItemStack(ItemProperties type = null, int amount = 1)
    {
      this.type = type;
      Amount = amount;
    }
    
    public static implicit operator ItemProperties(ItemStack item) => item.type;
    
    #region Serialization

    public static ItemStack Load(JObject json)
    {
      var result = new ItemStack();
      result.LoadJson(json);
      
      return result; 
    }
    
    public void LoadJson(JObject json)
    {
      type = items.GetValueOrDefault(json.Get("type", "none"));
      amount = json.Get("amount", 1);
    }

    public JObject ToJson()
    {
      return new()
      {
        ["type"] = type.name,
        ["amount"] = amount
      };
    }
    
    #endregion
  }
}