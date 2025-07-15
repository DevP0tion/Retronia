using System;
using System.Collections.Generic;
using Retronia.Core;
using Retronia.Networking.Packets;
using Retronia.Utils;
using Retronia.Worlds;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.Serialization;

namespace Retronia.Contents.Properties
{
  [CreateAssetMenu(fileName = "new Bullet Properties", menuName = "Properties/Bullet Properties")]
  public class BulletProperties : ScriptableObject
  {
    public static readonly Dictionary<string, BulletProperties> Bullets = new();

    /// <summary>
    /// 탄환을 풀링할 원본 프리팹의 어드레서블 경로
    /// </summary>
    public AssetReference bulletPath;
    public float speed = 1;
    public float damageMultiplier = 1;

    public Bullet Pooling()
    {
      var obj = (Bullet)bulletPath.Pooling();
      
      obj.Properties = this;
      
      return obj;
    }
    
    public void Shoot(Team team, Vector3 startPosition, Vector3 targetPosition, float damage = 1)
      => BulletManager.Shoot(new BulletPacket(this, team, startPosition, targetPosition, damage));

    #region Initialization
    public static bool Loaded { get; private set; } = false;
    public const string Label = "Bullet"; 
    
    public static AsyncOperationHandle Load()
    {
      if(Loaded) throw new InvalidOperationException("BulletProperties is already loaded.");
      Loaded = true;
      
      return Addressables.LoadAssetsAsync<BulletProperties>(new AssetLabelReference{labelString = Label}, properties =>
      {
        Bullets[properties.name] = properties;
#if UNITY_EDITOR 
        GameManager.Instance.loadedBullets[properties.name] = properties;
#endif
      });
    }

    #endregion
  }
}