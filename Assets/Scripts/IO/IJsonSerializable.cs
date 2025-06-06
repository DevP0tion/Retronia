using Newtonsoft.Json.Linq;

namespace Retronia.IO
{
  public interface IJsonSerializable
  {
    void LoadJson(JObject json);
    JObject ToJson();
  }
}