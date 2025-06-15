using System;
using System.Collections.Generic;
using Retronia.Core;
using Retronia.Worlds;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Retronia.Contents.Properties
{
  [CreateAssetMenu(fileName = "new Bullet Properties", menuName = "Properties/Bullet Properties")]
  public class BulletProperties : ScriptableObject
  {
    public static readonly Dictionary<string, BulletProperties> Bullets = new();

    /// <summary>
    ///   탄환을 풀링할 원본 프리팹의 이름
    /// </summary>
    public string bulletName;

    public float speed = 1;
    public float damageMultiplier = 1;

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