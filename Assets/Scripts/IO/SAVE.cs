using System;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Retronia.IO
{
  /// <summary>
  /// 임시로 단일 JSON으로 구현하였습니다. <br/>
  /// TODO 네임스페이스(폴더)단위 파일 저장 시스템 구축 필요
  /// </summary>
  [Serializable]
  public class SAVE : JObject
  {
    public readonly string name;
    
    public SAVE(string name, bool isRoot = true) : base()
    {
      this.name = name;
    }
    
    public async Task Save()
    {
      var path = System.IO.Path.Combine(SavePath, name + ".json");
      
      if (!Directory.Exists(SavePath))
        Directory.CreateDirectory(SavePath);
      
      var writer = File.CreateText(path);
      var jsonWriter = new JsonTextWriter(writer);
      jsonWriter.Formatting = Formatting.Indented;
      await WriteToAsync(jsonWriter);
      Debug.Log($"Save as {path} Complete!");
      
      writer.Close();
    }

    public bool CheckIntegrity()
    {
      return true;
    }

    #region Static

    public static readonly string SavePath = System.IO.Path.Combine(Application.persistentDataPath, "Saves");
    public static SAVE Current = null;
    
    public static async Task SaveCurrent()
    {
      if(Current != null)
        await Current.Save();
    }
    
    public static async Task<SAVE> Load(string name)
    {
      var path = System.IO.Path.Combine(SavePath, name + ".json");
      if (Directory.Exists(SavePath) && File.Exists(path))
      {
        var reader = new JsonTextReader(File.OpenText(path));
        var result = new SAVE(name);

        if (await ReadFromAsync(reader) is JObject obj)
        {
          foreach (var (key, value) in obj)
            result[key] = value;
          
          return result;
        }
      }
      
      return null;
    }

    public static FileInfo[] GetSaveNames()
    {
      FileInfo[] result = null;
      
      if (Directory.Exists(SavePath))
      {
        result = new DirectoryInfo(SavePath).GetFiles("*.json");
      }
      
      return result;
    }
    
    #endregion
  }
}