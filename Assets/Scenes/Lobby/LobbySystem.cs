using Retronia.Contents.Properties;
using Retronia.IO;
using Retronia.IO.Formats;
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
    
    private static PlayerInfo Player => PlayerInfo.Current;
    private static CharacterInfo Character => Player.SelectedCharacter;
    public static LobbySystem Instance { get; private set; }
    
    
    #region Binding
    [Header( "Binding" )]
    
    [SerializeField] private CharacterView characterView;
    public ItemDescriptionPanel itemDescriptionPanel;
    public UIItemSlot focusedSlot;
    
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
      if (Character != null)
      {
        elementalText.text = "속성 - " + Character.elemental;
        nameText.text = Character.name;
        levelText.text = Character.level.ToString();
        goldText.text = $"{Player.gold:#,###}";
        
        expBar.max = Character.MaxExp;
        expBar.Value = Character.Exp;
        expText.text = Character.Exp + " / " + Character.MaxExp;
      }
    }
    #endregion
    
    #region Status UI
    [Header( "Status UI / Binding" )]
    
    [SerializeField] private GameObject statusUI;
    [SerializeField] private TMP_Text atkText, defText, hpText, critChanceText;
    
    private void InitStatus()
    {
      atkText.text = Character.atk.ToString();
      defText.text = Character.def.ToString();
      hpText.text = Character.hp.ToString();
      critChanceText.text = Character.critChance.ToString();
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