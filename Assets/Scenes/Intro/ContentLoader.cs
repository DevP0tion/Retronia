using Retronia.Core;
using Retronia.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Tables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Retronia.Scenes.Intro
{
  public class ContentLoader : MonoBehaviour
  {
    private StringTable localization;
    [SerializeField] private Button entryButton;
    [SerializeField] private TMP_Text entryText;
    
    
    #region Unity Events
    
    private void Start()
    {
      Load();
      
      // 로딩이 완료됬을 시 게임을 시작할 수 있게 설정
      entryText.text = Localizer.Get("finished");
      entryButton.onClick.AddListener(() => SceneManager.LoadScene(SceneNames.MainMenu));
    }
    
    #endregion

    public void Load()
    {
      Localizer.Load();
      GameManager.Instance.Load();
    }
  }
}
