using System.Collections.Generic;
using Retronia.Contents.Items;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using CharacterInfo = Retronia.IO.Formats.CharacterInfo;

namespace Retronia.Contents.Properties
{
  [CreateAssetMenu(fileName = "new Character Properties", menuName = "Properties/Character Properties")]
  public class CharacterProperties : ScriptableObject
  {
    public static readonly Dictionary<string, CharacterProperties> Characters = new();
    public const string Label = "Character";
    
    public EquipmentPart[] allowedPart;

    public CharacterInfo Instantiate() => new (this);

    public static AsyncOperationHandle Load()
      => Addressables.LoadAssetsAsync<CharacterProperties>(new AssetLabelReference { labelString = Label }, properties =>
      {
        Characters[properties.name] = properties;
      });
  }
}