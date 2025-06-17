using Retronia.Utils;
using Retronia.Worlds;
using UnityEngine;

namespace Retronia.Scenes.World
{
  public sealed class WorldManager : MonoBehaviour
  {
    private static PlayerController Player => PlayerController.Instance;
    public static WorldManager Instance { get; private set; }

    [SerializeField, GetSet(nameof(Pause))] private bool pause = false;

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

    private void OnDestroy()
    {
      Instance = null;
    }
    
    #endregion
  }
}