using System.Linq;
using Mirror;
using NaughtyAttributes;
using Retronia.Contents.Entities;
using Retronia.Contents.Properties;
using Retronia.Utils;
using Retronia.Worlds;
using UnityEngine;

namespace Retronia.Contents
{
  public class Bullet : NetworkBehaviour
  {
    #region State

    [Header("Bullet State")] 
    
    [SerializeField] private new Camera camera;
    
    [SerializeField] private BulletProperties properties;
    public float speed = 1;
    public float damage = 1;
    public Team team = Team.None;
    public Vector2 direction;

    public bool Active
    {
      get => gameObject.activeSelf;
      set
      {
        if (NetworkServer.active) SetActive(value);
      }
    }

    #endregion
    
    #region Binding
    [Header("Bullet Binding")] 
    
    [SerializeField] protected Rigidbody2D body;
    #endregion

    #region Unity Event
    private bool released = true;

    /// <summary>
    ///   탄환이 풀링됬을 때 이벤트
    /// </summary>
    private void OnEnable()
    {
      camera = Camera.main;
      released = false;
    }

    private void FixedUpdate()
    {
      var viewPos = camera.WorldToViewportPoint(transform.position);

      if (viewPos.x < 0 || viewPos.x > 1 || viewPos.y < 0 || viewPos.y > 1)
      {
        if(!released)
        {
          Release();
          released = true;
        }
      }
      
      body.MovePosition(transform.position + (Vector3)(direction * speed));
      // body.AddForce(direction * speed, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
      if (other.CompareTag("Entity") && other.TryGetComponent(out Entity entity) && entity.team != team) 
        OnHit(entity);
    }

    #endregion
    
    #region Core
    
    protected virtual void ShootFunc(Vector2 targetPosition, float damage)
    {
      transform.rotation = ((Vector2)transform.position).GetDirection(targetPosition);
      direction = transform.rotation.ToVector2Direction();
      this.damage = damage;
    }

    protected virtual void InitFunc(BulletProperties properties, Team team, Vector3 position)
    {
      if (properties == null) return;
      
      Properties = properties;
      transform.position = position;
      this.team = team;
    }
    
    #endregion

    #region Interface

    public virtual BulletProperties Properties
    {
      get => properties;
      set
      {
        properties = value;
        speed = properties.speed;
        damage = properties.damageMultiplier;
      }
    }

    public void Shoot(Vector2 targetPosition, float damage)
    {
      if (NetworkServer.active)
      {
        ShootFunc(targetPosition, damage);
        foreach (var _ in NetworkServer.connections.Values)
        {
          ShootRpc(targetPosition, damage);
        }
      }
      else
      {
        ShootRequest(targetPosition, damage);
      }
    }

    public void Init(BulletProperties properties, Team team, Vector3 position)
    {
      if (NetworkServer.active)
      {
        InitFunc(properties, team, position);
        foreach (var _ in NetworkServer.connections.Values)
        {
          InitRpc(properties.name, team.Name, position);
        }
      }
      else
      {
        InitRequest(properties.name, team.Name, position);
      }
    }

    [Button]
    public virtual void Release()
    {
      BulletManager.Release(this);
    }

    public virtual void OnHit(Entity entity)
    {
      entity.healthPoint.Value -= damage;
      Release();
    }

    #endregion
    
    #region Networking

    [ClientRpc]
    private void SetActive(bool active)
    {
      gameObject.SetActive(active);
    }

    [Command]
    private void ShootRequest(Vector2 targetPosition, float damage)
    {
      ShootFunc(targetPosition, damage);

      foreach (var _ in NetworkServer.connections.Values.Where(conn => conn != connectionToClient))
      {
        ShootRpc(targetPosition, damage);
      }
    }

    [ClientRpc]
    private void ShootRpc(Vector2 targetPosition, float damage)
    {
      ShootFunc(targetPosition, damage);
    }

    [Command]
    private void InitRequest(string propertiesName, string teamName, Vector3 position)
    {
      InitFunc(BulletProperties.Bullets[propertiesName], teamName, position);
      foreach (var _ in NetworkServer.connections.Values.Where(conn => conn != connectionToClient))
      {
        InitRpc(propertiesName, teamName, position);
      }
    }
    
    [ClientRpc]
    private void InitRpc(string propertiesName, string teamName, Vector3 position)
    {
      InitFunc(BulletProperties.Bullets[propertiesName], teamName, position);
    }
    
    #endregion
  }
}