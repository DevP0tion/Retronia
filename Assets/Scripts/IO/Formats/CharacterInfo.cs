using System;
using Newtonsoft.Json.Linq;
using Retronia.Contents;
using Retronia.Utils;
using UnityEngine;

namespace Retronia.IO.Formats
{
  [Serializable]
  public class CharacterInfo : IJsonSerializable
  {
    #region Status
    
    public string name = "Alpha";
    public Elemental elemental = Elemental.Normal;
    public int level = 1;
    [SerializeField, GetSet(nameof(Exp))] private int exp = 0;

    public RangedStat hp = new(100);
    public Stat atk = new(35);
    public Stat def = new(40);
    public Stat critChance = new(25);
    
    #endregion
    
    #region Logic
    
    /// <summary>
    /// 10렙마다 요구량 크게 증가, 그 외에는 조금씩 증가
    /// </summary>
    public int MaxExp
    {
      get
      {
        var value = Math.Max(level / 10, 1);
        return value * (value + level % 10);
      }
    }

    public int Exp
    {
      get => exp;
      set
      {
        exp = value;
        while (exp >= MaxExp)
        {
          exp -= MaxExp;
          level++;
        }
      }
    }
    
    #endregion
    
    public CharacterInfo(JObject json = null)
    {
      if(json != null)
        LoadJson(json);
    }
    
    public void LoadJson(JObject json)
    {
      name = json.Get("name", "Alpha");
      level = json.Get("level", 1);
      exp = json.Get("exp", 0);
      hp.Max.BaseValue = json.Get("max" + nameof(hp), 100);
      hp.Value = json.Get(nameof(hp), 100);
      atk.BaseValue = json.Get(nameof(atk), 35);
      def.BaseValue = json.Get(nameof(def), 40);
      critChance.BaseValue = json.Get(nameof(critChance), 25);
      elemental = json.Get(nameof(elemental), Elemental.Normal);
    }

    public JObject ToJson()
    {
      return new JObject
      {
        [nameof(name)] = name,
        ["max" + nameof(hp)] = hp.Max.BaseValue,
        [nameof(hp)] = hp.Value,
        [nameof(atk)] = atk.Value,
        [nameof(def)] = def.Value,
        [nameof(critChance)] = critChance.Value,
        [nameof(level)] = level,
        [nameof(exp)] = exp,
        [nameof(elemental)] = elemental.ToToken()
      };
    }
  }
}