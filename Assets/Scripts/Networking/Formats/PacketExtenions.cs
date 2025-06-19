using Mirror;

namespace Retronia.Networking.Formats
{
  public static class PacketExtension
  {
    public static void Write(this NetworkWriter writer, BulletPacket packet)
    {
      writer.WriteString(packet.typeName);
      writer.WriteString(packet.teamName);
      writer.WriteVector3(packet.startPos);
      writer.WriteVector3(packet.targetPos);
      writer.WriteFloat(packet.damage);
    }
    
    public static BulletPacket Read(this NetworkReader reader)
    {
      return new BulletPacket
      {
        typeName = reader.ReadString(),
        teamName = reader.ReadString(),
        startPos = reader.ReadVector3(),
        targetPos = reader.ReadVector3(),
        damage = reader.ReadFloat()
      };
    }
  }
}