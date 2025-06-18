using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Retronia.Scenes.World
{
  public class WorldPanelSystem : MonoBehaviour
  {
    #region Main Panel
    [Header("Main Panel")]
    
    [SerializeField] private GameObject mainPanel;

    private void LoadMainPanel()
    {
      
    }
    
    #endregion
    
    #region Pause Panel
    [Header("Pause Panel")]
    
    [SerializeField] private GameObject pausePanel;

    private void LoadPausePanel()
    {
      
    }

    public void ExitWorld()
    {
      if(NetworkServer.active)
        NetworkManager.singleton.StopHost();
      else
      {
        NetworkClient.Disconnect();
        NetworkManager.singleton?.StopClient();
        SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Single);
      }
    }
    
    #endregion
    
    #region Unity Event
    
    private void Awake()
    {
      LoadMainPanel();
      LoadPausePanel();
    }

    private void Update()
    {
      if (Input.GetKeyDown(KeyCode.Escape))
      {
        WorldManager.Instance.Pause = !WorldManager.Instance.Pause;
        pausePanel.SetActive(WorldManager.Instance.Pause);
      }
    }
    
    #endregion
  }
}