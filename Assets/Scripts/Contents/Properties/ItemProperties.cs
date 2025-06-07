using UnityEngine;

namespace Retronia.Contents.Properties
{
  [CreateAssetMenu(fileName = "new Item Properties", menuName = "Retronia/Item Properties")]
  public class ItemProperties : ScriptableObject
  {
    public Color color;
    public Sprite sprite;
    public int price;
    public int maxAmount = 64;
    
    public static implicit operator ItemStack(ItemProperties item) => new (item);
  }
}