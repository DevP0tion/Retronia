using System;
using Newtonsoft.Json.Linq;

namespace Retronia.Utils
{
  public static class JsonUtility
  {
    public static JValue ToToken<T>(this T enumValue) where T : struct, Enum
    {
      var value = Convert.ToInt32(enumValue);
      return new JValue(value);
    }
    
    public static T ToEnum<T>(this JValue token, T defaultValue) where T : struct, Enum
    {
      var value = token.Value<int>();

      return Enum.IsDefined(typeof(T), value) ? (T) Enum.ToObject(typeof(T), token.Value<int>()) : defaultValue;
    }
    
    #region Getter 
    
    public static string GetString(this JObject json, string key, string defaultValue = null)
    {
      if (json.TryGetValue(key, out var token))
      {
        var value = token.Value<string>();
        
        return string.IsNullOrEmpty(value) ? defaultValue : value;
      }
      
      return defaultValue;
    }
    
    public static int GetInt(this JObject json, string key, int defaultValue = 0)
      => json.TryGetValue(key, out var token) ? token.Value<int>() : defaultValue;
    
    public static float GetFloat(this JObject json, string key, float defaultValue = 0)
    => json.TryGetValue(key, out var token) ? token.Value<float>() : defaultValue;

    public static bool GetBool(this JObject json, string key, bool defaultValue = false)
    => json.TryGetValue(key, out var token) ? token.Value<bool>() : defaultValue;

    public static T GetEnum<T>(this JObject json, string key, T defaultValue = default) where T : struct, Enum
    {
      if (json.TryGetValue(key, out var token))
      {
        var value = token.Value<int>();
        return Enum.IsDefined(typeof(T), value) ? (T) Enum.ToObject(typeof(T), value) : defaultValue;
      }
      
      return defaultValue;
    }

    public static T Get<T>(this JObject json, string key, T defaultValue = default)
      => json.TryGetValue(key, out var token) ? token.Value<T>() : defaultValue;
    
    #endregion

    #region Setter

    public static int Set<T>(this JObject json, string key, T value) where T : struct, Enum
    {
      var token = Convert.ToInt32(value);
      json[key] = token;
      
      return token;
    }

    #endregion
  }
}