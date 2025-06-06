using System;
using Newtonsoft.Json.Linq;

namespace Retronia.IO.Formats
{
  [Serializable]
  public class PlayerInfo : IJsonSerializable
  {
    public static PlayerInfo Current => SAVE.Current?.player;
    
    public string name = "Alpha";
    public int level = 1;
    public int exp = 0;
    public int gold = 1000;

    public PlayerInfo()
    {
    }

    public void LoadJson(JObject json)
    {
      name = json.TryGetValue(nameof(name), out var token) ? token.Value<string>() : name;
      level = json.TryGetValue(nameof(level), out token) ? token.Value<int>() : level;
      exp = json.TryGetValue(nameof(exp), out token) ? token.Value<int>() : exp;
      gold = json.TryGetValue(nameof(gold), out token) ? token.Value<int>() : gold;
    }

    public JObject ToJson()
    {
      return new JObject
      {
        [nameof(name)] = name,
        [nameof(level)] = level,
        [nameof(exp)] = exp,
        [nameof(gold)] = gold,
      };
    }
  }
}