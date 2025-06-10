using Retronia.Contents.Properties;
using Retronia.Core;
using Retronia.Utils;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Tables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Retronia.Scenes.Intro
{
  public class ContentLoader : MonoBehaviour
  {
    private StringTable localization;
    [SerializeField] private Button entryButton;
    [SerializeField] private LocalizeStringEvent entryText;
    
    
    #region Unity Events
    
    private void Start()
    {
      Load();
      
      // 로딩이 완료됬을 시 게임을 시작할 수 있게 설정
      entryText.StringReference = new LocalizedString("General", "Intro_finished");
      entryButton.onClick.AddListener(() => SceneManager.LoadScene(SceneNames.MainMenu));
    }
    
    #endregion

    public void Load()
    {
      var (sharedTableLoader, stringTableLoader) = Localizer.Load();
      GameManager.Instance.Load();

      var loader = new [] { sharedTableLoader, stringTableLoader, ItemProperties.Load(), AudioManager.Load() };

      foreach (var operation in loader)
      {
        operation.WaitForCompletion();
      }
    }
  }
}
