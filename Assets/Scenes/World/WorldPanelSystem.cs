using UnityEngine;

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
    
    #endregion
    
    #region Unity Event
    
    private void Awake()
    {
      LoadMainPanel();
      LoadPausePanel();
    }
    
    #endregion
  }
}