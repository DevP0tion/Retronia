using Mirror;
using NaughtyAttributes;
using Retronia.Players;
using Retronia.Utils;
using Retronia.Utils.Singletons;
using UnityEngine;

namespace Retronia.Scenes.World
{
  public class WorldManager : NetworkSingleton<WorldManager>
  {
    protected static PlayerController Player => PlayerController.Instance;

    #region State
    
    [SerializeField, GetSet(nameof(Pause))] private bool pause = false;
    
    #endregion
    
    #region Binding
    
    [SerializeField] protected Transform objectContainer;

#if UNITY_EDITOR
    [Foldout("View"), SerializeField] private SpriteRenderer background;
    [Foldout("View"), SerializeField] private Color backgroundColor;
#endif
    
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
    
    #region Unity Event

    #if UNITY_EDITOR

    private void OnDrawGizmos()
    {
      if(background)
      {
        Gizmos.color = backgroundColor;
        Gizmos.DrawCube(background.transform.position, background.transform.localScale);
      }
    }

#endif
    
    #endregion
  }
}