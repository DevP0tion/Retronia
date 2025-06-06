using System.Collections.Generic;
using Retronia.Contents.Properties;
using Retronia.Utils;
using Retronia.Worlds;
using UnityEngine;
using UnityEngine.Events;

namespace Retronia.Contents
{
  public class Entity : MonoBehaviour
  {
    #region State

    [Header("Entity State")] [SerializeField] [GetSet(nameof(Data))]
    protected EntityProperties data;

    public Stat speed = 1;
    public Stat rotateSpeed = 8;
    public Vector2 direction = Vector2.up;
    public RangedStat healthPoint;
    public Team team = Team.None;

    #endregion

    #region Bindings
    [Header("Entity Bindings")]
    
    [SerializeField] private new Camera camera;
    [SerializeField] protected Rigidbody2D body;
    public List<Weapon> weapons = new();

    #endregion

    #region Exports

    public UnityEvent<Vector3> onOverflowMap;
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

#if UNITY_EDITOR

    private void Reset()
    {
      camera = Camera.main;
    }

#endif

    private void Awake()
    {
      onOverflowMap.AddListener(viewPos =>
      {
        // 맵 밖으로 나갔을 시 반대편으로 오게끔
        transform.position = camera.ViewportToWorldPoint(new Vector3(
          viewPos.x > 1 ? 0 : viewPos.x < 0 ? 1 : viewPos.x,
          viewPos.y > 1 ? 0 : viewPos.y < 0 ? 1 : viewPos.y,
          0)).Z(0);
      });
    }

    private void FixedUpdate()
    {
      // 엔티티가 화면 밖으로 나갔을 때 이벤트
      {
        var viewPos = camera.WorldToViewportPoint(transform.position);

        if (viewPos.x < 0 || viewPos.x > 1 || viewPos.y < 0 || viewPos.y > 1)
          onOverflowMap?.Invoke(viewPos);
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