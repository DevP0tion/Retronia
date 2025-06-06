using Retronia.IO;
using Retronia.Utils.UI;
using TMPro;
using UnityEngine;

using CharacterInfo = Retronia.IO.Formats.CharacterInfo;

namespace Retronia.Scenes.Lobby
{
  public class LobbySystem : MonoBehaviour
  {
    private static SAVE Data => SAVE.Current;
    private static CharacterInfo PlayerData => CharacterInfo.Current;
    
    #region Menu UI
    [Header( "Menu UI / Binding" )]
    
    [SerializeField] private GameObject menuUI;
    [SerializeField] private TMP_Text elementalText, nameText, levelText, expText, goldText;
    [SerializeField] private UIGaugeBar expBar;

    private void InitMenu()
    {
      if (PlayerData != null)
      {
        elementalText.text = "속성 - " + PlayerData.elemental;
        nameText.text = PlayerData.name;
        levelText.text = PlayerData.level.ToString();
        goldText.text = $"{PlayerData.gold:#,###}";
        
        expBar.max = PlayerData.MaxExp;
        expBar.Value = PlayerData.exp;
        expText.text = PlayerData.exp + " / " + PlayerData.MaxExp;
      }
    }
    #endregion
    
    #region Status UI
    [Header( "Status UI / Binding" )]
    
    [SerializeField] private GameObject statusUI;
    [SerializeField] private TMP_Text atkText, defText, hpText, critChanceText;
    
    private void InitStatus()
    {
      atkText.text = PlayerData.status["atk"].ToString();
      defText.text = PlayerData.status["def"].ToString();
      hpText.text = PlayerData.status["hp"].ToString();
      critChanceText.text = PlayerData.status["critChance"].ToString();
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