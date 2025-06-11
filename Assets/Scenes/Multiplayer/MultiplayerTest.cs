using Retronia.Networking;
using TMPro;
using UnityEngine;

namespace Retronia.Scenes.Multiplayer
{
  public class MultiplayerTest : MonoBehaviour
  {
    public void CreateRoom(TMP_InputField field)
    {
      var manager = RoomManager.singleton;
      manager.networkAddress = field.text;
      
      manager.StartHost();
    }

    public void JoinRoom(TMP_InputField field)
    {
      var manager = RoomManager.singleton;
      manager.networkAddress = field.text;

      
      manager.StartClient();
    }
  }
}