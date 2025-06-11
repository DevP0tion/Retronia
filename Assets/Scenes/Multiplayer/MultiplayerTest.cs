using Retronia.Networking;
using UnityEngine;

namespace Retronia.Scenes.Multiplayer
{
  public class MultiplayerTest : MonoBehaviour
  {
    public void CreateRoom()
    {
      var manager = RoomManager.singleton;
      
      manager.StartHost();
    }

    public void JoinRoom()
    {
      var manager = RoomManager.singleton;
      
      
      
      manager.StartClient();
    }
  }
}