using Retronia.IO;
using Retronia.Utils;
using TMPro;
using UnityEngine;

namespace Retronia.Scenes.Lobby
{
  public class LobbySystem : MonoBehaviour
  {
    private static SAVE Data => SAVE.Current;
    
    #region Menu UI
    [Header( "Menu UI" )]
    
    [SerializeField] private GameObject menuUI;
    [SerializeField] private TMP_Text elementalText, nameText, levelText;

    private void InitMenu()
    {
      if (Data != null)
      {
        elementalText.text = Data.GetString("elemental", "normal");
        nameText.text = Data.name;
        levelText.text = Data.name;
      }
    }
    #endregion
    
    #region Status UI
    [Header( "Status UI" )]
    
    [SerializeField] private GameObject statusUI;
    
    private void InitStatus()
    {
      
    }
    #endregion
    
    #region Inventory UI
    [Header( "Inventory UI" )]
    
    [SerializeField] private GameObject inventoryUI;

    private void InitInventory()
    {
      
    }
    #endregion
    
    #region Unity Event
    
    private void Start()
    {
      InitMenu();
      InitStatus();
      InitInventory();
    }
    
    #endregion
  }
}