using NaughtyAttributes;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Audio;
using UnityEngine.Localization.Tables;
using UnityEngine.SceneManagement;

namespace Retronia.Core
{ 
  public class GameManager : MonoBehaviour
  {
    public static GameManager Instance { get; private set; }
    
    /// <summary>
    /// 현재 활성화된 언어의 키에 맞는 문자열을 가져올 수 있습니다.
    /// GameManager.Instance.Localization["가져올 문자열의 키"]로 값을 가져올 수 있습니다.
    /// </summary>
    // public StringTable Localization => (StringTable)localization.GetTable(LocalizationSettings.SelectedLocale);
    
    private void Awake()
    {
      if(Instance)
      {
        DestroyImmediate(this);
      }
      else
      {
        Instance = this;
        DontDestroyOnLoad(gameObject);
      }
    }

    public void Load()
    {
    }

    [Button]
    private void Test()
    {
      // var entry = tableData.GetEntry("IntroScene_loading");
      // Addressables.LoadAssetsAsync<StringTable>(new AssetLabelReference { labelString = "Locale" }, table =>
      // {
      //   Debug.Log(table.LocaleIdentifier);
      // });
      var generalTableData = Addressables.LoadAssetAsync<SharedTableData>(new AssetReference("General/Shared_Data")).WaitForCompletion();

      Debug.Log(generalTableData);
    }
  }
}
