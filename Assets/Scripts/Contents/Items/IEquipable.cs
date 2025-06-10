using System;
using Retronia.Contents.Items;
using Retronia.IO.Formats;

namespace Retronia.Contents.Properties
{
  public interface IEquipable
  {
    EquipmentPart Part { get; }
    string Name { get; }
    Action Equip(CharacterInfo character);
    void UnEquip(CharacterInfo character);
  }
}