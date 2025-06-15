using System;
using System.Collections.Generic;
using Retronia.Contents.Items;
using Retronia.Core;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Retronia.Contents.Properties
{
  [CreateAssetMenu(fileName = "new Item Properties", menuName = "Properties/Basic Item")]
  public class ItemProperties : ScriptableObject
  {
    public static readonly Dictionary<string, ItemProperties> items = new();
    public static ItemProperties None => items.GetValueOrDefault("None");
    
    public Color color;
    public Sprite sprite;
    public int price;
    public int maxAmount = 64;
    
    public virtual ItemStack Instantiate(int amount)
      => new (this, amount);
    
    #region Initialization
    public static bool Loaded { get; private set; } = false;
    public const string Label = "Item"; 

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