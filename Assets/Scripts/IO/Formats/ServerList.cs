using Newtonsoft.Json.Linq;

namespace Retronia.IO.Formats
{
  public class ServerList : IJsonSerializable
  {
    public ServerList(JObject json = null)
    {
      if(json != null)
        LoadJson(json);
    }
    
    public void LoadJson(JObject json)
    {
    }

    public JObject ToJson()
    {
      return new JObject
      {
        
      };
    }
  }

  public class ServerInfo
  {
    public string name;
    public string address;
    public int port;
  }
}