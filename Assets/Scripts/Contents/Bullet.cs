using NaughtyAttributes;
using Retronia.Contents.Properties;
using Retronia.Utils;
using Retronia.Worlds;
using UnityEngine;

namespace Retronia.Contents
{
  public class Bullet : MonoBehaviour
  {
    #region State

    [Header("Bullet State")] 
    
    [SerializeField] private new Camera camera;
    [SerializeField] private BulletProperties properties;
    public float speed = 1;
    public float damage = 1;
    public Team team = Team.None;
    public Vector2 direction;

    #endregion
    
    #region Binding
    [Header("Bullet Binding")] 
    
    [SerializeField] protected Rigidbody2D body;
    #endregion

    #region Unity Event

    /// <summary>
    ///   탄환이 풀링됬을 때 이벤트
    /// </summary>
    private void OnEnable()
    {
      camera = Camera.main;
    }

    private void FixedUpdate()
    {
      var viewPos = camera.WorldToViewportPoint(transform.position);

      if (viewPos.x < 0 || viewPos.x > 1 || viewPos.y < 0 || viewPos.y > 1)
      {
        Release();
      }
      
      body.MovePosition(transform.position + (Vector3)(direction * speed));
      // body.AddForce(direction * speed, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
      if (other.CompareTag("Entity") && other.TryGetComponent(out Entity entity) && entity.team != team) OnHit(entity);
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
  }
}