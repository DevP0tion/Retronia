using Retronia.Contents.Items;
using Retronia.IO.Formats;

namespace Retronia.Contents.Properties
{
  public interface IEquipable
  {
    EquipmentPart Part { get; }
    string Name { get; }
    void Equip(CharacterInfo character);
    void Unequip(CharacterInfo character);
  }
}