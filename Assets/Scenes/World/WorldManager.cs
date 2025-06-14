using Retronia.Utils;
using UnityEngine;

namespace Retronia.Scenes.World
{
  public sealed class WorldManager : MonoBehaviour
  {
    public static WorldManager Instance { get; private set; }

    [SerializeField, GetSet(nameof(Pause))] private bool pause = false;
    [SerializeField] private PlayerController player;

    public bool Pause
    {
      get => pause;
      set
      {
        pause = value;
        Time.timeScale = pause ? 0 : 1;
      }
    }
    
    #region Unity Event
    
    private void Awake()
    {
      if(Instance)
      {
        Destroy(gameObject);
      }
      else
      {
        Instance = this;
      }
    }
    
    #endregion
  }
}