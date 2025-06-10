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
        if (item != null)
        {
          nameString.StringReference = new LocalizedString("General", "");
        }
        else
        {
          
        }
      }
    }
  }
}