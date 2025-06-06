namespace Retronia.Contents
{
  public enum Elemental
  {
    Normal = 0,
    Fire = 1,
    Water = 2,
    Earth = 3,
    Wind = 4
  }

  public static class ElementalExpansion
  {
    public static string GetName(this Elemental elemental) => elemental switch
    {
      Elemental.Normal => "Normal",
      Elemental.Fire => "Fire",
      Elemental.Water => "Water",
      Elemental.Earth => "Earth",
      Elemental.Wind => "Wind",
      _ => "Unknown"
    };
  }
}