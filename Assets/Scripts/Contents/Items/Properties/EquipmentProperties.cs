using Retronia.Contents.Items;
using UnityEngine;
using CharacterInfo = Retronia.IO.Formats.CharacterInfo;

namespace Retronia.Contents.Properties
{
  [CreateAssetMenu(fileName = "new Equipment Properties", menuName = "Properties/Equipment Item")]
  public class EquipmentProperties : ItemProperties, IEquipable
  {
    public EquipmentPart part;
    public EquipmentPart Part => part;
    public string Name => name;
    public void Equip(CharacterInfo character)
    {
    }

    public void Unequip(CharacterInfo character)
    {
    }
  }
}