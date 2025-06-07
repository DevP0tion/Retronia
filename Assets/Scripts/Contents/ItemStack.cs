using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Retronia.Contents.Properties;
using Retronia.IO;
using Retronia.Utils;
using UnityEngine;

namespace Retronia.Contents
{
  [Serializable]
  public class ItemStack : IJsonSerializable
  {
    private static Dictionary<string, ItemProperties> items = new();

    public ItemProperties type;
    [SerializeField, GetSet(nameof(Amount))] protected int amount;
    public int Amount
    {
      get => amount;
      set => amount = Math.Max(Math.Min(type.maxAmount, value), 0);
    }

    public ItemStack()
    {
      type = null;
      amount = 0;
    }

    public ItemStack(ItemProperties type, int amount = 0)
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
      amount = json.Get("amount", 0);
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