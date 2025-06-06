using Retronia.IO;
using Retronia.Utils;
using TMPro;
using UnityEngine;

namespace Retronia.Scenes.Lobby
{
  public class LobbySystem : MonoBehaviour
  {
    [SerializeField] private SAVE data = new("Test Save");
    
    #region Menu UI
    [Header( "Menu UI" )]
    
    [SerializeField] private GameObject menuUI;
    [SerializeField] private TMP_Text elementalText, nameText, levelText;

    private void InitMenu()
    {
      if (data != null)
      {
        elementalText.text = data.GetString("elemental", "normal");
        nameText.text = data.name;
        levelText.text = data.name;
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
      data = SAVE.Current;
      
      InitMenu();
      InitStatus();
      InitInventory();
    }
    
    #endregion
  }
}