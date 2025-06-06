using System;
using System.Collections.Generic;
using Retronia.Utils;
using UnityEngine;

namespace Retronia.Worlds
{
  [Serializable]
  public struct Team : IEquatable<Team>
  {
    public static readonly List<Team> ActiveTeams = new();
    private static int nextId;

    #region Fields

    [SerializeField] private Color color;
    [SerializeField] private string name;
    [SerializeField] private int id;
    
    public Color Color => color;
    public string Name => name;
    public int Id => id;

    #endregion
    
    private Team(string name, Color color)
    {
      this.name = name;

      var index = ActiveTeams.FindIndex(team => team.name == name);

      if (index == -1)
      {
        id = nextId++;
        this.color = color;
        ActiveTeams.Add(this);
      }
      else
      {
        id = index;
        this.color = ActiveTeams[index].color;
      }
    }
    
    #region Operators

    public static bool operator ==(Team left, Team right) => left.id == right.id;
    public static bool operator !=(Team left, Team right) => !(left == right);
    public bool Equals(Team other) => id == other.id;
    public override bool Equals(object obj) => obj is Team other && Equals(other);
    public override int GetHashCode() => id;
    
    #endregion

    #region Preload

    public static readonly Team None, Blue, Red;

    static Team()
    {
      None = new Team("White", Color.white);
      Blue = new Team("Blue", Color.blue);
      Red = new Team("Red", Color.red);
    }

    #endregion
  }
}