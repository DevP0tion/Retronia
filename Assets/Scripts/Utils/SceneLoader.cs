using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace Retronia.Utils
{
  [AddComponentMenu("Util/Scene Loader")]
  public class SceneLoader : MonoBehaviour
  {
    /// <summary>
    /// 에디터 상에서 간단하게 버튼 이용하여 씬 이동할 수 있게 해주는 코드
    /// </summary>
    /// <param name="sceneName"></param>
    public void LoadScene(string sceneName)
    {
      LoadScene(sceneName, null);
    }

    public static void LoadScene(string sceneName, object message)
    {
      // 다음 씬에 데이터를 전달하는 코드
      if (message != null)
      {
        void MessageHandler(Scene scene, LoadSceneMode mode)
        {
          if (EventSystem.current is { } eventSystem) eventSystem.SendMessage("OnMessage", message);

          SceneManager.sceneLoaded -= MessageHandler;
        }

        SceneManager.sceneLoaded += MessageHandler;
      }

      SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
    }
  }
}