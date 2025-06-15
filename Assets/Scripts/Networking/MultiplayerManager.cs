using Mirror;
using Retronia.Contents.Entities;
using Retronia.Worlds;

namespace Retronia.Networking
{
  public class MultiplayerManager : NetworkManager
  {
    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
      base.OnServerAddPlayer(conn);

      if (conn.identity.isClient)
      {
        conn.identity.GetComponent<NetworkTransformUnreliable>().syncDirection = SyncDirection.ClientToServer;
        
        BulletManager.InitClientPool(conn);
      }
    }
  }
}