using System;
using System.Collections.Generic;
using Retronia.Contents.Items;
using Retronia.Core;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Retronia.Contents.Properties
{
  [CreateAssetMenu(fileName = "new Item Properties", menuName = "Retronia/Item Properties")]
  public class ItemProperties : ScriptableObject
  {
    public static bool Loaded { get; private set; } = false;
    public static readonly Dictionary<string, ItemProperties> items = new();
    public const string Label = "Item"; 
    public static ItemProperties None => items.GetValueOrDefault("None");
    
    public Color color;
    public Sprite sprite;
    public int price;
    public int maxAmount = 64;
    
    public static implicit operator ItemStack(ItemProperties item) => new (item);
    
    #region Initialization

    /// <summary>
    /// 멀티스레드 비동기 로딩 구현을 하고싶었는데 비동기 구현시 무한로딩 문제???
    /// </summary>
    public static AsyncOperationHandle Load()
    {
      if(Loaded) throw new InvalidOperationException("ItemProperties is already loaded.");
      Loaded = true;
      
      return Addressables.LoadAssetsAsync<ItemProperties>(new AssetLabelReference{labelString = Label}, properties =>
      {
        items[properties.name] = properties;
        #if UNITY_EDITOR 
        GameManager.Instance.loadedItems[properties.name] = properties;
        #endif
      });
    }
    
    #endregion
  }
}