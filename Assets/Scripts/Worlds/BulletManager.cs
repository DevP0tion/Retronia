using System.Collections.Generic;
using Mirror;
using Retronia.Contents;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Pool;

namespace Retronia.Worlds
{
  public class BulletManager : MonoBehaviour
  {
    #region Singleton

    private static BulletManager instance;

    public static BulletManager Instance
    {
      get
      {
        if (instance) return instance;

        instance = new GameObject("BulletManager").AddComponent<BulletManager>();
        instance.released = new GameObject("Released").transform;
        instance.released.transform.SetParent(instance.transform);

        return instance;
      }
    }

    
    #endregion

    #region Poolling
    
    [SerializeField] private Transform released;
    [SerializeField] private List<GameObject> instances = new();

    /// <summary>
    ///   불릿 매니저 인스턴스가 바뀔 경우를 대비해 인스턴스 변수로 선언
    /// </summary>
    private readonly Dictionary<string, ObjectPool<Bullet>> pools = new();

    private ObjectPool<Bullet> InitPool(GameObject bulletPrefab)
    {
      if (pools.TryGetValue(bulletPrefab.name, out var pool)) return pool;

      return pools[bulletPrefab.name] = new ObjectPool<Bullet>(() =>
        {
          // Create
          var bullet = Instantiate(bulletPrefab).GetComponent<Bullet>();
          instances.Add(bullet.gameObject);
          NetworkServer.Spawn(bullet.gameObject);
          bullet.transform.SetParent(Instance.transform);
          return bullet;
        },
        bullet =>
        {
          // Get
          bullet.Active = true;
          bullet.transform.SetParent(Instance.transform);
        },
        bullet =>
        {
          // Release
          bullet.Active = false;
          bullet.transform.SetParent(Instance.released);
        },
        bullet =>
        {
          // Destroy
          Destroy(bullet.gameObject);
        }
      );
    }

    public static void InitClientPool(NetworkConnectionToClient conn)
    {
      foreach (var obj in Instance.instances)
      {
        NetworkServer.Spawn(obj, conn);
      }
    }

    /// <summary>
    ///   이름에 맞는 탄환을 풀링하는 코드
    ///   임시 구현 / TODO 탄환을 한번에 관리하는 매니저가 필요함
    /// </summary>
    /// <param name="bulletName">풀링할 탄환 원본 프리팹의 명칭</param>
    /// <returns>풀링된 탄환 오브젝트의 컴포넌트</returns>
    public static Bullet Get(string bulletName)
    {
      if (!Instance.pools.TryGetValue(bulletName, out var pool))
        pool = Instance.InitPool(new AssetReference(bulletName).LoadAssetAsync<GameObject>().WaitForCompletion());

      return pool.Get();
    }

    /// <summary>
    ///   탄환 객체를 오브젝트 풀로 반환하거나 파괴하는 메서드
    /// </summary>
    /// <param name="bullet">반환 또는 파괴할 탄환 객체</param>
    /// <remarks>
    ///   해당 탄환이 유효하고 풀에 등록되어 있는 경우 풀로 반환하며,
    ///   그렇지 않은 경우 게임오브젝트를 완전히 파괴합니다.
    /// </remarks>
    public static void Release(Bullet bullet)
    {
      if (bullet && bullet.Properties && Instance.pools.TryGetValue(bullet.Properties.bulletName, out var pool))
        pool.Release(bullet);
      else
        Destroy(bullet.gameObject);
    }

    #endregion
  }
}