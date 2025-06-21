using System.Collections.Generic;
using Mirror;
using Retronia.Contents.Properties;
using Retronia.Scenes.World;
using Retronia.Utils;
using Retronia.Worlds;
using UnityEngine;
using UnityEngine.Events;

namespace Retronia.Contents.Entities
{
  public class Entity : NetworkBehaviour
  {
    #region State

    [Header("Entity State")] 
    
    [SerializeField] [GetSet(nameof(Data))] protected EntityProperties data;
    public Stat speed = 1;
    public Stat rotateSpeed = 8;
    public Vector2 direction = Vector2.up;
    public RangedStat healthPoint;
    public Team team = Team.None;

    
    
    #endregion

    #region Bindings
    [Header("Entity Bindings")]
    
    [SerializeField] protected Rigidbody2D body;
    public List<Weapon> weapons = new();

    #endregion

    #region Exports

    public Rigidbody2D Body => body;

    public virtual EntityProperties Data
    {
      get => data;
      set
      {
        if (value && data != value)
        {
          speed = value.speed;
          rotateSpeed = value.rotateSpeed;
          data = value;
          healthPoint = new RangedStat(data.maxHealthPoint, data.healthPoint);
        }
      }
    }

    #endregion

    #region Unity Events

    private void Awake()
    {
    }

    #endregion
    
    #region Networking

    public override void OnStartLocalPlayer()
    {
      base.OnStartLocalPlayer();
      PlayerController.Instance.Entity = this;
      if (!NetworkServer.active)
      {
        GetComponent<NetworkTransformUnreliable>().syncDirection = SyncDirection.ClientToServer;
      }
    }

    #endregion
    
    public void Shoot(Vector3 targetPosition)
    {
      foreach (var weapon in weapons)
      {
        weapon.Shoot(targetPosition);
      }
    }
  }
}