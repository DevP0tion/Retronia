using Retronia.Contents.Properties;
using Retronia.IO;
using Retronia.Scenes.Lobby.Panels;
using Retronia.Utils;
using Retronia.Utils.UI;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using CharacterInfo = Retronia.IO.Formats.CharacterInfo;

namespace Retronia.Scenes.Lobby
{
  public class LobbySystem : MonoBehaviour
  {
    private static SAVE Data => SAVE.Current;
    private static CharacterInfo PlayerData => CharacterInfo.Current;
    public static LobbySystem Instance { get; private set; }
    public UIItemSlot focusedSlot;
    
    #region Binding
    [Header( "Binding" )]
    
    [SerializeField] private CharacterView characterView;
    public ItemDescriptionPanel itemDescriptionPanel;
    
    #endregion

    public bool OpenEquipSlots
    {
      get => characterView && characterView.OpenSlots;
      set
      {
        if(characterView) characterView.OpenSlots = value;
      }
    }
    
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
    [SerializeField] private UIInventory inventory;

    private void InitInventory()
    {
      inventory.RegenerateInventoryUI();
    }
    #endregion
    
    #region Unity Event

    private void Awake()
    {
      if (Instance)
      {
        DestroyImmediate(this);
      }
      else
      {
        Instance = this;
      }
    }
    
    private void Start()
    {
      InitMenu();
      InitStatus();
      InitInventory();
    }
    
    #endregion
    
    #region Input Action

    public void OnInteract(InputAction.CallbackContext context)
    {
      if (context.ReadValue<float>() == 0)
      {
        var itemStack = focusedSlot?.Stack;
        if (itemStack is { type: IEquipable equipable })
        {
        }
      }
    }
    
    #endregion
    
    #if UNITY_EDITOR
    
    [SerializeField, GetSet(nameof(OpenEquipSlots))] private bool openEquipSlots = false;
    
    #endif
  }
}