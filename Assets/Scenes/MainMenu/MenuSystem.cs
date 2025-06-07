using Retronia.IO;
using Retronia.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Retronia.Scenes.MainMenu
{
  public class MenuSystem : MonoBehaviour
  {
    public void Exit()
    {
#if UNITY_EDITOR
      UnityEditor.EditorApplication.isPlaying = false;
#else
      Application.Quit();
#endif
    }

    #region SingleSetupPanel

    public void CreateSave(TMP_InputField nameLabel)
    {
      // TODO 생성시 명칭 설정 가능하게 구현
      SAVE.Current = new SAVE(nameLabel.text);
      SAVE.Current.Init();
      SceneManager.LoadScene(SceneNames.Lobby);
    }
    
    #endregion
  }
}