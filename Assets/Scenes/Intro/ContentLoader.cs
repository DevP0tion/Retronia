using System.Linq;
using System.Threading.Tasks;
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
    
    private async void Start()
    {
      await Load();
      
      // 로딩이 완료됬을 시 게임을 시작할 수 있게 설정
      entryText.StringReference = new LocalizedString("General", "Intro_finished");
      entryButton.onClick.AddListener(() => SceneManager.LoadScene(SceneNames.MainMenu));
    }
    
    #endregion

    public async Task Load()
    {
      var (sharedTableLoader, stringTableHandle) = Localizer.Load();
      var (mixerHandle, clipHandle ) = AudioManager.Load();
      GameManager.Instance.Load();
      var saveTask = SAVE.Load("default", true);

      (string name, AsyncOperationHandle loader)[] loaderList = {
        ("Shared Table", sharedTableLoader),
        ("언어 번들", stringTableHandle),
        ("아이템 정보", ItemProperties.Load()),
        ("탄환 정보", BulletProperties.Load()),
        ("소리 설정", mixerHandle),
        ("음원", clipHandle),
        ("캐릭터 정보", CharacterProperties.Load())
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

      var saveData = SAVE.Current = await saveTask;
      //
      // saveData.Init();
      // saveData.player.inventory.AddItem("Gem", 10);
      // saveData.player.inventory.AddItem("Meteorite", 2);
      // saveData.player.inventory.AddItem("Cannon", 1);
      // saveData.player.inventory.AddItem("Core", 1);
      // saveData.player.inventory.AddItem("Engine", 1);
      // saveData.player.AddCharacter("Alpha");
      
      loadingText.gameObject.SetActive(false);
      entryButton.gameObject.SetActive(true);
    }
  }
}
