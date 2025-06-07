using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Retronia.Contents.Properties
{
  [CreateAssetMenu(fileName = "new Item Properties", menuName = "Retronia/Item Properties")]
  public class ItemProperties : ScriptableObject
  {
    public static readonly Dictionary<string, ItemProperties> items = new();
    public const string Label = "Item"; 
    public static ItemProperties None => items.GetValueOrDefault("None");
    
    public Color color;
    public Sprite sprite;
    public int price;
    public int maxAmount = 64;
    
    public static implicit operator ItemStack(ItemProperties item) => new (item);
    
    #region Initialization

    public static async Task LoadItems()
    {
      await Addressables.LoadAssetsAsync<ItemProperties>(new AssetLabelReference{labelString = Label}, properties =>
      {
        items[properties.name] = properties;
      }).Task;
    }
    
    #endregion
  }
}