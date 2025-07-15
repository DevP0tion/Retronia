using System;
using System.Collections.Generic;
using Mirror;
using NaughtyAttributes;
using Retronia.Contents.Properties;
using Retronia.Players;
using Retronia.Scenes.World;
using Retronia.Utils;
using Retronia.Worlds;
using UnityEngine;
using UnityEngine.Events;

namespace Retronia.Contents.Entities
{
  public partial class Entity : NetworkBehaviour
  {
    #region State
    private const string State = "State";

    [Foldout(State), SerializeField, GetSet(nameof(Data))] protected EntityProperties data;
    [Foldout(State)] public float speed = 1;
    [Foldout(State)] public Stat rotateSpeed = 8;
    [Foldout(State)] public RangedStat healthPoint;
    [Foldout(State)] public Team team = Team.None;

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
    
    #region Bindings

    private const string Binding = "Binding";
    
    [Foldout(Binding)] public List<Weapon> weapons = new();

    #endregion

    #region Unity Events

    private void Update()
    {
      if(isMove)
        body.AddForce(body.rotation.ToDirection() * (speed * Time.deltaTime), ForceMode2D.Impulse);
    }

    private void FixedUpdate()
    {
      if (!NetworkServer.active) UpdateClientSync();
      else
      {
        Rotation = Mathf.LerpAngle(Rotation, preferredRotation, Time.fixedDeltaTime * rotateSpeed);
      }
    }

    #endregion
    
    #region Networking

    public override void OnStartLocalPlayer()
    {
      // 플레이어 캐릭터 설정
      base.OnStartLocalPlayer();
      PlayerController.Instance.Entity = this;
    }

    #endregion
    
    #region Feature
    
    public void Shoot(Vector3 targetPosition)
    {
      foreach (var weapon in weapons)
      {
        weapon.Shoot(targetPosition);
      }
    }
    
    #endregion
  }
}