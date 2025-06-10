using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

namespace Retronia.Core
{
  public static class Localizer
  {
    private static SharedTableData generalTableData;
    private static readonly Dictionary<LocaleIdentifier, StringTable> generalTables = new();
    
#if UNITY_EDITOR
    public readonly static SerializableDictionary<LocaleIdentifier, StringTable> TableCollection = new();
#endif

    public static (AsyncOperationHandle sharedTableLoader, AsyncOperationHandle stringTableLoader) Load()
    {
      var sharedTableLoader = Addressables.LoadAssetAsync<SharedTableData>("General/Shared_Data");
      var stringTableLoader = Addressables.LoadAssetsAsync<StringTable>(new AssetLabelReference { labelString = "Locale" }, table =>
      {
        #if UNITY_EDITOR
        TableCollection[table.LocaleIdentifier] = table;
        #endif
        generalTables[table.LocaleIdentifier] = table;
      });
      
      sharedTableLoader.Completed += handle =>
      {
        generalTableData = handle.Result;
      };
      
      return (sharedTableLoader, stringTableLoader);
    }

    public static Locale ActiveLocale
    {
      get => LocalizationSettings.SelectedLocale;
      set => LocalizationSettings.SelectedLocale = value;
    }
    
    public static string Localize(this string key, params string[] args)
    {
      var entryKey = generalTableData.GetEntry(key);
      if (generalTables.TryGetValue(ActiveLocale.Identifier, out var table))
      {
        var id = generalTableData.GetId(key);
        if(table.TryGetValue(id, out var entry)) return entry.Value;
      }
      
      return $"unknown key - {key}";
    }
    
    public static string LocalizeWithScene(this string key) => Localize(SceneManager.GetActiveScene().name + "_" + key);
    
  }
}