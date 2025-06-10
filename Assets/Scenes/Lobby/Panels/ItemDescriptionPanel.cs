using Retronia.Contents.Properties;
using Retronia.Utils;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;

namespace Retronia.Scenes.Lobby.Panels
{
  public class ItemDescriptionPanel : MonoBehaviour
  {
    #region Binding
    [Header("Binding")]
    
    [SerializeField] private LocalizeStringEvent nameString;
    [SerializeField] private LocalizeStringEvent descriptionString;
    #endregion
    
    [SerializeField, GetSet(nameof(Item))] private ItemProperties item;
    public ItemProperties Item
    {
      get => item;
      set
      {
        item = value;
        nameString.StringReference = item != null ? 
          new LocalizedString("General", $"Item_{value.name}_name") :
          new LocalizedString("General", "Global_blank");
        
        descriptionString.StringReference = item != null ? 
          new LocalizedString("General", $"Item_{value.name}_description") :
          new LocalizedString("General", "Global_blank");
      }
    }
  }
}