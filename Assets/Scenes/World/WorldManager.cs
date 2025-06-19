using Mirror;
using NaughtyAttributes;
using Retronia.Utils;
using Retronia.Utils.Singletons;
using Retronia.Worlds;
using UnityEngine;

namespace Retronia.Scenes.World
{
  public class WorldManager : NetworkSingleton<WorldManager>
  {
    protected static PlayerController Player => PlayerController.Instance;

    #region Inspector
    
    [SerializeField, GetSet(nameof(Pause))] private bool pause = false;
    
    #endregion
    
    #region Binding
    
    [SerializeField] protected Transform objectContainer;
    
    #endregion

    public bool Pause
    {
      get => pause;
      set
      {
        pause = value;
        if(NetworkManager.singleton && NetworkManager.singleton.isNetworkActive)
          Time.timeScale = pause ? 0 : 1;
      }
    }
  }
}