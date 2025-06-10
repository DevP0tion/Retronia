using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using Retronia.Contents;
using Retronia.Contents.Items;
using Retronia.Contents.Properties;
using Retronia.Utils;
using UnityEngine;

namespace Retronia.IO.Formats
{
  [Serializable]
  public class CharacterInfo : IJsonSerializable
  {
    public CharacterProperties Scheme { get; private set; } = null;
    public string Name => Scheme?.name;
    
    #region Status
    
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

    #region Equipment
    public List<StatOperator<float>> 
      hpEffect = new(),
      atkEffect = new(),
      defEffect = new(),
      critChanceEffect = new();

    private Dictionary<EquipmentPart, IEquipable> equipments = new();
    private Dictionary<EquipmentPart, Action> unEquipActions = new();

    public bool Equip(IEquipable equipment)
    {
      if (equipment == null || !Scheme) return false;

      if (Scheme.allowedPart.Contains(equipment.Part))
      {
        if (equipments.TryAdd(equipment.Part, equipment))
        {
          unEquipActions[equipment.Part] = equipment.Equip(this);
          
          return true;
        }
      }
      
      return false;
    }

    public void UnEquip(EquipmentPart part)
    {
      if (equipments.Remove(part, out var equipment))
      {
        equipment.UnEquip(this);
        if (unEquipActions.TryGetValue(part, out var action))
        {
          action();
          unEquipActions.Remove(part);
        }
      }
    }
    
    public IEquipable this[EquipmentPart part]
    {
      get => equipments[part];
      set
      {
        if (value == null)
          UnEquip(part);
        else
          Equip(value);
      }
    }
    #endregion
    
    public CharacterInfo(JObject json = null)
    {
      hp.MaxEffect = v => hpEffect.Aggregate(v, (current, effect) => effect(current));
      atk.effect = v => atkEffect.Aggregate(v, (current, effect) => effect(current));
      def.effect = v => defEffect.Aggregate(v, (current, effect) => effect(current));
      critChance.effect = v => critChanceEffect.Aggregate(v, (current, effect) => effect(current));
      
      if(json != null)
        LoadJson(json);
    }
    
    public CharacterInfo(CharacterProperties scheme, JObject json = null) : this(json)
    {
      this.Scheme = scheme;
    }
    
    public void LoadJson(JObject json)
    {
      level = json.Get("level", 1);
      exp = json.Get("exp", 0);
      hp.Max.BaseValue = json.Get("max" + nameof(hp), 100);
      hp.Value = json.Get(nameof(hp), 100);
      atk.BaseValue = json.Get(nameof(atk), 35);
      def.BaseValue = json.Get(nameof(def), 40);
      critChance.BaseValue = json.Get(nameof(critChance), 25);
      elemental = json.Get(nameof(elemental), Elemental.Normal);
      Scheme = CharacterProperties.Characters[json.Get(nameof(Scheme), "Alpha")];

      if (json.TryGetValue(nameof(equipments), out var equipmentsToken) && equipmentsToken is JArray jArray)
      {
        foreach (var token in jArray)
        {
          if (ItemProperties.items.TryGetValue(token.Value<string>(), out var item))
          {
            if (item is IEquipable equipable)
              Equip(equipable);
            else
              Debug.LogWarning($"Item {item.name} is not equipable.");
          }
        }
      }
    }

    public JObject ToJson()
    {
      return new JObject
      {
        ["max" + nameof(hp)] = hp.Max.BaseValue,
        [nameof(hp)] = hp.Value,
        [nameof(atk)] = atk.Value,
        [nameof(def)] = def.Value,
        [nameof(critChance)] = critChance.Value,
        [nameof(level)] = level,
        [nameof(exp)] = exp,
        [nameof(elemental)] = elemental.ToToken(),
        [nameof(Scheme)] = Scheme.name,
        [nameof(equipments)] = new JArray(from pair in equipments select pair.Value.Name)
      };
    }
  }
}