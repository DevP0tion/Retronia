using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Retronia.IO.Formats;
using UnityEngine;
using UnityEngine.Serialization;

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
    [SerializeField] private bool initialized = false;
    public bool IsInitialized => initialized;
    
    #region Modules
    
    public PlayerInfo player = null;
    
    #endregion
    
    public SAVE(string name, bool isRoot = true)
    {
      this.name = name;
    }

    public void Init()
    {
      if(initialized) return;
      initialized = true;

      if (player == null) player = new();
    }
    
    public async Task Save()
    {
      var path = System.IO.Path.Combine(SavePath, name + ".json");
      
      if (!Directory.Exists(SavePath))
        Directory.CreateDirectory(SavePath);
      
      var writer = File.CreateText(path);
      var jsonWriter = new JsonTextWriter(writer);
      jsonWriter.Formatting = Formatting.Indented;
      
      // 데이터를 json 문자열 데이터로 변환
      var writeData = new JObject(this)
      {
        [nameof(player)] = player.ToJson(),
      };

      await writeData.WriteToAsync(jsonWriter);
      
#if UNITY_EDITOR
      Debug.Log($"Save as {path} Complete!");
#endif
      writer.Close();
    }

    public void SaveSync()
    {
      Save().Wait();
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
    
    public static async Task<SAVE> Load(string name, bool create = false)
    {
      var path = System.IO.Path.Combine(SavePath, name + ".json");
      if (Directory.Exists(SavePath) && File.Exists(path))
      {
        var reader = new JsonTextReader(File.OpenText(path));
        var result = new SAVE(name);

        if (await ReadFromAsync(reader) is JObject obj)
        {
          foreach (var (key, value) in obj)
          {
            if(value is null) continue;

            switch (key)
            {
              case nameof(player):
                result.player = new((JObject)value);
                break;
              default:
                result[key] = value;
                break;
            }
          }
          result.Init();
          return result;
        }
      }
      else if(create)
      {
        var save = new SAVE(name);
        await save.Save();
        return save;
      }
      
      return null;
    }

    public static SAVE LoadSync(string name, bool create = false)
    {
      var task = Load(name, create);
      task.Wait();
      return task.Result;
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

    public static bool Exits(string name)
    {
      var path = System.IO.Path.Combine(SavePath, name + ".json");
      return Directory.Exists(SavePath) && File.Exists(path);
    }

    public static bool TryGet(string name, out SAVE save)
    {
      save = null;
      if (Exits(name))
      {
        var task = Load(name);
        task.Wait();
        save = task.Result;
        return true;
      }
      
      return false;
    }
    
    #endregion
  }
}