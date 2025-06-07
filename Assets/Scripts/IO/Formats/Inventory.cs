using System;
using Newtonsoft.Json.Linq;

namespace Retronia.IO.Formats
{
  [Serializable]
  public class Inventory : IJsonSerializable
  {
    public static Inventory Current => CharacterInfo.Current.inventory;

    public void LoadJson(JObject json)
    {
      throw new System.NotImplementedException();
    }

    public JObject ToJson()
    {
      throw new System.NotImplementedException();
    }
  }
}