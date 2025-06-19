using System;
using Mirror;
using Retronia.Contents.Properties;
using Retronia.Networking.Formats;
using Retronia.Worlds;
using UnityEngine;

namespace Retronia.Networking.Formats
{
  [Serializable]
  public struct BulletPacket : NetworkMessage
  {
    [NaughtyAttributes.ReadOnly] public string typeName;
    [NaughtyAttributes.ReadOnly] public string teamName;
    public Vector3 startPos;
    public Vector3 targetPos;
    public float damage;

    public BulletProperties Type => BulletProperties.Bullets[typeName];
    public Team Team => Team.Get(teamName);

    public BulletPacket(string typeName, string teamName, Vector3 startPos, Vector3 targetPos, float damage)
    {
      this.typeName = typeName;
      this.teamName = teamName;
      this.startPos = startPos;
      this.targetPos = targetPos;
      this.damage = damage;
    }

    public BulletPacket(BulletProperties type, Team team, Vector3 startPos, Vector3 targetPos, float damage) : this(
      type.name, team.Name, startPos, targetPos, damage = 1) {}
  }
}