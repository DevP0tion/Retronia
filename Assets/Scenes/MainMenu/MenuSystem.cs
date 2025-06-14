using System;
using System.Collections.Generic;
using kcp2k;
using Mirror;
using Mirror.Authenticators;
using Retronia.Core;
using Retronia.IO;
using Retronia.Networking;
using Retronia.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Retronia.Scenes.MainMenu
{
  public class MenuSystem : MonoBehaviour
  {
    public void Exit()
    {
#if UNITY_EDITOR
      UnityEditor.EditorApplication.isPlaying = false;
#else
      Application.Quit();
#endif
    }

    #region SingleSetup Canvas

    public void CreateSave(TMP_InputField nameLabel)
    {
      // TODO 생성시 명칭 설정 가능하게 구현
      var newData = SAVE.Current = new SAVE(nameLabel.text);
      newData.Init();
      newData.player.inventory.AddItem("Gem", 10);
      newData.player.inventory.AddItem("Meteorite", 2);
      newData.player.inventory.AddItem("Cannon", 1);
      newData.player.inventory.AddItem("Core", 1);
      newData.player.inventory.AddItem("Engine", 1);
      newData.player.AddCharacter("Alpha");
      SceneManager.LoadScene(SceneNames.BeforeJoin);
    }
    
    #endregion
    
    #region Setting Canvas
    [Header( "Setting Canvas" )]
    
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider backgroundVolumeSlider;
    [SerializeField] private Slider effectVolumeSlider;
    [SerializeField] private Slider objectVolumeSlider;

    private void LoadSettingCanvas()
    {
      masterVolumeSlider.value = AudioManager.MasterVolume;
      backgroundVolumeSlider.value = AudioManager.BackgroundVolume;
      effectVolumeSlider.value = AudioManager.EffectVolume;
      objectVolumeSlider.value = AudioManager.ObjectVolume;
      
      masterVolumeSlider.onValueChanged.AddListener(value => AudioManager.MasterVolume = value);
      backgroundVolumeSlider.onValueChanged.AddListener(value => AudioManager.BackgroundVolume = value);
      effectVolumeSlider.onValueChanged.AddListener(value => AudioManager.EffectVolume = value);
      objectVolumeSlider.onValueChanged.AddListener(value => AudioManager.ObjectVolume = value);
    }

    public void ChangeFullScreen(bool toggle)
    {
      Screen.fullScreen = toggle;
    }
    #endregion
    
    #region Multiplayer Canvas
    [Header( "Multiplayer Canvas" )]
    [SerializeField, ReadOnly] private MultiplayerManager netManager;
    [SerializeField, ReadOnly] private BasicAuthenticator authenticator;
    [SerializeField, ReadOnly] private KcpTransport transport;
    
    [SerializeField] private TMP_InputField createRoomPasswordField, createRoomPortField;
    [SerializeField] private TMP_InputField joinRoomAddressField, joinRoomPasswordField, joinRoomPortField;

    private void LoadMultiPlayerCanvas()
    {
      netManager = NetworkManager.singleton as MultiplayerManager;
      authenticator = netManager?.authenticator as BasicAuthenticator;
      transport = netManager?.transport as KcpTransport;
      
      createRoomPortField.onValueChanged.AddListener(text =>
        createRoomPortField.text = (ushort.TryParse(text, out var port) ? Math.Max(Math.Min(port, ushort.MaxValue), 1024u) : 7777).ToString()
        );
    }

    public void CreateRoom()
    {
      netManager.networkAddress = "localhost";
      authenticator.serverPassword = createRoomPasswordField.text;
      transport.port = ushort.Parse(createRoomPortField.text);
      
      netManager.StartHost();
    }

    public void JoinRoom()
    {
      netManager.networkAddress = joinRoomAddressField.text;
      authenticator.serverPassword = joinRoomPasswordField.text;
      transport.port = ushort.Parse(createRoomPortField.text);

      netManager.StartClient();
    }
    
    #endregion
    
    #region Unity Event

    private void Start()
    {
      LoadSettingCanvas();
      LoadMultiPlayerCanvas();
    }

    #endregion
  }
}