using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Retronia.Contents;

namespace Retronia.IO.Formats
{
  [Serializable]
  public class CharacterInfo : IJsonSerializable
  {
    public static CharacterInfo Current => SAVE.Current?.player;
    
    public string name = "Alpha";
    public Elemental elemental = Elemental.Normal;
    public int level = 1;
    public int exp = 0;
    public int gold = 1000;
    public Inventory inventory = new();

    /// <summary>
    /// 10렙마다 요구량 크게 증가, 그 외에는 조금씩 증가
    /// </summary>
    public int MaxExp
    {
      get
      {
        var value = Math.Max(level / 10, 1);
        return value * (value + level % 10);
      }
    }

    public readonly Dictionary<string, int> status = new()
    {
      ["hp"] = 100,
      ["atk"] = 35,
      ["def"] = 40,
      ["critChance"] = 25,
    };

    public CharacterInfo(JObject json = null)
    {
      if(json != null)
        LoadJson(json);
    }

    public void LoadJson(JObject json)
    {
      name = json.TryGetValue(nameof(name), out var token) ? token.Value<string>() : name;
      level = json.TryGetValue(nameof(level), out token) ? token.Value<int>() : level;
      exp = json.TryGetValue(nameof(exp), out token) ? token.Value<int>() : exp;
      gold = json.TryGetValue(nameof(gold), out token) ? token.Value<int>() : gold;
      elemental = json.TryGetValue(nameof(elemental), out token) ? token.ToEnum(elemental) : elemental;

      if (json.TryGetValue(nameof(status), out token) && token is JObject dic)
      {
        foreach (var (key, value) in dic)
        {
          if (value is not null)
            status[key] = value.Value<int>();
        }
      }
      
      if(json.TryGetValue(nameof(inventory), out token) && token is JObject inventoryJson)
        inventory.LoadJson(inventoryJson);
    }

    public JObject ToJson()
    {
      return new JObject
      {
        [nameof(name)] = name,
        [nameof(level)] = level,
        [nameof(exp)] = exp,
        [nameof(gold)] = gold,
        [nameof(elemental)] = elemental.ToToken(),
        [nameof(status)] = status.DicToJObject(),
        [nameof(inventory)] = inventory.ToJson()
      };
    }
  }
}