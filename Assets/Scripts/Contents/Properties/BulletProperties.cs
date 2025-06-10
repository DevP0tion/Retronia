using Retronia.Worlds;
using UnityEngine;

namespace Retronia.Contents.Properties
{
  [CreateAssetMenu(fileName = "new Bullet Properties", menuName = "Properties/Bullet Properties")]
  public class BulletProperties : ScriptableObject
  {
    /// <summary>
    ///   탄환을 풀링할 원본 프리팹의 이름
    /// </summary>
    public string bulletName;

    public float speed = 1;
    public float damageMultiplier = 1;

    public virtual Bullet Create(Team team)
    {
      var bullet = BulletManager.Get(bulletName);
      bullet.Properties = this;
      bullet.team = team;

      return bullet;
    }
    
    public virtual Bullet Create() => Create(Team.None);
  }
}