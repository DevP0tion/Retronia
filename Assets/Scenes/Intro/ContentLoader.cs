using System.Linq;
using Retronia.Contents.Properties;
using Retronia.Core;
using Retronia.IO;
using Retronia.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Tables;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Retronia.Scenes.Intro
{
  public class ContentLoader : MonoBehaviour
  {
    private StringTable localization;
    [SerializeField] private Button entryButton;
    [SerializeField] private TMP_Text loadingText;
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

      var loaderList = new[]
      {
        (name:"Shared Table", loader: sharedTableLoader),
        (name:"언어 번들", loader: stringTableLoader),
        (name:"아이템 정보", loader: ItemProperties.Load()),
        (name:"음원", loader: AudioManager.Load()),
        (name:"캐릭터 정보", loader: CharacterProperties.Load())
      };

      foreach (var operation in loaderList)
      {
        operation.loader.Completed += _ =>
        {
          var temp = (from op in loaderList
            where !op.loader.IsDone
            orderby op.loader.PercentComplete descending
            select op);

          if (temp.Any())
          {
            var next = temp.First();
            loadingText.text = "loading " + next.name + " ...";
          }
        };
      }

      foreach (var operation in loaderList)
      {
        operation.loader.WaitForCompletion();
      }

      var saveData = SAVE.Current = SAVE.LoadSync("default", true);
      saveData.Init();
      saveData.player.inventory.AddItem("Gem", 10);
      saveData.player.inventory.AddItem("Meteorite", 2);
      saveData.player.inventory.AddItem("Cannon", 1);
      saveData.player.inventory.AddItem("Core", 1);
      saveData.player.inventory.AddItem("Engine", 1);
      saveData.player.AddCharacter("Alpha");
      
      loadingText.gameObject.SetActive(false);
      entryButton.gameObject.SetActive(true);
    }
  }
}
