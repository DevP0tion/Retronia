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
  public class Weapon : MonoBehaviour
  {
    #region State
    [Header("BasicWeapon State")] 

    public Team team = Team.None;
    // 몇 초마다 공격할건지
    public float reloadTime = 2;
    public float damage = 1;
    [SerializeField] protected BulletProperties[] bullets;
    public BulletProperties[] Bullets => bullets.ToArray();
    [SerializeField] private float attackCoolTime = 0;

    #endregion

    private void FixedUpdate()
    {
      if(attackCoolTime > 0) attackCoolTime -= Time.deltaTime;
    }
    
    public virtual void Shoot(Vector2 targetPosition)
    {
      if(attackCoolTime > 0) return;
      attackCoolTime = reloadTime;
      
      var bullet = bullets.GetRandom().Create(team, transform.position);
      
      bullet.Shoot(targetPosition, damage);
    }
    
#if UNITY_EDITOR
    [SerializeField] private Entity entity; 
    
    [Button("TestShoot")]
    public void Shoot() => Shoot(entity.direction);
    
#endif

    public void SetBullets(params BulletProperties[] bullets)
    {
      this.bullets = bullets;
    }
  }
}