using Mirror;
using NaughtyAttributes;
using UnityEngine;

using ReadOnly = NaughtyAttributes.ReadOnlyAttribute;

namespace Retronia.Contents.Entities
{
  public partial class Entity
  {
    #region Property

    [SerializeField] public float syncDelay = 0.3f;
    [SerializeField, ReadOnly] private bool isMove;
    [SerializeField] private float preferredRotation = 0;
    
    public virtual float Rotation
    {
      get => body.rotation;
      set
      {
        if (NetworkServer.active)
          body.rotation = value;
        else SetRotation(value);
      }
    }
    
    public virtual float PreferredRotation
    {
      get => preferredRotation;
      set
      {
        if (NetworkServer.active)
          preferredRotation = value;
        else if(preferredRotationSyncTime <= 0)
        {
          preferredRotationSyncTime = syncDelay;
          SerPreferredRotation(value);
        }
      }
    }

    public virtual bool IsMove
    {
      get => isMove;
      set
      {
        if (NetworkServer.active)
          isMove = value;
        else SetMove(value);
      }
    }
    
    #endregion
    
    #region Binding
    
    [Foldout(Binding), SerializeField] protected Rigidbody2D body;
    
    #endregion
    
    #region Networking
    private float preferredRotationSyncTime = 0;

    private void UpdateClientSync()
    {
      if(preferredRotationSyncTime > 0)
        preferredRotationSyncTime -= Time.deltaTime;
      else preferredRotationSyncTime = 0;
    }

    [Command]
    private void SetRotation(float rotation) => body.rotation = rotation;
    
    [Command]
    private void SerPreferredRotation(float rotation) => preferredRotation = rotation;
    
    [Command]
    private void SetMove(bool isMove) => this.isMove = isMove;
    
    #endregion
  }
}